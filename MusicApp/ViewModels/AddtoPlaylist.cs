using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicApp.ViewModels
{
    public class AddtoPlaylist
    {

        public Guid trackid { get; set; }
        public Guid playlistid { get; set; }

        public string trackname { get; set; }

        public string playlist { get; set; }

        public int order { get; set; }

        public ICollection<string> PlaylistCollection { get; set; } 


        public List<UsercommentVM> UserCommentList { get; set; } 
        
    }

    public class UsercommentVM
    {
        public string username { get; set; }
        public string comment { get; set; }
        public string On { get; set; }
    }
}