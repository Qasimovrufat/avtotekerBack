using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Wheel.DAL.Context;
using Wheel.User.Helpers;
using Wheel.User.Models;
using res = Wheel.User.Properties.Resources;
namespace Wheel.User.Controllers
{
    public class PagesController : BaseController
    {
        // GET: Pages
        #region Properties
        //public UserManagerApp UserManagerApp => HttpContext.GetOwinContext().GetUserManager<UserManagerApp>();

        //public RoleManagerApp RoleManagerApp => HttpContext.GetOwinContext().GetUserManager<RoleManagerApp>();
        #endregion
        public ActionResult Aboutus()
        {
            cModel.breadCrumb = new List<Models.breadcrumbModel>{
                new Models.breadcrumbModel { home=true,name=res.home,url="/",style="homepage-link" },
                new Models.breadcrumbModel {home=false,name=res.aboutUs,url=null,style="" }
            };
            cModel.currency = Db.Currency.OrderByDescending(o => o.id).FirstOrDefault().DailyCurrency;
            cModel.lang = CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture());
            cModel.catalog = Db.TyreBrand.Where(w => w.TyreModel.Count > 1).Select(s => new catalogViewModel { Id = s.Id, Name = s.Name, Models = s.TyreModel.ToList() }).ToList();
            cModel.saleTyres.tyreList = Db.Tyre.Where(w => w.Sale > 0).Select(s => new DAL.Models.TyreViewModel { Id = s.Id, BrandName = s.TyreModel.TyreBrand.Name, ModelName = s.TyreModel.Name, Price = s.Price, Sale = s.Price - s.Sale, Images = s.Image.Where(w => w.AltText == "default").ToList() }).Take(4).ToList();
            cModel.pageContent = Db.Pages.Single(w => w.alias == "aboutus");
            return View(cModel);
        }
        public ActionResult Campaign() //todo renew model
        {
            cModel.breadCrumb = new List<Models.breadcrumbModel>{
                new Models.breadcrumbModel { home=true,name=res.home,url="/",style="homepage-link" },
                new Models.breadcrumbModel {home=false,name=res.campaign,url=null,style="" }
            };
            cModel.currency = Db.Currency.OrderByDescending(o => o.id).FirstOrDefault().DailyCurrency;
            cModel.lang = CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture());
            cModel.catalog = Db.TyreBrand.Where(w => w.TyreModel.Count > 1).Select(s => new catalogViewModel { Id = s.Id, Name = s.Name, Models = s.TyreModel.ToList() }).ToList();
            cModel.saleTyres.tyreList = Db.Tyre.Where(w => w.Sale > 0).Select(s => new DAL.Models.TyreViewModel { Id = s.Id, BrandName = s.TyreModel.TyreBrand.Name, ModelName = s.TyreModel.Name, Price = s.Price, Sale = s.Price - s.Sale, Images = s.Image.Where(w => w.AltText == "default").ToList() }).Take(4).ToList();
            cModel.pageContents = Db.Pages.Where(w => w.alias == "campaign").ToList();

            return View(cModel);
        }
        public ActionResult CampaignDetails(int id) //todo renew model
        {
            cModel.breadCrumb = new List<Models.breadcrumbModel>{
                new Models.breadcrumbModel { home=true,name=res.home,url="/",style="homepage-link" },
                new Models.breadcrumbModel {home=false,name=res.campaign,url="/page/campaign",style="" }
            };
            cModel.currency = Db.Currency.OrderByDescending(o => o.id).FirstOrDefault().DailyCurrency;
            cModel.lang = CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture());
            cModel.catalog = Db.TyreBrand.Where(w => w.TyreModel.Count > 1).Select(s => new catalogViewModel { Id = s.Id, Name = s.Name, Models = s.TyreModel.ToList() }).ToList();
            cModel.saleTyres.tyreList = Db.Tyre.Where(w => w.Sale > 0).Select(s => new DAL.Models.TyreViewModel { Id = s.Id, BrandName = s.TyreModel.TyreBrand.Name, ModelName = s.TyreModel.Name, Price = s.Price, Sale = s.Price - s.Sale, Images = s.Image.Where(w => w.AltText == "default").ToList() }).Take(4).ToList();
            cModel.pageContent = Db.Pages.FirstOrDefault(f => f.id==id);

            return View(cModel);
        }
        [HttpGet]
        public ActionResult Contact()
        {
            cModel.breadCrumb = new List<Models.breadcrumbModel>{
                new Models.breadcrumbModel { home=true,name=res.home,url="/",style="homepage-link" },
                new Models.breadcrumbModel {home=false,name=res.contact,url=null,style="" }
            };
            cModel.currency = Db.Currency.OrderByDescending(o => o.id).FirstOrDefault().DailyCurrency;
            cModel.catalog = Db.TyreBrand.Where(w => w.TyreModel.Count > 1).Select(s => new catalogViewModel { Id = s.Id, Name = s.Name, Models = s.TyreModel.ToList() }).ToList();
            cModel.lang = CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture());
            cModel.saleTyres.tyreList = Db.Tyre.Where(w => w.Sale > 0).Select(s => new DAL.Models.TyreViewModel { Id = s.Id, BrandName = s.TyreModel.TyreBrand.Name, ModelName = s.TyreModel.Name, Price = s.Price, Sale = s.Price - s.Sale, Images = s.Image.Where(w => w.AltText == "default").ToList() }).Take(4).ToList();
            cModel.pageContent = Db.Pages.Single(w => w.alias == "contact");
            return View(cModel);
        }
        [HttpPost]
        public JsonResult Contact(contactModel contact)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(contact.email);
                mail.To.Add("avtoteker@mail.ru");
                mail.Subject = "Mail From Avtoteker by " + contact.name;
                // mail.ReplyTo = new MailAddress(contact.email);
                mail.Body = contact.body;

                using (SmtpClient smtp = new SmtpClient("mail.avtoteker.az", 25))
                {
                    smtp.Credentials = new NetworkCredential("from@avtoteker.az", "FromAvtotekerAz!");
                    smtp.EnableSsl = false;
                    smtp.Send(mail);
                }

                return Json(new { status = "success", message = res.messageSuccess });

            }
            catch (Exception ex)
            {
                return Json(new { status = "warning", message = res.messageError });
            }

        }
    }
}