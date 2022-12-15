using System;

namespace AASTHA2.Services.DTO
{
    public class PatientDTO
    {
        public long id { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string fathername { get; set; }
        public string lastname { get; set; }
        public string fullname => $"{firstname} {middlename}{(!string.IsNullOrEmpty(fathername) ? $"({fathername})" : string.Empty)} {lastname}";
        public long addressId { get; set; }
        public string mobile { get; set; }
        public int age { get; set; }
        public bool? isDeleted { get; set; }
        public DateTime modifiedDate { get; set; }
        public LookupDTO address { get; set; }
        public int calculatedAge
        {
            get { return age + (modifiedDate != DateTime.MinValue ? DateTime.UtcNow.Year - modifiedDate.Year : 0); }
            set { age = value; }
        }
    }
}
