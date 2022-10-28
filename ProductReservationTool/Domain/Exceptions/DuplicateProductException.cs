using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Domain.Exceptions
{
    public class DuplicateProductException : Exception
    {
        public DuplicateProductException()
            : base($"duplicate product for a reservation")
        {

        }
    }
}
