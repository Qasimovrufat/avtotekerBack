using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Wheel.DAL.Context;
using Wheel.DAL.Models;

namespace Wheel.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ImageController : BaseController
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public ActionResult Delete(int id,int? tyreId)
        {
            try
            {
                var data = Db.Image.SingleOrDefault(i => i.Id == id);
                if (data != null)
                {
                    Db.Image.Remove(data);
                    Db.SaveChanges();
                    TempData["Notification"] = new Notification() {Message = "Image Deleted Succesfuly",Status = "success",Position = "top-right"};
                    return tyreId==null ? RedirectToAction("Create", "Tyre") : RedirectToAction("Edit","Tyre", new { id = tyreId });
                }
                else
                {
                    return tyreId == null ? RedirectToAction("Create", "Tyre") : RedirectToAction("Edit", "Tyre", new { id = tyreId });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<JsonResult> Upload(IEnumerable<HttpPostedFileBase> files,string id="0")
        {
            try
            {
                if (files.FirstOrDefault()==null)
                {
                    Response.StatusCode = 400;
                    return Json(new { ok = 400 }, JsonRequestBehavior.AllowGet);
                }
                List<string> imageId=new List<string>();
                List<string> imagePath = new List<string>();
                foreach (var file in files)
                {
                    if (file == null) continue;
                    Image image = new Image();
                    var fileName = Path.GetFileName(file.FileName) + DateTime.Now.ToString("yyyyMMdd-HHMMss") + Path.GetExtension(file.FileName);
                    var filePath = "/Images/" + fileName;
                    var serverPath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(serverPath);
                    image.Path = fileName;
                    image.TyreId = (id=="0"||id=="")?(int?)null:int.Parse(id);
                    image.Status = true;
                    image.AltText = "default";
                    Db.Image.Add(image);
                    await Db.SaveChangesAsync();
                    imageId.Add(image.Id.ToString());
                    imagePath.Add(filePath);
                }
                return Json(new {ImageId=imageId,ImagePath=imagePath, ok = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exx)
            {
                Response.StatusCode = 400;
                return Json(new { ok = 400 }, JsonRequestBehavior.AllowGet);
                throw;
            }
        }
    }
}