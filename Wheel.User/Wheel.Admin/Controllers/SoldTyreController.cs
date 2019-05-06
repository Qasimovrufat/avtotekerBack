using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wheel.DAL.Context;

namespace Wheel.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class SoldTyreController : BaseController
    {
        // GET: SoldTyre
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult SoldTyreList(int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                //Get data from database
                var query = Db.SoldTyre.Where(x=>x.SoldDate==DateTime.Today).Select(s => new
                {
                    Id = s.Id,
                    TyreId = s.TyreId,
                    Count = s.Count,
                    SoldDate = s.SoldDate,
                    Amount = s.Amount,
                    CustomerName = s.CustomerName,
                    Description = s.Description
                }).OrderBy(x => x.Id);

                var totalCount = query.Count();
                var data = query.Skip(jtStartIndex).Take(jtPageSize).ToList();

                //Return result to jTable
                return Json(new { Result = "OK", Records = data, TotalRecordCount = totalCount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateSoldTyre(SoldTyre soldTyre)
        {
            try
            {
                Db.SoldTyre.Add(soldTyre);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = soldTyre });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult UpdateSoldTyre(SoldTyre soldTyre)
        {
            try
            {
                Db.SoldTyre.AddOrUpdate(soldTyre);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = soldTyre });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult DeleteSoldTyre(int id)
        {
            try
            {
                var soldTyre = Db.SoldTyre.FirstOrDefault(tb => tb.Id == id);
                if (soldTyre == null)
                    return Json(new { Result = "ERROR", Message = "there is not any soldTyre Like this id!" });
                Db.SoldTyre.Remove(soldTyre);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = soldTyre });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult GetTyreOptions()
        {
            try
            {
                var cars = Db.Tyre.Select(t => new { DisplayText = t.TyreModel.TyreBrand.Name.ToString() + " " + t.TyreModel.Name + " " 
                    + t.Width.ToString() + "/" + t.Height.ToString() + "/" + t.Radius.ToString(), Value = t.Id });
                return Json(new { Result = "OK", Options = cars });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetTyres()
        {
            var data = Db.Tyre.Select(t => new
            {
                id = t.Id,
                title = t.TyreModel.TyreBrand.Name.ToString() + " " + t.TyreModel.Name + " " + t.Width.ToString() + "/" + t.Height.ToString() + "/" + t.Radius.ToString()
            }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}