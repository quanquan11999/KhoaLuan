//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KhoaLuan.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Criterion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Criterion()
        {
            this.MotelRooms = new HashSet<MotelRoom>();
        }
    
        public int CriteriaID { get; set; }
        public string CriteriaName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MotelRoom> MotelRooms { get; set; }
    }
}
