using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wheel.DAL.Context;
using Wheel.DAL.Models;

namespace Wheel.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class CarBodyController : BaseController
    {


        // GET: TyreBrand
        public ActionResult Index()
        {
            var data = Db.ModelYear.Select(s => s.Year).Distinct().ToList();
            ViewBag.Years = data;
            return View();
        }

        [HttpPost]
        public JsonResult CarBodyList(CarBodyViewModel carBodyViewModel, int jtStartIndex = 0, int jtPageSize = 10)
        {
            try
            {
                //Get data from database
                var query = Db.CarBody.Select(tb => new CarBodyViewModel()
                {
                    Id = tb.Id,
                    ModelId = tb.ModelYear.ModelId,
                    Name = tb.Name,
                    Year = tb.ModelYear.Year,
                    Wheel = tb.Wheels,
                    Description = tb.Description
                }).ToList();

                List<CarBodyViewModel> returnedData = query;


                for (int i = 0; i < query.Count; i++)
                {
                    returnedData[i].Width = JArray.Parse(query[i].Wheel)[0]["wheel"][0]["front"][0]["tire_width"][0].ToString();
                    returnedData[i].Height = JArray.Parse(query[i].Wheel)[0]["wheel"][0]["front"][0]["tire_aspect_ratio"][0].ToString();
                    returnedData[i].Radius = JArray.Parse(query[i].Wheel)[0]["wheel"][0]["front"][0]["rim_diameter"][0].ToString();
                }

                var totalCount = returnedData.Count();
                var data = returnedData.Skip(jtStartIndex).Take(jtPageSize).ToList();

                //Return result to jTable
                return Json(new { Result = "OK", Records = data, TotalRecordCount = totalCount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateCarBody(CarBodyViewModel carBodyViewModel)
        {
            try
            {

                var data = Db.ModelYear.FirstOrDefault(my => my.Year == carBodyViewModel.Year && my.ModelId == carBodyViewModel.ModelId);
                if (data == null)
                {
                    data = new ModelYear
                    {
                        Year = carBodyViewModel.Year,
                        ModelId = carBodyViewModel.ModelId
                    };
                    Db.ModelYear.Add(data);
                }

                List<object> front = new List<object>
                {
                   new {tire_width= new List<object> { int.Parse(carBodyViewModel.Width)},tire_aspect_ratio=new List<object> {int.Parse(carBodyViewModel.Height)},rim_diameter=new List<object> {int.Parse(carBodyViewModel.Radius)}}
                };
                var frontjs = new List<object> { new { front = front } };
                var wheel = new List<object> { new { wheel = frontjs } };
                var javaScriptSerializer = new
                System.Web.Script.Serialization.JavaScriptSerializer();
                string jsonWheel = javaScriptSerializer.Serialize(wheel);

                var carBody = new CarBody
                {
                    Name = carBodyViewModel.Name,
                    ModelYearId = data.Id,
                    Wheels = jsonWheel,
                    Description = carBodyViewModel.Description

                };
                Db.CarBody.Add(carBody);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = carBodyViewModel });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult UpdateCarBody(CarBodyViewModel carBodyViewModel)
        {
            try
            {

                var data = Db.ModelYear.FirstOrDefault(my => my.Year == carBodyViewModel.Year && my.ModelId == carBodyViewModel.ModelId);
                if (data == null)
                {
                    data = new ModelYear
                    {
                        Year = carBodyViewModel.Year,
                        ModelId = carBodyViewModel.ModelId
                    };
                    Db.ModelYear.Add(data);
                }

                List<object> front = new List<object>
                {
                   new {tire_width= new List<object> { int.Parse(carBodyViewModel.Width)},tire_aspect_ratio=new List<object> {int.Parse(carBodyViewModel.Height)},rim_diameter=new List<object> {int.Parse(carBodyViewModel.Radius)}}
                };
                var frontjs = new List<object> { new { front = front } };
                var wheel = new List<object> { new { wheel = frontjs } };
                var javaScriptSerializer = new
                System.Web.Script.Serialization.JavaScriptSerializer();
                string jsonWheel = javaScriptSerializer.Serialize(wheel);

                var carBody = new CarBody
                {
                    Id = carBodyViewModel.Id ?? 0,
                    Name = carBodyViewModel.Name,
                    ModelYearId = data.Id,
                    Wheels = jsonWheel,
                    Description = carBodyViewModel.Description
                };
                Db.CarBody.AddOrUpdate(carBody);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = carBodyViewModel });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult DeleteCarBody(int id)
        {
            try
            {
                var carBody = Db.CarBody.FirstOrDefault(tb => tb.Id == id);
                if (carBody == null)
                {
                    Response.StatusCode = 400;
                    return Json(new { Result = "ERROR", Message = "there is not any tyreModel Like this id!" });
                }
                Db.CarBody.Remove(carBody);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = carBody });
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