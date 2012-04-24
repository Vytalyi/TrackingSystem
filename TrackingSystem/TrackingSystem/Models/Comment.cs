using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingSystem.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Created { get; set; }

		public int Issue_Id { get; set; }
    }
}