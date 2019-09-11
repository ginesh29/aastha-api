using System.ComponentModel.DataAnnotations;

namespace AASTHA2.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Role { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
