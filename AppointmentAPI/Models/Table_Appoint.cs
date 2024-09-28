namespace AppointmentAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Table_Appoint
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Place { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
    }
}
