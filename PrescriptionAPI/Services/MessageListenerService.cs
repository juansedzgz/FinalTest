using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Diagnostics;
using PrescriptionAPI.Models;
using System.Web.Http.ModelBinding;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using PrescriptionAPI.Controllers;

namespace PrescriptionAPI.Services
{
    public class MessageListenerService
    {
        private Pre_Model db = new Pre_Model();

        private readonly MqConsumer _consumer;
        private readonly ConnectionFactory _factory;

        public MessageListenerService(ConnectionFactory factory)
        {
            _factory = factory;
            _consumer = new MqConsumer(factory);
        }

        public void StartListening()
        {
            _consumer.AddListener("EndAppointment", OnMessageReceived);
        }

        public void StopListening()
        {
            _consumer.RemoveListener(OnMessageReceived);
        }

        private async void OnMessageReceived(object sender, BasicDeliverEventArgs e)
        {
            // Aquí se maneja el mensaje recibido
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Debug.WriteLine($"Received message: {message}");
            // Actualiza la db
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var appointment = JsonConvert.DeserializeObject<Table_AppointFinished>(message);
            var test = new Table_Prescription();
            test.PatientId = appointment.PatientId;
            test.Prescription = "lorem ipsum";
            test.Date = appointment.Date;
            test.Status = "Activa";
            db.Table_Prescription.Add(test);
            await db.SaveChangesAsync();
        }
    }
}