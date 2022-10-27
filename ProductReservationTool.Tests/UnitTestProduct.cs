using ProductReservationTool.API;
using ProductReservationTool.Model;
using ProductReservationTool.Repository;
using ProductReservationTool.Service;
using System.Net.Http.Headers;

namespace ProductReservationTool.Tests
{
    [TestClass]
    public class UnitTestProduct
    {
        [TestMethod]
        public void TestCreate_Bulk()
        {
            var product1 = new Product() { Quantity = 10 };
            var product2 = new Product() { Quantity = 3 };
            var product3 = new Product() { Quantity = 7 };
            var products = new List<Product>() { product1, product2, product3 };

            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                for(int i = 0; i < products.Count; i++)
                {
                    var product = inventoryEndPoint.CreateProduct(products[i]);
                    if (product.ProductId == "0")
                        Assert.Fail("ProductId is zero, should be positive");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGet_Single()
        {
            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                var products = inventoryEndPoint.GetProducts(0, 1);
                if (products.Count != 1)
                    Assert.Fail($"GetProducts return {products.Count}, should return 1");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGet_Limit()
        {
            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                var products = inventoryEndPoint.GetProducts(0, 2);
                if (products.Count != 2)
                    Assert.Fail($"GetProducts return {products.Count}, should return 2");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGet_SetQuantity()
        {
            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                const string ID = "2";
                const int QUANTITY = 12;

                inventoryEndPoint.SetProduct(ID, QUANTITY);

                var product = inventoryEndPoint.GetProductByID("1");
                if (product == null)
                    Assert.Fail($"Product #{ID} does not exist");

                if (product.Quantity != QUANTITY)
                    Assert.Fail($"Product #1 has Quantity = {product.Quantity}, should has {QUANTITY}");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGet_Unique()
        {
            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                var products = inventoryEndPoint.GetAllProducts();

                var duplicates = products.GroupBy(p => p.ProductId)
                      .Where(p => p.Count() > 1)
                      .Select(p => p.Key)
                      .ToList();

                if (duplicates.Count > 0)
                    Assert.Fail($"Found {duplicates.Count} duplicates, should be zero");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}