using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Migration.Models
{
    public partial class AASTHAContext : DbContext
    {
        public AASTHAContext()
        {
        }

        public AASTHAContext(DbContextOptions<AASTHAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DeliveryMaster> DeliveryMaster { get; set; }
        public virtual DbSet<DiagnosisMaster> DiagnosisMaster { get; set; }
        public virtual DbSet<GeneralDiagnosis> GeneralDiagnosis { get; set; }
        public virtual DbSet<IpdChargeDetails> IpdChargeDetails { get; set; }
        public virtual DbSet<IpdChargesMaster> IpdChargesMaster { get; set; }
        public virtual DbSet<MedicineMaster> MedicineMaster { get; set; }
        public virtual DbSet<OperationMaster> OperationMaster { get; set; }
        public virtual DbSet<TblAppointment> TblAppointment { get; set; }
        public virtual DbSet<TblDelivery> TblDelivery { get; set; }
        public virtual DbSet<TblGeneral> TblGeneral { get; set; }
        public virtual DbSet<TblIpd> TblIpd { get; set; }
        public virtual DbSet<TblMedicineType> TblMedicineType { get; set; }
        public virtual DbSet<TblOpd> TblOpd { get; set; }
        public virtual DbSet<TblOperation> TblOperation { get; set; }
        public virtual DbSet<TblPatient> TblPatient { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=AASTHA;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<DeliveryMaster>(entity =>
            {
                entity.HasKey(e => e.DeliveryTypeId)
                    .HasName("PK__delivery__1CC4309C594A10AD");

                entity.ToTable("delivery_master");

                entity.Property(e => e.DeliveryTypeId).HasColumnName("delivery_typeId");

                entity.Property(e => e.Delivery)
                    .HasColumnName("delivery")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DiagnosisMaster>(entity =>
            {
                entity.HasKey(e => e.DigagnosisTypeId)
                    .HasName("PK__diagnosi__161B1854556821F0");

                entity.ToTable("diagnosis_master");

                entity.Property(e => e.DigagnosisTypeId).HasColumnName("digagnosis_typeId");

                entity.Property(e => e.DiagnosisType)
                    .HasColumnName("diagnosis_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GeneralDiagnosis>(entity =>
            {
                entity.ToTable("general_diagnosis");

                entity.Property(e => e.GeneralDiagnosisId).HasColumnName("general_diagnosis_Id");

                entity.Property(e => e.GeneralDiagnosisName)
                    .HasColumnName("general_diagnosis_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IpdChargeDetails>(entity =>
            {
                entity.ToTable("IPD_Charge_Details");

                entity.Property(e => e.Amount).HasDefaultValueSql("((0))");

                entity.Property(e => e.ChargeId).HasColumnName("Charge_Id");

                entity.Property(e => e.Days).HasDefaultValueSql("((0.0))");

                entity.Property(e => e.IpdId).HasColumnName("IPD_Id");

                entity.Property(e => e.Rate).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Ipd)
                    .WithMany(p => p.IpdChargeDetails)
                    .HasForeignKey(d => d.IpdId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_IPD_Charge_Details_tbl_Ipd");
            });

            modelBuilder.Entity<IpdChargesMaster>(entity =>
            {
                entity.HasKey(e => e.ChargeId);

                entity.ToTable("IPD_Charges_Master");

                entity.Property(e => e.ChargeId).HasColumnName("Charge_Id");

                entity.Property(e => e.ChargeTitle)
                    .HasColumnName("Charge_Title")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MedicineMaster>(entity =>
            {
                entity.HasKey(e => e.MedicineTypeId)
                    .HasName("PK__medicine__9B1E119557E3BC1C");

                entity.ToTable("medicine_master");

                entity.Property(e => e.MedicineTypeId).HasColumnName("medicine_typeId");

                entity.Property(e => e.MedicineName)
                    .HasColumnName("medicine_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MedicineType).HasColumnName("medicine_type");

                entity.HasOne(d => d.MedicineTypeNavigation)
                    .WithMany(p => p.MedicineMaster)
                    .HasForeignKey(d => d.MedicineType)
                    .HasConstraintName("FK__medicine___medic__51300E55");
            });

            modelBuilder.Entity<OperationMaster>(entity =>
            {
                entity.HasKey(e => e.OperationTypeId)
                    .HasName("PK__operatio__3BFB842637852D27");

                entity.ToTable("operation_master");

                entity.Property(e => e.OperationTypeId).HasColumnName("operation_typeId");

                entity.Property(e => e.OperationType)
                    .HasColumnName("operation_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblAppointment>(entity =>
            {
                entity.ToTable("tbl_appointment", "AASTHA");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnName("Appointment_Date")
                    .HasColumnType("date");

                entity.Property(e => e.PatientId).HasColumnName("Patient_Id");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblAppointment)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_tbl_appointment_tbl_patient");
            });

            modelBuilder.Entity<TblDelivery>(entity =>
            {
                entity.HasKey(e => e.DeliveryId)
                    .HasName("PK__tbl_deli__1CA3F92D9848C8CE");

                entity.ToTable("tbl_delivery");

                entity.Property(e => e.DeliveryId).HasColumnName("delivery_Id");

                entity.Property(e => e.BabyGender)
                    .HasColumnName("baby_gender")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BabyWeight)
                    .HasColumnName("baby_weight")
                    .HasColumnType("decimal(5, 3)");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnName("delivery_date")
                    .HasColumnType("date");

                entity.Property(e => e.DeliveryTime).HasColumnName("delivery_time");

                entity.Property(e => e.DeliveryTypeId)
                    .HasColumnName("delivery_typeId")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Diagnosis)
                    .HasColumnName("diagnosis")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IpdId).HasColumnName("Ipd_Id");

                entity.HasOne(d => d.Ipd)
                    .WithMany(p => p.TblDelivery)
                    .HasForeignKey(d => d.IpdId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tbl_delivery_tbl_Ipd");
            });

            modelBuilder.Entity<TblGeneral>(entity =>
            {
                entity.HasKey(e => e.GeneralId)
                    .HasName("PK__tmp_ms_x__4B52F86AE25FADFF");

                entity.ToTable("tbl_general");

                entity.Property(e => e.GeneralId).HasColumnName("general_Id");

                entity.Property(e => e.GeneralDiagnosis)
                    .HasColumnName("general_diagnosis")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IpdId).HasColumnName("Ipd_Id");

                entity.HasOne(d => d.Ipd)
                    .WithMany(p => p.TblGeneral)
                    .HasForeignKey(d => d.IpdId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tbl_general_tbl_Ipd");
            });

            modelBuilder.Entity<TblIpd>(entity =>
            {
                entity.HasKey(e => e.IpdId)
                    .HasName("PK__tmp_ms_x__72F929295FC1E440");

                entity.ToTable("tbl_Ipd");

                entity.Property(e => e.IpdId)
                    .HasColumnName("ipd_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddmissionDate)
                    .HasColumnName("addmission_date")
                    .HasColumnType("date");

                entity.Property(e => e.Bill)
                    .HasColumnName("bill")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Conssesion)
                    .HasColumnName("conssesion")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DeptName)
                    .HasColumnName("dept_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DischargeDate)
                    .HasColumnName("discharge_date")
                    .HasColumnType("date");

                entity.Property(e => e.IpdReceiptId)
                    .HasColumnName("ipd_receipt_id")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('IPD'+right('0000000'+CONVERT([varchar](7),[ipd_id]),(7)))");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.RoomType)
                    .HasColumnName("room_type")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblIpd)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tbl_Ipd_tbl_patient");
            });

            modelBuilder.Entity<TblMedicineType>(entity =>
            {
                entity.HasKey(e => e.MedicineId)
                    .HasName("PK__tbl_medi__E71182B3753846AF");

                entity.ToTable("tbl_medicine_type");

                entity.Property(e => e.MedicineId).HasColumnName("medicine_Id");

                entity.Property(e => e.MedicineType)
                    .HasColumnName("medicine_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblOpd>(entity =>
            {
                entity.HasKey(e => e.OpdReceiptId)
                    .HasName("PK_Table");

                entity.ToTable("tbl_opd");

                entity.Property(e => e.OpdReceiptId)
                    .HasColumnName("opd_receipt_id")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('OPD'+right('0000000'+CONVERT([varchar](7),[opd_Id]),(7)))")
                    .ValueGeneratedNever();

                entity.Property(e => e.CaseType)
                    .HasColumnName("case_type")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ConsultCharge)
                    .HasColumnName("consult_charge")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.InjCharge)
                    .HasColumnName("inj_charge")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.OpdId)
                    .HasColumnName("opd_Id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.OtherCharge)
                    .HasColumnName("other_charge")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.UptCharge)
                    .HasColumnName("upt_charge")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UsgCharge)
                    .HasColumnName("usg_charge")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TblOpd)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__tbl_opd__patient__6FE99F9F");
            });

            modelBuilder.Entity<TblOperation>(entity =>
            {
                entity.HasKey(e => e.OperationId)
                    .HasName("PK__tmp_ms_x__9DE65DFB6E62EAD6");

                entity.ToTable("tbl_operation");

                entity.Property(e => e.OperationId).HasColumnName("operation_Id");

                entity.Property(e => e.Diagnosis)
                    .HasColumnName("diagnosis")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IpdId).HasColumnName("Ipd_Id");

                entity.Property(e => e.OperationDate)
                    .HasColumnName("operation_date")
                    .HasColumnType("date");

                entity.Property(e => e.OperationType)
                    .HasColumnName("operation_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Ipd)
                    .WithMany(p => p.TblOperation)
                    .HasForeignKey(d => d.IpdId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_tbl_operation_tbl_Ipd");
            });

            modelBuilder.Entity<TblPatient>(entity =>
            {
                entity.HasKey(e => e.PatientId)
                    .HasName("PK__tbl_pati__4D45DFDEEFD71FE5");

                entity.ToTable("tbl_patient");

                entity.Property(e => e.PatientId).HasColumnName("patient_Id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Age)
                    .HasColumnName("age")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.FullName)
                    .HasColumnName("full_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.OpdId123)
                    .IsRequired()
                    .HasColumnName("opd_id123")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('OPD'+right('0000000'+CONVERT([varchar](7),[patient_Id]),(7)))");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("tbl_user");

                entity.Property(e => e.BiometricId)
                    .HasColumnName("Biometric_Id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Layout)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
