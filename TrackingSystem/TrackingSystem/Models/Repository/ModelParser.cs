using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingSystem.Models.Repository
{
    public static class ModelParser
    {
        public static Issue ParseIssue(int id, string title, string description, int statusId, string statusName,
            DateTime created, int assignedId, string assignedFname, string assignedLname, DateTime assignedRegistered)
        {
            Issue issue = new Issue();
            issue.Id = id;
            issue.Title = title;
            issue.Description = description;
            issue.Status = ParseStatus(statusId, statusName);
            issue.Created = created;
            issue.AssignedTo = ParseUser(assignedId, assignedFname, assignedLname, assignedRegistered);

            return issue;
        }

        public static User ParseUser(int id, string fName, string lName, DateTime registered)
        {
            User user = new User();
            user.Id = id;
            user.Fname = fName;
            user.Lname = lName;
            user.Registered = registered;

            return user;
        }

        public static Status ParseStatus(int id, string name)
        {
            Status status = new Status();
            status.Id = id;
            status.Name = name;
            
            return status;
        }

		public static Comment ParseComment(int id, string message, DateTime created, int issueId)
		{
			Comment comment = new Comment();
			comment.Id = id;
			comment.Message = message;
			comment.Created = created;
			comment.Issue_Id = issueId;

			return comment;
		}
    }
}