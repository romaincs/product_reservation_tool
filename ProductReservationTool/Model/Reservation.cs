using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Model
{
    public class Reservation
    {
        public int ReservationId { get; set;  }
        public DateTime CreatedAt { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public bool IsAvailable { get; set; }
    }
}
