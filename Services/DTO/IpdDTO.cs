using AASTHA2.Common;
using AASTHA2.Entities;
using System;
using System.Collections.Generic;

namespace AASTHA2.DTO
{
    public class IpdDTO
    {
        public long Id { get; set; }
        public string InvoiceNo => $"IPD{this.Id.ToString().PadLeft(7, '0')}";
        public IpdType Type { get; set; }
        public string IpdType => Enum.GetName(typeof(IpdType), this.Type);
        public RoomType RoomType { get; set; }
        public string RoomTypeName => Enum.GetName(typeof(RoomType), this.RoomType);
        public DateTime AddmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public decimal Discount { get; set; }

        public long PatientId { get; set; }
        public DeliveryDTO DeliveryDetail { get; set; }
        public OperationDTO OperationDetail { get; set; }
        public ICollection<ChargeDTO> Charges { get; set; }
        public ICollection<IpdLookupDTO> IpdLookups { get; set; }
    }    
}
