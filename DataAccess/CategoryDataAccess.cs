using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace e_commerce.DataAccess
{
    public class CategoryDataAccess
    {
        private readonly string _categoryDataAccess;

        public CategoryDataAccess()
        {
            _categoryDataAccess = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }

        public int SaveCategory()
        {
            int result = 0;
            using (SqlConnection sqlConnection = new SqlConnection(_categoryDataAccess))
            {
              
                sqlConnection.Open();
                using(SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = "createCategory";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add(new SqlParameter("@name", "name"));
                    cmd.Parameters.Add(new SqlParameter("@description", "description"));
                    cmd.Parameters.Add(new SqlParameter("@thumb", "thumb"));
                    cmd.Parameters.Add(new SqlParameter("@status", "status"));
                    cmd.Parameters.Add(new SqlParameter("@created_at", "created_at"));

                    try
                    {
                        result = cmd .ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        string sds = ex.Message;

                    }

                    cmd.Dispose();
                    sqlConnection.Close();
                }


            }

            return result;
        }


        public List<Category> CategoryList()
        {

            List<Category> list = new List<Category>();

            using (SqlConnection sqlConnection = new SqlConnection(_categoryDataAccess))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SpCategoryList";
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 0;

                    SqlDataReader
                        reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                     
                            Category item = new Category();
                            item.Id = Convert.ToInt32(reader["id"].ToString());
                            item.Name = reader["name"].ToString();
                            item.Description = reader["description"].ToString();
                            item.thumb = reader["thumb"].ToString();
                            item.created_at = Convert.ToDateTime(reader["created_at"].ToString());
                            

                            list.Add(item);
                            
                        }

                    }
                    sqlCommand.Dispose();
                    sqlConnection.Close();


                }

                return list;

            }

        }

    }
}


//[ID] INT IDENTITY(1,1) PRIMARY KEY,

//[name] VARCHAR(255) NOT NULL,

//[description] VARCHAR(255),

//[thumb] VARCHAR(255),

//[status] INT,

//[created_at] DATE,

//[updated_at] DATE