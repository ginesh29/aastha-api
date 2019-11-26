using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class Operation : BaseEntity
    {
        public long IpdId { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("IpdId")]
        public Ipd Ipd { get; set; }
    }
}
