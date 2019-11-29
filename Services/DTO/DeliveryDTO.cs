using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace AASTHA2.DTO
{
    public class DeliveryDTO
    {
        public long Id { get; set; }
        public long IpdId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Gender Gender { get; set; }
        public string GenderName => Enum.GetName(typeof(Gender), this.Gender);
        public decimal BabyWeight { get; set; }
    }
}
