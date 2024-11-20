using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Product Product { get; set; } // For binding the product details
    }

}