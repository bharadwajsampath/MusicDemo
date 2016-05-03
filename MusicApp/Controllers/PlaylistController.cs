using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MusicApp.Models;
using MusicApp.ViewModels;

namespace MusicApp.Controllers
{
    public class PlaylistController : Controller
    {
        // GET: Playlist
        public ActionResult Index()
        {
            var useremail = this.User.Identity.GetUserName();
            var thisuser = CassandraConnector.Connector.ExecuteQuery("select * from users where email='" + useremail + "';").First();
            var userid = (Guid)thisuser["userid"];

            var mapper = CassandraConnector.Connector.MapperList();
            var myplaylist = mapper.Fetch<UserPlaylist>("select * from user_playlist;").Where(x => x.userid == userid);



            return View(myplaylist);
        }


        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Create(UserPlaylist model)
        {
            var useremail = this.User.Identity.GetUserName();
            var thisuser = CassandraConnector.Connector.ExecuteQuery("select * from users where email='" + useremail + "';").First();
            var userid = thisuser["userid"];
            CassandraConnector.Connector.ExecuteQuery(
                "insert into user_playlist(playlistid,userid,playlistname,createdon) values (now()," + userid + ",'" +
                model.playlistname + "',dateof(now())); ");

return RedirectToAction("Index");        }



        public ActionResult Tracks(Guid id)
        {

            var mapper = CassandraConnector.Connector.MapperList();

            var useremail = this.User.Identity.GetUserName();
            var thisuser = CassandraConnector.Connector.ExecuteQuery("select * from users where email='" + useremail + "';").First();
            var userid = (Guid)thisuser["userid"];
            var playlist = mapper.Fetch<UserPlaylist>("select * from user_playlist").ToList().First(x => x.playlistid == id);
            var playlistTracks = mapper.Fetch<PlaylistTracks>("select * from playlist_track").ToList().Where(x => x.playlistid == id);
            var tracks = mapper.Fetch<Track>("select * from track").ToList();

            var trackratingsbyuser = mapper.Fetch<TrackRating>("select * from track_rating_by_user").ToList();
            var playlistTracksVM = new List<PlaylistTracksViewModel>();


            foreach (var playlisttrack in playlistTracks)
            {
                var track = tracks.FirstOrDefault(x => x.trackid == playlisttrack.trackid);
                var track_rating_byuser =
                    trackratingsbyuser.FirstOrDefault(x => x.userid == userid & x.trackid == playlisttrack.trackid);
                var albumtrack = new PlaylistTracksViewModel();
                albumtrack.albumid = track.albumid;
                albumtrack.albumname = mapper.Fetch<Album>("select * from album").ToList().FirstOrDefault(x => x.albumid == track.albumid).albumname; ;
                albumtrack.length = track.length;
                albumtrack.location = track.location;
                albumtrack.trackid = track.trackid;
                albumtrack.trackname = track.trackname;
                albumtrack.playlistname = playlist.playlistname;
                if (track_rating_byuser != null) albumtrack.rating = track_rating_byuser.rating;
                playlistTracksVM.Add(albumtrack);
            }

            return View(playlistTracksVM);
        }



        public ActionResult Remove(Guid id)
        {



            return View();

        }



        public ActionResult AddRating(Guid id)
        {
            UserTrackRatingComments userrating = new UserTrackRatingComments();
            userrating.trackid = id;
            var useremail = this.User.Identity.GetUserName();
            var thisuser = CassandraConnector.Connector.ExecuteQuery("select * from users where email='" + useremail + "';").First();
            userrating.userid = (Guid)thisuser["userid"];

            return View(userrating);

        }


        [HttpPost]
        public ActionResult AddRating(UserTrackRatingComments model)
        {

            CassandraConnector.Connector.ExecuteQuery("insert into track_rating_by_user(trackid,userid,rating) values (" + model.trackid + "," + model.userid + "," + model.rating + ");");
            CassandraConnector.Connector.ExecuteQuery("insert into comments_by_track(trackid,userid,comment,createdon) values (" + model.trackid + "," + model.userid + ",'" + model.comment + "',dateof(now()));");

return RedirectToAction("Index");
        }




    }
}