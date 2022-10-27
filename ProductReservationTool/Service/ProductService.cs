using ProductReservationTool.Exceptions;
using ProductReservationTool.Model;
using ProductReservationTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Service
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

        public Product? GetByID(int id)
        {
            return repository.GetProduct(id);
        }

        public int GetNewProductID()
        {
            Product? lastProd = repository.GetProduct();
            return (lastProd != null) ? lastProd.ProductId+1 : 1;
        }

        public void SetProduct(int productId, int quantity)
        {
            var product = GetByID(productId);
            if(product == null)
                throw new UnknownProductException(productId);

            product.Quantity = quantity;
            if(quantity == 0)
            {
                var resaService = new ReservationService(repository);
                var reservations = resaService.GetReservationsForProduct(product.ProductId);
                foreach (var reservation in reservations)
                {
                    resaService.UpdateAvailibility(reservation, false);
                }
            }

            repository.UpdateProduct(product);
        }
    }
}
