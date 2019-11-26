using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class TblOperation
    {
        public int OperationId { get; set; }
        public int? IpdId { get; set; }
        public string Diagnosis { get; set; }
        public DateTime? OperationDate { get; set; }
        public string OperationType { get; set; }

        public virtual TblIpd Ipd { get; set; }
    }
}
