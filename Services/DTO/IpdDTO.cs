using AASTHA2.Common;
using System;
using System.Collections.Generic;

namespace AASTHA2.DTO
{
    public class IpdDTO
    {
        public long Id { get; set; }
        public string InvoiceNo => $"IPD{this.Id.ToString().PadLeft(7, '0')}";
        public IpdType? Type { get; set; }
        public string IpdType => this.Type.ToString();
        public RoomType? RoomType { get; set; }
        public string RoomTypeName => this.RoomType.ToString();
        public DateTime AddmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public decimal Discount { get; set; }

        public long? PatientId { get; set; }
        public DeliveryDTO DeliveryDetail { get; set; }
        public OperationDTO OperationDetail { get; set; }
        public ICollection<ChargeDTO> Charges { get; set; }
        public ICollection<IpdLookupDTO> IpdLookups { get; set; }
    }
}
