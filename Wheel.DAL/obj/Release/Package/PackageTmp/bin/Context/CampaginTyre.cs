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
    
    public partial class CampaginTyre
    {
        public int Id { get; set; }
        public int TyreId { get; set; }
        public int CampaignId { get; set; }
        public string Description { get; set; }
    
        public virtual Pages Pages { get; set; }
        public virtual Tyre Tyre { get; set; }
    }
}
