using AASTHA2.Common;
using System;

namespace AASTHA2.DTO
{
    public class DeliveryDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Gender? Gender { get; set; }
        public string GenderName => this.Gender.ToString();
        public decimal BabyWeight { get; set; }
    }
}
