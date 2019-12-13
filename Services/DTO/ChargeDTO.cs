﻿namespace AASTHA2.DTO
{
    public class ChargeDTO
    {
        public long Id { get; set; }
        public decimal Days { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount => (decimal)this.Days * (decimal)this.Rate;
        public long LookupId { get; set; }
    }
}
