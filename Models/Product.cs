using e_commerce.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public int ProductCategory { get; set; }
        
        public  int review { get; set; }       


        public int ProductCount { get; set; }

        public int ProductQuantity { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set;}

        public readonly ProductDataAccess _productDataAccess;


        public Product() { 
        
        _productDataAccess =  new ProductDataAccess();
        }

        public List<Product> productsList() { 
            
            var products =  _productDataAccess.ListProduct();
            
            return products;
        
        }

    }
}