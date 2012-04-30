using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingSystem.Models;
using TrackingSystem.Models.Repository;

namespace TrackingSystem.Controllers
{
	public class IssuesController : BaseController
	{
		[HttpGet]
		public ActionResult List(string sort)
		{
			IEnumerable<Issue> viewModel = null;

			try
			{
				switch (sort)
				{
					case "id":
						viewModel = repo.GetIssues().OrderBy(r => r.Id);
						break;
					case "title":
						viewModel = repo.GetIssues().OrderBy(r => r.Title);
						break;
					case "description":
						viewModel = repo.GetIssues().OrderBy(r => r.Description);
						break;
					case "created":
						viewModel = repo.GetIssues().OrderBy(r => r.Created);
						break;
					case "assigned":
						viewModel = repo.GetIssues().OrderBy(r => r.AssignedTo.FullName);
						break;
					case "status":
						viewModel = repo.GetIssues().OrderBy(r => r.Status.Name);
						break;
					default:
						viewModel = repo.GetIssues();
						break;
				}

				// replace \r\n with <br />
				for (int i = 0; i < viewModel.Count(); i++)
					viewModel.ElementAt(i).Description = viewModel.ElementAt(i).Description.Replace(Environment.NewLine, "<br />");
			}
			catch (Exception ex)
			{
				return Error(ex);
			}

			return View(viewModel);
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			Issue viewModel = null;

			try
			{
				viewModel = repo.GetIssue(id);
			}
			catch (Exception ex)
			{
				return Error(ex);
			}

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Edit(Issue issue)
		{
			try
			{
				repo.UpdateIssue(issue);
			}
			catch (Exception ex)
			{
				return Error(ex);
			}

			return RedirectToAction("List");
		}

		[HttpGet]
		public ActionResult Add()
		{
			var viewModel = new Issue();

			try
			{
				viewModel.Title = "New Issue";
				viewModel.Description = "New Description";
				viewModel.AssignedTo = repo.GetDefaultUser();
				viewModel.Status = repo.GetDefaultStatus();
			}
			catch (Exception ex)
			{
				return Error(ex);
			}

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Add(Issue issue)
		{
			try
			{
				if (ModelState.IsValid)
				{
					repo.AddIssue(issue);

					return RedirectToAction("List");
				}
			}
			catch (Exception ex)
			{
				return Error(ex);
			}

			return View();
		}

		[HttpGet]
		public ActionResult Details(int id)
		{
			Issue viewModel = null;

			try
			{
				viewModel = repo.GetIssue(id);

				// replace \r\n with <br />
				viewModel.Description = viewModel.Description.Replace(Environment.NewLine, "<br />");

				for (int i = 0; i < viewModel.Comments.Count(); i++)
					viewModel.Comments.ElementAt(i).Message = viewModel.Comments.ElementAt(i).Message.Replace(Environment.NewLine, "<br />");
			}
			catch (Exception ex)
			{
				return Error(ex);
			}

			return View(viewModel);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			try
			{
				repo.DeleteIssue(id);
			}
			catch (Exception ex)
			{
				return Error(ex);
			}

			return RedirectToAction("List");
		}

		[HttpGet]
		public ActionResult AddIssueComment(int id)
		{
			Comment viewModel = new Comment();

			try
			{
				viewModel.Issue_Id = id;
			}
			catch (Exception ex)
			{
				return Error(ex);
			}

			return View(viewModel);
		}

		[HttpPost]
		public ActionResult AddIssueComment(Comment comment)
		{
			try
			{
				if (ModelState.IsValid)
				{
					repo.AddComment(comment);

					return RedirectToAction("Details", new { id = comment.Issue_Id });
				}
			}
			catch (Exception ex)
			{
				return Error(ex);
			}

			return View();
		}

		public ActionResult DeleteComment(int id, int issueId)
		{
			try
			{
				repo.DeleteComment(id);
			}
			catch (Exception ex)
			{
				return Error(ex);
			}

			return RedirectToAction("Edit", new { id = issueId });
		}
	}
}
