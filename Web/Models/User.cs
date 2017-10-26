using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class User
    {
        public string post { get; set; }
        public int salary { get; set; }

        public int wl()
        {
            return salary += 500;
        }
    }
}