using ProductReservationTool.Model;
using ProductReservationTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Reservation Create(List<OrderLine> order)
        {
            Reservation lastResa = this.GetReservation();
            var newResaID = (lastResa != null) ? lastResa.ReservationId + 1 : 0;

            var resa = new Reservation() { ReservationId = newResaID, CreatedAt = DateTime.Now, OrderLines = order };
            repository.InsertReservation(resa);
            return resa;
        }

        public Reservation GetReservation()
        {
            return repository.GetReservation();
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
