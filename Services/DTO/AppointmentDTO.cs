using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class AppointmentDTO : BaseEntity
    {        
        public DateTime Date { get; set; }
        public AppointmentType Type { get; set; }
        public long PatientId { get; set; }
    }
}
