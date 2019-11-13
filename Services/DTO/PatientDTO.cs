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
        public string Age { get; set; }
    }
}
