using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCHost.Models
{
    public class ProductViewModel
    {
        
        public int ProductId { get; set; }

       
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public string ProductCode { get; set; }
    }
}