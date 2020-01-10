using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class Charge : BaseEntity
    {   
        public decimal Days { get; set; }
        public decimal Rate { get; set; }
       
        public long LookupId { get; set; }
        [ForeignKey("LookupId")]
        public Lookup ChargeDetail { get; set; }
        public long IpdId { get; set; }
        [ForeignKey("IpdId")]
        public Ipd IpdDetail { get; set; }
    }
}
