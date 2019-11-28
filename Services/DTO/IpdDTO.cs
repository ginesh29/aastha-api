using AASTHA2.Common;
using AASTHA2.Entities;
using System;
using System.Collections.Generic;

namespace AASTHA2.DTO
{
    public class IpdDTO
    {
        public long Id { get; set; }
        public string InvoiceNo
        {
            get { return $"IPD{this.Id.ToString().PadLeft(7, '0')}"; }
        }
        public IpdType Type { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime AddmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public decimal Discount { get; set; }

        public long PatientId { get; set; }
        public Patient Patient { get; set; }
        public ICollection<Charge> Charges { get; set; }
    }
}
