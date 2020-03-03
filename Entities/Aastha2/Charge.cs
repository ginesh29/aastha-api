using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class Charge : BaseEntity
    {
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Days { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rate { get; set; }
       
        public long LookupId { get; set; }
        [ForeignKey("LookupId")]
        public Lookup ChargeDetail { get; set; }
        public long IpdId { get; set; }
        [ForeignKey("IpdId")]
        public Ipd IpdDetail { get; set; }
    }
}
