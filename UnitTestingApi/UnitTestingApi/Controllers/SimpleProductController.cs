using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UnitTestingApi.Models;

namespace UnitTestingApi.Controllers
{
    public class SimpleProductController : ApiController
    {
        readonly List<Product> products = new List<Product>();
        public SimpleProductController() { }

        public SimpleProductController(List<Product> products)
        {
            this.products = products;
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return products;
        }

        public  async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await Task.FromResult(GetAllProduct());
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault(f => f.Id == id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }


        public async Task<IHttpActionResult> GetProductAysnc(int id)
        {
            return await Task.FromResult(GetProduct(id));
        }
    }
}
