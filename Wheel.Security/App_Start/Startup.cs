using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Wheel.DAL.Context;
using Wheel.Security.Manager;

[assembly: OwinStartup(typeof(Wheel.Security.App_Start.Startup))]

namespace Wheel.Security.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<WheelDbContext>(() => new WheelDbContext());
            app.CreatePerOwinContext<UserManagerApp>(UserManagerApp.CreateUserManager);
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = "Avtoteker",
                LoginPath = new PathString("/Login/Index")
            });
            app.CreatePerOwinContext<RoleManagerApp>(RoleManagerApp.CreateRoleManager);
        }
    }
}
