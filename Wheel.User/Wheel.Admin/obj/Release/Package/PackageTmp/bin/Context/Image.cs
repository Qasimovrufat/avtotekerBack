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
    
    public partial class Image
    {
        public int Id { get; set; }
        public short MediaType { get; set; }
        public string UserId { get; set; }
        public Nullable<int> TyreId { get; set; }
        public bool Status { get; set; }
        public string AltText { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Tyre Tyre { get; set; }
    }
}