using System.ComponentModel.DataAnnotations;

namespace AASTHA2.DTO
{
    public class PatientDTO
    {
        public long Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Middlename { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Address { get; set; }
        public string Mobile { get; set; }
        [Required]
        public string Age { get; set; }
    }
}
