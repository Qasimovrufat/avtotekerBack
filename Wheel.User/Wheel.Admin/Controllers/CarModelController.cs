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
    public class CarModelController : BaseController
    {
        // GET: TyreBrand
        public ActionResult Index(){
            
            return View();
        }

        [HttpGet]
        public JsonResult CarModelList(int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                //Get data from database
                var query = Db.CarModel.Select(tb => new
                {
                    id = tb.Id,
                    CarId=tb.CarId,
                    Name = tb.Name,
                    Description = tb.Description
                }).OrderBy(x=>x.CarId);

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
        public JsonResult CreateCarModel(CarModel carModel)
        {
            try
            {
                Db.CarModel.Add(carModel);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = carModel });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult UpdateCarModel(CarModel carModel)
        {
            try
            {
                Db.CarModel.AddOrUpdate(carModel);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = carModel });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult DeleteCarModel(int id)
        {
            try
            {
                var carModel = Db.CarModel.FirstOrDefault(tb => tb.Id == id);
                if (carModel == null)
                    return Json(new { Result = "ERROR", Message = "there is not any tyreModel Like this id!" });
                Db.CarModel.Remove(carModel);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = carModel });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult GetCarOptions()
        {
            try
            {
                var cars = Db.Car.Select(c => new { DisplayText = c.Name, Value = c.Id });
                return Json(new { Result = "OK", Options =cars });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}