using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class TblMedicineType
    {
        public TblMedicineType()
        {
            MedicineMaster = new HashSet<MedicineMaster>();
        }

        public int MedicineId { get; set; }
        public string MedicineType { get; set; }

        public virtual ICollection<MedicineMaster> MedicineMaster { get; set; }
    }
}
