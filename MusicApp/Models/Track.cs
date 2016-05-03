using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicApp.Models
{
    public class Track
    {
        public Guid trackid { get; set; }

        public Guid albumid { get; set; }

        public decimal length { get; set; }

        public string location { get; set; }

        public string trackname { get; set; }

    }
}