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
            foreach(OrderLine order in orders)
            {
                repository.InsertOrderLine(order);
            }

            var resa = new Reservation() { ReservationId = GetNewReservationID(), CreatedAt = DateTime.Now, OrderLines = orders };
            repository.InsertReservation(resa);
            return resa;
        }

        public int GetNewReservationID()
        {
            Reservation? lastResa = repository.GetReservation();
            return (lastResa != null) ? lastResa.ReservationId++ : 1;
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
