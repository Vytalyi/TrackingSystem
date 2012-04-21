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
        public ActionResult List(IEnumerable<Issue> viewModel)
        {
            if (viewModel == null)
                viewModel = repo.GetIssues();

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
