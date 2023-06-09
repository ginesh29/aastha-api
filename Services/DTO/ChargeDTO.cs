﻿namespace AASTHA2.DTO
{
    public class ChargeDTO
    {
        public long id { get; set; }
        public long ipdId { get; set; }
        public decimal days { get; set; }
        public decimal rate { get; set; }
        public decimal amount => (decimal)this.days * (decimal)this.rate;
        public long lookupId { get; set; }
        public bool? isDeleted { get; set; }
        public LookupDTO chargeDetail { get; set; }
    }
}
