using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicApp.ViewModels
{
    public class AlbumTrack
    {

        public Guid trackid { get; set; }

        public Guid albumid { get; set; }

        public string albumname { get; set; }

        public decimal length { get; set; }

        public string location { get; set; }

        public string trackname { get; set; }
        public string rating { get; set; }
    }
}