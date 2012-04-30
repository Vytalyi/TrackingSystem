using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrackingSystem.Models
{
    public class Comment
    {
        public int Id { get; set; }

		[DisplayName("Message")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "* Please specify a message")]
        public string Message { get; set; }

        public DateTime Created { get; set; }

		public int Issue_Id { get; set; }

		public User AddedBy { get; set; }
    }
}