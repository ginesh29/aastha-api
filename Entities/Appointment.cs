using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class Appointment : BaseEntity
    {
        public long? PatientId { get; set; }
        public DateTime Date { get; set; }
        public AppointmentType Type { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
