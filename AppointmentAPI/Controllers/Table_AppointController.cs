using AppointmentAPI.Models;
using AppointmentAPI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.ComponentModel;
using MicroservicioCitas.Services;

namespace AppointmentAPI.Controllers
{
    [RoutePrefix("api/Table_Appoint")]
    public class Table_AppointController : ApiController
    {
           
        private A_Model db = new A_Model();

        private readonly MqProducer _producer;

        private readonly PersonService _personService = new PersonService();

        // Inyectamos MqProducer a través del constructor
        public Table_AppointController() : this(new MqProducer(new ConnectionFactory()))
        {

        }

        public Table_AppointController(MqProducer producer)
        {
            _producer = producer;
        }

        // GET: api/Table_Appoint
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(Table_Appoint))]
        public IQueryable<Table_Appoint> GetTable_Appoint()
        {
            return db.Table_Appoint;
        }

        [HttpGet]
        [Route("doctor")]
        [ResponseType(typeof(Table_Appoint))]
        public async Task<IHttpActionResult> GetDoctor()
        {
            var medicos = await _personService.GetDoctorAsync();
            if (medicos == null)
                return NotFound();
            return Ok(medicos);
        }

        [HttpGet]
        [Route("patient")]
        [ResponseType(typeof(Table_Appoint))]
        public async Task<IHttpActionResult> GetPatient()
        {
            var pacientes = await _personService.GetPatientAsync();
            if (pacientes == null)
                return NotFound();

            return Ok(pacientes);
        }

        // GET: api/Table_Appoint/5
        [HttpGet]
        [Route("{id:int}", Name="GetTableAppointById")]
        [ResponseType(typeof(Table_Appoint))]
        public async Task<IHttpActionResult> GetTable_Appoint(int id)
        {
            /*using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("localhost:55172");
                string endpoint = "/api/Table_Person";
                HttpResponseMessage response = await client.GetAsync($"{endpoint}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                }
            }*/


            Table_Appoint table_Appoint = await db.Table_Appoint.FindAsync(id);
            if (table_Appoint == null)
            {
                return NotFound();
            }
            table_Appoint.Place += " g ";
            return Ok(table_Appoint);
        }

        // GET FROM PERSONAPI

        /*public class ApiService
        {
            private readonly HttpClient _httpClient;

            public ApiService()
            {
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:5001/")  // Set the base URL of the target microservice
                };
            }

            public async Task<string> GetDataFromOtherMicroservice()
            {
                // Define the endpoint to call (e.g., '/api/formulas' on the target microservice)
                string endpoint = "api/formulas";

                try
                {
                    // Send the GET request to the endpoint
                    HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string responseData = await response.Content.ReadAsStringAsync();
                        return responseData;  // Return the response data (JSON, XML, etc.)
                    }
                    else
                    {
                        // Handle HTTP errors (like 404, 500, etc.)
                        throw new Exception($"Error calling API: {response.StatusCode}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Handle connection issues or other request failures
                    throw new Exception("Request failed: " + ex.Message);
                }
            }
        }*/



        // PUT: api/Table_Appoint/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTable_Appoint(int id, Table_Appoint table_Appoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != table_Appoint.Id)
            {
                return BadRequest();
            }

            db.Entry(table_Appoint).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Table_AppointExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //Debug.WriteLine(table_Appoint.Status + "Hola");

            if (ModelState.IsValid)
            {
                // Serializar el objeto Sale a JSON
                var jsonText = JsonConvert.SerializeObject(table_Appoint);
                var encodedText = Encoding.UTF8.GetBytes(jsonText);

                // Enviar el mensaje a RabbitMQ
                if (table_Appoint.Status == "Finished")
                {
                    Debug.WriteLine("-----ENTRE-----");
                    _producer.SendMessage("EndAppointment", encodedText);
                    return Ok("La cita ha terminado. Por favor, ingrese la receta.");
                    //Debug.WriteLine(table_Appoint.Status + "   RabbitMQ");
                    //AppointmentEndMessage(table_Appoint); //Aquí se debe pasar la prescription
                };

                // Retornar una respuesta exitosa
                //return Json(new { success = true, message = "Sale created and message sent to RabbitMQ." });
            }



            // Aquí el médico debe ingresar la receta manualmente (simulado o a través de una UI)
            //return Ok("La cita ha iniciado. Por favor, ingrese la receta.");            
            //AppointmentEndMessage(table_Appoint);


            return StatusCode(HttpStatusCode.NoContent);
        }

        //// SENDER

        //public void AppointmentEndMessage(Table_Appoint table_Appoint)
        //{
        //    var factory = new ConnectionFactory() { HostName = "localhost" };

        //    using (var connection = factory.CreateConnection())

        //    using (var channel = connection.CreateModel())
        //    {
        //        channel.QueueDeclare(queue: "End_of_appointment", durable: false, exclusive: false, autoDelete: false, arguments: null);

        //        var message = JsonConvert.SerializeObject(table_Appoint);
        //        var body = Encoding.UTF8.GetBytes(message);

        //        channel.BasicPublish(exchange: "", routingKey: "End_of_appointment", basicProperties: null, body: body);
        //    }


        //}


        // POST: api/Table_Appoint
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Table_Appoint))]
        public IHttpActionResult PostTable_Appoint(Table_Appoint table_Appoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Table_Appoint.Add(table_Appoint);
            db.SaveChanges();

            return CreatedAtRoute("GetTableAppointById", new { id = table_Appoint.Id }, table_Appoint);

        }

        // DELETE: api/Table_Appoint/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(Table_Appoint))]
        public async Task<IHttpActionResult> DeleteTable_Appoint(int id)
        {
            Table_Appoint table_Appoint = await db.Table_Appoint.FindAsync(id);
            if (table_Appoint == null)
            {
                return NotFound();
            }

            db.Table_Appoint.Remove(table_Appoint);
            await db.SaveChangesAsync();

            return Ok(table_Appoint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Table_AppointExists(int id)
        {
            return db.Table_Appoint.Count(e => e.Id == id) > 0;
        }
    }
}