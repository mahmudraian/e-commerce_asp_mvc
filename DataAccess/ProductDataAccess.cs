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
                                    Id = Convert.ToInt32(reader["ProductId"]),
                                    Name = reader["ProductName"].ToString(),
                                    Description = reader["ProductDescription"].ToString(),
                                    Quantity = Convert.ToInt32(reader["ProductQuantity"]),
                                    Stock = Convert.ToInt32(reader["ProductCount"]),
                                    CreatedAt = Convert.ToDateTime(reader["created_at"])
                                };

                                // Check if the updated_at field is not DBNull
                                if (reader["updated_at"] != DBNull.Value)
                                {
                                    item.UpdatedAt = Convert.ToDateTime(reader["updated_at"]);
                                }

                                
                               

                                products.Add(item);
                            }
                        }
                    }
                }
            }

            return products;
        }


        public int saveProduct(string name, string description,string imageurl, string thumb,int  status, int stock,int  quantity,string title, decimal price, int category_id,int  brand_id)
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
                    sqlCommand.Parameters.AddWithValue("@image", imageurl);

                    result = sqlCommand.ExecuteNonQuery();

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


       public bool updateproduct(int id, string name, string description, string thumb, int status, int stock, int quantity, string title, decimal price, int category_id, int brand_id)
        {
            bool result = false;




            return result;
        }


        //single product

        public Product SingleProduct(int id)
        {
            Product item = null;
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "[spGetProductById]";
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@productid", id);
                    sqlCommand.CommandTimeout = 0;

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    sqlCommand.Parameters.Clear();

                    if (reader.Read())
                    {
                        item = new Product();
                        item.Name = reader["name"].ToString();
                        item.Description = reader["description"].ToString();
                        item.Price = Convert.ToDecimal(reader["price"].ToString());
                        item.Quantity = Convert.ToInt32(reader["quantity"].ToString());
                        item.Stock = Convert.ToInt32(reader["stock"].ToString());
                        item.CategoryId = Convert.ToInt32(reader["category_id"].ToString());
                        item.CreatedAt = Convert.ToDateTime(reader["Created_at"].ToString());
                        item.Title = reader["title"].ToString();
                        item.Id = Convert.ToInt32(reader["id"].ToString());
                        item.BrandId = Convert.ToInt32(reader["brand_id"].ToString());
                        item.Status = Convert.ToInt32(reader["status"].ToString());
                        item.ImageUrl = ImageUrlPrepare(reader["image"].ToString());
                        item.ThumbnailUrl = reader["thumb"].ToString();


                        if (reader["Updated_at"] != DBNull.Value && !string.IsNullOrEmpty(reader["Updated_at"].ToString()))
                        {
                            item.UpdatedAt = Convert.ToDateTime(reader["Updated_at"]);
                        }
                        





                    }



                }


            }


            return item;
        }


        public bool UpdateProduct(int id, string name, string description, string thumb, string image, int status, int stock, int quantity, string title, decimal price, int category_id, int brand_id)
        {
            bool result = false;

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SpUpdateProduct";
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.Parameters.AddWithValue("@name", name);
                    sqlCommand.Parameters.AddWithValue("@description", description);
                    sqlCommand.Parameters.AddWithValue("@thumb", thumb);
                    sqlCommand.Parameters.AddWithValue("@image", image);
                    sqlCommand.Parameters.AddWithValue("@status", status);
                    sqlCommand.Parameters.AddWithValue("@stock", stock);
                    sqlCommand.Parameters.AddWithValue("@quantity", quantity);
                    sqlCommand.Parameters.AddWithValue("@title", title);
                    sqlCommand.Parameters.AddWithValue("@price", price);
                    sqlCommand.Parameters.AddWithValue("@category_id", category_id);
                    sqlCommand.Parameters.AddWithValue("@brand_id", brand_id);



                    int rows = sqlCommand.ExecuteNonQuery();
                    var result1 = rows;
                    if (rows > 0)
                    {
                        result = true;
                    }
                   

                }
            }

            return result;
        }

        public bool DeleteProduct(int id)
        {
            bool result = false;
             using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using(SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SpDeleteProduct";
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 0;
                    sqlCommand.Parameters.AddWithValue("@productId", id);

                    int rows = sqlCommand.ExecuteNonQuery();
                    if(rows > 0)
                    {
                        result = true;
                    }

                    sqlCommand.Dispose();


                }

                sqlConnection.Close();

            }


            return result;
        }



        private static string ImageUrlPrepare(string imageurl)
        {
            //~/Images/13910bf3-9b36-44cd-a4c5-6e884867a249.jpeg this is the path

            var trimurl = imageurl.TrimStart('~');

            var server_name = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

            var full_image_path = server_name + trimurl;

            return full_image_path;

        }





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