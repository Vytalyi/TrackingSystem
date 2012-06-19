using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TrackingSystem.Models
{
    public class SearchIssue
    {
		public bool IsFindById { get; set; }

		public bool IsFindByTitle { get; set; }

		public bool IsFindByDescription { get; set; }

		public bool IsFindByAssignedTo { get; set; }

		public bool IsFindByStatus { get; set; }

		public bool IsFindByCreatedBy { get; set; }

		public Issue Issue { get; set; }
    }
}