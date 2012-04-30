using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TrackingSystem.Models
{
    public class Issue
    {
        public int Id { get; set; }

		[DisplayName("Title")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "* Please specify a title")]
        public string Title { get; set; }

		[DisplayName("Description")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "* Please specify a description")]
        public string Description { get; set; }

        public User AssignedTo { get; set; }

        public Status Status { get; set; }

        public DateTime Created { get; set; }

		public IEnumerable<Comment> Comments { get; set; }
    }
}