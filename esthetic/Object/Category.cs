using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esthetic
{
    public class Category
    {
        [DisplayName("Identificador")]
        public int Id { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }

        public Category Parent;

        public Category() { }

        public Category(DataRow dr)
        {
            Id = Int32.Parse(dr["Id"].ToString());

            if (dr["ParentId"] == DBNull.Value)
                Parent = null;
            else
            {
                Parent = new Category();
                Parent.Id = Int32.Parse(dr["ParentId"].ToString());
                Parent.Name = dr["ParentName"].ToString();
                Parent.Description = dr["ParentDescription"] == DBNull.Value ? String.Empty : dr["ParentDescription"].ToString();
            }

            Name = dr["Name"].ToString();
            Description = dr["Description"] == DBNull.Value ? String.Empty : dr["Description"].ToString();
        }
    }
}
