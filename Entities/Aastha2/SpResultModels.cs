using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class Sp_GetStatistics_Result
    {
        [Key]
        public Guid Id { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public int TotalPatient { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalCollection { get; set; }
    }
    public class Sp_GetCalendarAppointments_Result
    {
        [Key]
        public long Id { get; set; }
        public long PatientId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Start
        {
            get
            {
                return Date.ToString("yyyy-MM-dd");
            }
        }
        public int Type { get; set; }
        public string AppointmentType { get; set; }
    }
}
