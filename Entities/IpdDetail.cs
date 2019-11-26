using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class IpdDetail : BaseEntity
    {
        public int IpdId { get; set; }
        public long LookupId { get; set; }
        [ForeignKey("LookupId")]
        public Lookup Lookup { get; set; }
    }
}
