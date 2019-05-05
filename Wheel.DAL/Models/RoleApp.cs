using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Wheel.DAL.Models
{
    public class RoleApp : IdentityRole
    {
        public string Description { get; set; }


        public RoleApp() : base()
        {

        }

        public RoleApp(string name) : base(name)
        {

        }
    }
}