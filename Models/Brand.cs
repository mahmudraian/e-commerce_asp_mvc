using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public string BrandTitle { get; set; }
        public string BrandDescription { get; set; }

        public string Thumb { get; set; }

        public DateTime Create_at { get; set; }

        public DateTime Update_at { get; set; }






    }
}