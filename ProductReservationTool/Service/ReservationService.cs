using ProductReservationTool.Exceptions;
using ProductReservationTool.Model;
using ProductReservationTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Service
{
    public class ReservationService
    {
        IInventoryRepository repository;

        public ReservationService(IInventoryRepository repo)
        {
            repository = repo;
        }

        public Reservation Create(List<OrderLine> orders)
        {
            CheckDuplicates(orders);
            CheckProductsExits(orders);

            foreach(OrderLine order in orders)
            {
                repository.InsertOrderLine(order);
            }

            var resa = new Reservation() { ReservationId = GetNewReservationID(), CreatedAt = DateTime.Now, OrderLines = orders };
            repository.InsertReservation(resa);
            return resa;
        }

        private void CheckDuplicates(List<OrderLine> orders)
        {
            var duplicates = orders.GroupBy(r => r.ProductId)
                      .Where(r => r.Count() > 1)
                      .Select(r => r.Key)
                      .ToList();

            if (duplicates.Count > 0)
                throw new DuplicateProductException();
        }

        private void CheckProductsExits(List<OrderLine> orders)
        {
            var prodService = new ProductService(repository);
            foreach (var order in orders)
            {
                var product = prodService.GetByID(order.ProductId);
                if(product == null)
                    throw new UnknownProductException(order.ProductId);
            }
        }

        public int GetNewReservationID()
        {
            Reservation? lastResa = repository.GetReservation();
            return (lastResa != null) ? lastResa.ReservationId + 1 : 1;
        }

        public IQueryable<Reservation> Get(int cursor, int limit)
        {
            return repository.GetReservations().Skip(cursor).Take(limit);
        }

        public IQueryable<Reservation> GetAll()
        {
            return repository.GetReservations();
        }
    }
}
