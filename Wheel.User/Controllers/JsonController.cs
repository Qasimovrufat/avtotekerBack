using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wheel.DAL.Context;
using Wheel.DAL.Models;
using Wheel.User.Models;
using res = Wheel.User.Properties.Resources;
namespace Wheel.User.Controllers
{
    public class JsonController : BaseController
    {
        // GET: Json
        [HttpGet]
        public JsonResult searchByTyres(TyreSearchModel search)
        {
            var prices = search.priceArray.Split(',');
            search.priceMax = Convert.ToInt16(prices[1]);
            search.priceMin = Convert.ToInt16(prices[0]);


            var tyres = Db.Tyre.Where(w =>
            search.BrandId != 0 ? w.TyreModel.BrandId == search.BrandId : w.Id > 0
            && search.ModelId != 0 ? w.ModelId == search.ModelId : w.Id > 0
            && w.Price < search.priceMax && w.Price > search.priceMin
            //&& search.Radius != "" ? w.Radius == search.Radius : w.Id > 0
            //&& search.Width != "" ? w.Width == search.Width : w.Id > 0
            //&& search.Height != "" ? w.Height == search.Height : w.Id > 0
            && search.usage != null ? w.UsageStatus == search.UsageStatus : w.Id > 0
            ) //todo improve search result
            //add winter or not to search
            //add price search result
                .Select(s => new DAL.Models.TyreViewModel
                {
                    Id = s.Id,
                    BrandId = s.TyreModel.BrandId,
                    BrandName = s.TyreModel.TyreBrand.Name,
                    ModelName = s.TyreModel.Name,
                    ModelId = s.ModelId,
                    Price = s.Price,
                    Sale = s.Price > 0 ? (s.Sale / s.Price) * 100 : 0,
                    Width = s.Width.ToString(),
                    Radius = s.Radius.ToString(),
                    Height = s.Height.ToString()
                }).ToList();
            var brands = tyres.GroupBy(g => new { g.BrandName, g.BrandId }).Select(s => new TyreBrand { Id = s.Key.BrandId, Name = s.Key.BrandName }).OrderBy(o=>o.Name).ToList();
            var models = tyres.GroupBy(g => new { g.ModelName, g.ModelId }).Select(s => new TyreModel { Id = s.Key.ModelId, Name = s.Key.ModelName }).OrderBy(o=>o.Name).ToList();
            brands.Insert(0, new TyreBrand { Id = 0, Name = /*res.pleaseSelect + " " +*/ res.brand });
            models.Insert(0, new TyreModel { Id = 0, Name = /*res.pleaseSelect + " " +*/ res.model });
            var width = tyres.GroupBy(g => new { g.Width }).Select(s => new { Id = s.Key.Width, Name = s.Key.Width.ToString() }).OrderBy(o=>o.Name).ToList();
            var height = tyres.GroupBy(g => new { g.Height }).Select(s => new { Id = s.Key.Height, Name = s.Key.Height.ToString() }).OrderBy(o => o.Name).ToList();
            var radius = tyres.GroupBy(g => new { g.Radius }).Select(s => new { Id = s.Key.Radius, Name = s.Key.Radius.ToString() }).OrderBy(o => o.Name).ToList();
            //var usages = tyres.GroupBy(g => new { g.UsageStatus }).Select(s => new { Id = s.Key.UsageStatus, Name = s.Key.UsageStatus.ToString() }).ToList();
            width.Insert(0, new { Id = "", Name = /*res.pleaseSelect + " " +*/ res.width });
            height.Insert(0, new { Id = "", Name = /*res.pleaseSelect + " " + */ res.height });
            radius.Insert(0, new { Id = "", Name = /*res.pleaseSelect + " " + */ res.radius });
            //usages.Insert(0, new { Name = res.pleaseSelect + " " + res.winter });
            var result = new { brand = brands, model = models, width = width, height = height, radius = radius };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult listTyres(TyreSearchModel search, int page = 1, int total = 6, string order = "id")
        {


            if (search == null) { return Json(new { tyres = "" }, JsonRequestBehavior.AllowGet); }
            try
            {
                search.priceMin = Convert.ToInt32(search.priceArray.Split(',')[0]);
                search.priceMax = Convert.ToInt32(search.priceArray.Split(',')[1]);
            }
            catch (Exception ex)
            {
                search.priceMin = 0;
                search.priceMax = 9999;
            }
            var tyres = Db.Tyre.Where(w =>
                        (search.BrandId > 0 ? w.TyreModel.BrandId == search.BrandId : true)
                        && (search.ModelId > 0 ? w.ModelId == search.ModelId : true)
                        && (search.priceMin > 0 ? w.Price > search.priceMin : true)
                        && (search.priceMax < 9999 ? w.Price < search.priceMax : true)
                        && (search.Radius != null ? w.Radius == search.Radius : true)
                        && (search.Width != null ? w.Width == search.Width : true)
                        && (search.Height != null ? w.Height == search.Height : true) 
                         && (search.Sale != null ? w.Sale > search.Sale : true)
                         && (search.Season >-1 ? w.Season > search.Season : true)
                         && (search.campaign > 0 ? w.CampaginTyre.Where(cw => cw.CampaignId == search.campaign).ToList().Count > 0 : true)
                           && (search.usage != null ? w.UsageStatus == search.UsageStatus : true)

                        ).Select(s => new
                        {
                            Id = s.Id,
                            BrandId = s.TyreModel.BrandId,
                            BrandName = s.TyreModel.TyreBrand.Name,
                            ModelName = s.TyreModel.Name,
                            ModelId = s.ModelId,
                            Price = s.Price,
                            //Sale = s.Price - s.Sale,
                            Sale = s.Price > 0 ? (s.Sale / s.Price) * 100 : 0,
                            Width = s.Width,
                            Radius = s.Radius,
                            Height = s.Height,
                            Images = s.Image.Where(wi => wi.AltText == "default").Select(si => new imageView { Id = si.Id, Path = si.Path }).ToList()
                        }).AsQueryable();
            var totalTyre = tyres.Count();
            tyres = tyres.OrderByDescending(o => o.Id);
            switch (order)
            {
                case "idDesc":
                    tyres = tyres.OrderByDescending(o => o.Id);
                    break;
                case "price":
                    tyres = tyres.OrderBy(o => o.Price);
                    break;
                case "priceDesc":
                    tyres = tyres.OrderByDescending(o => o.Price);
                    break;
                case "brandAsc":
                    tyres = tyres.OrderBy(o => o.BrandName);
                    break;

            }
            var lastTyresList = tyres.ToPagedList(page, total).ToList();
            var result = new { tyres = lastTyresList, total = totalTyre, page = page };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult listModels(int id)
        {
            var models = Db.CarModel.Where(w => w.CarId == id).Select(s => new { Id = s.Id, Name = s.Name }).ToList();
            return Json(new { model = models }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult listYears(int id)
        {
            var years = Db.ModelYear.Where(w => w.ModelId == id).Select(s => new { Id = s.Id, Year = s.Year }).ToList();
            return Json(new { year = years }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult listBans(int id)
        {
            var bans = Db.CarBody.Where(w => w.ModelYearId == id).Select(s => new { Id = s.Id, Name = s.Name }).ToList();
            return Json(new { ban = bans }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult listWheels(int id)
        {
            var wheels = Db.CarBody.Where(w => w.Id == id).Select(s => new { Id = s.Id, wheels = s.Wheels }).ToList();
            return Json(new { wheel = wheels }, JsonRequestBehavior.AllowGet);
        }

        public class imageView
        {
            public int Id { get; set; }
            public string Path { get; set; }
        }
    }
}