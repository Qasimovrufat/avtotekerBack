using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Wheel.DAL.Context;
using Wheel.DAL.Enums;
using Wheel.DAL.Models;

namespace Wheel.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class TyreController : BaseController
    {
        #region Properties



        #endregion

        #region Tyre Index

        /// <summary>
        /// Show Tyre List as TyreViewModel list
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var data = Db.Tyre.Where(t => t.Status == true).Select(t => new TyreViewModel
            {
                Id = t.Id,
                BrandId = t.TyreModel.BrandId,
                BrandName = t.TyreModel.TyreBrand.Name,
                ModelName = t.TyreModel.Name,
                ModelId = t.ModelId,
                Height = t.Height,
                Width = t.Width,
                Radius = t.Radius,
                Price = t.Price,
                DisplayInDefault = t.DisplayInDefault,
                Season = t.Season,
                Stok = t.Stok - ((t.SoldTyre.GroupBy(x => x.TyreId).Select(d => new
                {
                    sum = d.Sum(s => s.Count)
                })).FirstOrDefault().sum ?? 0),
                Images = t.Image.ToList(),
                DefaultImage =
                            t.Image.FirstOrDefault(s => s.AltText.Contains("default")).Path ?? "",
                UsageStatus = t.UsageStatus,
                Description = t.Description,

            }).ToList();
            return View(data);
        }

        #endregion

        #region Create Tyre

        public ActionResult Create()
        {
            return View(new TyreViewModel());
        }

        [HttpPost]
        public async Task<JsonResult> Create(TyreViewModel tyre)
        {
            try
            {
                var data = new Tyre
                {
                    ModelId = tyre.ModelId,
                    Width = tyre.Width,
                    Height = tyre.Height,
                    DisplayInDefault = tyre.DisplayInDefault,
                    Radius = tyre.Radius,
                    UsageStatus = tyre.UsageStatus,
                    Price = tyre.Price,
                    Stok = tyre.Stok,
                    Season = tyre.Season,
                    Status = tyre.Status,
                    Description = tyre.Description,
                    Sale = tyre.Sale
                };
                Db.Tyre.Add(data);
                await Db.SaveChangesAsync();

                var images = Db.Image.ToList();
                if (tyre.ImageId != null)
                {
                    foreach (var item in tyre.ImageId)
                    {
                        var image = images.FirstOrDefault(i => i.Id == int.Parse(item));
                        if (image != null) image.TyreId = data.Id;
                    }
                    await Db.SaveChangesAsync();
                    return Json(new { ok = 200 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { error = 400 }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                throw;
            }
        }
        #endregion

        #region Edit Tyre

        public ActionResult Edit(int id)
        {
            try
            {
                var data = Db.Tyre.Where(t => t.Id == id).Select(t => new TyreViewModel
                {
                    Id = t.Id,
                    BrandId = t.TyreModel.BrandId,
                    BrandName = t.TyreModel.TyreBrand.Name,
                    ModelName = t.TyreModel.Name,
                    ModelId = t.ModelId,
                    Height = t.Height,
                    DisplayInDefault =t.DisplayInDefault,
                    Width = t.Width,
                    Radius = t.Radius,
                    Price = t.Price,
                    Season = t.Season,
                    Sale = t.Sale,
                    Stok = t.Stok,
                    //Stok = t.Stok - ((t.SoldTyre.GroupBy(x => x.TyreId).Select(d => new
                    //{
                    //    sum = d.Sum(s => s.Count)
                    //})).FirstOrDefault().sum ?? 0),
                    Images = t.Image.Where(a=>a.Status==true).ToList(),
                    DefaultImage = "/Images/" + (
                        t.Image.FirstOrDefault(s => s.AltText.Contains("default")&&t.Status==true).Path ?? t.Image.FirstOrDefault(i=>i.Status==true).Path),
                    Status = t.Status,
                    UsageStatus = t.UsageStatus,
                    Description = t.Description,

                }).FirstOrDefault();
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpPost]
        public async Task<JsonResult> Edit(TyreViewModel tyre)
        {
            try
            {

                var data = Db.Tyre.FirstOrDefault(t => t.Id == tyre.Id);
                if (data == null) return Json(new { ok = 400 }, JsonRequestBehavior.AllowGet);
                data.ModelId = tyre.ModelId;
                data.Width = tyre.Width;
                data.Height = tyre.Height;
                data.Radius = tyre.Radius;
                data.DisplayInDefault=tyre.DisplayInDefault;
                data.UsageStatus = tyre.UsageStatus;
                data.Price = tyre.Price;
                data.Stok = tyre.Stok;
                data.Season = tyre.Season;
                data.Status = tyre.Status;
                data.Description = tyre.Description;
                data.Sale = tyre.Sale;

                await Db.SaveChangesAsync();

                var images = Db.Image.ToList();
                if (tyre.ImageId != null)
                {
                    foreach (var item in tyre.ImageId)
                    {
                        var image = images.FirstOrDefault(i => i.Id == int.Parse(item));
                        if (image != null) image.TyreId = data.Id;
                    }
                    await Db.SaveChangesAsync();
                    return Json(new { ok = 200 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { error = 400 }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                throw;
            }

        }

        #endregion

        #region Tyre Detail

        public ActionResult Detail(int id)
        {
            try
            {
                var data = Db.Tyre.Where(t => t.Id == id).Select(t => new TyreViewModel
                {
                    Id = t.Id,
                    BrandId = t.TyreModel.BrandId,
                    BrandName = t.TyreModel.TyreBrand.Name,
                    ModelName = t.TyreModel.Name,
                    ModelId = t.ModelId,
                    DisplayInDefault = t.DisplayInDefault,
                    Height = t.Height,
                    Width = t.Width,
                    Radius = t.Radius,
                    Price = t.Price,
                    Season = t.Season,
                    Stok = t.Stok,
                    Images = t.Image.ToList(),
                    DefaultImage = "/Images/" + (
                        t.Image.FirstOrDefault(s => s.AltText.Contains("default")).Path ?? t.Image.FirstOrDefault().Path),
                    Status = t.Status,
                    UsageStatus = t.UsageStatus,
                    Description = t.Description,
                }).FirstOrDefault();
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region Delete Tyre

        [HttpGet]
        public JsonResult Delete(int id)
        {
            try
            {
                var data = Db.Tyre.SingleOrDefault(i => i.Id == id);
                if (data != null)
                {
                    data.Status = false;
                    Db.Tyre.AddOrUpdate(data);
                    Db.SaveChanges();
                    return Json(new { ok = 200 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Response.StatusCode = 404;
                    return Json(new { ok = 400 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                return Json(new { ok = 400 }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
        #endregion

        #region Get brand and models

        [HttpGet]
        public JsonResult GetBrands(int? id)
        {
            List<TyreBrandDto> data;
            if (id == null)
            {
                data = Db.TyreModel.Select(x => new TyreBrandDto()
                {
                    Id = x.BrandId,
                    Name = x.TyreBrand.Name
                }).Distinct().ToList();
            }
            else
            {
                data = Db.TyreModel.Where(m => m.Id == id).Select(x => new TyreBrandDto
                {
                    Id = x.BrandId,
                    Name = x.TyreBrand.Name
                }).Distinct().ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetModels(int? id)
        {
            List<TyreModelDto> data;
            if (id == null)
            {
                data = Db.TyreModel.Select(x => new TyreModelDto()
                {
                    Id = x.Id,
                    Name = x.Name
                }).Distinct().ToList();
            }
            else
            {
                data = Db.TyreModel.Where(m => m.BrandId == id).Select(x => new TyreModelDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).Distinct().ToList();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
        public class TyreModelDto
        {
            public int? Id { get; set; }
            public string Name { get; set; }
        }

        public class TyreBrandDto
        {
            public int? Id { get; set; }
            public string Name { get; set; }
        }
    }
}