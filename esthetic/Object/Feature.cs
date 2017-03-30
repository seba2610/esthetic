using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esthetic
{
    public class Feature
    {
        public string Id { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
 
        public Feature() { }

        public Feature(DataRow dr)
        {
            Id = dr["Id"].ToString();
            Active = bool.Parse(dr["Active"].ToString());
            Description = dr["Description"] == DBNull.Value ? String.Empty : dr["Description"].ToString();
        }
    }
}
