using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductReservationTool.Domain.Exceptions
{
    public class UnknownProductException : Exception
    {
        public UnknownProductException(string productID)
            : base($"unknown product #" + productID)
        {

        }
    }
}
