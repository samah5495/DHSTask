using MVCHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCHost.Controllers
{
    public class  ProductController : Controller
    {
       
        public ActionResult Index()
        {
          
            return View();
        } 
        [HttpGet]

        public ActionResult getall()
        {
            IEnumerable<ProductViewModel> ProductsList = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59593/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Products");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductViewModel>>();
                    readTask.Wait();

                    ProductsList = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    ProductsList = Enumerable.Empty<ProductViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }  
            }
            return Json( new { ProductsList }, JsonRequestBehavior.AllowGet);  
        } 




        [HttpDelete]

        public JsonResult Delete(int id)
        {    

            
            bool Success= false; 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59593/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Products/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    Success = true;   

                    
                }
            }

            return Json( new { Success }, JsonRequestBehavior.AllowGet);
        }

   

        [HttpPost]
        public ActionResult create(ProductViewModel product)
        {
            bool success = false; 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59593/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ProductViewModel>("Products", product);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    success = true; 
                }
            }



            return Json(new { success }, JsonRequestBehavior.AllowGet); 
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductViewModel Product = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59593/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Products?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ProductViewModel>();
                    readTask.Wait();

                    Product = readTask.Result;
                }
            }
            return Json(new { Product }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel Product)
        {
            bool success = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59593/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ProductViewModel>("Products", Product);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    success = true;
                }
            }
            return Json(new { success }, JsonRequestBehavior.AllowGet);
        }


    }
}
