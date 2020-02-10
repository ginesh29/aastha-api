using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.DTO
{
    public class AppointmentDTO
    {
        public long id { get; set; }
        public DateTime date { get; set; }
        public AppointmentType? type { get; set; }
        public string AppointmentType => this.type.ToString();
        public long? patientId { get; set; }
        public PatientDTO patient { get; set; }
    }
}
