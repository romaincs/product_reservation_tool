using ProductReservationTool.Data;
using ProductReservationTool.Domain.Entities;
using ProductReservationTool.Domain.Exceptions;
using ProductReservationTool.Domain.Interfaces;
using ProductReservationTool.Domain.UseCases;
using ProductReservationTool.Logger;
using ProductReservationTool.Presentation;

namespace ProductReservationTool.Tests
{
    [TestClass]
    public class UnitTestReservation
    {
        InventoryEndPoint inventoryEndPoint;

        [TestInitialize]
        public void SetUp()
        {
            var imMemRep = new InventoryMemoryRepository();
            var consoleLogger = new ConsoleLogger(LogLevel.Info);
            var mockService = new MockDataService(imMemRep);
            mockService.Generate(TestData.Reservations, TestData.Products, TestData.Orders);
            inventoryEndPoint = new InventoryEndPoint(imMemRep, consoleLogger);
        }

        [TestMethod]
        public void TestCreate_Single()
        {
            var order1 = new OrderLine() { ProductId = "1", Quantity = 10 };
            var orders = new List<OrderLine>() { order1 };

            var reservation = inventoryEndPoint.CreateReservation(orders);
            Assert.IsNotNull(reservation);
        }

        [TestMethod]
        public void TestCreate_Bulk()
        {
            var order1 = new OrderLine() { ProductId = "1", Quantity = 4 };
            var order2 = new OrderLine() { ProductId = "2", Quantity = 2 };
            var order3 = new OrderLine() { ProductId = "3", Quantity = 8 };
            var orders = new List<OrderLine>() { order1, order2, order3 };

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
            Assert.AreEqual(reservations.Count, count + ITEMS_NB);
        }

        [TestMethod]
        public void TestCreate_UnknownProduct()
        {
            var order1 = new OrderLine() { ProductId = "999", Quantity = 10 };
            var orders = new List<OrderLine>() { order1 };

            Assert.ThrowsException<UnknownProductException>(() => inventoryEndPoint.CreateReservation(orders));
        }

        [TestMethod]
        public void TestCreate_SameProduct()
        {
            var order1 = new OrderLine() { ProductId = "1", Quantity = 10 };
            var order2 = new OrderLine() { ProductId = "1", Quantity = 6 };
            var orders = new List<OrderLine>() { order1, order2 };

            Assert.ThrowsException<DuplicateProductException>(() => inventoryEndPoint.CreateReservation(orders));
        }

        [TestMethod]
        public void TestCreate_OutofStockProduct()
        {
            var order1 = new OrderLine() { ProductId = "3", Quantity = 10 };
            var orders = new List<OrderLine>() { order1 };

            var reservation = inventoryEndPoint.CreateReservation(orders);
            Assert.IsNotNull(reservation);
        }

        [TestMethod]
        public void TestGet_Single()
        {
            const int LIMIT = 1;

            var reservations = inventoryEndPoint.GetReservations(0, LIMIT);
            Assert.AreEqual(reservations.Count, LIMIT);
        }

        [TestMethod]
        public void TestGet_Limit()
        {
            const int LIMIT = 3;

            var reservations = inventoryEndPoint.GetReservations(0, LIMIT);
            Assert.AreEqual(LIMIT, reservations.Count);
        }

        [TestMethod]
        public void TestGet_Unique()
        {
            var reservations = inventoryEndPoint.GetAllReservations();

            var duplicates = reservations.GroupBy(r => r.ReservationId)
                  .Where(r => r.Count() > 1)
                  .Select(r => r.Key)
                  .ToList();

            Assert.AreEqual(0, duplicates.Count);
        }

        [TestMethod]
        public void TestGet_ReservationUnavailability()
        {
            const string ID = "1";

            var reservation = inventoryEndPoint.GetReservationByID(ID);
            Assert.IsNotNull(reservation);
            Assert.IsTrue(reservation.IsAvailable);

            inventoryEndPoint.SetProduct(ID, 0);

            reservation = inventoryEndPoint.GetReservationByID(ID);
            Assert.IsNotNull(reservation);
            Assert.IsFalse(reservation.IsAvailable);
        }

        [TestMethod]
        public void TestGet_ReservationAvailability()
        {
            const string RESA_ID = "4";
            const string PRODUCT_ID = "3";
            const int QUANTITY = 12;

            var reservation = inventoryEndPoint.GetReservationByID(RESA_ID);
            Assert.IsNotNull(reservation);
            Assert.IsFalse(reservation.IsAvailable);

            inventoryEndPoint.SetProduct(PRODUCT_ID, QUANTITY);

            reservation = inventoryEndPoint.GetReservationByID(RESA_ID);
            Assert.IsNotNull(reservation);
            Assert.IsTrue(reservation.IsAvailable);
        }

        [TestMethod]
        public void TestGet_IsFIFO()
        {
            const int LIMIT = 3;

            var reservations = inventoryEndPoint.GetReservations(0, LIMIT);
            Assert.AreEqual(LIMIT, reservations.Count);

            bool isSorted = reservations[0].CreatedAt < reservations[1].CreatedAt && reservations[1].CreatedAt < reservations[2].CreatedAt;
            Assert.IsTrue(isSorted);
        }
    }
}