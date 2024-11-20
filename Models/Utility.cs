using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class Utility
    {

        public  static  string Status_name(int status)
        {
            if (status == 1)
            {
                return "Active";
            }
            else
            {
                return "Inactive";
            }

        }


    }
}