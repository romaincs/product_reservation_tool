using ProductReservationTool.Domain.Entities;
using ProductReservationTool.Domain.Interfaces;

namespace ProductReservationTool.Domain.UseCases
{
    public class MockDataService
    {
        IInventoryRepository repository;

        public MockDataService(IInventoryRepository repo)
        {
            repository = repo;
        }

        public void Generate(List<Reservation> reservations, List<Product> products, List<OrderLine> orders)
        {
            if(repository.GetProducts().Count() == 0)
                foreach (var product in products)
                    repository.InsertProduct(product);

            if (repository.GetReservations().Count() == 0)
            {
                foreach (var order in orders)
                    repository.InsertOrderLine(order);
                foreach (var reservation in reservations)
                    repository.InsertReservation(reservation);
            }                
        }

    }
}
