using AASTHA2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities.Models
{
    public class Lookup : BaseEntity
    {
        public string Name { get; set; }
        public LookupType Type { get; set; }
        public long? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Lookup Parent { get; set; }
        public ICollection<Lookup> Children { get; set; }
    }
}
