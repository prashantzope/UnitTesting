using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitTestingApi.Models;

namespace UnitTestingApi.Controllers
{
    public class ProductsV2Controller : ApiController
    {

        List<Product> products;
        public ProductsV2Controller()
        {
            products = GetTestProducts();
        }


        public IHttpActionResult Get(int id)
        {
            Product product = this.products.FirstOrDefault(first => first.Id == id);

            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        public IHttpActionResult Post(Product product)
        {
            int maxid = (this.products.Max(i => i.Id));
            product.Id = ++maxid;
            this.products.Add(product);
            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        public IHttpActionResult Delete(int id)
        {
            var product = this.products.FirstOrDefault(f => f.Id == id);
            if (product == null)
                return NotFound();
            this.products.Remove(product);
            return Ok();
        }

        public IHttpActionResult Put(Product product)
        {
            return Content(HttpStatusCode.Accepted, product);
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
