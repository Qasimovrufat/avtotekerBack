using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Wheel.Security.Manager;

namespace Wheel.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class HomeController : BaseController
    {
        #region Properties
        public UserManagerApp UserManagerApp => HttpContext.GetOwinContext().GetUserManager<UserManagerApp>();

        public RoleManagerApp RoleManagerApp => HttpContext.GetOwinContext().GetUserManager<RoleManagerApp>();
        #endregion
        public ActionResult Index()
        {
            return View(UserManagerApp.Users.ToList());
        }
    }
}