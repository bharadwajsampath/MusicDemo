using System;

namespace MusicApp.Models
{
    public class Album
    {
        public Guid albumid { get; set; }

        public Guid artistid { get; set; }

        public string genre { get; set; }

        public string albumname { get; set; }
    }
}