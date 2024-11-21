using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace e_commerce.DataAccess
{
    public class BrandDataAccess
    {
        private readonly string _connectionString;

        public BrandDataAccess()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }


        public   List<Brand> BrandList()
        {
           List<Brand> brands = new List<Brand>();

            using(SqlConnection sqlConnection = new SqlConnection(_connectionString)    )
            {
                sqlConnection.Open();
                using(SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SPGetBrandData";
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while(sqlDataReader.Read())
                        {
                            Brand brand = new Brand();
                            brand.BrandId = Convert.ToInt32(sqlDataReader["BrandId"]);
                            brand.BrandName = sqlDataReader["BrandName"].ToString();
                            brand.BrandTitle = sqlDataReader["BrandTitle"].ToString();
                            brand.BrandDescription = sqlDataReader["BrandDescription"].ToString();
                            brand.Thumb = sqlDataReader["Thumb"].ToString();
                            brand.Create_at = Convert.ToDateTime(sqlDataReader["Created_at"]);
                            brand.Update_at = Convert.ToDateTime(sqlDataReader["Updated_at"]);

                            brands.Add(brand);
                        }
                    }
                }
            }


            return brands;
        }



        public bool SaveBrand(string name,string  description, string brandThumb, int status)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "[InsertBrand]";
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Name", name);
                    sqlCommand.Parameters.AddWithValue("@Title", name);
                    sqlCommand.Parameters.AddWithValue("@Description", description);
                    sqlCommand.Parameters.AddWithValue("@Thumb", brandThumb);
                    sqlCommand.Parameters.AddWithValue("@Status", status);

                    sqlCommand.ExecuteNonQuery();
                }
            }

            return true;

        }


    }
}