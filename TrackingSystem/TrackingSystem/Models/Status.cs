using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace TrackingSystem.Models
{
    public class Status
    {
        public int Id { get; set; }

        [DisplayName("Status")]
        public string Name { get; set; }
    }
}