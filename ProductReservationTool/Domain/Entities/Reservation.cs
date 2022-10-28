using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Domain.Entities
{
    public class Reservation
    {
        public string ReservationId { get; set;  }
        public DateTime CreatedAt { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public bool IsAvailable { get; set; }
    }
}
