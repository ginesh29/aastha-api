using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Migration.Controllers
{
    public class HomeController : Controller
    {
        AASTHAEntities db = new AASTHAEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
            IpdDetailSql(out str7);
            ChargeSql(out str8);
            AppointmentSql(out str9);
            var query = "USE AASTHA2" + Environment.NewLine + str1 + Environment.NewLine + str2 + Environment.NewLine + str3 + Environment.NewLine + str4 + Environment.NewLine + str5 + Environment.NewLine + str6 + Environment.NewLine + str7 + Environment.NewLine + str8 + Environment.NewLine + str9;
            System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/SqlScripts"), "10. FinalMigrationScript.sql"), query);
        }
        public void PatientSql(out string str1)
        {
            var patients = db.tbl_patient;

            var query = "SET IDENTITY_INSERT [dbo].[Patients] ON " + Environment.NewLine;
            foreach (var item in patients)
            {
                var sp = item.full_name.Split(' ');
                var mobile = item.mobile > 0 ? item.mobile.ToString() : "null";
                var age = item.age > 0 ? item.age : 0;
                query += $@"INSERT INTO [dbo].[Patients] ([Id], [Firstname], [Middlename], [Lastname], [Address], [Mobile], [Age],[CreatedDate], [ModifiedDate]) VALUES ({item.patient_Id},'{sp[0]}','{sp[1]}','{sp[2]}','{item.address}',{mobile},{age},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Patients] OFF";
            str1 = query;
            System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/SqlScripts"), "1. PatientMigrationScript.sql"), query);
        }
        public void OpdSql(out string str2)
        {
            var opd = db.tbl_opd;

            var query = "SET IDENTITY_INSERT [dbo].[Opds] ON " + Environment.NewLine;
            foreach (var item in opd)
            {
                item.case_type = !string.IsNullOrEmpty(item.case_type) ? item.case_type : CaseType.New.ToString();
                item.consult_charge = item.consult_charge > 0 ? item.consult_charge : 0;
                item.usg_charge = item.usg_charge > 0 ? item.usg_charge : 0;
                item.upt_charge = item.upt_charge > 0 ? item.upt_charge : 0;
                item.inj_charge = item.inj_charge > 0 ? item.inj_charge : 0;
                item.other_charge = item.other_charge > 0 ? item.other_charge : 0;
                var casetype = (int)((CaseType)Enum.Parse(typeof(CaseType), item.case_type));
                query += $@"INSERT INTO [dbo].[Opds] ([Id], [PatientId], [Date], [CaseType], [ConsultCharge], [UsgCharge], [UptCharge], [InjectionCharge], [OtherCharge], [CreatedDate], [ModifiedDate]) VALUES ({item.opd_Id},{item.patient_id},'{item.date}',{casetype},{item.consult_charge},{item.usg_charge},{item.upt_charge},{item.inj_charge},{item.other_charge},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Opds] OFF";
            str2 = query;
            System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/SqlScripts"), "2. OpdMigrationScript.sql"), query);
        }
        public void LookupSql(out string str3)
        {
            //Delivery type
            var deliveries = db.delivery_master;
            var query = "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in deliveries)
            {
                item.delivery = item.delivery.Contains("'") ? item.delivery.Replace("'", "''") : item.delivery;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.delivery_typeId},'{item.delivery}',{(int)LookupType.DeliveryType},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //Operation Diagnosis + 100
            var diagnoses = db.diagnosis_master;
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in diagnoses)
            {
                item.diagnosis_type = item.diagnosis_type.Contains("'") ? item.diagnosis_type.Replace("'", "''") : item.diagnosis_type;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.digagnosis_typeId + 100},'{item.diagnosis_type}',{(int)LookupType.OperationDiagnosis},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //General Diagnosis + 200
            var generals = db.general_diagnosis;
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in generals)
            {
                item.general_diagnosis_name = item.general_diagnosis_name.Contains("'") ? item.general_diagnosis_name.Replace("'", "''") : item.general_diagnosis_name;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.general_diagnosis_Id + 200},'{item.general_diagnosis_name}',{(int)LookupType.GeneralDiagnosis},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //Operation Type +300
            var operations = db.operation_master;
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in operations)
            {
                item.operation_type = item.operation_type.Contains("'") ? item.operation_type.Replace("'", "''") : item.operation_type;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.operation_typeId + 300},'{item.operation_type}',{(int)LookupType.OperationType},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;


            //Medicine Type + 400
            var medicine_Types = db.tbl_medicine_type;
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in medicine_Types)
            {
                item.medicine_type = item.medicine_type.Contains("'") ? item.medicine_type.Replace("'", "''") : item.medicine_type;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.medicine_Id + 400},'{item.medicine_type}',{(int)LookupType.MedicinType},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //Medicine + 500
            var medicines = db.medicine_master.ToList();
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in medicines)
            {
                item.medicine_name = item.medicine_name.Contains("'") ? item.medicine_name.Replace("'", "''") : item.medicine_name;
                var parent = item.medicine_type > 0 ? (item.medicine_type + 400).ToString() : "null";
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [ParentId], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.medicine_typeId + 500},'{item.medicine_name}',{parent},{(int)LookupType.Medicine},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //Charges Master + 65000
            var charges_Masters = db.IPD_Charges_Master;
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            foreach (var item in charges_Masters)
            {
                item.Charge_Title = item.Charge_Title.Contains("'") ? item.Charge_Title.Replace("'", "''") : item.Charge_Title;
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.Charge_Id + 65000},'{item.Charge_Title}',{(int)LookupType.ChargeType},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;

            //Delivery Diagnosis + 65050
            var delivery_diagnosis = db.tbl_delivery.Where(m => !string.IsNullOrEmpty(m.diagnosis)).Select(m => m.diagnosis).Distinct().ToList();
            query += "SET IDENTITY_INSERT [dbo].[Lookups] ON " + Environment.NewLine;
            int cnt = 1;
            foreach (var item in delivery_diagnosis)
            {
                query += $@"INSERT INTO [dbo].[Lookups] ([Id], [Name], [Type], [CreatedDate], [ModifiedDate]) VALUES ({65050 + cnt},'{item}',{(int)LookupType.DeliveryDiagnosis},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                cnt++;
            }
            query += "SET IDENTITY_INSERT [dbo].[Lookups] OFF" + Environment.NewLine + Environment.NewLine;
            str3 = query;
            System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/SqlScripts"), "3. LookupMigrationScript.sql"), query);
        }
        public void IpdSql(out string str4)
        {
            var appointments = db.tbl_Ipd.Where(m => m.patient_id > 0);
            var query = "SET IDENTITY_INSERT [dbo].[Ipds] ON " + Environment.NewLine;
            foreach (var item in appointments)
            {
                item.dept_name = !string.IsNullOrEmpty(item.dept_name) ? item.dept_name : IpdType.Delivery.ToString();
                var type = (int)((IpdType)Enum.Parse(typeof(IpdType), item.dept_name));

                item.room_type = !string.IsNullOrEmpty(item.room_type) ? item.room_type.Replace("-", "") : RoomType.General.ToString();
                var room_type = (int)((RoomType)Enum.Parse(typeof(RoomType), item.room_type));
                item.conssesion = item.conssesion > 0 ? item.conssesion : 0;
                query += $@"INSERT INTO [dbo].[Ipds] ([Id], [Type], [RoomType], [AddmissionDate], [DischargeDate], [Discount], [PatientId], [CreatedDate], [ModifiedDate]) VALUES ({item.ipd_Id},{type},{room_type},'{item.addmission_date}','{item.discharge_date}',{item.conssesion},{item.patient_id},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            query += "SET IDENTITY_INSERT [dbo].[Ipds] OFF" + Environment.NewLine + Environment.NewLine;
            str4 = query;
            System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/SqlScripts"), "4. IpdMigrationScript.sql"), query);
        }
        public void DeliverySql(out string str5)
        {
            var deliveries = db.tbl_delivery;
            var query = string.Empty;
            foreach (var item in deliveries)
            {
                item.baby_gender = !string.IsNullOrEmpty(item.baby_gender) ? item.baby_gender : Gender.Boy.ToString();
                item.baby_weight = item.baby_weight > 0 ? item.baby_weight : 0;
                var gender = (int)((Gender)Enum.Parse(typeof(Gender), item.baby_gender));
                query += $@"INSERT INTO [dbo].[Deliveries] ([IpdId], [Date], [Time], [Gender], [BabyWeight], [CreatedDate], [ModifiedDate]) VALUES ({item.Ipd_Id},'{item.delivery_date}','{item.delivery_time}',{gender},{item.baby_weight},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            str5 = query;
            System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/SqlScripts"), "5. DeliveryMigrationScript.sql"), query);
        }
        public void OperationSql(out string str6)
        {
            var deliveries = db.tbl_operation;
            var query = string.Empty;
            foreach (var item in deliveries)
            {
                query += $@"INSERT INTO [dbo].[Operations] ([IpdId], [Date], [CreatedDate], [ModifiedDate]) VALUES ({item.Ipd_Id},'{item.operation_date}','{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            str6 = query;
            System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/SqlScripts"), "6. OperationMigrationScript.sql"), query);
        }
        public void IpdDetailSql(out string str7)
        {
            //Delivery type
            var deliveries = db.tbl_delivery;
            var query = string.Empty;
            foreach (var item in deliveries)
            {
                var a = item.delivery_typeId.Split(',');
                foreach (var item1 in a)
                {
                    bool b = db.delivery_master.Select(m => m.delivery_typeId).Contains(Convert.ToInt32(item1));
                    if (b)
                        query += $@"INSERT INTO [dbo].[IpdDetails] ([IpdId], [LookupId], [CreatedDate], [ModifiedDate]) VALUES ({item.Ipd_Id},{item1},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                }
            }

            //General Diagnosis
            var generals = db.tbl_general.Where(m => m.Ipd_Id > 0);
            query += Environment.NewLine + Environment.NewLine;
            foreach (var item in generals)
            {
                var a = item.general_diagnosis.Split(',');
                foreach (var item1 in a)
                {
                    var lookup = Convert.ToInt32(item1) + 200;
                    //bool b = db.general_diagnosis.Select(m => m.general_diagnosis_Id).Contains(Convert.ToInt32(item1));
                    //if (b)
                    query += $@"INSERT INTO [dbo].[IpdDetails] ([IpdId], [LookupId], [CreatedDate], [ModifiedDate]) VALUES ({item.Ipd_Id},{lookup},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                }
            }
            //Operation Type
            var operations = db.tbl_operation;
            query += Environment.NewLine + Environment.NewLine;
            foreach (var item in operations)
            {
                var a = item.operation_type.Split(',');
                foreach (var item1 in a)
                {
                    var lookup = Convert.ToInt32(item1) + 300;
                    bool b = db.operation_master.Select(m => m.operation_typeId).Contains(Convert.ToInt32(item1));
                    if (b)
                        query += $@"INSERT INTO [dbo].[IpdDetails] ([IpdId], [LookupId], [CreatedDate], [ModifiedDate]) VALUES ({item.Ipd_Id},{lookup},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                }
            }

            //Operation Dignosis
            query += Environment.NewLine + Environment.NewLine;
            foreach (var item in operations)
            {
                var a = item.operation_type.Split(',');
                foreach (var item1 in a)
                {
                    int n;
                    bool isNumeric = int.TryParse(item1, out n);
                    int lookup;
                    bool b;
                    if (isNumeric)
                    {
                        lookup = Convert.ToInt32(item1);
                        b = db.diagnosis_master.Select(m => m.digagnosis_typeId).Contains(Convert.ToInt32(item1));
                    }
                    else
                    {
                        lookup = db.diagnosis_master.FirstOrDefault(m => m.diagnosis_type == item1).digagnosis_typeId;
                        b = db.diagnosis_master.Select(m => m.digagnosis_typeId).Contains(Convert.ToInt32(lookup));
                    }
                    lookup = lookup + 100;
                    if (b)
                        query += $@"INSERT INTO [dbo].[IpdDetails] ([IpdId], [LookupId], [CreatedDate], [ModifiedDate]) VALUES ({item.Ipd_Id},{lookup},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                }
            }

            var delivery_diagnosis = db.tbl_delivery.Where(m => !string.IsNullOrEmpty(m.diagnosis)).Select(m => m.diagnosis).Distinct().ToList();
            var list = new List<Model>();
            int cnt = 1;
            foreach (var item in delivery_diagnosis)
            {
                list.Add(new Model { Id = 65050 + cnt, Value = item });
                cnt++;
            }

            foreach (var item in deliveries.Where(m => !string.IsNullOrEmpty(m.diagnosis)))
            {
                var a = item.diagnosis.Split(',');
                foreach (var item1 in a)
                {
                    var lookup = list.FirstOrDefault(m => m.Value == item1).Id;
                    query += $@"INSERT INTO [dbo].[IpdDetails] ([IpdId], [LookupId], [CreatedDate], [ModifiedDate]) VALUES ({item.Ipd_Id},{lookup},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
                }
            }
            str7 = query;
            System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/SqlScripts"), "7. IpdDetailMigrationScript.sql"), query);
        }
        public void ChargeSql(out string str8)
        {
            var opd = db.IPD_Charge_Details.Where(m => m.IPD_Id > 0);
            var query = string.Empty;
            foreach (var item in opd)
            {
                query += $@"INSERT INTO [dbo].[Charges] ([Days], [Rate], [Amount], [LookupId], [IpdId], [CreatedDate], [ModifiedDate]) VALUES ({item.Days},{item.Rate},{item.Amount},{item.Charge_Id + 65000},{item.IPD_Id},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            str8 = query;
            System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/SqlScripts"), "8. ChargeMigrationScript.sql"), query);
        }
        public void AppointmentSql(out string str9)
        {
            var appointments = db.tbl_appointment.Where(m => m.Patient_Id > 0);
            var query = string.Empty;
            foreach (var item in appointments)
            {
                item.Type = !string.IsNullOrEmpty(item.Type) ? item.Type : AppointmentType.Date.ToString();
                var type = (int)((AppointmentType)Enum.Parse(typeof(AppointmentType), item.Type));
                query += $@"INSERT INTO [dbo].[Appointments] ([PatientId], [Date], [Type], [CreatedDate], [ModifiedDate]) VALUES ({item.Patient_Id},'{item.Appointment_Date}',{type},'{DateTime.UtcNow}','{DateTime.UtcNow}')" + Environment.NewLine;
            }
            str9 = query;
            System.IO.File.WriteAllText(Path.Combine(Server.MapPath("~/SqlScripts"), "9. AppointmentMigrationScript.sql"), query);
        }
        public class Model
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
    }
}