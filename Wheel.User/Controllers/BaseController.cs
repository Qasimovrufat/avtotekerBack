using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Wheel.DAL.Context;
using Wheel.User.Helpers;
using Wheel.User.Models;

namespace Wheel.User.Controllers
{
    public class BaseController : Controller
    {

        #region Properties

        private wheelEntities _entities;

        public wheelEntities Db
        {
            get { return _entities ?? (_entities = new wheelEntities()); }
            set { _entities = value; }

        }
        private commonModel _cmodel;

        public commonModel cModel
        {
            get { return _cmodel ?? (_cmodel = new commonModel()); }
            set { _cmodel = value; }
        }
        #endregion

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;            

            return base.BeginExecuteCore(callback, state);
        }
        public BaseController()
        {
            //cModel.lang = CultureHelper.GetNeutralCulture(CultureHelper.GetCurrentCulture());
        }
    }
}