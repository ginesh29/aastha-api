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
    [DbContext(typeof(AASTHA2Context))]
    [Migration("20200202190739_sp_patient1")]
    partial class sp_patient1
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

                    b.Property<long>("PatientId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("AASTHA2.Entities.Charge", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<decimal>("Days")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long>("IpdId");

                    b.Property<bool?>("IsDeleted");

                    b.Property<long>("LookupId");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("IpdId");

                    b.HasIndex("LookupId");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Charges");
                });

            modelBuilder.Entity("AASTHA2.Entities.Delivery", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("BabyWeight")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Gender");

                    b.Property<long>("IpdId");

                    b.Property<bool?>("IsDeleted");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<TimeSpan>("Time");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("IpdId")
                        .IsUnique();

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("AASTHA2.Entities.Ipd", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddmissionDate");

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("DischargeDate");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<bool?>("IsDeleted");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("PatientId");

                    b.Property<int>("RoomType");

                    b.Property<int>("Type");

                    b.Property<long>("UniqueId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("PatientId");

                    b.HasIndex("UniqueId")
                        .IsUnique();

                    b.ToTable("Ipds");
                });

            modelBuilder.Entity("AASTHA2.Entities.IpdLookup", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("IpdId");

                    b.Property<bool?>("IsDeleted");

                    b.Property<long>("LookupId");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("IpdId");

                    b.HasIndex("LookupId");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("IpdLookups");
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

                    b.Property<decimal>("ConsultCharge")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("InjectionCharge")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<bool?>("IsDeleted");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<decimal>("OtherCharge")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long>("PatientId");

                    b.Property<decimal>("UptCharge")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("UsgCharge")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.HasIndex("PatientId");

                    b.ToTable("Opds");
                });

            modelBuilder.Entity("AASTHA2.Entities.Operation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("Date");

                    b.Property<long>("IpdId");

                    b.Property<bool?>("IsDeleted");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("IpdId")
                        .IsUnique();

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("AASTHA2.Entities.Patient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AddressId");

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

                    b.HasIndex("AddressId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ModifiedBy");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("AASTHA2.Entities.Sp_GetCollection_Result", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Month");

                    b.Property<string>("MonthName");

                    b.Property<decimal>("TotalCollection");

                    b.Property<int>("TotalPatient");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Sp_GetCollection");
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
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AASTHA2.Entities.Charge", b =>
                {
                    b.HasOne("AASTHA2.Entities.User", "CreaterInfo")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("AASTHA2.Entities.Ipd", "IpdDetail")
                        .WithMany("Charges")
                        .HasForeignKey("IpdId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AASTHA2.Entities.Lookup", "ChargeDetail")
                        .WithMany()
                        .HasForeignKey("LookupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AASTHA2.Entities.User", "ModifierInfo")
                        .WithMany()
                        .HasForeignKey("ModifiedBy");
                });

            modelBuilder.Entity("AASTHA2.Entities.Delivery", b =>
                {
                    b.HasOne("AASTHA2.Entities.User", "CreaterInfo")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("AASTHA2.Entities.Ipd", "Ipd")
                        .WithOne("DeliveryDetail")
                        .HasForeignKey("AASTHA2.Entities.Delivery", "IpdId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AASTHA2.Entities.User", "ModifierInfo")
                        .WithMany()
                        .HasForeignKey("ModifiedBy");
                });

            modelBuilder.Entity("AASTHA2.Entities.Ipd", b =>
                {
                    b.HasOne("AASTHA2.Entities.User", "CreaterInfo")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("AASTHA2.Entities.User", "ModifierInfo")
                        .WithMany()
                        .HasForeignKey("ModifiedBy");

                    b.HasOne("AASTHA2.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AASTHA2.Entities.IpdLookup", b =>
                {
                    b.HasOne("AASTHA2.Entities.User", "CreaterInfo")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("AASTHA2.Entities.Ipd", "Ipd")
                        .WithMany("IpdLookups")
                        .HasForeignKey("IpdId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AASTHA2.Entities.Lookup", "Lookup")
                        .WithMany()
                        .HasForeignKey("LookupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AASTHA2.Entities.User", "ModifierInfo")
                        .WithMany()
                        .HasForeignKey("ModifiedBy");
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
                        .WithMany("Children")
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
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AASTHA2.Entities.Operation", b =>
                {
                    b.HasOne("AASTHA2.Entities.User", "CreaterInfo")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("AASTHA2.Entities.Ipd", "Ipd")
                        .WithOne("OperationDetail")
                        .HasForeignKey("AASTHA2.Entities.Operation", "IpdId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AASTHA2.Entities.User", "ModifierInfo")
                        .WithMany()
                        .HasForeignKey("ModifiedBy");
                });

            modelBuilder.Entity("AASTHA2.Entities.Patient", b =>
                {
                    b.HasOne("AASTHA2.Entities.Lookup", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

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
