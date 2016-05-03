using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicApp.ViewModels
{
    public class ArtistAlbums
    {
        public Guid albumid { get; set; }

        public Guid artistid { get; set; }

        public string genre { get; set; }

        public string albumname { get; set; }


        public int age { get; set; }

        public string artistname { get; set; }
    }
}