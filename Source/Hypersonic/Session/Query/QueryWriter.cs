using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hypersonic.Core;
using Hypersonic.Session.Query.Filters;

namespace Hypersonic.Session.Query
{
    public class QueryWriter : IQueryWriter
    {
        List<IFilter> _queries = new List<IFilter>();
        List<Order> _orderBys = new List<Order>();  

        /// <summary> Adds a filter. </summary>
        /// <exception cref="NotImplementedException">
        /// Thrown when the requested operation is unimplemented. </exception>
        /// <param name="filter"> A filter specifying the. </param>
        public void AddFilter(IFilter filter)
        {
            _queries.Add(filter);
        }

        /// <summary> Adds an order by. </summary>
        /// <param name="column">    The column. </param>
        /// <param name="direction"> The direction. </param>
        public void AddOrder(string column, string direction)
        {
            _orderBys.Add(new Order(column, direction));
        }

        /// <summary> Query if this object contains filter type of where. </summary>
        /// <returns> true if it succeeds, false if it fails. </returns>
        public bool ContainsFilterTypeOfWhere()
        {
            return _queries.Any(k=> k.GetType() == typeof(WhereFilter));
        }

        /// <summary> Gets the where. </summary>
        /// <returns> . </returns>
        private string Where()
        {
            return RenderFilter() + RenderOrderBy();
        }

        /// <summary> Renders the order by. </summary>
        /// <returns> . </returns>
        private string RenderOrderBy()
        {
            StringBuilder builder = new StringBuilder();

            if(_orderBys.Count > 0)
            {
                builder.Append(Environment.NewLine);
                builder.Append("Order By ");

                var order = _orderBys.Select(o => o.Column + " " + o.Direction).ToArray();

                builder.Append(string.Join(", ", order));
            }

            return builder.ToString();
        }

        /// <summary> Renders the filter. </summary>
        /// <returns> . </returns>
        private string RenderFilter()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var query in _queries)
            {
                string name = query.GetType().Name.Replace("Filter", string.Empty);
                string q = query.Query();
                string val = string.Format("{0} {1} ", name, q);
                builder.Append(val);
            }

            return builder.ToString();
        }

        /// <summary> Gets the columns. </summary>
        /// <param name="instance"> The instance. </param>
        /// <returns> The columns. </returns>
        public string[] GetColumns(object instance)
        {
            Flattener flattener = new Flattener();
            return flattener.GetColumnNames(instance).ToArray();
        }
        
        /// <summary> Gets the select. </summary>
        /// <returns> . </returns>
        public string Query(object instance, string name)
        {
            Flattener flattener = new Flattener();
            string[] columns = flattener.GetColumnNames(instance).ToArray();
            
            return Query(columns, name);
        }

        /// <summary> Gets the select. </summary>
        /// <returns> . </returns>
        public string Query(string[] columns, string name)
        {
            columns = columns.Select(s => string.Format("[{0}]", s)).ToArray();

            string columnList = String.Join(", ", columns);
            string filter = Where();
            string sql = String.Format("SELECT {0} FROM [{1}] {2}", columnList, name, filter);

            return sql;
        }

        /// <summary> Gets the query. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <returns> . </returns>
        public string Query<T>() where T: class, new()
        {
            T instance = new T();
            return Query(instance, instance.GetType().Name);
        }

        /// <summary> Gets the query. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <returns> . </returns>
        public string Query<T>(string[] columns) where T : class, new()
        {
            T instance = new T();
            return Query(columns, instance.GetType().Name);
        }

        /// <summary> Gets the query. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <returns> . </returns>
        public string Query<T>(object columns) where T : class, new()
        {
            T instance = new T();
            return Query(columns, instance.GetType().Name);
        }
        
        private class Order
        {
            public Order(string column, string direction)
            {
                Column = column;
                Direction = direction;
            }

            /// <summary> Gets or sets the column. </summary>
            /// <value> The column. </value>
            public string Column { get; private set; }

            /// <summary> Gets or sets the direction. </summary>
            /// <value> The direction. </value>
            public string Direction { get; private set; }
        }

        /// <summary> Dispose of this object, cleaning up any resources it uses. </summary>
        public void Dispose()
        {
            _queries = new List<IFilter>();
            _orderBys = new List<Order>();
        }
    }
}
