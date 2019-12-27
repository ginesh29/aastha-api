using System;

namespace AASTHA2.DTO
{
    public class PatientDTO
    {
        public long id { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string fullname => $@"{this.firstname } {this.middlename} {this.lastname}";
        public string address { get; set; }
        public string mobile { get; set; }
        public int age { get; set; }
        public DateTime createdDate { get; set; }
        public int updatedAge
        {
            get { return age + (createdDate != DateTime.MinValue ? (DateTime.UtcNow.Year - createdDate.Year) : 0); }
            set { age = value; }
        }
    }
}
