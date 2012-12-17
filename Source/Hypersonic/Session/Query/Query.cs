using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Hypersonic.Core;
using Hypersonic.Session.Query.Filters;
using Hypersonic.Session.Query.Restrictions;

namespace Hypersonic.Session.Query
{
    public class Query<T> : IQuery<T> where T : class, new()
    {
        private readonly string _tableName;
        private readonly string[] _columns;
        private readonly IDatabase _database;
        private readonly IQueryWriter _queryWriter;
        private readonly ITransaction _transaction;

        /// <summary> Constructor. </summary>
        /// <param name="tableName">   Name of the table. </param>
        /// <param name="columns">     The columns. </param>
        /// <param name="database">    The database. </param>
        /// <param name="queryWriter"> The query writer. </param>
        /// <param name="transaction"> The transaction. </param>
        public Query(string tableName, string[] columns, IDatabase database, IQueryWriter queryWriter, ITransaction transaction)
        {
            _tableName = tableName;
            _columns = columns;
            _database = database;
            _queryWriter = queryWriter;
            _transaction = transaction;

            //Clear out older queries if Hypersonic is used as a Singleton.
            _queryWriter.Dispose();
        }

        /// <summary> Constructor. </summary>
        /// <param name="tableName">   Name of the table. </param>
        /// <param name="columns">     The columns. </param>
        /// <param name="database">    The database. </param>
        /// <param name="queryWriter"> The query writer. </param>
        public Query(string tableName, string[] columns, IDatabase database, IQueryWriter queryWriter) : this(tableName, columns, database, queryWriter, null) { }

        /// <summary>   Define the where for this query. This method can be called once during the lifetime of this query. </summary>
        ///
        /// <param name="where">   The expression. </param>
        ///
        /// <returns>   . </returns>
        public IQuery<T> Where(Expression<Func<T, bool>> @where)
        {
            CodeContract.Assert(!_queryWriter.ContainsFilterTypeOfWhere(), Constants.WhereAlreadyCalledErrorMessage);

            string query = RenderExpression(@where);
            _queryWriter.AddFilter(new WhereFilter(query));
            return this;
        }

        /// <summary> Likes. </summary>
        /// <param name="like"> The like. </param>
        /// <returns> . </returns>
        public IQuery<T> Like(Expression<Func<T, bool>> like)
        {
            string query = RenderExpression(like);
            _queryWriter.AddFilter(new LikeFilter(query));
            return this;
        }

        /// <summary> Likes. </summary>
        /// <param name="like"> The like. </param>
        /// <returns> . </returns>
        public IQuery<T> Like(string like)
        {
            _queryWriter.AddFilter(new LikeFilter(like));
            return this;
        }

        /// <summary> Renders the expression described by expression. </summary>
        /// <param name="expression"> The expression. </param>
        /// <returns> . </returns>
        private static string RenderExpression(Expression<Func<T, bool>> expression)
        {
            ExpressionProcessor processor = new ExpressionProcessor();
            var query = processor.Process(expression);

            return query;
        }

        /// <summary>   Define the where for this query. This method can be called once during the lifetime of this query. </summary>
        ///
        /// <param name="where">   The where. </param>
        ///
        /// <returns>   . </returns>
        public IQuery<T> Where(string @where)
        {
            CodeContract.Assert(!_queryWriter.ContainsFilterTypeOfWhere(), Constants.WhereAlreadyCalledErrorMessage);
            _queryWriter.AddFilter(new WhereFilter(@where));
            return this;
        }

        /// <summary>   Includes an 'AND' condition for this query. </summary>
        ///
        /// <param name="and">  The and expression. </param>
        ///
        /// <returns>   . </returns>
        public IQuery<T> And(Expression<Func<T, bool>> and)
        {
            CodeContract.Assert(_queryWriter.ContainsFilterTypeOfWhere(), Constants.WhereMustBeCalledBeforeUsingAnd);

            string query = RenderExpression(and);
            _queryWriter.AddFilter(new AndFilter(query));
            return this;
        }

        /// <summary>   Includes an 'AND' condition for this query. </summary>
        ///
        /// <param name="and">  The and expression. </param>
        ///
        /// <returns>   . </returns>
        public IQuery<T> And(string and)
        {
            CodeContract.Assert(_queryWriter.ContainsFilterTypeOfWhere(), Constants.WhereMustBeCalledBeforeUsingAnd);
            _queryWriter.AddFilter(new AndFilter(and));
            return this;
        }

        /// <summary>   Includes an 'OR' condition for this query.  </summary>
        ///
        /// <param name="or">   The or. </param>
        ///
        /// <returns>   . </returns>
        public IQuery<T> Or(Expression<Func<T, bool>> or)
        {
            CodeContract.Assert(_queryWriter.ContainsFilterTypeOfWhere(), Constants.WhereMustBeCalledBeforeUsingOr);
            string query = RenderExpression(or);
            _queryWriter.AddFilter(new OrFilter(query));
            return this;
        }

        /// <summary> Includes an 'OR' condition for this query. </summary>
        /// <param name="restrictions">
        /// A variable-length parameters list containing restrictions. </param>
        /// <returns> . </returns>
        public IQuery<T> Or(params IRestriction[] restrictions)
        {
            return this;
        }
        
        /// <summary>   Includes an 'OR' condition for this query.  </summary>
        ///
        /// <param name="or">   The or. </param>
        ///
        /// <returns>IOrderBy of type T</returns>
        public IQuery<T> Or(string or)
        {
            CodeContract.Assert(_queryWriter.ContainsFilterTypeOfWhere(), Constants.WhereMustBeCalledBeforeUsingOr);
            _queryWriter.AddFilter(new OrFilter(or));
            return this;
        }

        /// <summary>   Includes an orderby clause to the current query. This can be called as many times as 
        /// 			needed. Orderbys are defined in the order this method is called.</summary>
        ///
        /// <param name="field">    The field. </param>
        ///
        /// <returns>IOrderBy of type T</returns>
        public IOrderBy<T> OrderBy(Expression<Func<T, object>> field)
        {
            LambdaExpression expression = field;
            MemberExpression memberExpression = expression.Body.NodeType == ExpressionType.Convert
                                                    ? ((UnaryExpression) expression.Body).Operand as MemberExpression
                                                    : expression.Body as MemberExpression;

            IOrderBy<T> orderBy = new OrderBy<T>(memberExpression.Member.Name, this, _queryWriter);
            return orderBy;
        }

        public T Single()
        {
            return List().FirstOrDefault();
        }

        /// <summary>   Gets the list. </summary>
        ///
        /// <returns>List of type T</returns>
        public IList<T> List()
        {
            string sql = _queryWriter.Query(_columns, _tableName);
            CommandType orginalCommandType = _database.CommandType;

            _database.CommandType = CommandType.Text;
            IList<T> collection = _database.List(sql, _database.AutoPopulate<T>, _transaction);
            _database.CommandType = orginalCommandType;

            return collection;
        }
    }
}