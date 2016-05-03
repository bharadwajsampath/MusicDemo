using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicApp.Models
{
    public class User
    {
        public Guid userid { get; set; }

        public string email { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
    }
}