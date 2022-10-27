using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Exceptions
{
    public class UnknownProductException : Exception
    {
        public UnknownProductException(int productID)
            : base($"unknown product #" + productID)
        {

        }
    }
}
