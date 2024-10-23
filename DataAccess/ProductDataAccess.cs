using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Internal;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace e_commerce.DataAccess
{
    public class ProductDataAccess
    {

        private readonly string _connectionString;

        public ProductDataAccess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }


        public  List<Product> ListProduct()
        {
          
            List<Product> products = new List<Product>();

            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "spGetProducts";
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 0;

                    SqlDataReader 
                        reader = sqlCommand.ExecuteReader();
                     if(reader.HasRows) {
                        while (reader.Read())
                        {
                            Product item = new Product();
                            item.ProductId = Convert.ToInt32(reader["id"].ToString());
                            item.ProductName = reader["name"].ToString();
                            item.ProductDescription = reader["description"].ToString();
                            item.ProductQuantity = Convert.ToInt32(reader["quantity"].ToString());
                            item.ProductCount = Convert.ToInt32(reader["stock"].ToString());
                            item.created_at = Convert.ToDateTime(reader["created_at"].ToString());
                            item.updated_at = Convert.ToDateTime((reader["updated_at"].ToString()).ToString());
                            products.Add(item);
                        }
                    
                    }
                    sqlCommand.Dispose();
                    sqlConnection.Close();


                }

                return products;

            }
           
        }



    }
}