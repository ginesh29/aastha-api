using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace AASTHA2.DTO
{
    public class OperationDTO
    {
        public long Id { get; set; }
        public long IpdId { get; set; }
        public DateTime Date { get; set; }
    }
}
