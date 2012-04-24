using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TrackingSystem.Models.Repository
{
    public class MsSqlRepository : IRepository
    {
        #region private items

        private string ConnStr
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            }
        }

        #endregion

        #region get

        public Issue GetIssue(int id)
        {
            Issue issue = new Issue();

            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                string txt = @"
                    SELECT i.Id, i.Title, i.Description, s.Id, s.Name, i.Created, u.Id, u.Fname, u.Lname, u.Registered
                    FROM Issues i
                    INNER JOIN Users u on u.Id = i.Assigned_Id
                    INNER JOIN Statuses s on s.Id = i.Status_Id
                    WHERE i.Id = @Id";
                using (SqlCommand cmd = new SqlCommand(txt, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                    rdr.Read();

                    issue = ModelParser.ParseIssue((int)rdr[0], (string)rdr[1], (string)rdr[2], (int)rdr[3], (string)rdr[4],
                        DateTime.Parse(rdr[5].ToString()), (int)rdr[6], (string)rdr[7], (string)rdr[8], DateTime.Parse(rdr[9].ToString()));
                }
            }

			issue.Comments = GetCommentsForIssue(issue.Id);

            return issue;
        }

        public IEnumerable<Issue> GetIssues()
        {
            List<Issue> list = new List<Issue>();

            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                string txt = @"
                    SELECT i.Id, i.Title, i.Description, s.Id, s.Name, i.Created, u.Id, u.Fname, u.Lname, u.Registered
                    FROM Issues i
                    INNER JOIN Users u on u.Id = i.Assigned_Id
                    INNER JOIN Statuses s on s.Id = i.Status_Id";
                using (SqlCommand cmd = new SqlCommand(txt, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        Issue issue = ModelParser.ParseIssue((int)rdr[0], (string)rdr[1], (string)rdr[2], (int)rdr[3], (string)rdr[4],
                            DateTime.Parse(rdr[5].ToString()), (int)rdr[6], (string)rdr[7], (string)rdr[8], DateTime.Parse(rdr[9].ToString()));

                        list.Add(issue);
                    }
                }
            }

			foreach (Issue issue in list)
			{
				issue.Comments = GetCommentsForIssue(issue.Id);
			}

            return list;
        }

        public IEnumerable<Status> GetStatuses()
        {
            List<Status> list = new List<Status>();

            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                string txt = @"
                    SELECT Id, Name
                    FROM Statuses";
                using (SqlCommand cmd = new SqlCommand(txt, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        Status status = ModelParser.ParseStatus((int)rdr[0], (string)rdr[1]);

                        list.Add(status);
                    }
                }
            }

            return list;
        }

        public IEnumerable<User> GetUsers()
        {
            List<User> list = new List<User>();

            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                string txt = @"
                    SELECT Id, Fname, Lname, Registered
                    FROM Users";
                using (SqlCommand cmd = new SqlCommand(txt, con))
                {
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        User user = ModelParser.ParseUser((int)rdr[0], (string)rdr[1], (string)rdr[2], DateTime.Parse(rdr[3].ToString()));

                        list.Add(user);
                    }
                }
            }

            return list;
        }

		public IEnumerable<Comment> GetCommentsForIssue(int issueId)
		{
			List<Comment> list = new List<Comment>();

            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                string txt = @"
                    SELECT Id, Message, Created, Issue_Id
                    FROM Comments
					WHERE Issue_Id = @issueId";
                using (SqlCommand cmd = new SqlCommand(txt, con))
                {
					cmd.Parameters.AddWithValue("@issueId", issueId);

                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        Comment comment = ModelParser.ParseComment((int)rdr[0], (string)rdr[1], DateTime.Parse(rdr[2].ToString()), (int)rdr[3]);

						list.Add(comment);
                    }
                }
            }

            return list;
		}

        #endregion

        #region update

        public void UpdateIssue(Issue newIssue)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                string txt = @"
                    UPDATE Issues
                    SET Title = @Title,
                        Description = @Description,
                        Assigned_Id = @Assigned_Id,
                        Status_Id = @Status_Id
                    WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(txt, con))
                {
                    cmd.Parameters.AddWithValue("@Title", newIssue.Title);
                    cmd.Parameters.AddWithValue("@Description", newIssue.Description);
                    cmd.Parameters.AddWithValue("@Assigned_Id", newIssue.AssignedTo.Id);
                    cmd.Parameters.AddWithValue("@Status_Id", newIssue.Status.Id);
                    cmd.Parameters.AddWithValue("@Id", newIssue.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region add

        public void AddIssue(Issue newIssue)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                string txt = @"
                    INSERT INTO Issues (Title, Description, Assigned_Id, Status_Id)
                    VALUES (@Title, @Description, @Assigned_Id, @Status_Id)
                ";
                using (SqlCommand cmd = new SqlCommand(txt, con))
                {
                    cmd.Parameters.AddWithValue("@Title", newIssue.Title);
                    cmd.Parameters.AddWithValue("@Description", newIssue.Description);
                    cmd.Parameters.AddWithValue("@Assigned_Id", 4); // no user
                    cmd.Parameters.AddWithValue("@Status_Id", 1); // open status
                    cmd.ExecuteNonQuery();
                }
            }
        }

		public void AddComment(Comment newComment)
		{
			using (SqlConnection con = new SqlConnection(ConnStr))
			{
				con.Open();

				string txt = @"
                    INSERT INTO Comments (Message, Issue_Id)
                    VALUES (@Message, @Issue_Id)
                ";
				using (SqlCommand cmd = new SqlCommand(txt, con))
				{
					cmd.Parameters.AddWithValue("@Message", newComment.Message);
					cmd.Parameters.AddWithValue("@Issue_Id", newComment.Issue_Id);
					cmd.ExecuteNonQuery();
				}
			}
		}

        #endregion

        #region delete

        public void DeleteIssue(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnStr))
            {
                con.Open();

                string txt = @"
                    DELETE FROM Issues
                    WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(txt, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

		public void DeleteComment(int id)
		{
			using (SqlConnection con = new SqlConnection(ConnStr))
			{
				con.Open();

				string txt = @"
                    DELETE FROM Comments
                    WHERE Id = @Id";
				using (SqlCommand cmd = new SqlCommand(txt, con))
				{
					cmd.Parameters.AddWithValue("@Id", id);
					cmd.ExecuteNonQuery();
				}
			}
		}

        #endregion
    }
}