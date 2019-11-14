using AASTHA2.Common;
using System;

namespace AASTHA2.DTO
{
    public class OpdDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public CaseType CaseType { get; set; }
        public long ConsultCharge { get; set; }
        public long UsgCharge { get; set; }
        public long UptCharge { get; set; }
        public long InjectionCharge { get; set; }
        public long OtherCharge { get; set; }
        public long PatientId { get; set; }
    }
}
