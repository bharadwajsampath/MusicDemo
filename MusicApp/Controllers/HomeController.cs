using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cassandra;

namespace MusicApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Connecting to cassandra cluster
          var rows=  CassandraConnector.Connector.ExecuteQuery("Select * from users").First();

            var email=rows["email"];

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}