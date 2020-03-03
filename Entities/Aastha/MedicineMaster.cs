using System;
using System.Collections.Generic;

namespace AASTHA.Entities
{
    public partial class MedicineMaster
    {
        public int MedicineTypeId { get; set; }
        public string MedicineName { get; set; }
        public int? MedicineType { get; set; }

        public virtual TblMedicineType MedicineTypeNavigation { get; set; }
    }
}
