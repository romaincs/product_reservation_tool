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

        public Reservation CreateReservation(List<OrderLine> order)
        {
            var resService = new ReservationService(repository);
            return resService.Create(order);
        }

        public List<Product> GetProducts(int cursor, int limit)
        {
            throw new NotImplementedException();
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

        public void SetProduct(string productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
