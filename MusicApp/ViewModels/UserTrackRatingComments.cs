using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicApp.ViewModels
{
    public class UserTrackRatingComments
    {

        public Guid trackid { get; set; }
        public Guid userid { get; set; }
        public int rating { get; set; }

        public string comment { get; set; }
    }
}