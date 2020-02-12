using AASTHA2.Common;
using System;

namespace AASTHA2.DTO
{
    public class DeliveryDTO
    {
        public long id { get; set; }
        public DateTime date { get; set; }
        public TimeSpan time { get; set; }
        public Gender? gender { get; set; }
        public string genderName => this.gender.ToString();
        public decimal babyWeight { get; set; }
        public bool? isDeleted { get; set; }
    }
}
