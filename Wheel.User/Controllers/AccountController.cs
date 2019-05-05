using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Wheel.Security.Manager;
using Wheel.Security;
using Wheel.User.Controllers;
using Wheel.DAL.Models;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace Wheel.User.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AccountController : BaseController
    {
        #region Properties

        public UserManagerApp UserManagerApp
        {
            get { return HttpContext.GetOwinContext().GetUserManager<UserManagerApp>(); }
        }

        public RoleManagerApp RoleManagerApp
        {
            get { return HttpContext.GetOwinContext().GetUserManager<RoleManagerApp>(); }
        }

        #endregion

        // Show Users
        public ActionResult Index()
        {
            return View(UserManagerApp.Users);
        }

        [HttpPost]
        public async Task<ActionResult> Index(IEnumerable<string> RoleName, string userid)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    IEnumerable<string> rolenames = RoleName ?? new List<string>();
                    IEnumerable<string> selectedRoleNames = rolenames;
                   // IEnumerable<string> unselectedRoleNAmes = Helper.GetRoles().Select(a => a.Name).Except(rolenames);

                    foreach (var role in selectedRoleNames.ToList())
                    {
                        if (!UserManagerApp.IsInRole(userid, role))
                        {
                            await UserManagerApp.AddToRoleAsync(userid, role);
                           // Success("User added to role" + role, true);
                        }
                    }

                    //foreach (var srole in unselectedRoleNAmes.ToList())
                    //{
                    //    if (UserManagerApp.IsInRole(userid, srole))
                    //    {
                    //        await UserManagerApp.RemoveFromRolesAsync(userid, srole);
                    //       // Success("User removed from role " + srole, true);
                    //    }
                    //}

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                //Danger("Somethings was wrong!", true);
                ModelState.AddModelError("", "Something was wrong");
                return View();
            }

        }

        //Create Users
        public ActionResult Create()
        {
            return View(new UserApp());
        }

        public JsonResult Login(UserApp user,string password)
        {
            UserApp thisUser = UserManagerApp.Find(user.Email, password);
            if(thisUser != null)
            {
                ClaimsIdentity Ident = UserManagerApp.CreateIdentity(thisUser, DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.GetOwinContext().Authentication.SignOut();

                HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
                {
                    IsPersistent = false
                }, Ident);
                return Json(new { status = "ok" });
            }
            else
            {
                return Json(new { status = "error" });
            }
        }

        public ActionResult Logout(UserApp user, string password)
        {
          
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<JsonResult> Create(UserApp user, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserApp u = new UserApp();
                    u.UserName = user.Email;
                    u.LastLoginDateTime = DateTime.Now;
                    u.FullName = user.FullName;
                    u.PhoneNumber = user.PhoneNumber;
                    u.Email = user.Email;

                    IdentityResult UserAddResult = await UserManagerApp.CreateAsync(u, password);

                    if (UserAddResult.Succeeded)
                    {

                        UserApp thisUser = UserManagerApp.Find(user.Email, password);
                        if (thisUser != null)
                        {
                            ClaimsIdentity Ident = UserManagerApp.CreateIdentity(thisUser, DefaultAuthenticationTypes.ApplicationCookie);
                            HttpContext.GetOwinContext().Authentication.SignOut();

                            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties()
                            {
                                IsPersistent = false
                            }, Ident);
                            return Json(new { status = "login" });
                        }

                        return Json(new { status = "ok" });
                        //IdentityResult RoleAddResult = await UserManagerApp.AddToRoleAsync(u.Id, "User");
                        //if (RoleAddResult.Succeeded)
                        //{
                        //   // Success("User Added Successfuly!");
                        //    return Json(new { status = "ok" });
                        //}

                        //else
                        //{                            
                        //    return Json(new { status = "error",message= UserAddResult.Errors});
                        //}
                    }

                    else
                    {                      
                        return Json(new { status = "error", message = UserAddResult.Errors });
                    }

                }
                else
                {

                    return Json(new { status = "error", message = new {text = "error"} });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ActionResult> Edit(string id)
        {
            UserApp user = await UserManagerApp.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                ModelState.AddModelError("", "There Isn't any role like this id");
                return View(new UserApp());
            }

        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserApp user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserApp oldUser = await UserManagerApp.FindByIdAsync(user.Id);
                    if (oldUser != null)
                    {
                        oldUser.UserName = user.UserName;
                        oldUser.Email = user.Email;
                        oldUser.PhoneNumber = user.PhoneNumber;
                        IdentityResult result = await UserManagerApp.UpdateAsync(oldUser);

                        if (result.Succeeded)
                        {
                           // Success("User Updated", true);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                           // Danger("Failed! Something was wrong!");
                            result.Errors.ToList().ForEach(err => ModelState.AddModelError("", err));
                            return View(user);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "There Isn't any role like this id");
                        return View(new UserApp());
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
               // Danger("Failed! Something was wrong!");
                throw;
            }

        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {

            try
            {
                UserApp user = await UserManagerApp.FindByIdAsync(id);
                IdentityResult result = await UserManagerApp.DeleteAsync(user);
                if (result.Succeeded)
                {
                   // Success("User Successfuly deleted!");
                    return RedirectToAction("Index");
                }
                else
                {
                    //Danger("Failed! Something was wrong!");
                    ModelState.AddModelError("", "error");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                //Danger("Failed! Something was wrong!");
                throw;
            }
        }

        public ActionResult Detail(string id)
        {
            return View(UserManagerApp.FindById(id));
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(string confirmPassword, string newPassword,
            string currentPassword)
        {
            try
            {
                var hashedPassword = UserManagerApp.PasswordHasher.HashPassword(currentPassword);
                var user = await UserManagerApp.FindByIdAsync(this.User.Identity.GetUserId());
                if (newPassword == confirmPassword)
                {
                    IdentityResult result =
                        await
                            UserManagerApp.ChangePasswordAsync(User.Identity.GetUserId(), currentPassword, newPassword);
                    if (result.Succeeded)
                    {
                      //  Success("Password updated succesfully");
                        return RedirectToAction("Index", "Home");
                    }
                }

              //  Danger("Something was wrong");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ActionResult> Profile(string id)
        {
            try
            {
                if (id == User.Identity.GetUserId())
                {
                    UserApp user = await UserManagerApp.FindByIdAsync(id);
                    return View(user);
                }

               // Danger("Failed! Something was wrong!");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Profile(UserApp user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (user.Id == User.Identity.GetUserId())
                    {
                        UserApp oldUser = await UserManagerApp.FindByIdAsync(user.Id);
                        if (oldUser != null)
                        {
                            oldUser.UserName = user.UserName;
                            oldUser.Email = user.Email;
                            oldUser.PhoneNumber = user.PhoneNumber;
                            IdentityResult result = await UserManagerApp.UpdateAsync(oldUser);

                            if (result.Succeeded)
                            {
                               // Success("User Updated", true);
                                return RedirectToAction("Index");
                            }
                            else
                            {
                               // Danger("Failed! Something was wrong!");
                                result.Errors.ToList().ForEach(err => ModelState.AddModelError("", err));
                                return View(user);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "There Isn't any role like this id");
                            return View(new UserApp());
                        }
                    }
                   // Danger("Failed! Something was wrong!");
                    return View();
                }
                else
                {
                   // Warning("Model is not valid!");
                    return View(user);
                }
            }
            catch (Exception)
            {
             //   Danger("Failed! Something was wrong!");
                throw;
            }

        }
    }
}