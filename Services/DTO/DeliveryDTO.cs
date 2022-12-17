using AASTHA2.Common;
using System;

namespace AASTHA2.Services.DTO
{
    public class DeliveryDTO
    {
        public long Id { get; set; }
        public long IpdId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Gender? Gender { get; set; }
        public string GenderName => Gender.ToString();
        public DateTime DateTime => Date + Time;
        public decimal BabyWeight { get; set; }
        public bool? IsDeleted { get; set; }
    }
}