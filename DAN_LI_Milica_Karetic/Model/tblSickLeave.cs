//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAN_LI_Milica_Karetic.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSickLeave
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblSickLeave()
        {
            this.tblUsers = new HashSet<tblUser>();
        }
    
        public int SickLeaveID { get; set; }
        public System.DateTime SickLeaveDate { get; set; }
        public string Reason { get; set; }
        public string CompanyName { get; set; }
        public string EmergencyCase { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUser> tblUsers { get; set; }
    }
}
