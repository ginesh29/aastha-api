using AASTHA2.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AASTHA2.Services.DTO
{
    public class OpdDTO
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public string InvoiceNo => $"OPD{Id.ToString().PadLeft(7, '0')}";
        public CaseType? CaseType { get; set; }
        public string CaseTypeName => CaseType.ToString();
        public decimal? ConsultCharge { get; set; }
        public decimal? UsgCharge { get; set; }
        public decimal? UptCharge { get; set; }
        public decimal? InjectionCharge { get; set; }
        public decimal? OtherCharge { get; set; }
        public decimal? TotalCharge => ConsultCharge + UsgCharge + UptCharge + InjectionCharge + OtherCharge;
        public bool? IsDeleted { get; set; }
        public long? PatientId { get; set; }
        public PatientDTO Patient { get; set; }
        public bool CheckExist { get; set; }
    }
}
