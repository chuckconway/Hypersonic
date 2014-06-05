using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using Hypersonic.Session.Persistance;
using Hypersonic.Session.Persistence;
using Hypersonic.Session.Query;

namespace Hypersonic.Session
{
    public class SqlServerSession: ISession
    {
        private readonly IQueryWriter _queryWriter;
        private readonly IPersistence _persistence;
        private ITransaction _transaction;

        public IDatabase Database { get; private set; }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="database"> The database. </param>
        public SqlServerSession(IDatabase database) :  this(database, new QueryWriter(), new Persistence.Persistence()){}

        /// <summary> Constructor. </summary>
        /// <param name="database">    The database. </param>
        /// <param name="queryWriter"> The query writer. </param>
        /// <param name="persistence"> The persistence. </param>
        public SqlServerSession(IDatabase database, IQueryWriter queryWriter, IPersistence persistence)
        {
            Database = database;
            _queryWriter = queryWriter;
            _persistence = persistence;
        }

        /// <summary> Begins a transaction. </summary>
        /// <param name="isolationLevel"> The isolation level. </param>
        /// <returns> . </returns>
        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            _transaction = new Transaction(Database);
            _transaction.Begin(isolationLevel);
            return _transaction;
        }

        /// <summary>   Begins a transaction. </summary>
        /// <returns>instance of ITransaction</returns>
        public ITransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.Unspecified);
        }

        /// <summary> Gets the type of T for expression. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="where"> The where. </param>
        /// <returns> instance of type of T. </returns>
        public T Get<T>(Expression<Func<T, bool>> @where) where T : class, new()
        {
            var instance = new T();
            var columns = _queryWriter.GetColumns(instance);

            var query = new Query<T>(instance.GetType().Name, columns, Database, _queryWriter, _transaction);
            return query.Where(@where).Single();
        }

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The name. </param>
        /// <param name="where"> The where. </param>
        /// <returns> instance of type of T. </returns>
        public T Save<T>(T item, Expression<Func<T, bool>> @where) where T : class, new()
        {
            var persist = _persistence.Persist(item, @where);
            string sql = Intercept(persist);
            NonQuery(sql);

            return item;
        }

        private string Intercept(IEnumerable<Persist> persists)
        {
            List<Persist> newPersists = persists.Select(p => PersistIntercept(p, this)).ToList();

            var builder = new StringBuilder();
            newPersists.ForEach(p => builder.AppendLine(p.Sql));

            return builder.ToString();
        }

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="collection"> The ICollection to save. </param>
        /// <param name="where">     The where. </param>
        public void Save<T>(ICollection collection, Expression<Func<T, bool>> @where) where T : class, new()
        {
            var persist = _persistence.Persist(collection, @where);

            string sql = Intercept(persist);
            NonQuery(sql);
        }

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item"> The where. </param>
        /// <returns> instance of type of T. </returns>
        public T SaveAnonymous<T>(object item) where T : class, new()
        {
            var persist = _persistence.Persist<T>(item);
            string sql = Intercept(persist);
            NonQuery(sql);

            return item as T;
        }

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The name. </param>
        /// <param name="where"> The where. </param>
        /// <returns> instance of type of T. </returns>
        public T SaveAnonymous<T>(object item, Expression<Func<T, bool>> @where) where T : class, new()
        {
            var persist = _persistence.Persist(item, @where);

            string sql = Intercept(persist);
            NonQuery(sql);

            return item as T;
        }

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The name. </param>
        /// <param name="where"> The where. </param>
        /// <returns> instance of type of T. </returns>
        public T SaveAnonymous<T>(object item, string tableName, Expression<Func<T, bool>> @where) where T : class, new()
        {
            var persist = _persistence.Persist(item, tableName, @where);

            string sql = Intercept(persist);
            NonQuery(sql);

            return item as T;
        }

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The name. </param>
        /// <param name="where"> The where. </param>
        /// <returns> instance of type of T. </returns>
        public void SaveAnonymous(object item, string tableName, string @where)
        {
            var persist = _persistence.Persist(item, tableName, @where);
            string sql = Intercept(persist);
            NonQuery(sql);
        }

        /// <summary> Saves. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item"> The where. </param>
        /// <returns> . </returns>
        public T Save<T>(T item) where T : class, new()
        {
            var persist = _persistence.Persist<T>(item);

            string sql = Intercept(persist);
            NonQuery(sql);

            return item;
        }

        /// <summary> Saves the item. </summary>
        /// <exception cref="NotImplementedException">
        /// Thrown when the requested operation is unimplemented. </exception>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">      The where. </param>
        /// <param name="tableName"> The name. </param>
        /// <returns> instance of type of T. </returns>
        public T Save<T>(T item, string tableName)
        {
            var persist = _persistence.Persist(item, tableName);

            string sql = Intercept(persist);
            NonQuery(sql);

            return item;
        }

        /// <summary> Saves. </summary>
        /// <param name="collection"> The ICollection to save. </param>
        public void Save(ICollection collection)
        {
            var persist = _persistence.Persist(collection);

            string sql = Intercept(persist);
            NonQuery(sql);
        }

        /// <summary> Saves. </summary>
        /// <param name="collection"> The ICollection to save. </param>
        /// <param name="tableName">The table name </param>
        public void Save(ICollection collection, string tableName)
        {
            var persist = _persistence.Persist(collection, tableName);

            string sql = Intercept(persist);
            NonQuery(sql);
        }


        /// <summary> Non query. </summary>
        /// <param name="sql"> The sql. </param>
        /// <returns> . </returns>
        private int NonQuery(string sql)
        {
            CommandType orginalCommandType = Database.CommandType;

            Database.CommandType = CommandType.Text;
            int returnVal = Database.NonQuery(sql);
            Database.CommandType = orginalCommandType;

            return returnVal;
        }

        /// <summary> Creates a new query of type T. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <returns> . </returns>
        public IQuery<T> Query<T>() where T : class, new()
        {
            T instance = new T();
            string[] columns = _queryWriter.GetColumns(instance);
            return new Query<T>(instance.GetType().Name, columns, Database, _queryWriter, _transaction);
        }

        /// <summary> Gets the query. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <returns> . </returns>
        public IQuery<T> Query<T>(string[] columns) where T : class, new()
        {
            T instance = new T();
            return new Query<T>(instance.GetType().Name, columns, Database, _queryWriter, _transaction);
        }

        /// <summary> Gets the query. </summary>
        /// <typeparam name="TReturn"> Type of the return. </typeparam>
        /// <typeparam name="TTable">  Type of the table. </typeparam>
        /// <returns> . </returns>
        public IQuery<TReturn> Query<TReturn, TTable>() where TReturn : class, new() where TTable : new()
        {
            TTable instance = new TTable();
            TReturn outInstance = new TReturn();
            return new Query<TReturn>(instance.GetType().Name, _queryWriter.GetColumns(outInstance), Database, _queryWriter, _transaction);
        }

        /// <summary> Gets the query. </summary>
        /// <typeparam name="TReturn"> Type of the return. </typeparam>
        /// <returns> . </returns>
        public IQuery<TReturn> Query<TReturn>(string tableName) where TReturn : class, new()
        {
            TReturn outInstance = new TReturn();
            return new Query<TReturn>(tableName, _queryWriter.GetColumns(outInstance), Database, _queryWriter, _transaction);
        }

        /// <summary> Gets the select. </summary>
        /// <returns> . </returns>
        public IQuery<T> Query<T>(object instance, string name) where T : class, new()
        {
            string[] columns = _queryWriter.GetColumns(instance);
            return new Query<T>(instance.GetType().Name, columns, Database, _queryWriter, _transaction);
        }

        /// <summary> Gets the select. </summary>
        /// <returns> . </returns>
        public IQuery<T> Query<T>(string name, params string[] columns)  where T : class, new()
        {
            return new Query<T>(name, columns, Database, _queryWriter, _transaction);
        }

        /// <summary> Gets the query. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <returns> . </returns>
        public IQuery<T> Query<T>(object columns) where T : class, new()
        {
            T instance = new T();
            string[] cols = _queryWriter.GetColumns(columns);
            return new Query<T>(instance.GetType().Name, cols, Database, _queryWriter, _transaction);
        }

        /// <summary>   Dispose of this object, cleaning up any resources it uses. </summary>
        public void Dispose()
        {
            if(_transaction !=null)
            {
              _transaction.Dispose();

              if (_transaction.Connection != null && _transaction.Connection.State == ConnectionState.Open)
                {
                    _transaction.Connection.Close();
                    _transaction.Connection.Dispose();
                }
            }

            _queryWriter.Dispose();
        }
    }
}
