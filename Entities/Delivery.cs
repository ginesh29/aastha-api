using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class Delivery : BaseEntity
    {
        public long IpdId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Gender Gender { get; set; }       
        public decimal BabyWeight { get; set; }

        [ForeignKey("IpdId")]
        public Ipd Ipd { get; set; }
    }
}
