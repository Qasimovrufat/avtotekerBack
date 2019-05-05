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
    public class CurrencyController : BaseController
    {
        // GET: TyreBrand
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public JsonResult CurrencyList(int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                //Get data from database
                // var totalCount=Db.Car.Count();
                var query = Db.Currency.Select(tb => new
                {
                    id = tb.id,
                    DailyCurrency = tb.DailyCurrency,
                    Day=tb.Day,
                    Description = tb.Description
                }).OrderByDescending(x => x.id);

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
        public JsonResult CreateCurrency(Currency currency)
        {
            try
            {
                Db.Currency.Add(currency);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = currency });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult UpdateCurrency(Currency currency)
        {
            try
            {
                Db.Currency.AddOrUpdate(currency);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = currency });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult DeleteCurrency(int id)
        {
            try
            {
                var currency = Db.Currency.FirstOrDefault(tb => tb.id == id);
                if (currency == null)
                    return Json(new { Result = "ERROR", Message = "there is not any currency Like this id!" });
                Db.Currency.Remove(currency);
                Db.SaveChanges();
                return Json(new { Result = "OK", Record = currency });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });

            }
        }
    }
}