using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Wheel.DAL.Models
{
    public class Notification
    {
        public string Message { get; set; }
        public string Status { get; set; } = "succes";
        public string Position { get; set; } = "top-right";
    }
}