using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Wheel.DAL.Context;
using Wheel.DAL.Models;

namespace Wheel.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class CampaignController : BaseController
    {
        // GET: Campaign
        public ActionResult Index()
        {
            var data = Db.Pages.Where(t =>  t.status == 1).ToList();
            return View(data);
        }

        [HttpPost]
        public ActionResult Index(Campaign campaign)
        {
            var data = new Pages
            {
                alias = campaign.alias,
                az = campaign.az,
                en = campaign.en,
                ru = campaign.ru,
                status = 1
            };

            Db.Pages.Add(data);
            Db.SaveChanges();
            return View(campaign);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Campaign campaign)
        {
            var data = new Pages
            {
                alias = campaign.alias,
                az = campaign.az,
                en = campaign.en,
                ru = campaign.ru,
                status = 1
            };

            Db.Pages.Add(data);
            Db.SaveChanges();

            if (campaign.TyreId.Count <= 0) return View(campaign);
            foreach (var tyre in campaign.TyreId)
            {
                var campaginTyre = new CampaginTyre
                {
                    CampaignId = data.id,
                    TyreId = int.Parse(tyre)
                };
                Db.CampaginTyre.Add(campaginTyre);
            }
            Db.SaveChanges();
            return View(campaign);

        }

        public ActionResult Edit(int id)
        {
            try
            {
                var data = Db.Pages.First(p => p.id == id /*&& p.alias == "campaign"*/);
                var campagin = new Campaign
                {
                    alias = data.alias,
                    az = data.az,
                    en = data.en,
                    ru = data.ru,
                    TyreId = data.CampaginTyre.Where(x => x.CampaignId == data.id).Select(x => x.TyreId.ToString()).ToList(),
                    TyreName = data.CampaginTyre.Where(x => x.CampaignId == data.id).Select(x => x.Tyre.TyreModel.TyreBrand.Name + " " + x.Tyre.TyreModel.Name + " " + x.Tyre.Width + "/" + x.Tyre.Height + "/R " + x.Tyre.Radius).ToList()

                };
                return View(campagin);

            }
            catch (Exception )
            {

                throw;
            }


        }

        [HttpPost]
        public ActionResult Edit(Campaign campaign)
        {
            var data = new Pages
            {
                id = campaign.id,
                alias = campaign.alias,
                az = campaign.az,
                en = campaign.en,
                ru = campaign.ru,
                status = 1
            };

            Db.Pages.AddOrUpdate(data);
            Db.SaveChanges();

            if (campaign.TyreId == null) return RedirectToAction("Index", "Campaign");

            var curentList = Db.CampaginTyre.Where(t => t.CampaignId == data.id).ToList();
            var rejectList = campaign.TyreId.Select(tyreId => new CampaginTyre
            {
                CampaignId = data.id,
                TyreId = int.Parse(tyreId.ToString())
            }).ToList();
            var filteredList = curentList.Except(rejectList).ToList();
            List<CampaginTyre> addedList = rejectList.Except(curentList).ToList();
            Db.CampaginTyre.RemoveRange(filteredList);
            Db.CampaginTyre.AddRange(addedList);
            Db.SaveChanges();
            return RedirectToAction("Index", "Campaign");

        }


        #region Delete Tyre

        [HttpGet]
        public JsonResult Delete(int id)
        {
            try
            {
                var data = Db.Pages.SingleOrDefault(i => i.id == id);
                if (data != null)
                {
                    data.status = 0;
                    Db.Pages.AddOrUpdate(data);
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
                
            }
        }
        #endregion
    }
}