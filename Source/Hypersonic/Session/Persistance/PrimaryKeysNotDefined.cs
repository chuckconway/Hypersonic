using System.Collections.Generic;
using Hypersonic.Core;

namespace Hypersonic.Session.Persistance
{
    public class PrimaryKeysNotDefined : IKeysDefined
    {
        /// <summary> Gets a value indicating whether the primary keys exist. </summary>
        /// <value> true if primary keys exist, false if not. </value>
        public bool PrimaryKeysExist
        {
            get { return false; }
        }

        /// <summary> Generates the Sql </summary>
        /// <param name="name">        The name. </param>
        /// <param name="primaryKeys"> The item. </param>
        /// <param name="properties">  The properties. </param>
        /// <returns> The sql. </returns>
        public string GenerateSql(string name, IList<Property> primaryKeys, IList<Property> properties)
        {
            SqlGenerator generator = new SqlGenerator();
            string sql = generator.Insert(name, properties);

            return sql;
        }
    }
}
