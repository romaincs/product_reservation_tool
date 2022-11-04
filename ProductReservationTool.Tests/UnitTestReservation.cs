using ProductReservationTool.Data;
using ProductReservationTool.Domain.Entities;
using ProductReservationTool.Domain.Exceptions;
using ProductReservationTool.Domain.UseCases;

namespace ProductReservationTool.Tests
{
    [TestClass]
    public class UnitTestReservation
    {
        ReservationService reservationService;
        ProductService productService;

        [TestInitialize]
        public void SetUp()
        {
            var imMemRep = new InventoryMemoryRepository();
            var mockService = new MockDataService(imMemRep);
            mockService.Generate(TestData.Reservations, TestData.Products, TestData.Orders);
            reservationService = new ReservationService(imMemRep);
            productService = new ProductService(imMemRep);
        }

        [TestMethod]
        public void TestCreate_Single()
        {
            var order1 = new OrderLine() { ProductId = "1", Quantity = 10 };
            var orders = new List<OrderLine>() { order1 };

            var reservation = reservationService.Create(orders);
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
            var reservations = reservationService.GetAll();
            int count = reservations.Count();

            for (int i = 0; i < ITEMS_NB; i++)
            {
                var reservation = reservationService.Create(orders);
                if (reservation == null)
                    Assert.Fail("Reservation is null");
            }

            reservations = reservationService.GetAll();
            Assert.AreEqual(reservations.Count(), count + ITEMS_NB);
        }

        [TestMethod]
        public void TestCreate_UnknownProduct()
        {
            var order1 = new OrderLine() { ProductId = "999", Quantity = 10 };
            var orders = new List<OrderLine>() { order1 };

            Assert.ThrowsException<UnknownProductException>(() => reservationService.Create(orders));
        }

        [TestMethod]
        public void TestCreate_SameProduct()
        {
            var order1 = new OrderLine() { ProductId = "1", Quantity = 10 };
            var order2 = new OrderLine() { ProductId = "1", Quantity = 6 };
            var orders = new List<OrderLine>() { order1, order2 };

            Assert.ThrowsException<DuplicateProductException>(() => reservationService.Create(orders));
        }

        [TestMethod]
        public void TestCreate_OutofStockProduct()
        {
            var order1 = new OrderLine() { ProductId = "3", Quantity = 10 };
            var orders = new List<OrderLine>() { order1 };

            var reservation = reservationService.Create(orders);
            Assert.IsNotNull(reservation);
        }

        [TestMethod]
        public void TestGet_Single()
        {
            const int LIMIT = 1;

            var reservations = reservationService.Get(0, LIMIT);
            Assert.AreEqual(reservations.Count(), LIMIT);
        }

        [TestMethod]
        public void TestGet_Limit()
        {
            const int LIMIT = 3;

            var reservations = reservationService.Get(0, LIMIT);
            Assert.AreEqual(LIMIT, reservations.Count());
        }

        [TestMethod]
        public void TestGet_Unique()
        {
            var reservations = reservationService.GetAll();

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

            var reservation = reservationService.GetByID(ID);
            Assert.IsNotNull(reservation);
            Assert.IsTrue(reservation.IsAvailable);

            productService.SetProduct(ID, 0);
            reservation = reservationService.GetByID(ID);

            Assert.IsNotNull(reservation);
            Assert.IsFalse(reservation.IsAvailable);
        }

        [TestMethod]
        public void TestGet_ReservationAvailability()
        {
            const string RESA_ID = "4";
            const string PRODUCT_ID = "3";
            const int QUANTITY = 12;

            var reservation = reservationService.GetByID(RESA_ID);
            Assert.IsNotNull(reservation);
            Assert.IsFalse(reservation.IsAvailable);

            productService.SetProduct(PRODUCT_ID, QUANTITY);
            reservation = reservationService.GetByID(RESA_ID);

            Assert.IsNotNull(reservation);
            Assert.IsTrue(reservation.IsAvailable);
        }

        [TestMethod]
        public void TestGet_IsFIFO()
        {
            const int LIMIT = 3;

            var reservations = reservationService.Get(0, LIMIT).ToList();
            Assert.AreEqual(LIMIT, reservations.Count);

            bool isSorted = reservations[0].CreatedAt < reservations[1].CreatedAt && reservations[1].CreatedAt < reservations[2].CreatedAt;
            Assert.IsTrue(isSorted);
        }
    }
}