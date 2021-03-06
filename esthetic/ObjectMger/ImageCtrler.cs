﻿using System;
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
            parameters.Add("@Width", image.Width);
            parameters.Add("@Height", image.Height);

            if (!String.IsNullOrEmpty(image.Title))
                parameters.Add("@Title", image.Title);
            else
                parameters.Add("@Title", DBNull.Value);

            if (!String.IsNullOrEmpty(image.Description))
                parameters.Add("@Description", image.Description);
            else
                parameters.Add("@Description", DBNull.Value);

            ds = _dataAccess.ExecuteStoreProcedure("CreateImage", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public Image GetImage(string id)
        {
            Image result = new Image();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            parameters.Add("@Id", id);

            ds = _dataAccess.ExecuteStoreProcedure("GetImage", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result = new Image(dr);
                }
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
            parameters.Add("@Name", category.Name);

            if (!String.IsNullOrEmpty(category.Description))
                parameters.Add("@Description", category.Description);
            else
                parameters.Add("@Description", DBNull.Value);

            parameters.Add("@ParentId", category.Parent.Id);

            ds = _dataAccess.ExecuteStoreProcedure("CreateCategory", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int DeleteCategory(int id)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@Id", id);

            ds = _dataAccess.ExecuteStoreProcedure("DeleteCategory", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int UpdateCategory(Category category)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@Id", category.Id);
            parameters.Add("@ParentId", category.Parent.Id);
            parameters.Add("@Name", category.Name);
            parameters.Add("@Description", category.Description);

            ds = _dataAccess.ExecuteStoreProcedure("UpdateCategory", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public List<Category> GetCategories(int parent_id = -1)
        {
            List<Category> result = new List<Category>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            if (parent_id > -1)
                parameters.Add("@ParentId", parent_id);
            else
                parameters.Add("@ParentId", DBNull.Value);

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

        public List<Category> GetRootCategories()
        {
            List<Category> result = new List<Category>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            ds = _dataAccess.ExecuteStoreProcedure("GetRootCategories", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new Category(dr));
                }
            }

            return result;
        }

        public List<Category> GetCategoriesFromImage(string imageId)
        {
            List<Category> result = new List<Category>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            parameters.Add("@ImageId", imageId);

            ds = _dataAccess.ExecuteStoreProcedure("GetCategoriesFromImage", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new Category(dr));
                }
            }

            return result;
        }

        public List<Image> GetAllImages()
        {
            List<Image> result = new List<Image>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            ds = _dataAccess.ExecuteStoreProcedure("GetAllImages", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new Image(dr));
                }
            }

            foreach (Image image in result)
            {
                parameters = new Dictionary<string, object>();
                parameters.Add("@ImageId", image.Id);

                ds = _dataAccess.ExecuteStoreProcedure("GetImageCategories", parameters);

                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        image.Categories.Add(new Category(dr));
                    }
                }
            }

            return result;
        }

        public List<Image> GetImagesFromCategory(int categoryId)
        {
            List<Image> result = new List<Image>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@CategoryId", categoryId);

            DataSet ds = _dataAccess.ExecuteStoreProcedure("GetImagesFromCategory", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new Image(dr));
                }
            }

            foreach (Image image in result)
            {
                parameters = new Dictionary<string, object>();
                parameters.Add("@ImageId", image.Id);

                ds = _dataAccess.ExecuteStoreProcedure("GetImageCategories", parameters);

                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        image.Categories.Add(new Category(dr));
                    }
                }
            }

            return result;
        }

        public int DeleteImagesFromCategory(int categoryId)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@CategoryId", categoryId);

            ds = _dataAccess.ExecuteStoreProcedure("DeleteImagesFromCategory", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int DeleteImageFromCategory(int categoryId, string imageId)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@CategoryId", categoryId);
            parameters.Add("@ImageId", imageId);

            ds = _dataAccess.ExecuteStoreProcedure("DeleteImageFromCategory", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int DeleteImage(string imageId)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@ImageId", imageId);

            ds = _dataAccess.ExecuteStoreProcedure("DeleteImage", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public int UpdateImage(Image image)
        {
            int result = -1;

            Dictionary<string, object> parameters = null;
            DataSet ds = null;

            parameters = new Dictionary<string, object>();
            parameters.Add("@Id", image.Id);

            if (!String.IsNullOrEmpty(image.Title))
                parameters.Add("@Title", image.Title);
            else
                parameters.Add("@Title", DBNull.Value);

            if (!String.IsNullOrEmpty(image.Description))
                parameters.Add("@Description", image.Description);
            else
                parameters.Add("@Description", DBNull.Value);


            ds = _dataAccess.ExecuteStoreProcedure("UpdateImage", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }

            return result;
        }

        public Category GetCategory(int id)
        {
            Category result = new Category();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DataSet ds = null;

            parameters.Add("@Id", id);

            ds = _dataAccess.ExecuteStoreProcedure("GetCategory", parameters);

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result = new Category(dr);
                }
            }

            return result;
        }
    }
}
