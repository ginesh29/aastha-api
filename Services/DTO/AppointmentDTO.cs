using AASTHA2.Common;
using System;

namespace AASTHA2.Services.DTO
{
    public class AppointmentDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public AppointmentType? Type { get; set; }
        public string AppointmentType => Type.ToString();
        public long? PatientId { get; set; }
        public bool? IsDeleted { get; set; }
        public PatientDTO Patient { get; set; }
    }
}
