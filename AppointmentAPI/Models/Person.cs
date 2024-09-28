using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppointmentAPI.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Email { get; set; }
    }
}