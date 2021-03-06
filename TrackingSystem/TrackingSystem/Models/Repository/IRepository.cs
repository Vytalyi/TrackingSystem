﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingSystem.Models.Repository
{
    public interface IRepository
    {
        Issue GetIssue(int id);
        IEnumerable<Issue> GetIssues();
        void AddIssue(Issue newIssue);
        void UpdateIssue(Issue newIssue);
        void DeleteIssue(int id);
        
        IEnumerable<User> GetUsers();
		User GetUser(int id);
		User GetDefaultUser();
		int? GetUserId(string username, string password);

        IEnumerable<Status> GetStatuses();
		Status GetDefaultStatus();

		IEnumerable<Comment> GetCommentsForIssue(int issueId);
		void DeleteComment(int id);

		IEnumerable<Priority> GetPriorities();
    }
}