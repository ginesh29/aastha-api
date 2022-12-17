namespace AASTHA2.Services.DTO
{
    public class ChargeDTO
    {
        public long Id { get; set; }
        public long IpdId { get; set; }
        public decimal Days { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount => Days * Rate;
        public long LookupId { get; set; }
        public bool? IsDeleted { get; set; }
        public LookupDTO ChargeDetail { get; set; }
    }
}
