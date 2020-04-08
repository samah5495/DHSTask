
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


using System.Web;
namespace API.Models
{
    public class DataContext: DbContext
    {  
            public DataContext() : base("name=myConnectionString")
    {

    }


        public DbSet<Product> Products { get; set; }
    } 
}