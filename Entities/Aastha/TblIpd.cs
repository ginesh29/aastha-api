using System;
using System.Collections.Generic;

namespace AASTHA.Entities
{
    public partial class TblIpd
    {
        public TblIpd()
        {
            IpdChargeDetails = new HashSet<IpdChargeDetails>();
            TblDelivery = new HashSet<TblDelivery>();
            TblGeneral = new HashSet<TblGeneral>();
            TblOperation = new HashSet<TblOperation>();
        }

        public int IpdId { get; set; }
        public string IpdReceiptId { get; set; }
        public string DeptName { get; set; }
        public int? PatientId { get; set; }
        public string RoomType { get; set; }
        public DateTime? AddmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public decimal? Bill { get; set; }
        public decimal? Conssesion { get; set; }

        public virtual TblPatient Patient { get; set; }
        public virtual ICollection<IpdChargeDetails> IpdChargeDetails { get; set; }
        public virtual ICollection<TblDelivery> TblDelivery { get; set; }
        public virtual ICollection<TblGeneral> TblGeneral { get; set; }
        public virtual ICollection<TblOperation> TblOperation { get; set; }
    }
}
