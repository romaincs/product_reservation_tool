using ProductReservationTool.Data;
using ProductReservationTool.Domain.Entities;
using ProductReservationTool.Domain.Interfaces;
using ProductReservationTool.Domain.UseCases;

namespace ProductReservationTool.Presentation
{
    public class InventoryEndPoint : IBackEndService
    {
        IInventoryRepository repository;

        public InventoryEndPoint()
        {
            repository = new InventoryMemoryRepository();
        }

        public InventoryEndPoint(IInventoryRepository rep)
        {
            repository = rep;
        }

        public Reservation CreateReservation(List<OrderLine> order)
        {
            var resService = new ReservationService(repository);
            return resService.Create(order);
        }

        public List<Reservation> GetReservations(int cursor, int limit)
        {
            var resService = new ReservationService(repository);
            return resService.Get(cursor, limit).ToList();
        }

        public List<Reservation> GetAllReservations()
        {
            var resService = new ReservationService(repository);
            return resService.GetAll().ToList();
        }


        public Reservation? GetReservationByID(string id)
        {
            var resService = new ReservationService(repository);
            return resService.GetByID(id);
        }

        public Product CreateProduct(Product product)
        {
            var prodService = new ProductService(repository);
            return prodService.Create(product);
        }
        public List<Product> GetProducts(int cursor, int limit)
        {
            var prodService = new ProductService(repository);
            return prodService.Get(cursor, limit).ToList();
        }

        public List<Product> GetAllProducts()
        {
            var prodService = new ProductService(repository);
            return prodService.GetAll().ToList();
        }

        public Product? GetProductByID(string id)
        {
            var prodService = new ProductService(repository);
            return prodService.GetByID(id);
        }

        public void SetProduct(string productId, int quantity)
        {
            var prodService = new ProductService(repository);
            prodService.SetProduct(productId, quantity);
        }
    }
}
