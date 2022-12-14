using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AASTHA2.Entities.Models
{
    public class Sp_GetCollection_Result
    {
        [Key]
        public Guid Id { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public int TotalPatient { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalCollection { get; set; }
    }
}
