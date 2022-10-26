using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductReservationTool.Model;

namespace ProductReservationTool.Repository
{
    public interface IInventoryRepository
    {
        IQueryable<Product> GetProducts();
        void InsertProduct(Product product);
        Product? GetProduct();
        public Product? GetProduct(int id);
        public void UpdateProduct(Product product);
        IQueryable<Reservation> GetReservations();
        void InsertReservation(Reservation reservation);
        Reservation? GetReservation();
        Reservation? GetReservation(int id);
        void InsertOrderLine(OrderLine order);

    }
}
