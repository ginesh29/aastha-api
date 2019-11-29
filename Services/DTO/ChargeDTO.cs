using AASTHA2.Common;
using AASTHA2.Entities;
using System;
using System.Collections.Generic;

namespace AASTHA2.DTO
{
    public class ChargeDTO
    {
        public long Id { get; set; }
        public decimal Days { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public long LookupId { get; set; }
        //public Lookup Lookup { get; set; }
        public long IpdId { get; set; }
    }
}
