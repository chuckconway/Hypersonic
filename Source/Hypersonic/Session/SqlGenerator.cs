using System;
using System.Collections.Generic;
using System.Linq;
using Hypersonic.Core;
using Hypersonic.Session.Query.Expressions;

namespace Hypersonic.Session
{
    public class SqlGenerator
    {
        /// <summary> Combine to where. </summary>
        /// <param name="statement"> The statement. </param>
        /// <param name="where">     The where. </param>
        /// <returns> . </returns>
        public string CombineToWhere(string statement, string where)
        {
            return string.Format("{0} WHERE {1}", statement, where);
        }

        /// <summary> Updates the builder described by item. </summary>
        /// <param name="item"> The item. </param>
        /// <returns> . </returns>
        public string Update(object item, string name = null)
        {
            Flattener flattener = new Flattener();
            var values = flattener.GetNamesAndValues(item);
            string tableName = name ?? item.GetType().Name;
            return Update(tableName, values);
        }

        /// <summary> Updates the builder described by item. </summary>
        /// <param name="name">   The name. </param>
        /// <param name="values"> The values. </param>
        /// <returns> . </returns>
        public string Update(string name, IEnumerable<Property> values)
        {
            QuotifyValues quotify = new QuotifyValues();

            string sql = string.Format("UPDATE [{0}] SET ", name);
            string[] nameValuePairs = values.Select(p => string.Format("[{0}] = {1}", p.Name, quotify.Quotify(p.Value))).ToArray();

            sql += string.Join(", ", nameValuePairs);

            return sql;
        }

        /// <summary> Inserts. </summary>
        /// <param name="typeName">   Name of the type. </param>
        /// <param name="nameValues"> The name values. </param>
        /// <returns> . </returns>
        public string Insert(string typeName, IEnumerable<Property> nameValues)
        {
            var quotify = new QuotifyValues();
            nameValues = nameValues.Where(s => ShowColumn<int>(s.PropertyDescriptor.PropertyType, s.Value)).Where(s => ShowColumn<Guid>(s.PropertyDescriptor.PropertyType, s.Value));

            string[] columns = nameValues.Select(p => string.Format("[{0}]", p.Name)).ToArray();
            object[] values = nameValues.Select(p => quotify.Quotify(p.Value)).ToArray();

            string sql = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})", typeName, string.Join(", ", columns), string.Join(", ", values));
            return sql;
        }

        /// <summary> Shows the column. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="type">  The type. </param>
        /// <param name="value"> The value. </param>
        /// <returns> true if it succeeds, false if it fails. </returns>
        public bool ShowColumn<T>(Type type, object value)
        {
            bool showColumn = true;

            if (typeof(T) == type)
            {
                showColumn = Convert.ToString(value) != Convert.ToString(default(T));
            }

            return showColumn;
        }
    }
}
