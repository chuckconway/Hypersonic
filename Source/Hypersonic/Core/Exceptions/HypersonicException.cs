using System;

namespace Hypersonic.Core.Exceptions
{
    public abstract class HypersonicException : Exception
    {
        /// <summary> Constructor. </summary>
        /// <param name="message"> The message. </param>
        protected HypersonicException(string message): base(message){}
    }
}
