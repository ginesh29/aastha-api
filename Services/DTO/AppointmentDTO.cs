using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.DTO
{
    public class AppointmentDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public AppointmentType? Type { get; set; }
        public string AppointmentType => this.Type.ToString();
        public long? PatientId { get; set; }
    }
}
