using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esthetic
{
    public class Image
    {
        public string Id;
        public string Title;
        public string Description;

        public List<Category> Categories;

        public Image() { }

        public Image(string id)
        {
            Id = id;
            Title = String.Empty;
            Description = String.Empty;
            Categories = new List<Category>();
        }

        public Image(DataRow dr)
        {
            Id = dr["Id"].ToString();
            Title = dr["Title"] == DBNull.Value ? String.Empty : dr["Title"].ToString();
            Description = dr["Description"] == DBNull.Value ? String.Empty : dr["Description"].ToString();
            Categories = new List<Category>();
        }
    }
}
