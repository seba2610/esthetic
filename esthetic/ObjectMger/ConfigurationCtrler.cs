using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esthetic
{
    public class ConfigurationCtrler
    {
        #region
        private static ConfigurationCtrler _instance;
        private static DataAccess _dataAccess;
        private static string _connStr;
        private static EnumConst.DataAccessProvider _databaseProvider;
        #endregion

        public static void SetConnStrDataBase(EnumConst.DataAccessProvider provider, string conn_str)
        {
            _connStr = conn_str;
            _databaseProvider = provider;
        }

        private ConfigurationCtrler()
        {
            _dataAccess = DataAccessFactory.CreateDataAccess(_databaseProvider, _connStr);
        }

        /// <summary>
        /// Método para obtener la instancia del manager
        /// </summary>
        public static ConfigurationCtrler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationCtrler();
                }
                return _instance;
            }
        }

        public Configuration GetConfiguration(EnumConst.ConfigurationParam param)
        {
            Configuration result = null;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@Name", param.ToString());

            ds = _dataAccess.ExecuteStoreProcedure("GetConfiguration", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = new Configuration(ds.Tables[0].Rows[0]);
            }

            return result;
        }
    }
}
