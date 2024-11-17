using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Internal;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Xml.Linq;

namespace e_commerce.DataAccess
{
    public class ProductDataAccess
    {

        private readonly string _connectionString;

        public ProductDataAccess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }


        //public  List<Product> ListProduct()
        //{

        //    List<Product> products = new List<Product>();

        //    using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
        //    {
        //        sqlConnection.Open();

        //        using (SqlCommand sqlCommand = new SqlCommand())
        //        {
        //            sqlCommand.Connection = sqlConnection;
        //            sqlCommand.CommandText = "spGetProducts";
        //            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //            sqlCommand.CommandTimeout = 0;

        //            SqlDataReader 
        //                reader = sqlCommand.ExecuteReader();
        //             if(reader.HasRows) {
        //                while (reader.Read())
        //                {
        //                    Product item = new Product();
        //                    item.ProductId = Convert.ToInt32(reader["ProductId"].ToString());
        //                    item.ProductName = reader["ProductName"].ToString();
        //                    item.ProductDescription = reader["ProductDescription"].ToString();
        //                    item.ProductQuantity = Convert.ToInt32(reader["ProductQuantity"].ToString());
        //                    item.ProductCount = Convert.ToInt32(reader["ProductCount"].ToString());
        //                    item.created_at = Convert.ToDateTime(reader["created_at"].ToString());
        //                    var categories = reader["CategoryIds"];
        //                    if (reader["updated_at"] != DBNull.Value)
        //                    {
        //                        item.updated_at = Convert.ToDateTime(reader["updated_at"].ToString());
        //                    }
        //                    reader.Close();
        //                    if (categories)
        //                    {
        //                        //get categories by product id

        //                        using (SqlCommand cmd = new SqlCommand())
        //                        {
        //                            cmd.Connection = sqlConnection;
        //                            cmd.CommandText = "spGetProductCategory";
        //                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                            cmd.Parameters.AddWithValue("@product_id", item.ProductId);
        //                            cmd.CommandTimeout = 0;

        //                            SqlDataReader rdr = cmd.ExecuteReader();
        //                            if (rdr.HasRows)
        //                            {
        //                                while (rdr.Read())
        //                                {
        //                                    Category category = new Category();
        //                                    category.CategoryId = Convert.ToInt32(rdr["id"].ToString());
        //                                    category.Name = rdr["name"].ToString();
        //                                    category.Description = rdr["description"].ToString();
        //                                    category.thumb = rdr["thumb"].ToString();
        //                                    category.status = Convert.ToInt32(rdr["status"].ToString());
        //                                    item.Categories.Add(category);
        //                                }
        //                            }
        //                            rdr.Close();
        //                            cmd.Dispose();
        //                        }   

        //                    }


        //                    products.Add(item);
        //                }

        //            }
        //            sqlCommand.Dispose();
        //            sqlConnection.Close();


        //        }

        //        return products;

        //    }



        // }


        public List<Product> ListProduct()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand("spGetProducts", sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 0;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Product item = new Product
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    ProductDescription = reader["ProductDescription"].ToString(),
                                    ProductQuantity = Convert.ToInt32(reader["ProductQuantity"]),
                                    ProductCount = Convert.ToInt32(reader["ProductCount"]),
                                    created_at = Convert.ToDateTime(reader["created_at"])
                                };

                                // Check if the updated_at field is not DBNull
                                if (reader["updated_at"] != DBNull.Value)
                                {
                                    item.updated_at = Convert.ToDateTime(reader["updated_at"]);
                                }

                                // Check if categories data exists
                                if (reader["CategoryIds"] != DBNull.Value)
                                {
                                    // Use a separate connection for the nested query
                                    using (SqlConnection categoryConnection = new SqlConnection(_connectionString))
                                    {
                                        categoryConnection.Open();

                                        using (SqlCommand cmd = new SqlCommand("spGetProductCategory", categoryConnection))
                                        {
                                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                                            cmd.CommandTimeout = 0;

                                            using (SqlDataReader rdr = cmd.ExecuteReader())
                                            {
                                                if (rdr.HasRows)
                                                {
                                                    while (rdr.Read())
                                                    {
                                                        Category category = new Category
                                                        {
                                                            CategoryId = Convert.ToInt32(rdr["CategoryId"]),
                                                            Name = rdr["CategoryName"].ToString()

                                                        };
                                                        item.Categories.Add(category);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                products.Add(item);
                            }
                        }
                    }
                }
            }

            return products;
        }


        public int saveProduct(string name, string description, string thumb,int  status, int stock,int  quantity,string title, decimal price, int category_id,int  brand_id)
        {
            int result = 0;

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();


                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SpInsertProduct";
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@name", name);
                    sqlCommand.Parameters.AddWithValue("@description", description);
                    sqlCommand.Parameters.AddWithValue("@category_id", category_id);
                    sqlCommand.Parameters.AddWithValue("@brand_id", brand_id);
                    sqlCommand.Parameters.AddWithValue("@stock", stock);
                    sqlCommand.Parameters.AddWithValue("@quantity", quantity);
                    sqlCommand.Parameters.AddWithValue("@title", title);
                    sqlCommand.Parameters.AddWithValue("@status", status);
                    sqlCommand.Parameters.AddWithValue("@thumb", thumb);
                    sqlCommand.Parameters.AddWithValue("@price", price);
                  
                    result= sqlCommand.ExecuteNonQuery();

                    sqlCommand.Dispose();
                    sqlConnection.Close();



                }
            }

            if (result > 0)
            {
                return result;
            }
            else
            {
                return 0;
            }

        }


        //single product






    }
}



//([name]
//         , [description]
//         , [category_id]
//         , [brand_id]
//         , [stock]
//         , [quantity]
//         , [title]
//         , [status]
//         , [created_at]
//         , [updated_at]
//         , [thumb])