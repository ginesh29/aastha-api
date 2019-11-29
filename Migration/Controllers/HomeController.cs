using AASTHA2.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Migration.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Migration.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }
        AASTHAContext db = new AASTHAContext();
        private IHostingEnvironment _env;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public void GenerateScript()
        {
            string str1, str2, str3, str4, str5, str6, str7, str8, str9;
            PatientSql(out str1);
            OpdSql(out str2);
            LookupSql(out str3);
            IpdSql(out str4);
            DeliverySql(out str5);
            OperationSql(out str6);
            IpdLookupSql(out str7);
            ChargeSql(out str8);
            AppointmentSql(out str9);
            var query = "USE AASTHA2" + Environment.NewLine + str1 + Environment.NewLine + str2 + Environment.NewLine + str3 + Environment.NewLine + str4 + Environment.NewLine + str5 + Environment.NewLine + str6 + Environment.NewLine + str7 + Environment.NewLine + str8 + Environment.NewLine + str9;
            System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "SqlScripts", "10. FinalMigrationScript.sql"), query);
            var script = @"sqlcmd -S.\SQLEXPRESS -i ""10. FinalMigrationScript.sql""" + Environment.NewLine + "@pause";
            System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "SqlScripts", "Execute Script.bat"), script);
        }
        public void PatientSql(out string str1)
        {
            var patients = db.TblPatient;

            var query = "SET IDENTITY_INSERT [dbo].[Patients] ON " + Environment.NewLine;
            foreach (var item in patients)
            {
                var sp = item.FullName.Split(' ');
                var mobile = item.Mobile > 0 ? item.Mobile.ToString() : "null";
                var age = item.Age > 0 ? item.Age : 0;
                query += $@"INSERT INTO [dbo].[Patients] ([Id], [Firstname], [Middlename], [Lastname], [Address], [Mobile], [Age],[CreatedDate], [ModifiedDate]) VALUES ({item.PatientId},'{sp[0]}','{sp[1]}','{sp[2]}','{item.Address}',{mobile},{age},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Patients] OFF";
            str1 = query;
            System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "SqlScripts", "1. PatientMigrationScript.sql"), query);
        }
        public void OpdSql(out string str2)
        {
            var opd = db.TblOpd;

            var query = "SET IDENTITY_INSERT [dbo].[Opds] ON " + Environment.NewLine;
            foreach (var item in opd)
            {
                item.CaseType = !string.IsNullOrEmpty(item.CaseType) ? item.CaseType : CaseType.New.ToString();
                item.ConsultCharge = item.ConsultCharge > 0 ? item.ConsultCharge : 0;
                item.UsgCharge = item.UsgCharge > 0 ? item.UsgCharge : 0;
                item.UptCharge = item.UptCharge > 0 ? item.UptCharge : 0;
                item.InjCharge = item.InjCharge > 0 ? item.InjCharge : 0;
                item.OtherCharge = item.OtherCharge > 0 ? item.OtherCharge : 0;
                var casetype = (int)((CaseType)Enum.Parse(typeof(CaseType), item.CaseType));
                query += $@"INSERT INTO [dbo].[Opds] ([Id], [PatientId], [Date], [CaseType], [ConsultCharge], [UsgCharge], [UptCharge], [InjectionCharge], [OtherCharge], [CreatedDate], [ModifiedDate]) VALUES ({item.OpdId},{item.PatientId},'{item.Date}',{casetype},{item.ConsultCharge},{item.UsgCharge},{item.UptCharge},{item.InjCharge},{item.OtherCharge},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Opds] OFF";
            str2 = query;
            System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "SqlScripts", "2. OpdMigrationScript.sql"), query);
        }
        public void LookupSql(out string str3)
        {
            //Delivery type
            var deliveries = db.DeliveryMaster;
            var query = "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in deliveries)
            {
                item.Delivery = item.Delivery.Contains("'") ? item.Delivery.Replace("'", "''") : item.Delivery;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.DeliveryTypeId},'{item.Delivery}',{(int)LookupType.DeliveryType},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //Operation Diagnosis + 100
            var diagnoses = db.DiagnosisMaster;
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in diagnoses)
            {
                item.DiagnosisType = item.DiagnosisType.Contains("'") ? item.DiagnosisType.Replace("'", "''") : item.DiagnosisType;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.DigagnosisTypeId + 100},'{item.DiagnosisType}',{(int)LookupType.OperationDiagnosis},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //General Diagnosis + 200
            var generals = db.GeneralDiagnosis;
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in generals)
            {
                item.GeneralDiagnosisName = item.GeneralDiagnosisName.Contains("'") ? item.GeneralDiagnosisName.Replace("'", "''") : item.GeneralDiagnosisName;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.GeneralDiagnosisId + 200},'{item.GeneralDiagnosisName}',{(int)LookupType.GeneralDiagnosis},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //Operation Type +300
            var operations = db.OperationMaster;
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in operations)
            {
                item.OperationType = item.OperationType.Contains("'") ? item.OperationType.Replace("'", "''") : item.OperationType;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.OperationTypeId + 300},'{item.OperationType}',{(int)LookupType.OperationType},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;


            //Medicine Type + 400
            var medicineTypes = db.TblMedicineType;
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in medicineTypes)
            {
                item.MedicineType = item.MedicineType.Contains("'") ? item.MedicineType.Replace("'", "''") : item.MedicineType;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.MedicineId + 400},'{item.MedicineType}',{(int)LookupType.MedicinType},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //Medicine + 500
            var medicines = db.MedicineMaster;
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in medicines)
            {
                item.MedicineName = item.MedicineName.Contains("'") ? item.MedicineName.Replace("'", "''") : item.MedicineName;
                var parent = item.MedicineType > 0 ? (item.MedicineType + 400).ToString() : "null";
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [ParentId], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.MedicineTypeId + 500},'{item.MedicineName}',{parent},{(int)LookupType.Medicine},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //Charges Master + 65000
            var chargesMasters = db.IpdChargesMaster;
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in chargesMasters)
            {
                item.ChargeTitle = item.ChargeTitle.Contains("'") ? item.ChargeTitle.Replace("'", "''") : item.ChargeTitle;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.ChargeId + 65000},'{item.ChargeTitle}',{(int)LookupType.ChargeType},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //Delivery Diagnosis + 65050
            var deliverydiagnosis = db.TblDelivery.Where(m => !string.IsNullOrEmpty(m.Diagnosis)).Select(m => m.Diagnosis).Distinct();
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            int cnt = 1;
            foreach (var item in deliverydiagnosis)
            {
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({65050 + cnt},'{item}',{(int)LookupType.DeliveryDiagnosis},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                cnt++;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;
            str3 = query;
            System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "SqlScripts", "3. LookupMigrationScript.sql"), query);
        }
        public void IpdSql(out string str4)
        {
            var appointments = db.TblIpd.Where(m => m.PatientId > 0);
            var query = "SET IDENTITY_INSERT [dbo].[Ipds] ON " + Environment.NewLine;
            foreach (var item in appointments)
            {
                item.DeptName = !string.IsNullOrEmpty(item.DeptName) ? item.DeptName : IpdType.Delivery.ToString();
                var type = (int)((IpdType)Enum.Parse(typeof(IpdType), item.DeptName));

                item.RoomType = !string.IsNullOrEmpty(item.RoomType) ? item.RoomType.Replace("-", "") : RoomType.General.ToString();
                var roomtype = (int)((RoomType)Enum.Parse(typeof(RoomType), item.RoomType));
                item.Conssesion = item.Conssesion > 0 ? item.Conssesion : 0;
                query += $@"INSERT INTO [dbo].[Ipds] ([Id], [Type], [RoomType], [AddmissionDate], [DischargeDate], [Discount], [PatientId], [CreatedDate], [ModifiedDate]) VALUES ({item.IpdId},{type},{roomtype},'{item.AddmissionDate}','{item.DischargeDate}',{item.Conssesion},{item.PatientId},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Ipds] OFF" + Environment.NewLine + Environment.NewLine;
            str4 = query;
            System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "SqlScripts", "4. IpdMigrationScript.sql"), query);
        }
        public void DeliverySql(out string str5)
        {
            var deliveries = db.TblDelivery;
            var query = string.Empty;
            foreach (var item in deliveries)
            {
                item.BabyGender = !string.IsNullOrEmpty(item.BabyGender) ? item.BabyGender : Gender.Boy.ToString();
                item.BabyWeight = item.BabyWeight > 0 ? item.BabyWeight : 0;
                var gender = (int)((Gender)Enum.Parse(typeof(Gender), item.BabyGender));
                query += $@"INSERT INTO [dbo].[Deliveries] ([IpdId], [Date], [Time], [Gender], [BabyWeight], [CreatedDate], [ModifiedDate]) VALUES ({item.IpdId},'{item.DeliveryDate}','{item.DeliveryTime}',{gender},{item.BabyWeight},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            str5 = query;
            System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "SqlScripts", "5. DeliveryMigrationScript.sql"), query);
        }
        public void OperationSql(out string str6)
        {
            var deliveries = db.TblOperation;
            var query = string.Empty;
            foreach (var item in deliveries)
            {
                query += $@"INSERT INTO [dbo].[Operations] ([IpdId], [Date], [CreatedDate], [ModifiedDate]) VALUES ({item.IpdId},'{item.OperationDate}','{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            str6 = query;
            System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "SqlScripts", "6. OperationMigrationScript.sql"), query);
        }
        public void IpdLookupSql(out string str7)
        {
            //Delivery type
            var deliveries = db.TblDelivery;
            var query = string.Empty;
            foreach (var item in deliveries)
            {
                var a = item.DeliveryTypeId.Split(',');
                foreach (var item1 in a)
                {
                    bool b = db.DeliveryMaster.Select(m => m.DeliveryTypeId).Contains(Convert.ToInt32(item1));
                    if (b)
                        query += $@"INSERT INTO [dbo].[IpdLookups] ([IpdId], [LookupId], [CreatedDate], [ModifiedDate]) VALUES ({item.IpdId},{item1},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                }
            }

            //General Diagnosis
            var generals = db.TblGeneral.Where(m => m.IpdId > 0);
            query += Environment.NewLine + Environment.NewLine;
            foreach (var item in generals)
            {
                var a = item.GeneralDiagnosis.Split(',');
                foreach (var item1 in a)
                {
                    var lookup = Convert.ToInt32(item1) + 200;
                    //bool b = db.generaldiagnosis.Select(m => m.generaldiagnosisId).Contains(Convert.ToInt32(item1));
                    //if (b)
                    query += $@"INSERT INTO [dbo].[IpdLookups] ([IpdId], [LookupId], [CreatedDate], [ModifiedDate]) VALUES ({item.IpdId},{lookup},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                }
            }
            //Operation Type
            var operations = db.TblOperation;
            query += Environment.NewLine + Environment.NewLine;
            foreach (var item in operations)
            {
                var a = item.OperationType.Split(',');
                foreach (var item1 in a)
                {
                    var lookup = Convert.ToInt32(item1) + 300;
                    bool b = db.OperationMaster.Select(m => m.OperationTypeId).Contains(Convert.ToInt32(item1));
                    if (b)
                        query += $@"INSERT INTO [dbo].[IpdLookups] ([IpdId], [LookupId], [CreatedDate], [ModifiedDate]) VALUES ({item.IpdId},{lookup},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                }
            }

            //Operation Dignosis
            query += Environment.NewLine + Environment.NewLine;
            foreach (var item in operations)
            {
                var a = item.OperationType.Split(',');
                foreach (var item1 in a)
                {
                    int n;
                    bool isNumeric = int.TryParse(item1, out n);
                    int lookup;
                    bool b;
                    if (isNumeric)
                    {
                        lookup = Convert.ToInt32(item1);
                        b = db.DiagnosisMaster.Select(m => m.DigagnosisTypeId).Contains(Convert.ToInt32(item1));
                    }
                    else
                    {
                        lookup = db.DiagnosisMaster.FirstOrDefault(m => m.DiagnosisType == item1).DigagnosisTypeId;
                        b = db.DiagnosisMaster.Select(m => m.DigagnosisTypeId).Contains(Convert.ToInt32(lookup));
                    }
                    lookup = lookup + 100;
                    if (b)
                        query += $@"INSERT INTO [dbo].[IpdLookups] ([IpdId], [LookupId], [CreatedDate], [ModifiedDate]) VALUES ({item.IpdId},{lookup},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                }
            }

            var deliverydiagnosis = db.TblDelivery.Where(m => !string.IsNullOrEmpty(m.Diagnosis)).Select(m => m.Diagnosis).Distinct();
            var list = new List<Model>();
            int cnt = 1;
            foreach (var item in deliverydiagnosis)
            {
                list.Add(new Model { Id = 65050 + cnt, Value = item });
                cnt++;
            }

            foreach (var item in deliveries.Where(m => !string.IsNullOrEmpty(m.Diagnosis)))
            {
                var a = item.Diagnosis.Split(',');
                foreach (var item1 in a)
                {
                    var lookup = list.FirstOrDefault(m => m.Value == item1).Id;
                    query += $@"INSERT INTO [dbo].[IpdLookups] ([IpdId], [LookupId], [CreatedDate], [ModifiedDate]) VALUES ({item.IpdId},{lookup},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                }
            }
            str7 = query;
            System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "SqlScripts", "7. IpdLookupMigrationScript.sql"), query);
        }
        public void ChargeSql(out string str8)
        {
            var opd = db.IpdChargeDetails.Where(m => m.IpdId > 0);
            var query = string.Empty;
            foreach (var item in opd)
            {
                query += $@"INSERT INTO [dbo].[Charges] ([Days], [Rate], [Amount], [LookupId], [IpdId], [CreatedDate], [ModifiedDate]) VALUES ({item.Days},{item.Rate},{item.Amount},{item.ChargeId + 65000},{item.IpdId},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            str8 = query;
            System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "SqlScripts", "8. ChargeMigrationScript.sql"), query);
        }
        public void AppointmentSql(out string str9)
        {
            var appointments = db.TblAppointment.Where(m => m.PatientId > 0);
            var query = string.Empty;
            foreach (var item in appointments)
            {
                item.Type = !string.IsNullOrEmpty(item.Type) ? item.Type : AppointmentType.Date.ToString();
                var type = (int)((AppointmentType)Enum.Parse(typeof(AppointmentType), item.Type));
                query += $@"INSERT INTO [dbo].[Appointments] ([PatientId], [Date], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.PatientId},'{item.AppointmentDate}',{type},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            str9 = query;
            System.IO.File.WriteAllText(Path.Combine(_env.WebRootPath, "SqlScripts", "9. AppointmentMigrationScript.sql"), query);
        }
        public class Model
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
    }
}
