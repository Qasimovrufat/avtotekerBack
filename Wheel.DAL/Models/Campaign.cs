using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wheel.DAL.Models
{
    public class Campaign
    {
        [Key]
        public int id { get; set; }

        public string alias { get; set; } = "campaign";
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Makale Detayı")]
        public string az { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Makale ru")]
        public string ru { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Makale en")]
        public string en { get; set; }

        public List<string> TyreId { get; set; }
        public List<string> TyreName { get; set; }
        public short? status { get; set; }
    }
}