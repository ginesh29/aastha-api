using AASTHA2.Common;
using System;
using System.Collections.Generic;

namespace AASTHA2.DTO
{
    public class IpdDTO
    {
        public long id { get; set; }
        public long uniqueId { get; set; }
        public string invoiceNo => $"IPD{this.uniqueId.ToString().PadLeft(7, '0')}";
        public IpdType? type { get; set; }
        public string ipdType => this.type.ToString();
        public RoomType? roomType { get; set; }
        public string roomTypeName => this.roomType.ToString();
        public DateTime addmissionDate { get; set; }
        public DateTime dischargeDate { get; set; }
        public decimal discount { get; set; }

        public long? patientId { get; set; }
        public DeliveryDTO deliveryDetail { get; set; }
        public OperationDTO operationDetail { get; set; }
        public ICollection<ChargeDTO> charges { get; set; }
        public ICollection<IpdLookupDTO> ipdLookups { get; set; }
    }
}
