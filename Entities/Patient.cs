using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AASTHA2.Entities
{
    public class Patient : BaseEntity
    {
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Mobile  { get; set; }
        public int Age { get; set; }

        public ICollection<Opd> Opds { get; set; }
        public ICollection<Ipd> Ipds { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
