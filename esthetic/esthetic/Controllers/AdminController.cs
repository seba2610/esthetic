using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Esthetic;
using Esthetic.Models;

namespace Esthetic.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Categories()
        {
            AdminModel model = new AdminModel();
            model.Categories = ImageCtrler.Instance.GetCategories();
            return View(model);
        }

        public ActionResult Images()
        {
            AdminModel model = new AdminModel();
            model.Categories = ImageCtrler.Instance.GetCategories();
            model.Images = ImageCtrler.Instance.GetAllImages();

            foreach (Image image in model.Images)
            {
                image.Categories = ImageCtrler.Instance.GetCategoriesFromImage(image.Id);
            }

            model.ImageAbsolutePath = new DirectoryInfo(string.Format(EnumConst.ImagesPath, Server.MapPath("/"))).ToString();
            model.ImageThumbnailAbsolutePath = string.Format(EnumConst.ThumbnailImagesPath, model.ImageAbsolutePath);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCategory(AdminModel model)
        {
            string name = model.NewCategoryName;
            string desc = model.NewCategoryDescription;
            int parent_id = -1;

            if (model.CategorySelected.HasValue)
                parent_id = model.CategorySelected.Value;

            ImageCtrler.Instance.CreateCategory(new Category() { Name = name, Description = desc, Parent = new Category() { Id = parent_id } });
            return RedirectToAction("Categories");
        }

        [HttpPost]
        public ActionResult UpdateCategory(AdminModel model, int id)
        {
            string name = model.EditCategoryName;
            string desc = model.EditCategoryDescription;

            int parent_id = -1;

            if (model.CategorySelected.HasValue)
                parent_id = model.CategorySelected.Value;

            AdminModel new_model = new AdminModel();
            Category category = new Category() { Id = id, Name = name, Description = desc, Parent = new Category() { Id = parent_id } };

            ImageCtrler.Instance.UpdateCategory(category);

            new_model.Categories = ImageCtrler.Instance.GetCategories();

            new_model.Category = new_model.Categories.Find(c => c.Id == id);

            return PartialView("Category", new_model);
        }

        public ActionResult DeleteCategory(int id)
        {
            List<Image> images = ImageCtrler.Instance.GetImagesFromCategory(id);
            List<string> imagesToDelete = new List<string>();

            foreach (Image image in images)
            {
                if (ImageCtrler.Instance.GetCategoriesFromImage(image.Id).Count == 1)
                    imagesToDelete.Add(image.Id);
            }

            foreach (string imageId in imagesToDelete)
            {
                ImageCtrler.Instance.DeleteImage(imageId);
            }

            ImageCtrler.Instance.DeleteCategory(id);

            return RedirectToAction("Categories");
        }

        [HttpPost]
        public ActionResult AddImagesToCategory(int categoryId = -1)
        {
            bool isSavedSuccessfully = true;
            string file_name = String.Empty;
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];

                    if (file != null && file.ContentLength > 0)
                    {
                        Image image = new Image(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

                        file_name = image.Id;

                        string path = new DirectoryInfo(string.Format(EnumConst.ImagesPath, Server.MapPath("/"))).ToString();

                        string thumbnail_path = string.Format(EnumConst.ThumbnailImagesPath, path);

                        string absolute_file_name = string.Format(EnumConst.AbsoluteFileName, path, file_name);

                        Bitmap bitmap = Utilities.IsImage(file);

                        if (bitmap != null)
                        {
                            int thumb_height = EnumConst.ThumbnailDefaultSize;
                            int thumb_width = EnumConst.ThumbnailDefaultSize;

                            if (bitmap.Height > bitmap.Width)
                            {
                                thumb_width = (int)(bitmap.Width * ((float)thumb_height / (float)bitmap.Height));
                            }
                            else
                            {
                                thumb_height = (int)(bitmap.Height * ((float)thumb_width / (float)bitmap.Width));
                            }

                            System.Drawing.Image thumbnail = bitmap.GetThumbnailImage(thumb_width, thumb_height, null, IntPtr.Zero);
                            string absolute_thumb_file_name = string.Format(EnumConst.AbsoluteFileName, thumbnail_path, file_name);
                            thumbnail.Save(absolute_thumb_file_name);
                            thumbnail.Dispose();

                            image.Height = bitmap.Height;
                            image.Width = bitmap.Width;

                            bitmap.Dispose();
                        }

                        file.SaveAs(absolute_file_name);

                        if (ImageCtrler.Instance.CreateImage(image) > 0)
                            ImageCtrler.Instance.AddImageToCategory(image.Id, categoryId);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
                return Json(new { Message = ex.Message + " - Stacktrace: " + ex.InnerException + ex.StackTrace });
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = file_name });
            }
            else
            {
                return Json(new { Message = "Hubo un error al guardar el archivo" });
            }
        }

        [HttpPost]
        public ActionResult AddImageToCategory(AdminModel model, string imageId)
        {
            if (model.CategoriesSelected != null) {
                foreach (int categoryId in model.CategoriesSelected)
                {
                    ImageCtrler.Instance.AddImageToCategory(imageId, categoryId);
                }
            }

            return RedirectToAction("Images");
        }

        public ActionResult DeleteImagesFromCategory(int categoryId)
        {
            List<Image> images = ImageCtrler.Instance.GetImagesFromCategory(categoryId);
            ImageCtrler.Instance.DeleteImagesFromCategory(categoryId);

            //Elimino las imagenes que estaban unicamente en esta categoria.
            foreach (Image image in images.Where(i => i.Categories.Count == 1))
            {
                ImageCtrler.Instance.DeleteImage(image.Id);
            }

            return RedirectToAction("Images");
        }

        [HttpPost]
        public ActionResult UpdateImage(AdminModel model, string id)
        {
            string title = model.EditImageTitle;
            string desc = model.EditImageDescription;

            AdminModel new_model = new AdminModel();
            new_model.Image = new Image() { Id = id, Title = title, Description = desc };

            new_model.Image.Categories = ImageCtrler.Instance.GetCategoriesFromImage(id);

            ImageCtrler.Instance.UpdateImage(new_model.Image);

            return PartialView("Image", new_model);
        }

        public ActionResult RemoveImageFromCategory(int categoryId, string imageId)
        {

            ImageCtrler.Instance.DeleteImageFromCategory(categoryId, imageId);

            List<Category> categories = ImageCtrler.Instance.GetCategoriesFromImage(imageId);

            if (categories.Count == 0)
                ImageCtrler.Instance.DeleteImage(imageId);

            return RedirectToAction("Images");
        }

        public ActionResult Services()
        {
            AdminModel model = new AdminModel();
            model.Services = ServiceCtrler.Instance.GetAllServices();
            model.ServicesType = ServiceCtrler.Instance.GetAllServicesType();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateServiceType(AdminModel model)
        {
            string name = model.NewServiceName;
            string desc = model.NewServiceDescription;

            ServiceCtrler.Instance.CreateServiceType(new ServiceType() { Name = name, Description = desc});
            return RedirectToAction("Services");
        }

        [HttpPost]
        public ActionResult UpdateServiceType(AdminModel model, int id)
        {
            string name = model.EditServiceName;
            string desc = model.EditServiceDescription;

            AdminModel new_model = new AdminModel();
            new_model.ServiceType = new ServiceType() { Id = id, Name = name, Description = desc };

            ServiceCtrler.Instance.UpdateServiceType(new_model.ServiceType);

            return PartialView("ServiceType", new_model);
        }

        public ActionResult DeleteServiceType(int id)
        {
            List<Service> services = ServiceCtrler.Instance.GetServiceTypeServices(id);

            foreach (Service service in services)
            {
                ServiceCtrler.Instance.DeleteService(service.Id);
            }

            ServiceCtrler.Instance.DeleteServiceType(id);

            return RedirectToAction("Services");
        }

        public ActionResult DeleteService(int id)
        {
            List<Service> services = ServiceCtrler.Instance.GetServiceTypeServices(id);

            ServiceCtrler.Instance.DeleteService(id);

            return RedirectToAction("Services");
        }

        [HttpPost]
        public ActionResult CreateService(AdminModel model)
        {
            string name = model.NewServiceName;
            string desc = model.NewServiceDescription;
            string cost = model.NewServiceCost;
            int serviceTypeId = model.ServiceTypeSelected;

            ServiceCtrler.Instance.CreateService(new Service() { Name = name, Description = desc, Cost = cost, ServiceTypeId = serviceTypeId });
            return RedirectToAction("Services");
        }

        public ActionResult UpdateService(AdminModel model, int id)
        {
            string name = model.EditServiceName;
            string desc = model.EditServiceDescription;
            string cost = model.EditServiceCost;
            int serviceTypeId = model.ServiceTypeSelected;

            AdminModel new_model = new AdminModel();
            new_model.Service = new Service() { Id = id, Name = name, Description = desc, Cost = cost, ServiceTypeId = serviceTypeId };
            new_model.ServiceType = ServiceCtrler.Instance.GetServiceType(serviceTypeId);
            new_model.ServicesType = ServiceCtrler.Instance.GetAllServicesType();
            ServiceCtrler.Instance.UpdateService(new_model.Service);

            return PartialView("Service", new_model);
        }

        public ActionResult Features()
        {
            AdminModel model = new AdminModel();
            model.Features = FeatureCtrler.Instance.GetAllFeatures();
            model.ActiveFeature = false;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddFeature(AdminModel model)
        {
            string description = model.DescriptionFeature;
            bool active = false;

            if (model.ActiveFeature.HasValue)
                active = model.ActiveFeature.Value;

            string id = String.Empty, file_name = String.Empty;

            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];

                if (file != null && file.ContentLength > 0)
                {
                    string path = new DirectoryInfo(string.Format(EnumConst.FeaturesImagesPath, Server.MapPath("/"))).ToString();

                    id = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    string absolute_file_name = string.Format(EnumConst.AbsoluteFileName, path, id);

                    Bitmap bitmap = Utilities.IsImage(file);

                    if (bitmap != null)
                    {
                        file.SaveAs(absolute_file_name);

                        FeatureCtrler.Instance.CreateFeature(new Feature() { Id = id, Description = description, Active = active });
                    }
                }
            }

            AdminModel newModel = new AdminModel();
            newModel.Features = FeatureCtrler.Instance.GetAllFeatures();
            newModel.ActiveFeature = false;

            return View("Features", newModel);
        }

        public ActionResult UpdateFeature(AdminModel model, string id)
        {
            string description = model.EditFeatureDescription;
            bool active = model.EditFeatureActive;

            AdminModel newModel = new AdminModel();
            newModel.Feature = new Feature() { Id = id, Active = active, Description = description };

            FeatureCtrler.Instance.UpdateFeature(newModel.Feature);

            return PartialView("Feature", newModel);
        }

        public ActionResult DeleteFeature(string id)
        {
            FeatureCtrler.Instance.DeleteFeature(id);

            return RedirectToAction("Features");
        }
    }
}