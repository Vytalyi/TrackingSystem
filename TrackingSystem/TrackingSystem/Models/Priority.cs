using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace TrackingSystem.Models
{
    public class Priority
    {
        public int Id { get; set; }

		[DisplayName("Priority")]
        public string Name { get; set; }
    }
}