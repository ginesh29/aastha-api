using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities.Models
{
    public class IpdLookup : BaseEntity
    {
        public long IpdId { get; set; }
        public long LookupId { get; set; }

        [ForeignKey("IpdId")]
        public Ipd Ipd { get; set; }
        [ForeignKey("LookupId")]
        public Lookup Lookup { get; set; }
    }
}
