using Hypersonic.Core.Exceptions;

namespace Hypersonic.Core
{
    public class CodeContract
    {
        /// <summary> Requires. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="condition"> true to condition. </param>
        /// <param name="message">   The message. </param>
        public static void Requires(bool condition, string message)
        {
            if (!condition)
            {
                throw new GenericException(message);
            }
        }

        /// <summary> Asserts. </summary>
        /// <exception cref="GenericException"> Thrown when a generic error condition occurs. </exception>
        /// <param name="condition"> true to condition. </param>
        /// <param name="message">   The message. </param>
        public static void Assert(bool condition, string message)
        {
            if(!condition)
            {
                throw new GenericException(message);
            }
        }
    }
}
