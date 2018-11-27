using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using UnitTestingApi.Controllers;
using UnitTestingApi.Models;

namespace UnitTestingApi.Tests.Controllers
{
    [TestClass]
    public class ProductTestController
    {

        ProductController productController;

        [TestInitialize]
        public void SetUp()
        {
            ///setup
            productController = new ProductController();
            productController.Request = new HttpRequestMessage();
            productController.Configuration = new System.Web.Http.HttpConfiguration();
        }

        [TestMethod]
        public void GetReturnsProduct()
        {               
            var response = productController.Get(3);

            Product product;
            Assert.IsTrue(response.TryGetContentValue<Product>(out product));
            Assert.AreEqual(3, product.Id);
        }

        [TestMethod]
        public void PostSetsLocationHeader()
        {
            productController.Request.RequestUri = new Uri("http://localhost/api/products");
            productController.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            productController.RequestContext.RouteData = new HttpRouteData(

                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "products" } });
            Product product = new Product { Name = "test product", Price = 563.36m };
            var response = productController.Post(product);

            Assert.AreEqual("http://localhost/api/products/5", response.Headers.Location.AbsoluteUri);

        }


    }
}
