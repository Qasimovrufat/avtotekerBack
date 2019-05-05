using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wheel.DAL.Context;

namespace Wheel.DAL.Models
{
    public class TyreViewModel
    {
        public HttpPostedFileBase FileBase { get; set; }
        public List<Image> Images { get; set; }
        public string[] ImageId { get; set; }
        public string DefaultImage { get; set; }
        public int Id { get; set; }
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Radius { get; set; }
        public bool UsageStatus { get; set; }
        public double Price { get; set; }
        public int Stok { get; set; }
        public short Season { get; set; } = -1;
        public bool Status { get; set; }
        public string Description { get; set; }
        public string ModelDescription { get; set; }
        public double? Sale { get; set; } = 0;
        public bool DisplayInDefault { get; set; } = false;
        public string usedByText { get; set; }
        public string isWinterByText { get; set; }
    }
}