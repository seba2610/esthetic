using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Esthetic.Models
{
    public class AdminModel
    {
        public List<Category> Categories = new List<Category>();
        [DisplayName("Nombre")]
        public string NewCategoryName { get; set; }

        [DisplayName("Descripción")]
        public string NewCategoryDescription { get; set; }

        [DisplayName("Nombre")]
        public string EditCategoryName { get; set; }

        [DisplayName("Descripción")]
        public string EditCategoryDescription { get; set; }

        [DisplayName("Categoría superior")]
        public int CategorySelected { get; set; }

        public List<Image> Images = new List<Image>();
        public List<Image> NewImages = new List<Image>();

        public string ImageAbsolutePath = String.Empty;
        public string ImageThumbnailAbsolutePath = String.Empty;

        public AdminModel()
        {
            CategorySelected = -1;
        }
    }

    public class CategorySelection
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}