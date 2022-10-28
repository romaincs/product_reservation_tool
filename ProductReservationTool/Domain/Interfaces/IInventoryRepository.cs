using ProductReservationTool.Domain.Entities;

namespace ProductReservationTool.Domain.Interfaces
{
    public interface IInventoryRepository
    {
        IQueryable<Product> GetProducts();
        void InsertProduct(Product product);
        Product? GetProduct();
        public Product? GetProduct(string id);
        public void UpdateProduct(Product product);
        IQueryable<Reservation> GetReservations();
        void InsertReservation(Reservation reservation);
        Reservation? GetReservation();
        Reservation? GetReservation(string id);
        public void UpdateReservation(Reservation reservation);
        void InsertOrderLine(OrderLine order);

    }
}
