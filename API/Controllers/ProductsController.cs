using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace API.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: Products   




        public IHttpActionResult GetAllProducts()
        {
            List<Product> Products = null;

            using (var ctx = new DataContext())
            {
                Products = ctx.Products.Select(m=>m).ToList();
            }

            if (Products.Count == 0)
            {
                return NotFound();
            }

            return Ok(Products);
        }


        public IHttpActionResult GetProductById(int id)
        {
            Product Product = null;

            using (var ctx = new DataContext())
            {
                Product = ctx.Products.FirstOrDefault(p => p.ProductId == id); 
            }

            if (Product == null)
            {
                return NotFound();
            }

            return Ok(Product);
        }

        public IHttpActionResult PostNewProduct(Product NewProduct)
        {


            using (var ctx = new DataContext())
            {
                ctx.Products.Add(new Product()
                {
                    ProductName = NewProduct.ProductName,
                    ProductPrice = NewProduct.ProductPrice,
                    ProductCode = NewProduct.ProductCode
                });

                ctx.SaveChanges();
            }

            return Ok();
        }

        public IHttpActionResult Put(Product ModProduct)
        {
           

            using (var ctx = new DataContext())
            {
                var existingProduct = ctx.Products.Where(p => p.ProductId == ModProduct.ProductId)
                                                        .FirstOrDefault();

                if (existingProduct != null)
                {   
                    existingProduct.ProductName = ModProduct.ProductName;
                    existingProduct.ProductPrice = ModProduct.ProductPrice;
                    existingProduct.ProductCode = ModProduct.ProductCode;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }


        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new DataContext())
            {
                var SelProduct = ctx.Products
                    .Where(p => p.ProductId == id)
                    .FirstOrDefault();

                ctx.Entry(SelProduct).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }

}
