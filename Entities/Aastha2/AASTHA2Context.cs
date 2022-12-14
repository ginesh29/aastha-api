using AASTHA2.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AASTHA2.Entities
{
    public class AASTHA2Context : DbContext
    {
        public AASTHA2Context(DbContextOptions<AASTHA2Context> option) : base(option)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ipd>().HasIndex(u => u.UniqueId).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Opd> Opds { get; set; }
        public DbSet<Ipd> Ipds { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<IpdLookup> IpdLookups { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (EntityEntry entry in entities)
            {
                if (entry.State == EntityState.Added)
                    ((BaseEntity)entry.Entity).CreatedDate = DateTime.UtcNow;
                ((BaseEntity)entry.Entity).ModifiedDate = DateTime.UtcNow;
            }
            return base.SaveChanges();
        }
    }
}