﻿using Microsoft.AspNetCore.Http;
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
    public class AASTHAContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AASTHAContext(DbContextOptions<AASTHAContext> option, IHttpContextAccessor httpContextAccessor) : base(option)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        public DbSet<IpdDetail> IpdDetails { get; set; }
        public DbSet<Charge> Charges { get; set; }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            //JwtTokenHelper jwt = new JwtTokenHelper(_httpContextAccessor);
            //var UserId = Convert.ToInt64(jwt.ExtractToken("UserId"));
            foreach (EntityEntry entry in entities)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedDate = DateTime.UtcNow;
                    //((BaseEntity)entry.Entity).CreatedBy = UserId;
                }
                else
                {
                    foreach (var item in entry.Properties)
                    {
                        var orgVal = item.OriginalValue;
                        var curVal = item.CurrentValue;
                        bool isNumeric = int.TryParse(curVal.ToString(), out int n);
                        if (isNumeric && n > 0)
                            item.IsModified = false;
                        else if (curVal == null && curVal == orgVal)
                            item.IsModified = false;
                    }
                }
                ((BaseEntity)entry.Entity).ModifiedDate = DateTime.UtcNow;
                //((BaseEntity)entry.Entity).ModifiedBy = UserId;                
            }
            var errors = new List<ValidationResult>(); // all errors are here
            foreach (var e in entities)
            {
                var vc = new ValidationContext(e.Entity, null, null);
                Validator.TryValidateObject(e.Entity, vc, errors, validateAllProperties: true);
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            //JwtTokenHelper jwt = new JwtTokenHelper(_httpContextAccessor);
            //var UserId = Convert.ToInt64(jwt.ExtractToken("UserId"));
            foreach (EntityEntry entry in entities)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedDate = DateTime.UtcNow;
                    //((BaseEntity)entry.Entity).CreatedBy = UserId;
                    ((BaseEntity)entry.Entity).IsDeleted = false;
                }
                else
                {
                    bool isDeleted = (bool)entry.Property("IsDeleted").CurrentValue;
                    entry.Property("CreatedDate").IsModified = false;
                    entry.Property("CreatedBy").IsModified = false;
                    entry.Property("IsDeleted").IsModified = isDeleted ? isDeleted : false;
                }
                ((BaseEntity)entry.Entity).ModifiedDate = DateTime.UtcNow;
                //((BaseEntity)entry.Entity).ModifiedBy = UserId;
            }
            var errors = new List<ValidationResult>(); // all errors are here
            foreach (var e in entities)
            {
                var vc = new ValidationContext(e.Entity, null, null);
                Validator.TryValidateObject(e.Entity, vc, errors, validateAllProperties: true);
            }
            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }
}
