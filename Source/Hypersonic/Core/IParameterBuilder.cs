using System.Collections.Generic;
using System.Data.Common;

namespace Hypersonic.Core
{
    public interface IParameterBuilder
    {
        /// <summary>
        /// Renders public properties into a DbParameter collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramters">The paramters.</param>
        /// <returns></returns>
        List<DbParameter> GetParameters<T>(T paramters);

    }
}
