using AASTHA2.Common;
using AASTHA2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Services.DTO
{
    public class IpdDTO
    {
        public long Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string InvoiceNo
        {
            get { return this.InvoiceNo; }
            set { this.InvoiceNo = this.Id.ToString().PadLeft(7); }

        }
        public IpdType Type { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime AddmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public decimal Discount { get; set; }

        public long PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
