using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Esthetic.Models;
using static Esthetic.EnumConst;

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
            ViewBag.Message = "Fotos";
            ImagesModel model = new ImagesModel();
            model.Categories = ImageCtrler.Instance.GetCategories();
            Configuration count = ConfigurationCtrler.Instance.GetConfiguration(ConfigurationParam.InitImagesGalleryCount);
            int galleryCount = int.Parse(count.Value);
            int random = -1;
            List<Image> images = ImageCtrler.Instance.GetAllImages();

            if (images.Count > galleryCount)
            {
                Random rnd = new Random();
                List<int> numbers = new List<int>();

                for (int i = 0; i < galleryCount; i++)
                {
                    random = rnd.Next(0, images.Count);

                    while (numbers.Contains(random))
                        random = rnd.Next(0, images.Count);

                    numbers.Add(random);
                    model.Images.Add(images.ElementAt(random));
                }
            }
            else
            {
                model.Images = images;
            }
            return View(model);
        }

        public ActionResult ImagesCategory(int id)
        {
            ViewBag.Message = "Fotos";
            ImagesModel model = new ImagesModel();
            model.Categories = ImageCtrler.Instance.GetCategories(id);
            Configuration count = ConfigurationCtrler.Instance.GetConfiguration(ConfigurationParam.InitImagesGalleryCount);
            int galleryCount = int.Parse(count.Value);
            int random = -1;
            List<Image> images = new List<Image>();
            model.ParentCategory = ImageCtrler.Instance.GetCategory(id);

            foreach (Category category in model.Categories)
            {
                images = images.Union(ImageCtrler.Instance.GetImagesFromCategory(category.Id)).ToList();
            }

            images = images.Union(ImageCtrler.Instance.GetImagesFromCategory(id)).ToList();

            if (images.Count > galleryCount)
            {
                Random rnd = new Random();
                List<int> numbers = new List<int>();

                for (int i = 0; i < galleryCount; i++)
                {
                    random = rnd.Next(0, images.Count);

                    while (numbers.Contains(random))
                        random = rnd.Next(0, images.Count);

                    numbers.Add(random);
                    model.Images.Add(images.ElementAt(random));
                }
            }
            else
            {
                model.Images = images;
            }

            return View("Images",model);
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