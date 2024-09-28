using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using AppointmentAPI.Models;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace MicroservicioCitas.Services
{
    public class PersonService
    {
        private readonly string _baseUrl = "http://localhost:59128/api/Table_Person"; // Cambia esto a la URL real de tu servicio de Personas

        public async Task<List<Person>> GetPeopleAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_baseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Person>>(json);
                }

                return null;
            }
        }

        //public async Task<Person> GetPeopleByIdAsync(int id)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.GetAsync($"{_baseUrl}/{id}");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var json = await response.Content.ReadAsStringAsync();
        //            return JsonConvert.DeserializeObject<Person>(json);
        //        }

        //        return null;
        //    }
        //}

        public async Task<List<Person>> GetDoctorAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_baseUrl}/doctor");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Person>>(json);
                }

                return null;
            }
        }

        public async Task<List<Person>> GetPatientAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_baseUrl}/patient");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Person>>(json);
                }

                return null;
            }
        }
    }
}
