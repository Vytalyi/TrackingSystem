using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingSystem.Models.Repository;
using TrackingSystem.Models;

namespace TrackingSystem.Controllers
{
	public abstract class BaseController : Controller
	{
		protected MsSqlRepository repo = new MsSqlRepository();

		protected ActionResult Error(Exception ex)
		{
			return View("Error", ex);
		}

		protected User GetLoggedUser()
		{
			// todo... must be a logged user id
			return repo.GetDefaultUser();
		}

		public bool Login(string username, string password)
		{
			JsonResult json = new JsonResult();

			int? id = repo.GetUserId(username, password);
			if (id != null)
			{
				User user = repo.GetUser((int)id);

				if (Session["loggedFullName"] != null)
					Session.Add("loggedFullName", user.FullName);
				else
					Session["loggedFullName"] = user.FullName;

				return true;
			}

			return false;
		}

		public void Logout()
		{
			if (Session["loggedFullName"] != null)
				Session.Remove("loggedFullName");
		}
	}
}
