using ProductReservationTool.Model;
using ProductReservationTool.Repository;
using ProductReservationTool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.API
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

        #region RESERVATIONS --------------------------------------------------------------------------------
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
        #endregion

        #region PRODUCTS ------------------------------------------------------------------------------------
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

        public Product? GetProductByID(int id)
        {
            var prodService = new ProductService(repository);
            return prodService.GetByID(id);
        }

        public void SetProduct(int productId, int quantity)
        {
            var prodService = new ProductService(repository);
            prodService.SetProduct(productId, quantity);
        } 
        #endregion
    }
}
