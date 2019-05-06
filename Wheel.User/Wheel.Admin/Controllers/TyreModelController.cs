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
    public class TyreModelController : BaseController
    {
      // GET: TyreBrand
        public ActionResult Index(){
            
            return View();
        }

        [HttpGet]
        public JsonResult TyreModelList(int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                //Get data from database
                var query = Db.TyreModel.Select(tb => new
                {
                    id = tb.Id,
                    BrandId=tb.BrandId,
                    Name = tb.Name,
                    Description = tb.Description
                }).OrderBy(x=>x.Name);

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
        public JsonResult CreateTyreModel(TyreModel tyreModel)
        {
            try
            {
                Db.TyreModel.Add(tyreModel);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = tyreModel });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult UpdateTyreModel(TyreModel tyreModel)
        {
            try
            {
                Db.TyreModel.AddOrUpdate(tyreModel);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = tyreModel });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult DeleteTyreModel(int id)
        {
            try
            {
                var tyreModel = Db.TyreModel.FirstOrDefault(tb => tb.Id == id);
                if (tyreModel == null)
                    return Json(new { Result = "ERROR", Message = "there is not any tyreModel Like this id!" });
                Db.TyreModel.Remove(tyreModel);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = tyreModel });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult GetBrandOptions()
        {
            try
            {
                var tyreBrands = Db.TyreBrand.Select(c => new { DisplayText = c.Name, Value = c.Id });
                return Json(new { Result = "OK", Options = tyreBrands });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}