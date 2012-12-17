using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hypersonic.Core.Exceptions
{
    public class InvalidTypeException : HypersonicException {
        public InvalidTypeException(string message) : base(message)
        {
        }
    }
}
