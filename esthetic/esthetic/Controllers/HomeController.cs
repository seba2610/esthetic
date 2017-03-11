using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace esthetic.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Albums()
        {
            ViewBag.Message = "Fotografías";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Acerca de";

            return View();
        }
        public ActionResult Services()
        {
            ViewBag.Message = "Servicios";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacto";

            return View();
        }

        public ActionResult Features()
        {
            ViewBag.Message = "Promociones";

            return View();
        }
    }
}