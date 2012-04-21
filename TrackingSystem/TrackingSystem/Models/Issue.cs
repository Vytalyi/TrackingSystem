using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingSystem.Models
{
    public class Issue
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public User AssignedTo { get; set; }

        public Status Status { get; set; }

        public DateTime Created { get; set; }
    }
}