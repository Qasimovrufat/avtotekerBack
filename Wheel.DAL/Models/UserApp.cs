using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Wheel.DAL.Models
{
    public class UserApp : IdentityUser
    {
        public DateTime LastLoginDateTime { get; set; }
        public string FullName { get; set; }
       // public string LastName { get; set; }
    }
}