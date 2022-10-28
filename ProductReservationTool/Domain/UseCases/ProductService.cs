using ProductReservationTool.Domain.Entities;
using ProductReservationTool.Domain.Exceptions;
using ProductReservationTool.Domain.Interfaces;

namespace ProductReservationTool.Domain.UseCases
{
    public class ProductService
    {
        IInventoryRepository repository;

        public ProductService(IInventoryRepository repo)
        {
            repository = repo;
        }

        public Product Create(Product product)
        {
            product.ProductId = GetNewProductID();
            repository.InsertProduct(product);
            return product;
        }

        public IQueryable<Product> Get(int cursor, int limit)
        {
            return repository.GetProducts().Skip(cursor).Take(limit);
        }

        public IQueryable<Product> GetAll()
        {
            return repository.GetProducts();
        }

        public Product? GetByID(string id)
        {
            return repository.GetProduct(id);
        }

        public string GetNewProductID()
        {
            Product? lastProd = repository.GetProduct();
            var newID = lastProd != null ? int.Parse(lastProd.ProductId) + 1 : 1;
            return newID.ToString();
        }

        public void SetProduct(string productId, int quantity)
        {
            var product = GetByID(productId);
            if (product == null)
                throw new UnknownProductException(productId);

            product.Quantity = quantity;
            var resaService = new ReservationService(repository);
            var reservations = resaService.GetReservationsForProduct(product.ProductId);

            if (quantity == 0)
            {
                foreach (var reservation in reservations)
                {
                    resaService.UpdateAvailibility(reservation, false);
                }
            }
            else
            {
                foreach (var reservation in reservations)
                {
                    var orderProduct = reservation.OrderLines.Where(o => o.ProductId == productId).First();
                    if (orderProduct.Quantity < quantity)
                    {
                        quantity = quantity - orderProduct.Quantity;
                        resaService.UpdateAvailibility(reservation, true);
                    }
                }
            }

            repository.UpdateProduct(product);
        }
    }
}
