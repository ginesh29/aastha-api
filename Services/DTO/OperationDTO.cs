using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace AASTHA2.Services.DTO
{
    public class OperationDTO
    {
        public long id { get; set; }
        public long ipdId { get; set; }
        public DateTime date { get; set; }
    }
}
