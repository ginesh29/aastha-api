﻿using AASTHA2.Common;
using System;

namespace AASTHA2.DTO
{
    public class OpdDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNo=> $"OPD{this.Id.ToString().PadLeft(7, '0')}";
        public CaseType CaseType { get; set; }
        public string CaseTypeName => Enum.GetName(typeof(CaseType), this.CaseType);
        public long ConsultCharge { get; set; }
        public long UsgCharge { get; set; }
        public long UptCharge { get; set; }
        public long InjectionCharge { get; set; }
        public long OtherCharge { get; set; }
        public long PatientId { get; set; }
        public PatientDTO Patient { get; set; }
    }
}
