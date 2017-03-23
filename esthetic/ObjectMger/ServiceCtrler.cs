using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esthetic
{
    public class ServiceCtrler
    {
        #region Fields

        private static ServiceCtrler _instance;
        private static DataAccess _dataAccess;
        private static string _connStr;
        private static EnumConst.DataAccessProvider _databaseProvider;

        #endregion

        public static void SetConnStrDataBase(EnumConst.DataAccessProvider provider, string conn_str)
        {
            _connStr = conn_str;
            _databaseProvider = provider;
        }

        private ServiceCtrler()
        {
            _dataAccess = DataAccessFactory.CreateDataAccess(_databaseProvider, _connStr);
        }

        /// <summary>
        /// Método para obtener la instancia del manager
        /// </summary>
        public static ServiceCtrler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceCtrler();
                }
                return _instance;
            }
        }

        public List<Service> GetAllServices()
        {
            List<Service> result = new List<Service>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            ds = _dataAccess.ExecuteStoreProcedure("GetAllServices", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new Service(dr));
                }
            }

            return result;
        }

        public List<ServiceType> GetAllServicesType()
        {
            List<ServiceType> result = new List<ServiceType>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            ds = _dataAccess.ExecuteStoreProcedure("GetAllServicesType", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new ServiceType(dr));
                }
            }

            return result;
        }

        public int CreateServiceType(ServiceType serviceType)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();

            parameters.Add("@Name", serviceType.Name);

            if (!String.IsNullOrEmpty(serviceType.Description))
                parameters.Add("@Description", serviceType.Description);
            else
                parameters.Add("@Description", DBNull.Value);

            ds = _dataAccess.ExecuteStoreProcedure("CreateServiceType", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }
    }
}
