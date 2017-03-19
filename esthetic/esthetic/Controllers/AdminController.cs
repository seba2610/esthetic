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
            model.ImageAbsolutePath = new DirectoryInfo(string.Format(EnumConst.ImagesPath, Server.MapPath("/"))).ToString();
            model.ImageThumbnailAbsolutePath = string.Format(EnumConst.ThumbnailImagesPath, model.ImageAbsolutePath);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCategory(AdminModel model)
        {
            string name = model.NewCategoryName;
            string desc = model.NewCategoryDescription;
            int parent_id = model.CategorySelected;

            ImageCtrler.Instance.CreateCategory(new Category() { Name = name, Description = desc, Parent = new Category() { Id = parent_id } });
            return RedirectToAction("Categories");
        }

        [HttpPost]
        public ActionResult UpdateCategory(AdminModel model, int id)
        {
            string name = model.EditCategoryName;
            string desc = model.EditCategoryDescription;
            int parent_id = model.CategorySelected;

            ImageCtrler.Instance.UpdateCategory(new Category() {Id = id, Name = name, Description = desc, Parent = new Category() { Id = parent_id } });
            return RedirectToAction("Categories");
        }

        public ActionResult DeleteCategory(int id)
        {
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
    }
}