using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities.Models
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public long? AddressId { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsSuperAdmin { get; set; }
        //[ForeignKey("AddressId")]
        //[NotMapped]
        //public Lookup Address { get; set; }       
    }
}
