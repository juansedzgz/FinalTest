using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PersonAPI.Models
{
    public partial class P_Model : DbContext
    {
        public P_Model()
            : base("name=P_Model")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<Table_Person> Table_Person { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table_Person>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Table_Person>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Table_Person>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
