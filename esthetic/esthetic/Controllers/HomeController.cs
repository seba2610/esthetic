using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Esthetic.Models;

namespace Esthetic.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Images()
        {
            ViewBag.Message = "Fotografías";
            ImagesModel model = new ImagesModel();
            model.Categories = ImageCtrler.Instance.GetCategories();

            return View(model);
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