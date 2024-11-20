using e_commerce.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{

    public class Product
    {
        [Required]

        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public int Status { get; set; }

        public int BrandId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

}