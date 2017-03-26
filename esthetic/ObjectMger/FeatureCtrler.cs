using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esthetic
{
    public class FeatureCtrler
    {
        #region Fields

        private static FeatureCtrler _instance;
        private static DataAccess _dataAccess;
        private static string _connStr;
        private static EnumConst.DataAccessProvider _databaseProvider;

        #endregion

        public static void SetConnStrDataBase(EnumConst.DataAccessProvider provider, string conn_str)
        {
            _connStr = conn_str;
            _databaseProvider = provider;
        }

        private FeatureCtrler()
        {
            _dataAccess = DataAccessFactory.CreateDataAccess(_databaseProvider, _connStr);
        }

        /// <summary>
        /// Método para obtener la instancia del manager
        /// </summary>
        public static FeatureCtrler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FeatureCtrler();
                }
                return _instance;
            }
        }

        public List<Feature> GetAllFeatures()
        {
            List<Feature> result = new List<Feature>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            ds = _dataAccess.ExecuteStoreProcedure("GetAllFeatures", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new Feature(dr));
                }
            }

            return result;
        }
    }
}
