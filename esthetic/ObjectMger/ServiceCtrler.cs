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

        public ServiceType GetServiceType(int serviceTypeId)
        {
            ServiceType result = new ServiceType();

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("@Id", serviceTypeId);

            DataSet ds = _dataAccess.ExecuteStoreProcedure("GetServiceType", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = new ServiceType(ds.Tables[0].Rows[0]);
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

        public int UpdateServiceType(ServiceType serviceType)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@Id", serviceType.Id);

            parameters.Add("@Name", serviceType.Name);

            if (!String.IsNullOrEmpty(serviceType.Description))
                parameters.Add("@Description", serviceType.Description);
            else
                parameters.Add("@Description", DBNull.Value);

            ds = _dataAccess.ExecuteStoreProcedure("UpdateServiceType", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public List<Service> GetServiceTypeServices(int id)
        {
            List<Service> result = new List<Service>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            parameters.Add("@Id", id);

            ds = _dataAccess.ExecuteStoreProcedure("GetServiceTypeServices", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new Service(dr));
                }
            }

            return result;
        }

        public int DeleteService(int id)
        {
            int result = -1;

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            parameters.Add("@Id", id);

            ds = _dataAccess.ExecuteStoreProcedure("DeleteService", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int DeleteServiceType(int id)
        {
            int result = -1;

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            parameters.Add("@Id", id);

            ds = _dataAccess.ExecuteStoreProcedure("DeleteServiceType", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int CreateService(Service service)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();

            parameters.Add("@Name", service.Name);
            parameters.Add("@ServiceTypeId", service.ServiceTypeId);

            if (!String.IsNullOrEmpty(service.Description))
                parameters.Add("@Description", service.Description);
            else
                parameters.Add("@Description", DBNull.Value);

            if (!String.IsNullOrEmpty(service.Cost))
                parameters.Add("@Cost", service.Cost);
            else
                parameters.Add("@Cost", DBNull.Value);

            ds = _dataAccess.ExecuteStoreProcedure("CreateService", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int UpdateService(Service service)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@Id", service.Id);

            parameters.Add("@Name", service.Name);

            parameters.Add("@ServiceTypeId", service.ServiceTypeId);

            if (!String.IsNullOrEmpty(service.Description))
                parameters.Add("@Description", service.Description);
            else
                parameters.Add("@Description", DBNull.Value);

            if (!String.IsNullOrEmpty(service.Cost))
                parameters.Add("@Cost", service.Cost);
            else
                parameters.Add("@Cost", DBNull.Value);

            ds = _dataAccess.ExecuteStoreProcedure("UpdateService", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }
    }
}
