using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Services.DTO
{
    public class AppointmentDTO
    {
        public long id { get; set; }
        public DateTime date { get; set; }
        public AppointmentType? type { get; set; }
        public string AppointmentType => type.ToString();
        public long? patientId { get; set; }
        public bool? isDeleted { get; set; }
        public PatientDTO patient { get; set; }
    }
}
