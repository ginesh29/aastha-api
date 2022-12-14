using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities.Models
{
    public class Opd : BaseEntity
    {
        public DateTime Date { get; set; }
        public CaseType CaseType { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ConsultCharge { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UsgCharge { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UptCharge { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal InjectionCharge { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OtherCharge { get; set; }

        public long PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
