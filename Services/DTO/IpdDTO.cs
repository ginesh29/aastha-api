using AASTHA2.Common;
using System;
using System.Collections.Generic;

namespace AASTHA2.Services.DTO
{
    public class IpdDTO
    {
        public long Id { get; set; }
        public long UniqueId { get; set; }
        public string InvoiceNo => $"IPD{UniqueId.ToString().PadLeft(7, '0')}";
        public IpdType? Type { get; set; }
        public string IpdType => Type.ToString();
        public RoomType? RoomType { get; set; }
        public string RoomTypeName => RoomType.ToString();
        public DateTime AddmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public decimal? Discount { get; set; }
        public bool? IsDeleted { get; set; }

        public long? PatientId { get; set; }
        public PatientDTO Patient { get; set; }
        public DeliveryDTO DeliveryDetail { get; set; }
        public OperationDTO OperationDetail { get; set; }
        public ICollection<ChargeDTO> Charges { get; set; }
        public ICollection<IpdLookupDTO> IpdLookups { get; set; }
    }
}