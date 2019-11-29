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
        public decimal ConsultCharge { get; set; }
        public decimal UsgCharge { get; set; }
        public decimal UptCharge { get; set; }
        public decimal InjectionCharge { get; set; }
        public decimal OtherCharge { get; set; }

        public long PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
