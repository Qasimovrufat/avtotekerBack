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
        public ActionResult Index(TyreSearchModel model,int page=1,int limit=8, string order = "id")
        {
            try
            {
                model.priceMin = Convert.ToInt32(model.priceArray.Split(',')[0]);
                model.priceMax = Convert.ToInt32(model.priceArray.Split(',')[1]);
            }
            catch (Exception ex)
            {
                model.priceMin = 0;
                model.priceMax = 9999;
            }

            if (limit != 8 && limit != 16 && limit != 24)
            {
                return HttpNotFound();
            }


            cModel.breadCrumb = new List<Models.breadcrumbModel>{
                new Models.breadcrumbModel { home=true,name=res.home,url="/",style="homepage-link" },
                new Models.breadcrumbModel {home=false,name=res.tyres,url=null,style="" }
            };
            cModel.currency = Db.Currency.OrderByDescending(o => o.id).FirstOrDefault().DailyCurrency;
            cModel.lang = CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture());
            cModel.catalog = Db.TyreBrand.Where(w => w.TyreModel.Count > 1).Select(s => new catalogViewModel { Id = s.Id, Name = s.Name, Models = s.TyreModel.ToList() }).ToList();
            ViewBag.page = page;
           
            cModel.saleTyres.tyreList = Db.Tyre.Where(w => w.Sale > 0).Where(t=>(model.BrandId!=0?t.TyreModel.BrandId == model.BrandId:true) 
            && (model.ModelId!=0 ? t.ModelId==model.ModelId : true) && (model.Width!=null? t.Width == model.Width:true)
            && (model.Height !=null ? t.Height == model.Height : true) && (model.Radius !=null? t.Radius == model.Radius :true)
               && (model.priceMin > 0 ? t.Price > model.priceMin : true)
                        && (model.priceMax < 9999 ? t.Price < model.priceMax : true)
            )
            .Select(s => new DAL.Models.TyreViewModel
            {
                Id = s.Id,
                BrandId = s.TyreModel.TyreBrand.Id,
                BrandName = s.TyreModel.TyreBrand.Name,
                ModelName = s.TyreModel.Name,
                ModelId = s.TyreModel.Id,
                Width = s.Width,
                Height = s.Height,
                Radius = s.Radius,
                Price = s.Price,
                Sale = s.Price - s.Sale,
                Images = s.Image.Where(w => w.AltText == "default").ToList()
            }).
            OrderBy(s => s.BrandName).ToList();
            switch (order)
            {
                case "idDesc":
                    cModel.saleTyres.tyreList = cModel.saleTyres.tyreList.OrderByDescending(o => o.Id);
                    break;
                case "price":
                    cModel.saleTyres.tyreList = cModel.saleTyres.tyreList.OrderBy(o => o.Price);
                    break;
                case "priceDesc":
                    cModel.saleTyres.tyreList = cModel.saleTyres.tyreList.OrderByDescending(o => o.Price);
                    break;
                case "brandAsc":
                    cModel.saleTyres.tyreList = cModel.saleTyres.tyreList.OrderBy(o => o.BrandName);
                    break;

            }
            ViewBag.total = (int)Math.Ceiling(((cModel.saleTyres.tyreList.Count() > 0 ? cModel.saleTyres.tyreList.Count() : 1) / (decimal)(limit > 0 ? limit : 1)));
            cModel.saleTyres.tyreList = cModel.saleTyres.tyreList.Skip(((int)page - 1) * (int)limit).Take((int)limit);
            ViewBag.order = order;
            ViewBag.limit = limit;
            cModel.tyreSearchModel = model;

            if (ViewBag.page > ViewBag.total)
            {
                return HttpNotFound();
            }
          

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