using System;
using System.Linq;
using System.Reflection;
using Hypersonic.Attributes;

namespace Hypersonic.Core
{
    internal class AttributeService
    {
        /// <summary>   Gets an attribute. </summary>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="propertyInfo"> The property info. </param>
        ///
        /// <returns>   The attribute&lt; t&gt; </returns>
        public T GetAttribute<T>(PropertyInfo propertyInfo ) where T : Attribute
        {
            var attributes = propertyInfo.GetCustomAttributes(typeof(T), true).OfType<T>().ToArray();
            return attributes.Any() ? attributes.FirstOrDefault() : null;
        }

        /// <summary>   Gets a name. </summary>
        /// <param name="info"> The information. </param>
        /// <returns>   The name. </returns>
        public string GetName<T>(PropertyInfo info) where T : Attribute, IName
        {
          return GetAttribute<T>(info).Name;
        }
    }
}
