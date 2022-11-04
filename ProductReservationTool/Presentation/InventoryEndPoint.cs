using ProductReservationTool.Data;
using ProductReservationTool.Domain.Entities;
using ProductReservationTool.Domain.Interfaces;
using ProductReservationTool.Domain.UseCases;

namespace ProductReservationTool.Presentation
{
    public class InventoryEndPoint : IBackEndService
    {
        IInventoryRepository repository;
        ILogger logger;

        public InventoryEndPoint()
        {
            repository = new InventoryMemoryRepository();
        }

        public InventoryEndPoint(IInventoryRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public Reservation CreateReservation(List<OrderLine> order)
        {
            try
            {
                var resService = new ReservationService(repository);
                return resService.Create(order);
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }

        public List<Reservation> GetReservations(int cursor, int limit)
        {
            try
            {
                var resService = new ReservationService(repository);
                return resService.Get(cursor, limit).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }

        public List<Reservation> GetAllReservations()
        {
            try
            {
                var resService = new ReservationService(repository);
                return resService.GetAll().ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }


        public Reservation? GetReservationByID(string id)
        {
            try
            {
                var resService = new ReservationService(repository);
                return resService.GetByID(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }

        public Product CreateProduct(Product product)
        {
            try
            {
                var prodService = new ProductService(repository);
                return prodService.Create(product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }
        public List<Product> GetProducts(int cursor, int limit)
        {
            try
            {
                var prodService = new ProductService(repository);
                return prodService.Get(cursor, limit).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                var prodService = new ProductService(repository);
                return prodService.GetAll().ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }

        public Product? GetProductByID(string id)
        {
            try
            {
                var prodService = new ProductService(repository);
                return prodService.GetByID(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }

        public void SetProduct(string productId, int quantity)
        {
            try
            {
                var prodService = new ProductService(repository);
                prodService.SetProduct(productId, quantity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
                throw;
            }
        }
    }
}
