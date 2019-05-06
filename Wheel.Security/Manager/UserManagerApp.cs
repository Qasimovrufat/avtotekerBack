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
    public class UserManagerApp : UserManager<UserApp>
    {
        public UserManagerApp(IUserStore<UserApp> store) : base(store)
        {

        }

        public static UserManagerApp CreateUserManager(IdentityFactoryOptions<UserManagerApp> identityFactoryOptions, IOwinContext owinContext)
        {
            WheelDbContext context = owinContext.Get<WheelDbContext>();
            UserManagerApp user = new UserManagerApp(new UserStore<UserApp>(context));
            Microsoft.AspNet.Identity.PasswordValidator passwordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true
            };

            UserValidator<UserApp> userValidator = new UserValidator<UserApp>(user)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = true
            };

            user.UserValidator = userValidator;
            user.PasswordValidator = passwordValidator;
            return user;
        }
    }
}