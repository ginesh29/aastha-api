using System;
using System.Collections.Generic;

namespace AASTHA.Entities
{
    public partial class IpdChargeDetails
    {
        public int Id { get; set; }
        public int? IpdId { get; set; }
        public int? ChargeId { get; set; }
        public double? Days { get; set; }
        public int? Rate { get; set; }
        public int? Amount { get; set; }

        public virtual TblIpd Ipd { get; set; }
    }
}
