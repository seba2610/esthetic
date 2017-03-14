﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esthetic
{
    public class Configuration
    {
        public int Id;
        public string Name;
        public string Value;
        public string Description;

        public Configuration() { }

        public Configuration(DataRow dr)
        {
            Id = Int32.Parse(dr["Id"].ToString());
            Name = dr["Name"].ToString();
            Value = dr["Value"] == DBNull.Value ? String.Empty : dr["Value"].ToString();
            Description = dr["Description"] == DBNull.Value ? String.Empty : dr["Description"].ToString();
        }
    }
}
