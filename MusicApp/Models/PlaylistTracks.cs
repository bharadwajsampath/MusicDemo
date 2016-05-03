using System;

namespace MusicApp.Models
{
    public class PlaylistTracks
    {
        public Guid playlistid { get; set; }

        public Guid trackid { get; set; }

        public int trackorder { get; set; }

    }
}