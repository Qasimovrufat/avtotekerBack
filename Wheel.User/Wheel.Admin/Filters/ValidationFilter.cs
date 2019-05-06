using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wheel.User.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var viewData = filterContext.Controller.ViewData;
            if (!viewData.ModelState.IsValid)
            {
                filterContext.HttpContext.Response.StatusCode = 400;
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {

                    var jsonResult = new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = (new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." })
                    };
                    filterContext.Result = jsonResult;

                }
                else
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewData = viewData,
                        TempData = filterContext.Controller.TempData
                    };
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}