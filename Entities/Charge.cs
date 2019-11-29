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
        public decimal Amount { get; set; }
       
        public long LookupId { get; set; }
        [ForeignKey("LookupId")]
        public Lookup Lookup { get; set; }
        public long IpdId { get; set; }
        [ForeignKey("IpdId")]
        public Ipd Ipd { get; set; }
    }
}
