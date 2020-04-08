using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    public class Product
    {
         [Key]
         public int ProductId { get; set; }

        [Required]
         public string ProductName { get; set; } 
         public float ProductPrice { get; set; }
         public string ProductCode { get; set; }
    }
}