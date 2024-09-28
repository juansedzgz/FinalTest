namespace PrescriptionAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Table_Prescription
    {
        
        public int Id { get; set; }

        public int PatientId { get; set; }

        [Required]
        [StringLength(50)]
        public string Prescription { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
    }
}
