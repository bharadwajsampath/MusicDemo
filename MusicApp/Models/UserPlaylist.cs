using System;

namespace MusicApp.Models
{
    public class UserPlaylist
    {

        public Guid userid { get; set; }


        public Guid playlistid { set; get; }

        public DateTime createdon { get; set; }


        public string playlistname { get; set; }
    }
}