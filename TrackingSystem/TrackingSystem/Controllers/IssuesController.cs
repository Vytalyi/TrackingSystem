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
            IEnumerable<Issue> viewModel;

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

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var viewModel = repo.GetIssue(id);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Issue issue)
        {
            repo.UpdateIssue(issue);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new Issue();
            viewModel.Title = "New Issue";
            viewModel.Description = "New Description";
            viewModel.AssignedTo = new User();
            viewModel.Status = new Status();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(Issue issue)
        {
            repo.AddIssue(issue);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var viewModel = repo.GetIssue(id);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            repo.DeleteIssue(id);

            return RedirectToAction("List");
        }
    }
}
