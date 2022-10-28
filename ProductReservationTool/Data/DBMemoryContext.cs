using ProductReservationTool.Domain.Entities;

namespace ProductReservationTool.Data
{
    class DBMemoryContext
    {
        public List<Reservation> Reservations;
        public List<Product> Products;
        public List<OrderLine> Orders;

        private static DBMemoryContext _instance;

        private DBMemoryContext(List<Reservation>? reservations, List<Product>? products, List<OrderLine>? orders)
        {
            this.Reservations = reservations ?? new List<Reservation>();
            this.Products = products ?? new List<Product>();
            this.Orders = orders ?? new List<OrderLine>();
        }

        public static DBMemoryContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DBMemoryContext(null, null, null);
            }
            return _instance;
        }

        public static DBMemoryContext GetInstance(List<Reservation> reservations, List<Product> products, List<OrderLine> orders)
        {
            if (_instance == null)
            {
                _instance = new DBMemoryContext(reservations, products, orders);
            }
            return _instance;
        }
    }
}
