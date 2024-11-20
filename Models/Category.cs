using e_commerce.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace e_commerce.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string thumb { get; set; }

        public int status { get; set; }

        public DateTime created_at {  get; set; }

        public DateTime updated_at { get; set; }

        [JsonIgnore]
        public readonly CategoryDataAccess _categoryDataAccess;

        public Category()
        {
            _categoryDataAccess = new CategoryDataAccess();
        }

        public List<Category> categoryList()
        {
            var categories = _categoryDataAccess.CategoryList();

            return categories;

        }

    }
}


