using System;

namespace AASTHA2.Services.DTO
{
    public class PatientDTO
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Fathername { get; set; }
        public string Lastname { get; set; }
        public string Fullname => $"{Firstname} {Middlename}{(!string.IsNullOrEmpty(Fathername) ? $"({Fathername})" : string.Empty)} {Lastname}";
        public long AddressId { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime ModifiedDate { get; set; }
        public LookupDTO Address { get; set; }
        public int CalculatedAge
        {
            get { return Age + (ModifiedDate != DateTime.MinValue ? DateTime.UtcNow.Year - ModifiedDate.Year : 0); }
            set { Age = value; }
        }
    }
}
