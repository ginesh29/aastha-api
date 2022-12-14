using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities.Models
{
    public class Patient : BaseEntity
    {
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Fathername { get; set; }
        public string Lastname { get; set; }
        [NotMapped]
        public string FullName => $"{Firstname} {Middlename}{(!string.IsNullOrEmpty(Fathername) ? $"({Fathername})" : string.Empty)} {Lastname}";
        public long? AddressId { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }

        [ForeignKey("AddressId")]
        public Lookup Address { get; set; }
    }
}
