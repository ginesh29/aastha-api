namespace AASTHA2.DTO
{
    public class ChargeDTO
    {
        public long id { get; set; }
        public decimal days { get; set; }
        public decimal rate { get; set; }
        public decimal amount => (decimal)this.days * (decimal)this.rate;
        public long lookupId { get; set; }
    }
}
