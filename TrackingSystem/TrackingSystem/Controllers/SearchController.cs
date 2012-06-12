using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingSystem.Models;

namespace TrackingSystem.Controllers
{
    public class SearchController : BaseController
    {
        public ActionResult Index()
        {
			Issue viewModel = new Issue();
			viewModel.AssignedTo = repo.GetDefaultUser();
			viewModel.CreatedBy = repo.GetDefaultUser();

            return View(viewModel);
        }

		[HttpPost]
		public ActionResult Index(Issue issue)
		{
			List<Issue> viewModel = new List<Issue>();
			viewModel = repo.GetIssues().Where(
				r => (issue.Id != 0) ? r.Id == issue.Id : true &&
					(!string.IsNullOrEmpty(issue.Title)) ? r.Title == issue.Title : true &&
					(!string.IsNullOrEmpty(issue.Description)) ? r.Description == issue.Description : true &&
					(issue.AssignedTo != null) ? r.AssignedTo.Id == issue.AssignedTo.Id : true &&
					(issue.CreatedBy != null) ? r.CreatedBy.Id == issue.CreatedBy.Id : true
				).ToList();

			ViewBag.List = viewModel;

			return View(issue);
		}
    }
}
