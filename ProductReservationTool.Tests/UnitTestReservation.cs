using ProductReservationTool.API;
using ProductReservationTool.Exceptions;
using ProductReservationTool.Model;
using ProductReservationTool.Repository;
using ProductReservationTool.Service;
using System.Net.Http.Headers;

namespace ProductReservationTool.Tests
{
    [TestClass]
    public class UnitTestReservation
    {
        [TestMethod]
        public void TestCreate_Single()
        {
            var order1 = new OrderLine() { ProductId = "1", Quantity = 10 };
            var order2 = new OrderLine() { ProductId = "2", Quantity = 6 };
            var order3 = new OrderLine() { ProductId = "3", Quantity = 7 };
            var orders = new List<OrderLine>() { order1, order2, order3 };

            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                var reservation = inventoryEndPoint.CreateReservation(orders);
                if (reservation == null)
                    Assert.Fail("Reservation is null");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestCreate_Bulk()
        {
            var order1 = new OrderLine() { ProductId = "1", Quantity = 4 };
            var order2 = new OrderLine() { ProductId = "2", Quantity = 2 };
            var order3 = new OrderLine() { ProductId = "3", Quantity = 8 };
            var orders = new List<OrderLine>() { order1, order2, order3 };

            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                const int ITEMS_NB = 10;
                var reservations = inventoryEndPoint.GetAllReservations();
                int count = reservations.Count();

                for (int i = 0; i < ITEMS_NB; i++)
                {
                    var reservation = inventoryEndPoint.CreateReservation(orders);
                    if (reservation == null)
                        Assert.Fail("Reservation is null");
                }

                reservations = inventoryEndPoint.GetAllReservations();
                if(reservations.Count != count + ITEMS_NB)
                    Assert.Fail("Must return " + count + ITEMS_NB + " values, not " + reservations.Count);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestCreate_UnknownProduct()
        {
            var order1 = new OrderLine() { ProductId = "999", Quantity = 10 };
            var orders = new List<OrderLine>() { order1 };

            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                inventoryEndPoint.CreateReservation(orders);
                Assert.Fail($"reservation created without error. Should generate one.");
            }
            catch (UnknownProductException)
            {
                // Success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestCreate_SameProduct()
        {
            var order1 = new OrderLine() { ProductId = "1", Quantity = 10 };
            var order2 = new OrderLine() { ProductId = "1", Quantity = 6 };
            var order3 = new OrderLine() { ProductId = "2", Quantity = 7 };
            var orders = new List<OrderLine>() { order1, order2, order3 };

            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                inventoryEndPoint.CreateReservation(orders);
                Assert.Fail($"reservation created without error. Should generate one.");
            }
            catch (DuplicateProductException) 
            {
                // Success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestCreate_OutofStockProduct()
        {
            var order1 = new OrderLine() { ProductId = "3", Quantity = 10 };
            var orders = new List<OrderLine>() { order1 };

            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                var reservation = inventoryEndPoint.CreateReservation(orders);
                if (reservation == null)
                    Assert.Fail("Reservation is null");
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

                const int LIMIT = 1;

                var reservations = inventoryEndPoint.GetReservations(0, LIMIT);
                if (reservations.Count != LIMIT)
                    Assert.Fail($"Must return {LIMIT} values, not {reservations.Count}");
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

                const int LIMIT = 3;

                var reservations = inventoryEndPoint.GetReservations(0, LIMIT);
                if (reservations.Count != LIMIT)
                    Assert.Fail($"Must return {LIMIT} values, not " + reservations.Count);
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

                var reservations = inventoryEndPoint.GetAllReservations();

                var duplicates = reservations.GroupBy(r => r.ReservationId)
                      .Where(r => r.Count() > 1)
                      .Select(r => r.Key)
                      .ToList();

                if (duplicates.Count > 0)
                    Assert.Fail($"Found {duplicates.Count} duplicates, should be zero");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGet_ReservationUnavailability()
        {
            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                const string ID = "1";

                var reservation = inventoryEndPoint.GetReservationByID(ID);
                if (reservation == null)
                    Assert.Fail($"reservation #{ID} does not exists.");

                if (!reservation.IsAvailable)
                    Assert.Fail("existing reservation is not available.");

                inventoryEndPoint.SetProduct(ID, 0);

                reservation = inventoryEndPoint.GetReservationByID(ID);
                if (reservation == null)
                    Assert.Fail($"reservation #{ID} does not exists.");

                if (reservation.IsAvailable) 
                    Assert.Fail($"reservation #{ID} still available. Should not be.");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGet_ReservationAvailability()
        {
            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                const string RESA_ID = "4";
                const string PRODUCT_ID = "3";
                const int QUANTITY = 12;

                var reservation = inventoryEndPoint.GetReservationByID(RESA_ID);
                if (reservation == null)
                    Assert.Fail($"reservation #{RESA_ID} does not exists.");

                if (reservation.IsAvailable)
                    Assert.Fail("existing reservation is available. Should not be.");

                inventoryEndPoint.SetProduct(PRODUCT_ID, QUANTITY);

                reservation = inventoryEndPoint.GetReservationByID(RESA_ID);
                if (reservation == null)
                    Assert.Fail($"reservation #{RESA_ID} does not exists.");

                if (!reservation.IsAvailable)
                    Assert.Fail($"reservation #{RESA_ID} still unavailable. Should be.");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGet_IsFIFO()
        {
            try
            {
                var imMemRep = new InventoryMemoryRepository(TestData.products, TestData.orders, TestData.reservations);
                var inventoryEndPoint = new InventoryEndPoint(imMemRep);

                const int LIMIT = 3;

                var reservations = inventoryEndPoint.GetReservations(0, LIMIT);
                if (reservations.Count != LIMIT)
                    Assert.Fail($"Must return {LIMIT} values, not " + reservations.Count);
                if (reservations[0].CreatedAt > reservations[1].CreatedAt || reservations[1].CreatedAt > reservations[2].CreatedAt)
                    Assert.Fail("reservations return are not sorted by date. Should be.");
                    
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}