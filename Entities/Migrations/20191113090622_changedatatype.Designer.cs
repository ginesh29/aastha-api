﻿// <auto-generated />
using System;
using AASTHA2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AASTHA2.Entities.Migrations
{
    [DbContext(typeof(AASTHAContext))]
    [Migration("20191113090622_changedatatype")]
    partial class changedatatype
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AASTHA2.Entities.Appointment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("Date");

                    b.Property<bool?>("IsDeleted");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long?>("PatientId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("AASTHA2.Entities.Lookup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool?>("IsDeleted");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<long?>("ParentId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("ParentId");

                    b.ToTable("Lookups");
                });

            modelBuilder.Entity("AASTHA2.Entities.Opd", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseType");

                    b.Property<long>("ConsultCharge");

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("Date");

                    b.Property<long>("InjectionCharge");

                    b.Property<bool?>("IsDeleted");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("OtherCharge");

                    b.Property<long>("PatientId");

                    b.Property<long>("UptCharge");

                    b.Property<long>("UsgCharge");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("PatientId");

                    b.ToTable("Opds");
                });

            modelBuilder.Entity("AASTHA2.Entities.Patient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int>("Age");

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Firstname");

                    b.Property<bool?>("IsDeleted");

                    b.Property<string>("Lastname");

                    b.Property<string>("Middlename");

                    b.Property<string>("Mobile");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("AASTHA2.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool?>("IsDeleted");

                    b.Property<bool>("IsSuperAdmin");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AASTHA2.Entities.Appointment", b =>
                {
                    b.HasOne("AASTHA2.Entities.User", "CreaterInfo")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("AASTHA2.Entities.User", "ModifierInfo")
                        .WithMany()
                        .HasForeignKey("ModifiedBy");

                    b.HasOne("AASTHA2.Entities.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("AASTHA2.Entities.Lookup", b =>
                {
                    b.HasOne("AASTHA2.Entities.User", "CreaterInfo")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("AASTHA2.Entities.User", "ModifierInfo")
                        .WithMany()
                        .HasForeignKey("ModifiedBy");

                    b.HasOne("AASTHA2.Entities.Lookup", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("AASTHA2.Entities.Opd", b =>
                {
                    b.HasOne("AASTHA2.Entities.User", "CreaterInfo")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("AASTHA2.Entities.User", "ModifierInfo")
                        .WithMany()
                        .HasForeignKey("ModifiedBy");

                    b.HasOne("AASTHA2.Entities.Patient", "Patient")
                        .WithMany("Opds")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AASTHA2.Entities.Patient", b =>
                {
                    b.HasOne("AASTHA2.Entities.User", "CreaterInfo")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("AASTHA2.Entities.User", "ModifierInfo")
                        .WithMany()
                        .HasForeignKey("ModifiedBy");
                });

            modelBuilder.Entity("AASTHA2.Entities.User", b =>
                {
                    b.HasOne("AASTHA2.Entities.User", "CreaterInfo")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("AASTHA2.Entities.User", "ModifierInfo")
                        .WithMany()
                        .HasForeignKey("ModifiedBy");
                });
#pragma warning restore 612, 618
        }
    }
}
