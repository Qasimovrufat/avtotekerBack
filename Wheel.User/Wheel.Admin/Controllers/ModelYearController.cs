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
    public class ModelYearController : BaseController
    {
        

        // GET: TyreBrand
        public ActionResult Index()
        {
            var data = Db.ModelYear.Select(s => s.Year).Distinct().ToList();
            ViewBag.Years = data;
            return View();
        }

        [HttpPost]
        public JsonResult ModelYearList(string year="", int jtStartIndex = 0, int jtPageSize = 10, string name = "")
        {
            try
            {

                //Get data from database
                var query = Db.ModelYear.Where(x=>x.Year.Contains(year)&&x.CarModel.Name.Contains(name)).Select(tb => new
                {
                    Id = tb.Id,
                    ModelId = tb.ModelId,
                    Year = tb.Year,
                    Description = tb.Description
                }).OrderBy(x=>x.ModelId);

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
        public JsonResult CreateModelYear(ModelYear modelYear)
        {
            try
            {
                Db.ModelYear.Add(modelYear);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = modelYear });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult UpdateModelYear(ModelYear modelYear)
        {
            try
            {
                Db.ModelYear.AddOrUpdate(modelYear);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = modelYear });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult DeleteModelYear(int id)
        {
            try
            {
                var modelYear = Db.ModelYear.FirstOrDefault(tb => tb.Id == id);
                if (modelYear == null)
                    return Json(new { Result = "ERROR", Message = "there is not any tyreModel Like this id!" });
                Db.ModelYear.Remove(modelYear);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = modelYear });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult GetModelOptions()
        {
            try
            {
                var carModels = Db.CarModel.Select(c => new { DisplayText = c.Name, Value = c.Id });
                return Json(new { Result = "OK", Options = carModels });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}