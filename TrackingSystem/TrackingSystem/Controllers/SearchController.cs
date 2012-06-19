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
			SearchIssue viewModel = new SearchIssue();
			viewModel.Issue = new Issue();

			try
			{
				viewModel.Issue.AssignedTo = GetLoggedUser();
				viewModel.Issue.CreatedBy = GetLoggedUser();
			}
			catch (Exception)
			{
				viewModel.Issue.AssignedTo = repo.GetDefaultUser();
				viewModel.Issue.CreatedBy = repo.GetDefaultUser();
			}

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Index(SearchIssue search)
		{
			IEnumerable<Issue> viewModel = repo.GetIssues();

			if (search.Issue.Id != 0 && search.IsFindById)
				viewModel = viewModel.Where(r => r.Id == search.Issue.Id);
			if (!string.IsNullOrEmpty(search.Issue.Title) && search.IsFindByTitle)
				viewModel = viewModel.Where(r => r.Title.Contains(search.Issue.Title));
			if (!string.IsNullOrEmpty(search.Issue.Description) && search.IsFindByDescription)
				viewModel = viewModel.Where(r => r.Description.Contains(search.Issue.Description));
			if (search.Issue.AssignedTo != null && search.IsFindByAssignedTo)
				viewModel = viewModel.Where(r => r.AssignedTo.Id == search.Issue.AssignedTo.Id);
			if (search.Issue.Status != null && search.IsFindByStatus)
				viewModel = viewModel.Where(r => r.Status.Id == search.Issue.Status.Id);
			if (search.Issue.CreatedBy != null && search.IsFindByCreatedBy)
				viewModel = viewModel.Where(r => r.CreatedBy.Id == search.Issue.CreatedBy.Id);

			ViewBag.List = viewModel.ToList();

			return View(search);
		}
	}
}
