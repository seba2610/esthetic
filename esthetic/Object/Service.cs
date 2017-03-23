using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esthetic
{
    public class Service
    {
        public int Id { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }

        [DisplayName("Tipo de servicio")]
        public int ServiceTypeId { get; set; }

        public Service() { }

        public Service(DataRow dr)
        {
            Id = Int32.Parse(dr["Id"].ToString());
            Name = dr["Name"].ToString();
            Description = dr["Description"] == DBNull.Value ? String.Empty : dr["Description"].ToString();
            ServiceTypeId = Int32.Parse(dr["ServiceTypeId"].ToString());
        }
    }
}
