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
    
    public partial class CarBody
    {
        public int Id { get; set; }
        public int ModelYearId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Wheels { get; set; }
    
        public virtual ModelYear ModelYear { get; set; }
    }
}