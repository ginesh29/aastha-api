using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class Patient : BaseEntity
    {
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public long? AddressId { get; set; }        
        public string Mobile  { get; set; }
        public int Age { get; set; }

        [ForeignKey("AddressId")]
        public Lookup Address { get; set; }

        //public ICollection<Opd> Opds { get; set; }
        //public ICollection<Ipd> Ipds { get; set; }
        //public ICollection<Appointment> Appointments { get; set; }
    }
}
