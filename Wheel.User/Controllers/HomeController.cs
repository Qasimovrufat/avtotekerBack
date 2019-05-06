using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Wheel.Security.Manager;
using Wheel.User.Helpers;
using Wheel.User.Models;
using Wheel.DAL.Models;

namespace Wheel.User.Controllers
{
    public class HomeController : BaseController
    {
        #region Properties
        //public UserManagerApp UserManagerApp => HttpContext.GetOwinContext().GetUserManager<UserManagerApp>();

        //public RoleManagerApp RoleManagerApp => HttpContext.GetOwinContext().GetUserManager<RoleManagerApp>();
        #endregion
        public ActionResult Index()
        {
            cModel.breadCrumb = null;
            cModel.lang = CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture());
            cModel.currency = Db.Currency.OrderByDescending(o=>o.id).FirstOrDefault().DailyCurrency;
            cModel.index = new indexModel();
            cModel.index.sliders = Db.slider.Where(w => w.style == "dynamic").ToList();
            cModel.index.slide = Db.slider.SingleOrDefault(s => s.style == "static");
            cModel.catalog = Db.TyreBrand.Where(w=>w.TyreModel.Count>1).Select(s => new catalogViewModel { Id = s.Id, Name = s.Name, Models = s.TyreModel.ToList() }).ToList();
            cModel.tyres.tyreList = Db.Tyre.Where(w=>w.DisplayInDefault==true).Select(s => new TyreViewModel { Id = s.Id, BrandName = s.TyreModel.TyreBrand.Name, ModelName = s.TyreModel.Name, Price = s.Price, Sale = s.Price > 0 ? (s.Sale / s.Price) * 100 : 0, Images = s.Image.Where(w => w.AltText == "default").ToList(),Width=s.Width,Height=s.Height,Radius=s.Radius }).OrderBy(r => Guid.NewGuid()).Take(8).ToList();
            cModel.saleTyres.tyreList = Db.Tyre.Where(w=>w.Sale>0).Select(s => new TyreViewModel { Id = s.Id, BrandName = s.TyreModel.TyreBrand.Name, ModelName = s.TyreModel.Name, Price = s.Price, Sale = s.Price - s.Sale, Images = s.Image.Where(w => w.AltText == "default").ToList() }).Take(4).ToList();
            cModel.carbrand = Db.Car.ToList();
            cModel.pageContent = Db.Pages.Single(w => w.alias == "contact");
            cModel.tyreDetails = Db.Tyre.Where(w=>w.Id==5270).Select(s => new TyreViewModel { Id = s.Id, BrandName = s.TyreModel.TyreBrand.Name, ModelName = s.TyreModel.Name, Price = s.Price, Sale = s.Price > 0 ? (s.Sale / s.Price) * 100 : 0, Images = s.Image.Where(w => w.AltText == "default").ToList(),Description=s.Description }).FirstOrDefault();
            return View(cModel);
        }
        [HttpGet]
        public ActionResult SetCulture(string culture, string href)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(href);
        }
     
    }
}