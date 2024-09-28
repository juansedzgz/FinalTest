using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AppointmentAPI.Models
{
    public partial class A_Model : DbContext
    {
        public A_Model()
            : base("name=A_Model")
        {
        }

        public virtual DbSet<Table_Appoint> Table_Appoint { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table_Appoint>()
                .Property(e => e.Place)
                .IsUnicode(false);

            modelBuilder.Entity<Table_Appoint>()
                .Property(e => e.Status)
                .IsUnicode(false);
        }
    }
}
