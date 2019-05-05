using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Wheel.DAL.Models;
using Wheel.Security.Manager;

namespace Wheel.Admin.Controllers
{
    public class LoginController : Controller
    {
        #region Properties

        public UserManagerApp UserManagerApp => HttpContext.GetOwinContext().GetUserManager<UserManagerApp>();
        public RoleManagerApp RoleManagerApp => HttpContext.GetOwinContext().GetUserManager<RoleManagerApp>();

        #endregion

        #region Login

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Index(UserLogin userLogin, string ReturnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserApp user = await UserManagerApp.FindByEmailAsync(userLogin.Email);
                    UserApp curentUser = await UserManagerApp.FindAsync(user.UserName, userLogin.Password);
                    if (curentUser == null)
                    {
                        ModelState.AddModelError("Login Error:", "Email or Password Is incorrect");
                        return View();
                    }
                    else
                    {
                        var data = curentUser.Roles;
                        
                        ClaimsIdentity ident = await UserManagerApp.CreateIdentityAsync(curentUser,
                            DefaultAuthenticationTypes.ApplicationCookie);
                        
                        HttpContext.GetOwinContext().Authentication.SignOut();
                        if (userLogin.Remember)
                        {
                            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
                            {
                                IsPersistent = true
                            }, ident);
                        }
                        else
                        {
                            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
                            {
                                IsPersistent = false
                            }, ident);
                        }
                    }
                    var x = User.IsInRole("Admin");

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index","Home");

                }


                return View(userLogin);

            }
            catch (Exception)
            {

                throw;
            }


        }

        #endregion

        #region Logout

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index");
        }

        #endregion
    }
}