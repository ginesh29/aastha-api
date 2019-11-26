using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class OperationMaster
    {
        public int OperationTypeId { get; set; }
        public string OperationType { get; set; }
    }
}
