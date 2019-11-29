using AASTHA2.Common;
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
        public decimal ConsultCharge { get; set; }
        public decimal UsgCharge { get; set; }
        public decimal UptCharge { get; set; }
        public decimal InjectionCharge { get; set; }
        public decimal OtherCharge { get; set; }
        public long PatientId { get; set; }
        //public PatientDTO Patient { get; set; }
    }
}
