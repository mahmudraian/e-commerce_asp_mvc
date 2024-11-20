//using e_commerce.DataAccess;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Web;

//namespace e_commerce.Models
//{
//    public class test
//    {
//        public int ProductId { get; set; }
//        public string ProductName { get; set; }

//        public string ProductTitle { get; set; }
//        public string ProductDescription { get; set; }

//        //public int ProductCategory_id { get; set; }

//        public List<Category> Categories { get; set; } = new List<Category>();

//        public int Brand_id { get; set; }

//        public int Status_id { get; set; }

//        public int ProductThumb { get; set; }

//        public Decimal ProductPrice { get; set; }


//        public int ProductCount { get; set; }

//        public int ProductQuantity { get; set; }

//        public DateTime created_at { get; set; }

//        public DateTime updated_at { get; set;}

//        public readonly ProductDataAccess _productDataAccess;


//        public Product() { 
        
//        _productDataAccess =  new ProductDataAccess();
//        }

//        public List<Product> productsList() { 
            
//            var products =  _productDataAccess.ListProduct();
            
//            return products;
        
//        }

//    }
//}