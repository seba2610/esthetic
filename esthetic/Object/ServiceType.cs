using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esthetic
{
    public class ServiceType
    {
        public int Id { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }

        [DisplayName("Servicios")]
        public List<Service> Services { get; set; }

        public ServiceType() { }

        public ServiceType(DataRow dr)
        {
            Id = Int32.Parse(dr["Id"].ToString());
            Name = dr["Name"].ToString();
            Description = dr["Description"] == DBNull.Value ? String.Empty : dr["Description"].ToString();
        }
    }
}
