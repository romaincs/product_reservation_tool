using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Model
{
    public class OrderLine
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
