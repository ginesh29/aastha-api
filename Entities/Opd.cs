using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class Opd : BaseEntity
    {
        
        public DateTime Date { get; set; }
        public CaseType CaseType { get; set; }
        public long ConsultCharge { get; set; }
        public long UsgCharge { get; set; }
        public long UptCharge { get; set; }
        public long InjectionCharge { get; set; }
        public long OtherCharge { get; set; }

        public long PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
