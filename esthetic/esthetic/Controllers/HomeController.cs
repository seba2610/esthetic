using System;
using System.Collections.Generic;
using System.IO;
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
            HomeModel model = new HomeModel();

            string file_name = EnumConst.CoverImageName;

            string path = new DirectoryInfo(string.Format(EnumConst.ImagesPath, Server.MapPath("/"))).ToString();

            model.CoverMainText = ConfigurationCtrler.Instance.GetConfiguration(ConfigurationParam.CoverMainText).Value;
            model.CoverDescriptionText = ConfigurationCtrler.Instance.GetConfiguration(ConfigurationParam.CoverDescriptionText).Value;
            model.CoverImagePath = string.Format(EnumConst.AbsoluteFileName, path, file_name);
            model.GalleryCount = Int32.Parse(ConfigurationCtrler.Instance.GetConfiguration(ConfigurationParam.GalleryCount).Value);
            model.ShowCoverGallery = bool.Parse(ConfigurationCtrler.Instance.GetConfiguration(ConfigurationParam.ShowCoverGallery).Value);

            return View(model);
        }

        public ActionResult Images()
        {
            ViewBag.Message = "Fotos";
            ImagesModel model = new ImagesModel();
            model.Categories = ImageCtrler.Instance.GetRootCategories();
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
            model.Category = ImageCtrler.Instance.GetCategory(id);
            Category parentIter = model.Category.Parent;
            Category categoryIter = model.Category;

            List<int> parentIds = new List<int>();

            while (parentIter != null && model.ParentCategory.FindIndex(c => c.Id == parentIter.Id) == -1)
            {
                categoryIter.Parent = parentIter;
                categoryIter = parentIter;
                model.ParentCategory.Insert(0, parentIter);
                parentIter = ImageCtrler.Instance.GetCategory(parentIter.Id).Parent;
            }

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
            ServicesModel model = new ServicesModel();
            model.Services = ServiceCtrler.Instance.GetAllServices();
            model.ServiceTypes = ServiceCtrler.Instance.GetAllServicesType();

            ViewBag.Message = "Servicios";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacto";

            return View();
        }

        public ActionResult Features()
        {
            List<Feature> features = new List<Feature>();

            features = FeatureCtrler.Instance.GetAllFeatures().Where(f => f.Active).ToList();

            return View(features);
        }
    }
}