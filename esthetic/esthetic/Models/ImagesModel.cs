using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esthetic.Models
{
    public class ImagesModel
    {
        public List<Category> Categories = new List<Category>();
        public List<Image> Images = new List<Image>();
        public Category Category = null;
        public List<Category> ParentCategory = new List<Category>();
    }
}