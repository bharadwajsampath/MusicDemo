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
    public class AlbumController : Controller
    {
        // GET: Album
        public ActionResult Index()
        {

            var mapper = CassandraConnector.Connector.MapperList();
            List<Album> albums = mapper.Fetch<Album>("select * from album;").ToList();

            List<Artist> artists = mapper.Fetch<Artist>("select * from artist;").ToList();


            List<ArtistAlbums> artistalbums = new List<ArtistAlbums>();



            foreach (var album in albums)
            {
                var artistalbum = new ArtistAlbums();
                var artist = artists.FirstOrDefault(x => x.artistid == album.artistid);
                artistalbum.albumid = album.albumid;
                artistalbum.albumname = album.albumname;
                artistalbum.artistid = artist.artistid;
                artistalbum.artistname = artist.artistname;
                artistalbum.genre = album.genre;

                artistalbums.Add(artistalbum);
            }


            return View(artistalbums);
        }


        public ActionResult Tracks(Guid id)
        {
            var mapper = CassandraConnector.Connector.MapperList();
            List<Track> tracks = mapper.Fetch<Track>("select * from track ").Where(x => x.albumid == id).ToList();
            var albumname = mapper.Fetch<Album>("select * from album").ToList().FirstOrDefault(x => x.albumid == id).albumname;
            var albumtracks = new List<AlbumTrack>();


            foreach (var track in tracks)
            {
                //get track ratings

              var ratings=  mapper.Fetch<TrackRating>("select * from track_rating_by_user;").ToList().Where(x=>x.trackid==track.trackid);

                var albumtrack = new AlbumTrack();
              
                if (ratings.Count()>0)
                {
                    var totalratings = ratings.Select(x => x.rating).ToList().Sum();
                    var avgRatings = totalratings / ratings.Count();
                    albumtrack.rating = avgRatings.ToString();
                }
                else
                {
                    albumtrack.rating = "Not yet rated !";

                }





                albumtrack.albumid = track.albumid;
                albumtrack.albumname = albumname;
                albumtrack.length = track.length;
                albumtrack.location = track.location;
                albumtrack.trackid = track.trackid;
              
                albumtrack.trackname = track.trackname;
                albumtracks.Add(albumtrack);
            }



            return View("Tracks", albumtracks);
        }


        /// <summary>
        /// </summary>
        /// <param name="id">Trackid</param>
        /// <returns></returns>
        public ActionResult AddToPlaylist(Guid id)
        {

            var useremail = this.User.Identity.GetUserName();
            var thisuser = CassandraConnector.Connector.ExecuteQuery("select * from users where email='" + useremail + "';").First();
            var userid = (Guid)thisuser["userid"];

            var mapper = CassandraConnector.Connector.MapperList();
            var Playlists = mapper.Fetch<UserPlaylist>("Select * from user_playlist;").ToList().Where(x => x.userid == userid);
            List<Track> tracks = mapper.Fetch<Track>("select * from track ").ToList();
            var thistrack = tracks.FirstOrDefault(x => x.trackid == id);
            AddtoPlaylist atp = new AddtoPlaylist();

            atp.trackname = thistrack.trackname;
            atp.trackid = id;

            ViewBag.PlaylistCollection = Playlists;

            //get all user comments for the track

            var allcommentsforthistrack =mapper.Fetch<UserComments>("select * from comments_by_track;").ToList().Where(x=>x.trackid==id);
            List<UsercommentVM> usercomments = new List<UsercommentVM>();

            foreach (var usercomment in allcommentsforthistrack)
            {
                UsercommentVM uservm = new UsercommentVM();

                var userinfo =
                    mapper.Fetch<User>("select * from users;").ToList().First(x => x.userid == usercomment.userid);

                uservm.username = userinfo.email;
                uservm.comment = usercomment.comment;
                uservm.On = usercomment.creadtedon;

                usercomments.Add(uservm);
            }

            atp.UserCommentList = usercomments;

            return View(atp);
        }

        [HttpPost]
        public ActionResult AddToPlaylist(AddtoPlaylist model)
        {
            CassandraConnector.Connector.ExecuteQuery("insert into playlist_track(trackid,playlistid,trackorder) values (" +
                                                      model.trackid + "," + model.playlist + "," + model.order + ");");


            return RedirectToAction("Index");
        }
    }
}