using System.ComponentModel.DataAnnotations;

namespace AASTHA2.Entities
{
    public class Patient : BaseEntity
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Middlename { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Address { get; set; }
        public string Mobile  { get; set; }
        [Required]
        public string Age { get; set; }
    }
}
