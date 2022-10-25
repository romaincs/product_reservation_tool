using ProductReservationTool.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Repository
{
    public class InventoryMemoryRepository : IInventoryRepository
    {
        static List<Reservation> reservations;

        public InventoryMemoryRepository()
        {
            if(reservations == null)
                reservations = new List<Reservation>(); 
        }

        public IQueryable<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Reservation> GetReservations()
        {
            return reservations.AsQueryable();
        }

        public void InsertReservation(Reservation reservation)
        {
            reservations.Add(reservation);
        }

        public Reservation? GetReservation()
        {
            if(reservations.Count == 0)
                return null;

            return reservations.Last();
        }

        public Reservation? GetReservation(int id)
        {
            return reservations.Where(r => r.ReservationId == id).FirstOrDefault();
        }
    }
}
