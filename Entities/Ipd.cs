using AASTHA2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class Ipd : BaseEntity
    {        
        public IpdType Type { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime AddmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public decimal Discount { get; set; }

        public long PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        public Delivery DeliveryDetail { get; set; }
        public Operation OperationDetail { get; set; }
        public ICollection<Charge> Charges { get; set; }
        public ICollection<IpdLookup> IpdLookups { get; set; }
    }
}
