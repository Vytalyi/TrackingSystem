using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingSystem.Models.Repository
{
    public static class ModelParser
    {
        public static Issue ParseIssue(int id, string title, string description, int statusId, string statusName, DateTime created,
			int assignedId, string assignedFname, string assignedLname, DateTime assignedRegistered, string assignedPassword, string assignLogin,
			int createdId, string createdFname, string createdLname, DateTime createdRegistered, string createdPassword, string createdLogin,
			DateTime lastModified)
        {
            Issue issue = new Issue();
            issue.Id = id;
            issue.Title = title;
            issue.Description = description;
            issue.Status = ParseStatus(statusId, statusName);
            issue.Created = created;
			issue.AssignedTo = ParseUser(assignedId, assignedFname, assignedLname, assignedRegistered, assignedPassword, assignLogin);
			issue.CreatedBy = ParseUser(createdId, createdFname, createdLname, createdRegistered, createdPassword, createdLogin);
			issue.LastModified = lastModified;

            return issue;
        }

        public static User ParseUser(int id, string fName, string lName, DateTime registered, string password, string login)
        {
            User user = new User();
            user.Id = id;
            user.Fname = fName;
            user.Lname = lName;
            user.Registered = registered;
			user.Password = password;
			user.Login = login;

            return user;
        }

        public static Status ParseStatus(int id, string name)
        {
            Status status = new Status();
            status.Id = id;
            status.Name = name;
            
            return status;
        }

		public static Comment ParseComment(int id, string message, DateTime created, int issueId, int addedById, int addedId, string addedFName, string addedLName,
			DateTime addedRegistered, string addedPassword, string addedLogin)
		{
			Comment comment = new Comment();
			comment.Id = id;
			comment.Message = message;
			comment.Created = created;
			comment.Issue_Id = issueId;

			comment.AddedBy = ParseUser(addedId, addedFName, addedLName, addedRegistered, addedPassword, addedLogin);

			return comment;
		}
    }
}