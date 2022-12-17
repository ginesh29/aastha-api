namespace AASTHA2.Services.DTO
{
    public class IpdLookupDTO
    {
        public long Id { get; set; }
        public long IpdId { get; set; }
        public long LookupId { get; set; }
        public LookupDTO Lookup { get; set; }
    }
}
