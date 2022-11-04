using ProductReservationTool.Data;
using ProductReservationTool.Domain.Entities;
using ProductReservationTool.Domain.Interfaces;
using ProductReservationTool.Domain.UseCases;
using ProductReservationTool.Logger;
using ProductReservationTool.Presentation;

namespace ProductReservationTool.Tests
{
    [TestClass]
    public class UnitTestProduct
    {
        InventoryEndPoint inventoryEndPoint;

        [TestInitialize]
        public void SetUp()
        {
            var imMemRep = new InventoryMemoryRepository();
            var mockService = new MockDataService(imMemRep);
            var consoleLogger = new ConsoleLogger(LogLevel.Info);
            mockService.Generate(TestData.Reservations, TestData.Products, TestData.Orders);
            inventoryEndPoint = new InventoryEndPoint(imMemRep, consoleLogger);
        }

        [TestMethod]
        public void TestCreate_Bulk()
        {
            var product1 = new Product() { Quantity = 10 };
            var product2 = new Product() { Quantity = 3 };
            var product3 = new Product() { Quantity = 7 };
            var products = new List<Product>() { product1, product2, product3 };

            for (int i = 0; i < products.Count; i++)
            {
                var product = inventoryEndPoint.CreateProduct(products[i]);
                Assert.AreNotEqual(product.ProductId, "0");
            }
        }

        [TestMethod]
        public void TestGet_Single()
        {
            var products = inventoryEndPoint.GetProducts(0, 1);
            Assert.AreEqual(1, products.Count);
        }

        [TestMethod]
        public void TestGet_Limit()
        {
            var products = inventoryEndPoint.GetProducts(0, 2);
            Assert.AreEqual(2, products.Count);
        }

        [TestMethod]
        public void TestGet_SetQuantity()
        {
            const string ID = "2";
            const int QUANTITY = 12;

            inventoryEndPoint.SetProduct(ID, QUANTITY);

            var product = inventoryEndPoint.GetProductByID("1");
            Assert.IsNotNull(product);
            Assert.AreEqual(QUANTITY, product.Quantity);
        }

        [TestMethod]
        public void TestGet_Unique()
        {
            var products = inventoryEndPoint.GetAllProducts();

            var duplicates = products.GroupBy(p => p.ProductId)
                  .Where(p => p.Count() > 1)
                  .Select(p => p.Key)
                  .ToList();

            Assert.AreEqual(0, duplicates.Count);
        }
    }
}