using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Wheel.DAL.Context;

namespace Wheel.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class TyreBrandController : BaseController
    {
        // GET: TyreBrand
        public ActionResult Index()
        {
            var data = Db.TyreBrand.ToList();
            return View(data);
        }

        [HttpGet]
        public JsonResult TyreBrandList(int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                //Get data from database
                var query = Db.TyreBrand.Select(tb => new
                {
                    id = tb.Id,
                    Name = tb.Name,
                    Description = tb.Description
                }).OrderBy(x=>x.Name);

                var totalCount = query.Count();
                var data = query.Skip(jtStartIndex).Take(jtPageSize).ToList();
                //Return result to jTable
                return Json(new {Result = "OK", Records = data, TotalRecordCount = totalCount}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new {Result = "ERROR", Message = ex.Message});
            }
        }

        [HttpPost]
        public JsonResult CreateTyreBrand(TyreBrand tyreBrand)
        {
            try
            {
                Db.TyreBrand.Add(tyreBrand);
                Db.SaveChanges();

                var model = new TyreModel
                {
                    BrandId = tyreBrand.Id,
                    Name = ""
                };
                Db.TyreModel.Add(model);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = tyreBrand });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
                
            }
        }

        [HttpPost]
        public JsonResult UpdateTyreBrand(TyreBrand tyreBrand)
        {
            try
            {
                Db.TyreBrand.AddOrUpdate(tyreBrand);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = tyreBrand });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult DeleteTyreBrand(int id)
        {
            try
            { 
                var at=new WheelDbContext();
                var tyreBrand = Db.TyreBrand.FirstOrDefault(tb => tb.Id == id);
                if (tyreBrand == null)
                    return Json(new {Result = "ERROR", Message = "there is not any TyreBrand Like this id!"});
                Db.TyreBrand.Remove(tyreBrand);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = tyreBrand });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }
    }
}