using ProductReservationTool.Domain.Entities;
using ProductReservationTool.Domain.Interfaces;

namespace ProductReservationTool.Data
{
    public class InventoryMemoryRepository : IInventoryRepository
    {
        static List<Reservation> reservations;
        static List<Product> products;
        static List<OrderLine> orders;

        public InventoryMemoryRepository()
        {
            if (products == null)
                products = new List<Product>();
            if (orders == null)
                orders = new List<OrderLine>();
            if (reservations == null)
                reservations = new List<Reservation>();
        }

        public InventoryMemoryRepository(List<Product> initProducts, List<OrderLine> initOrders, List<Reservation> initResa)
        {
            products = initProducts;
            orders = initOrders;
            reservations = initResa;
        }

        public void InsertProduct(Product product)
        {
            products.Add(product);
        }

        public IQueryable<Product> GetProducts()
        {
            return products.AsQueryable();
        }

        public Product? GetProduct()
        {
            if (products.Count == 0)
                return null;

            return products.OrderByDescending(o => int.Parse(o.ProductId)).FirstOrDefault();
        }

        public Product? GetProduct(string id)
        {
            return products.Where(r => r.ProductId == id).FirstOrDefault();
        }

        public void UpdateProduct(Product product)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (product.ProductId == product.ProductId)
                {
                    products[i].Quantity = product.Quantity;
                    return;
                }
            }
        }

        #region RESERVATIONS --------------------------------------------------------------------------------
        public IQueryable<Reservation> GetReservations()
        {
            return reservations.OrderBy(r => r.CreatedAt).AsQueryable();
        }

        public void InsertReservation(Reservation reservation)
        {
            reservations.Add(reservation);
        }

        public Reservation? GetReservation()
        {
            return reservations.OrderByDescending(r => int.Parse(r.ReservationId)).FirstOrDefault();
        }

        public Reservation? GetReservation(string id)
        {
            return reservations.Where(r => r.ReservationId == id).FirstOrDefault();
        }

        public void UpdateReservation(Reservation reservation)
        {
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservation.ReservationId == reservation.ReservationId)
                {
                    reservations[i].OrderLines = reservation.OrderLines;
                    reservations[i].IsAvailable = reservation.IsAvailable;
                    return;
                }
            }
        }
        #endregion

        #region ORDER LINES ---------------------------------------------------------------------------------
        public void InsertOrderLine(OrderLine order)
        {
            orders.Add(order);
        }
        #endregion
    }
}
