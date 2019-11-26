using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class TblGeneral
    {
        public int GeneralId { get; set; }
        public int? IpdId { get; set; }
        public string GeneralDiagnosis { get; set; }

        public virtual TblIpd Ipd { get; set; }
    }
}
