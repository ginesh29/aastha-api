//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Migration
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_general
    {
        public int general_Id { get; set; }
        public Nullable<int> Ipd_Id { get; set; }
        public string general_diagnosis { get; set; }
    
        public virtual tbl_Ipd tbl_Ipd { get; set; }
    }
}
