using AASTHA2.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace AASTHA2.DTO
{
    public class PatientDTO
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedAge
        {
            get { return Age + (CreatedDate != DateTime.MinValue ? (DateTime.UtcNow.Year - CreatedDate.Year) : 0); }
            set { Age = value; }
        }
    }
}
