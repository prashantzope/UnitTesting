using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestingApi.Controllers;
using UnitTestingApi.Models;

namespace UnitTestingApi.Tests.Controllers
{
    [TestClass]
    public class TestSimpleProductController
    {

        List<Product> testProduct;
        SimpleProductController controller;
        [TestInitialize]
        public void TestSetup()
        {
             testProduct = GetTestProducts();
             controller = new SimpleProductController(testProduct);
        }

        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProduct()
        {
            
            var result = controller.GetAllProduct() as List<Product>;
            Assert.AreEqual(testProduct.Count, result.Count);
        }

        [TestMethod]
        public async Task GetAllProuctAsync_ShouldReturnAllProduct()
        {
            var result = await controller.GetAllProductAsync() as List<Product>;
            Assert.AreEqual(testProduct.Count, result.Count);                 
        }

        [TestMethod]
        public void GetProduct_ShouldReturnCorrectProduct()
        {
            

            var result = controller.GetProduct(4) as OkNegotiatedContentResult<Product>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProduct[3].Name, result.Content.Name);
        }

        [TestMethod]
        public async Task GetProductAsync_ShouldReturnCorrectProduct()
        {
           

            var result = await controller.GetProductAysnc(4) as OkNegotiatedContentResult<Product>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProduct[3].Name, result.Content.Name);
        }

        [TestMethod]
        public void GetProduct_ShouldNotFindProduct()
        {
            var controller = new SimpleProductController(GetTestProducts());

            var result = controller.GetProduct(999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
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
