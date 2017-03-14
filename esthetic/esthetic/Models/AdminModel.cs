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

        public List<Image> Images = new List<Image>();
        public List<Image> NewImages = new List<Image>();
    }
}