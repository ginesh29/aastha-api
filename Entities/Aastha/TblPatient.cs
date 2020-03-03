using System;
using System.Collections.Generic;

namespace AASTHA.Entities
{
    public partial class TblPatient
    {
        public TblPatient()
        {
            TblAppointment = new HashSet<TblAppointment>();
            TblIpd = new HashSet<TblIpd>();
            TblOpd = new HashSet<TblOpd>();
        }

        public int PatientId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public decimal? Mobile { get; set; }
        public decimal? Age { get; set; }
        public string OpdId123 { get; set; }

        public virtual ICollection<TblAppointment> TblAppointment { get; set; }
        public virtual ICollection<TblIpd> TblIpd { get; set; }
        public virtual ICollection<TblOpd> TblOpd { get; set; }
    }
}
