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
			int userId = (int)Session["loggedId"];

			User user = repo.GetUser(userId);

			return user;
		}

		public bool Login(string username, string password)
		{
			JsonResult json = new JsonResult();

			int? id = repo.GetUserId(username, password);
			if (id != null)
			{
				User user = repo.GetUser((int)id);

				// write full name
				if (Session["loggedFullName"] != null)
					Session.Add("loggedFullName", user.FullName);
				else
					Session["loggedFullName"] = user.FullName;

				// write user id
				if (Session["loggedId"] != null)
					Session.Add("loggedId", user.Id);
				else
					Session["loggedId"] = user.Id;

				return true;
			}

			return false;
		}

		public void Logout()
		{
			// clear user full name
			if (Session["loggedFullName"] != null)
				Session.Remove("loggedFullName");

			// clear user id
			if (Session["loggedId"] != null)
				Session.Remove("loggedId");
		}
	}
}
