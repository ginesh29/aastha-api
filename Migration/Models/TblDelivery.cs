using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class TblDelivery
    {
        public int DeliveryId { get; set; }
        public int? IpdId { get; set; }
        public string DeliveryTypeId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public TimeSpan? DeliveryTime { get; set; }
        public string BabyGender { get; set; }
        public decimal? BabyWeight { get; set; }
        public string Diagnosis { get; set; }

        public virtual TblIpd Ipd { get; set; }
    }
}
