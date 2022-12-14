using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities.Models
{
    public class Delivery : BaseEntity
    {
        public long IpdId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Gender Gender { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BabyWeight { get; set; }

        [ForeignKey("IpdId")]
        public Ipd Ipd { get; set; }
    }
}
