using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Domain.Entities
{
    public class OrderLine
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
