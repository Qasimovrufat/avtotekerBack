using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wheel.User.Helpers;
using Wheel.User.Models;
using res = Wheel.User.Properties.Resources;
namespace Wheel.User.Controllers
{
    public class TyresController : BaseController
    {
        // GET: Tyres
        public ActionResult Index()
        {
            cModel.breadCrumb = new List<Models.breadcrumbModel>{
                new Models.breadcrumbModel { home=true,name=res.home,url="/",style="homepage-link" },
                new Models.breadcrumbModel {home=false,name=res.tyres,url=null,style="" }
            };
            cModel.currency = Db.Currency.OrderByDescending(o => o.id).FirstOrDefault().DailyCurrency;
            cModel.lang = CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture());
            cModel.catalog = Db.TyreBrand.Where(w => w.TyreModel.Count > 1).Select(s => new catalogViewModel { Id = s.Id, Name = s.Name, Models = s.TyreModel.ToList() }).ToList();
            cModel.saleTyres.tyreList = Db.Tyre.Where(w => w.Sale > 0).Select(s => new DAL.Models.TyreViewModel
            {
                Id = s.Id,
                BrandName = s.TyreModel.TyreBrand.Name,
                ModelName = s.TyreModel.Name,
                Price = s.Price,
                Sale = s.Price - s.Sale,
                Images = s.Image.Where(w => w.AltText == "default").ToList()
            }).Take(4).ToList();
            return View(cModel);
        }

        public ActionResult Details(int id)
        {
            cModel.breadCrumb = new List<Models.breadcrumbModel>{
                new Models.breadcrumbModel { home=true,name=res.home,url="/",style="homepage-link" },
                new Models.breadcrumbModel {home=false,name=res.tyres,url=null,style="" }
            };
            cModel.currency = Db.Currency.OrderByDescending(o => o.id).FirstOrDefault().DailyCurrency;
            cModel.lang = CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture());
            cModel.catalog = Db.TyreBrand.Where(w => w.TyreModel.Count > 1).Select(s => new catalogViewModel { Id = s.Id, Name = s.Name, Models = s.TyreModel.ToList() }).ToList();
            cModel.saleTyres.tyreList = Db.Tyre.Where(w => w.Sale > 0).Select(s => new DAL.Models.TyreViewModel
            {
                Id = s.Id,
                BrandName = s.TyreModel.TyreBrand.Name,
                ModelName = s.TyreModel.Name,
                Price = s.Price,
                Sale = s.Price - s.Sale,
                Images = s.Image.Where(w => w.AltText == "default").ToList()
            }).Take(4).ToList();
            cModel.tyreDetails = Db.Tyre.Where(w => w.Id == id)
                .Select(s => new DAL.Models.TyreViewModel
                {
                    Id = s.Id,
                    BrandName = s.TyreModel.TyreBrand.Name,
                    ModelName = s.TyreModel.Name,
                    Price = s.Price,
                    Sale = s.Price - s.Sale,
                    Stok = s.Stok - (s.SoldTyre.GroupBy(sg => sg.TyreId).Select(st => new { say = st.Sum(sg => sg.Count) }).FirstOrDefault().say ?? 0),
                    Width = s.Width,
                    Height = s.Height,
                    Radius = s.Radius,
                    Season = s.Season,
                    Images = s.Image.ToList(),
                    Description=s.Description,
                    ModelDescription = s.TyreModel.Description,
                    isWinterByText = s.Season == 1 ? res.summer : (s.Season == 0?res.winter:res.fourSeason),
                    usedByText = s.UsageStatus == false ? res.menuNew : res.used
                }).FirstOrDefault(); 
            cModel.tyres.tyreList = Db.Tyre.Where(w => w.Id != cModel.tyreDetails.Id && w.Width == cModel.tyreDetails.Width && w.Height == cModel.tyreDetails.Height && w.Radius == cModel.tyreDetails.Radius).Select(s => new DAL.Models.TyreViewModel { Id = s.Id, BrandName = s.TyreModel.TyreBrand.Name, ModelName = s.TyreModel.Name, Price = s.Price, Sale = s.Price > 0 ? (s.Sale / s.Price) * 100 : 0, Images = s.Image.Where(w => w.AltText == "default").ToList() }).Take(8).ToList();
            return View(cModel);
        }
    }
}