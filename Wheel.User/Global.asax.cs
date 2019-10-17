using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Wheel.User.App_Start;
using Wheel.User.Filters;

namespace Wheel.User
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
            {
                HttpContext.Current.Cache.Remove((string)entry.Key);
            }
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new ValidationFilter());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
