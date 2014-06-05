using System.Collections.Generic;
using Hypersonic.Core;

namespace Hypersonic.Session.Persistance
{
    public interface IKeysDefined
    {
        /// <summary> Gets a value indicating whether the primary keys exist. </summary>
        /// <value> true if primary keys exist, false if not. </value>
        bool PrimaryKeysExist { get; }

        /// <summary> Generates the SQL. </summary>
        /// <param name="name">        The name. </param>
        /// <param name="primaryKeys"> The item. </param>
        /// <param name="properties">  The properties. </param>
        /// <returns> The sql. </returns>
        string GenerateSql(string name, IList<Property> primaryKeys, IList<Property> properties);
    }
}
