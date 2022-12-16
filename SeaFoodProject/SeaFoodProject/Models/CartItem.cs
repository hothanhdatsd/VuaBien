using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaFoodProject.Models
{
    public class CartItem
    {
        public HAISAN ProductOrder { get; set; }
        public int qty { get; set; }
    }
}