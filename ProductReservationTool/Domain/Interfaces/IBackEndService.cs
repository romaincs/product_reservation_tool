using ProductReservationTool.Domain.Entities;

namespace ProductReservationTool.Domain.Interfaces
{
    internal interface IBackEndService
    {
        Reservation CreateReservation(List<OrderLine> order);
        List<Reservation> GetReservations(int cursor, int limit);
        void SetProduct(string productId, int quantity);
        List<Product> GetProducts(int cursor, int limit);
    }
}
