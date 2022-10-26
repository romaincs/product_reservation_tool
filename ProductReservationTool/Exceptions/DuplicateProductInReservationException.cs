using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Exceptions
{
    public class DuplicateProductInReservationException : Exception
    {
        public DuplicateProductInReservationException()
            : base($"duplicate product for a reservation")
        {

        }
    }
}
