using AASTHA2.Common;
using System;

namespace AASTHA2.DTO
{
    public class OpdDTO
    {
        public long id { get; set; }
        public DateTime date { get; set; }
        public string invoiceNo => $"OPD{this.id.ToString().PadLeft(7, '0')}";
        public CaseType? caseType { get; set; }
        public string caseTypeName => this.caseType.ToString();
        public decimal consultCharge { get; set; }
        public decimal usgCharge { get; set; }
        public decimal uptCharge { get; set; }
        public decimal injectionCharge { get; set; }
        public decimal otherCharge { get; set; }
        public long? patientId { get; set; }
        public PatientDTO Patient { get; set; }
    }
}
