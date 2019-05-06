using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wheel.DAL.Models
{
    public class CarBodyViewModel
    {
        public int? Id { get; set; }
        public int? ModelId { get; set; }
        public string Name { get; set; } = "";
        public string Year { get; set; } = "";
        public string Wheel { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Radius { get; set; }
        public string Description { get; set; }
        

    }
}