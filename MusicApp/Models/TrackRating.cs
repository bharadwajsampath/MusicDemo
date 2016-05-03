using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicApp.Models
{
    public class TrackRating
    {
        public Guid trackid { get; set; }
        public Guid userid { get; set; }
        public int rating { get; set; }
    }
}