using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductReservationTool.Model;

namespace ProductReservationTool.API
{
    interface IBackEndService
    {
        Reservation CreateReservation(List<OrderLine> order);
        List<Reservation> GetReservations(int cursor, int limit);
        void SetProduct(int productId, int quantity);
        List<Product> GetProducts(int cursor, int limit);
    }
}
