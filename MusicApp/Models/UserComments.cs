using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicApp.Models
{
    public class UserComments
    {
        public Guid trackid { get; set; }
        public Guid userid { get; set; }
        public string comment { get; set; }
        public string creadtedon { get; set; }
    }
}