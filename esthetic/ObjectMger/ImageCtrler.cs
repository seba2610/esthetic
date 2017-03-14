using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Esthetic;

namespace Esthetic
{
    public class ImageCtrler
    {
        #region Fields

        private static ImageCtrler _instance;
        private static DataAccess _dataAccess;
        private static string _connStr;
        private static EnumConst.DataAccessProvider _databaseProvider;

        #endregion

        public static void SetConnStrDataBase(EnumConst.DataAccessProvider provider, string conn_str)
        {
            _connStr = conn_str;
            _databaseProvider = provider;
        }

        private ImageCtrler()
        {
            _dataAccess = DataAccessFactory.CreateDataAccess(_databaseProvider, _connStr);
        }

        /// <summary>
        /// Método para obtener la instancia del manager
        /// </summary>
        public static ImageCtrler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ImageCtrler();
                }
                return _instance;
            }
        }

        public int CreateImage(Image image)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@Id", image.Id);
            parameters.Add("@Title", image.Title);
            parameters.Add("@Description", image.Description);

            ds = _dataAccess.ExecuteStoreProcedure("CreateImage", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int AddImageToCategory(string image_id, int category_id)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@ImageId", image_id);
            parameters.Add("@CategoryId", category_id);

            ds = _dataAccess.ExecuteStoreProcedure("AddImageToCategory", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int CreateCategory(Category category)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@Id", category.Id);
            parameters.Add("@Name", category.Name);
            parameters.Add("@Description", category.Description);

            ds = _dataAccess.ExecuteStoreProcedure("CreateCategory", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public List<Category> GetCategories(int parent_id = -1)
        {
            List<Category> result = new List<Category>();

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            if (parent_id > -1)
            parameters.Add("@ParentId", parent_id);

            ds = _dataAccess.ExecuteStoreProcedure("GetCategories", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new Category(dr));
                }
            }

            return result;
        }
    }
}
