using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class ProductViewModel
    {
        //public IEnumerable<Category> Categories { get; set; }
        //public Product Product { get; set; } // For binding the product details

        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Status_name { get; set; }

        public int BrandId { get; set; }

        public String CreatedAt { get; set; }

        public String UpdatedAt { get; set; }
    }

}