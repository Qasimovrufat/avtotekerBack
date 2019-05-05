using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Wheel.DAL.Context;
using Wheel.DAL.Models;

namespace Wheel.Security.Manager
{
    public class RoleManagerApp : RoleManager<RoleApp>
    {
        public RoleManagerApp(IRoleStore<RoleApp, string> store) : base(store)
        {
        }

        public static RoleManagerApp CreateRoleManager(IdentityFactoryOptions<RoleManagerApp> identityFactoryOptions, IOwinContext owinContext)
        {
            WheelDbContext context = owinContext.Get<WheelDbContext>();
            RoleManagerApp role = new RoleManagerApp(new RoleStore<RoleApp>(context));
            return role;
        }
    }
}