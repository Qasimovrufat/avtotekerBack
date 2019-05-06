using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wheel.DAL.Context;

namespace Wheel.Admin.Controllers
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

        #endregion
    }
}