using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace TrackingSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Fname { get; set; }

        public string Lname { get; set; }

        public DateTime Registered { get; set; }

        [DisplayName("Assigned")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", Fname, Lname);
            }
        }

		public string Password { get; set; }

		public string Login { get; set; }
    }
}