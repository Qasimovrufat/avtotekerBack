using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wheel.DAL.Models
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public int? TyreId { get; set; }
        public bool Status { get; set; }
        public string AltText { get; set; }
        public string Description { get; set; }
    }
}