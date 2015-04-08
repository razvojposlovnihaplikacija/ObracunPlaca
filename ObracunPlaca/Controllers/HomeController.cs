using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ObracunPlaca.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Novi početci su uvijek teški!";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Zanimljivosti";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontaktirajte nas!";

            return View();
        }
    }
}
