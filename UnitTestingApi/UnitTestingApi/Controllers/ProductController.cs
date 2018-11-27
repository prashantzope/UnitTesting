using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitTestingApi.Models;

namespace UnitTestingApi.Controllers
{
    public class ProductController : ApiController
    {

        List<Product> products;
        public ProductController()
        {
            products = GetTestProducts();
        }


        /// <summary>
        /// Get product list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            Product product = products.FirstOrDefault(first => first.Id == id);
            if(product == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(product);
        }


        public HttpResponseMessage Post(Product product)
        {
            var maxId = this.products.Max(m => m.Id);
            product.Id = ++maxId;
            this.products.Add(product);
            var response = Request.CreateResponse(HttpStatusCode.Created, product);
            string uri = Url.Link("DefaultApi", new { id = product.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        private List<Product> GetTestProducts()
        {
            var testProducts = new List<Product>();
            testProducts.Add(new Product { Id = 1, Name = "Demo1", Price = 1 });
            testProducts.Add(new Product { Id = 2, Name = "Demo2", Price = 3.75M });
            testProducts.Add(new Product { Id = 3, Name = "Demo3", Price = 16.99M });
            testProducts.Add(new Product { Id = 4, Name = "Demo4", Price = 11.00M });

            return testProducts;
        }
    }
}
