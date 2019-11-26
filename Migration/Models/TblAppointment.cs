using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class TblAppointment
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Type { get; set; }

        public virtual TblPatient Patient { get; set; }
    }
}
