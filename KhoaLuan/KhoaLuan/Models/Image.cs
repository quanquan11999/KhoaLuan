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
    
    public partial class Image
    {
        public int ImageID { get; set; }
        public string Url { get; set; }
        public Nullable<int> MotelID { get; set; }
    
        public virtual MotelRoom MotelRoom { get; set; }
    }
}
