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
        IQueryable<Reservation> GetReservations();
        void InsertReservation(Reservation reservation);
        Reservation? GetReservation();
        Reservation? GetReservation(int id);

    }
}
