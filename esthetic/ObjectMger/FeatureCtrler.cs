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

        public int CreateFeature(Feature feature)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();

            parameters.Add("@Id", feature.Id);
            parameters.Add("@Active", feature.Active);

            if (!String.IsNullOrEmpty(feature.Description))
                parameters.Add("@Description", feature.Description);
            else
                parameters.Add("@Description", DBNull.Value);

            ds = _dataAccess.ExecuteStoreProcedure("CreateFeature", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int UpdateFeature(Feature feature)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();

            parameters.Add("@Id", feature.Id);
            parameters.Add("@Active", feature.Active);

            if (!String.IsNullOrEmpty(feature.Description))
                parameters.Add("@Description", feature.Description);
            else
                parameters.Add("@Description", DBNull.Value);

            ds = _dataAccess.ExecuteStoreProcedure("UpdateFeature", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int DeleteFeature(string id)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();

            parameters.Add("@Id", id);

            ds = _dataAccess.ExecuteStoreProcedure("DeleteFeature", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }
    }
}
