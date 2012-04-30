using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingSystem.Models.Repository;

namespace TrackingSystem.Controllers
{
    public abstract class BaseController : Controller
    {
        protected MsSqlRepository repo = new MsSqlRepository();

		protected ActionResult Error(Exception ex)
		{
			return View("Error", ex);
		}
    }
}
