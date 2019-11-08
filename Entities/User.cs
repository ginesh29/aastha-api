using System.ComponentModel.DataAnnotations;

namespace AASTHA2.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
