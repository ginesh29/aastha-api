using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class TblOpd
    {
        public int OpdId { get; set; }
        public string OpdReceiptId { get; set; }
        public int? PatientId { get; set; }
        public DateTime? Date { get; set; }
        public string CaseType { get; set; }
        public int? ConsultCharge { get; set; }
        public int? UsgCharge { get; set; }
        public int? UptCharge { get; set; }
        public int? InjCharge { get; set; }
        public int? OtherCharge { get; set; }

        public virtual TblPatient Patient { get; set; }
    }
}
