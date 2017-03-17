using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

                    //if (file != null && file.ContentLength > 0)
                    //{
                    //    file_name = file.FileName;

                    //    string path = new DirectoryInfo(string.Format(EnumConst.EntityImagesPath, Server.MapPath("/"), entity_id.ToString())).ToString();

                    //    string thumbnail_path = string.Format(EnumConst.EntityThumbnailImagesPath, path, entity_id.ToString());

                    //    string absolute_file_name = string.Format(EnumConst.AbsoluteFileName, path, file_name);

                    //    if (!Directory.Exists(path))
                    //        Directory.CreateDirectory(path);

                    //    if (!Directory.Exists(thumbnail_path))
                    //        Directory.CreateDirectory(thumbnail_path);

                    //    if (Directory.GetFiles(path).Where(f => f != EnumConst.ThumbnailDir).Count() == 0)
                    //    {
                    //        Property p = EntityMger.Instance.GetPropertyByName(EnumConst.PropertyNameMainPicture);
                    //        EntityProperty ep = new EntityProperty();
                    //        ep.EntityId = entity_id;
                    //        ep.PropertyId = p.Id;
                    //        ep.Value = file_name;
                    //        ep.Id = EntityMger.Instance.AddOrUpdateEntityProperty(ep);
                    //    }

                    //    Bitmap bitmap = Utilities.IsImage(file);

                    //    if (bitmap != null)
                    //    {
                    //        int thumb_height = EnumConst.ThumbnailDefaultSize;
                    //        int thumb_width = EnumConst.ThumbnailDefaultSize;

                    //        if (bitmap.Height > bitmap.Width)
                    //        {
                    //            thumb_width = (int)(bitmap.Width * ((float)thumb_height / (float)bitmap.Height));
                    //        }
                    //        else
                    //        {
                    //            thumb_height = (int)(bitmap.Height * ((float)thumb_width / (float)bitmap.Width));
                    //        }

                    //        Image thumbnail = bitmap.GetThumbnailImage(thumb_width, thumb_height, null, IntPtr.Zero);
                    //        thumbnail.Save(string.Format(EnumConst.AbsoluteFileName, thumbnail_path, file_name));
                    //        thumbnail.Dispose();
                    //        bitmap.Dispose();
                    //    }

                    //    file.SaveAs(absolute_file_name);
                    //}
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