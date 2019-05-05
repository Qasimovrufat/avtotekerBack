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
    public class CarController : BaseController
    {
        // GET: TyreBrand
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public JsonResult CarList(int jtStartIndex = 0, int jtPageSize = 0 )
        {
            try
            {
                //Get data from database
               // var totalCount=Db.Car.Count();
                var query = Db.Car.Select(tb => new
                {
                    id = tb.Id,
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
        public JsonResult CreateCar(Car car)
        {
            try
            {
                Db.Car.Add(car);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = car });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult UpdateCar(Car car)
        {
            try
            {
                Db.Car.AddOrUpdate(car);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = car });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult DeleteCar(int id)
        {
            try
            {
                var car = Db.Car.FirstOrDefault(tb => tb.Id == id);
                if (car == null)
                    return Json(new { Result = "ERROR", Message = "there is not any tyreModel Like this id!" });
                Db.Car.Remove(car);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = car });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }
        
    }
}