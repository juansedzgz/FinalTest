using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PrescriptionAPI.Models
{
    public partial class Pre_Model : DbContext
    {
        public Pre_Model()
            : base("name=Pre_Model")
        {
        }

        public virtual DbSet<Table_Prescription> Table_Prescription { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table_Prescription>()
                .Property(e => e.Prescription)
                .IsUnicode(false);

            modelBuilder.Entity<Table_Prescription>()
                .Property(e => e.Status)
                .IsUnicode(false);
        }
    }
}
