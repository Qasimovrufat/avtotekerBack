//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wheel.DAL.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pages()
        {
            this.CampaginTyre = new HashSet<CampaginTyre>();
        }
    
        public int id { get; set; }
        public string alias { get; set; }
        public string az { get; set; }
        public string ru { get; set; }
        public string en { get; set; }
        public Nullable<short> status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CampaginTyre> CampaginTyre { get; set; }
    }
}
