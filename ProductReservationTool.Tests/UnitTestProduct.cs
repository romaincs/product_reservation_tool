using ProductReservationTool.Data;
using ProductReservationTool.Domain.Entities;
using ProductReservationTool.Domain.UseCases;

namespace ProductReservationTool.Tests
{
    [TestClass]
    public class UnitTestProduct
    {
        ProductService productService;

        [TestInitialize]
        public void SetUp()
        {
            var imMemRep = new InventoryMemoryRepository();
            var mockService = new MockDataService(imMemRep);
            mockService.Generate(TestData.Reservations, TestData.Products, TestData.Orders);
            productService = new ProductService(imMemRep);
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
                var product = productService.Create(products[i]);
                Assert.AreNotEqual(product.ProductId, "0");
            }
        }

        [TestMethod]
        public void TestGet_Single()
        {
            var products = productService.Get(0, 1);
            Assert.AreEqual(1, products.Count());
        }

        [TestMethod]
        public void TestGet_Limit()
        {
            var products = productService.Get(0, 2);
            Assert.AreEqual(2, products.Count());
        }

        [TestMethod]
        public void TestGet_SetQuantity()
        {
            const string ID = "2";
            const int QUANTITY = 12;

            productService.SetProduct(ID, QUANTITY);
            var product = productService.GetByID("1");

            Assert.IsNotNull(product);
            Assert.AreEqual(QUANTITY, product.Quantity);
        }

        [TestMethod]
        public void TestGet_Unique()
        {
            var products = productService.GetAll();

            var duplicates = products.GroupBy(p => p.ProductId)
                  .Where(p => p.Count() > 1)
                  .Select(p => p.Key)
                  .ToList();

            Assert.AreEqual(0, duplicates.Count);
        }
    }
}