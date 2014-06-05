using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Hypersonic.Attributes;
using Hypersonic.Core;
using Hypersonic.Session.Persistance;

namespace Hypersonic.Session.Persistence
{
    public interface IPersistence
    {
        /// <summary> Persists the passed in object. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item"> The item. </param>
        /// <returns> . </returns>
        List<Persist> Persist<T>(object item) where T : class, new();

        /// <summary> Persists. </summary>
        /// <param name="item">  The item. </param>
        /// <param name="table"> The table. </param>
        /// <returns> . </returns>
        List<Persist> Persist(object item, string table);

        /// <summary> Persists the passed in object. </summary>
        /// <param name="collection"> The item. </param>
        /// <returns> . </returns>
        List<Persist> Persist(ICollection collection);

        List<Persist> Persist(ICollection collection, string tableName);

        List<Persist> Persist(object item, string tablename, string @where);

        /// <summary> Persists. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The item. </param>
        /// <param name="where"> The where. </param>
        /// <returns> . </returns>
        List<Persist> Persist<T>(object item, Expression<Func<T, bool>> @where) where T : class, new();

        List<Persist> Persist<T>(object item, string tablename, Expression<Func<T, bool>> @where) where T : class, new();

        /// <summary> Persists. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <param name="where">     The where. </param>
        /// <returns> . </returns>
        List<Persist> Persist<T>(ICollection collection, Expression<Func<T, bool>> @where) where T : class, new();
    }

    public class Persistence : IPersistence
    {
        /// <summary> Persists the passed in object. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item"> The item. </param>
        /// <returns> . </returns>
        public List<Persist> Persist<T>(object item) where T : class, new()
        {
            string name = typeof(T).Name;
            string sql = GenerateSql(item, name);
            Persist persist = new Persist(sql, item, name);

            return new List<Persist>{persist};
        }

        /// <summary> Persists. </summary>
        /// <param name="item">  The item. </param>
        /// <param name="table"> The name of the table. </param>
        /// <returns> . </returns>
        public List<Persist> Persist(object item, string table)
        {
            string sql = GenerateSql(item, table);
            Persist persist = new Persist(sql, item, table);

             return new List<Persist> { persist };
        }

        /// <summary> Persists. </summary>
        /// <param name="collection"> The collection. </param>
        /// <returns> . </returns>
        public List<Persist> Persist(ICollection collection)
        {
            return (from object o in collection
                    let name = o.GetType().Name
                    let sql = GenerateSql(o, name)
                    select new Persist(sql, o, name)).ToList();
        }

        /// <summary> Persists. </summary>
        /// <param name="collection"> The collection. </param>
        /// <param name="tableName"> The table Name </param>
        /// <returns> . </returns>
        public List<Persist> Persist(ICollection collection, string tableName)
        {
            return (from object o in collection 
                    let name = tableName 
                    let sql = GenerateSql(o, name) 
                    select new Persist(sql, o, name)).ToList();
        }

        /// <summary> Generates a sql. </summary>
        /// <param name="item"> The item. </param>
        /// <param name="name"> The name. </param>
        /// <returns> The sql. </returns>
        private static string GenerateSql(object item, string name)
        {
            IKeysDefined[] keysDefineds = {new HasPrimaryKeys(), new PrimaryKeysNotDefined()};

            var flattener = new Flattener();
            var primaryKeys = flattener.GetPropertiesWithDefaultValues<PrimaryKeyAttribute>(item).ToList();
            var properties = flattener.GetNamesAndValues(item).ToList();
            string nameTable = name ?? item.GetType().Name;

            bool primaryKeysExists = primaryKeys.Any();
            var first = keysDefineds.First(k => k.PrimaryKeysExist == primaryKeysExists);

            string sql = first.GenerateSql(nameTable, primaryKeys, properties);
            return sql;
        }

        /// <summary> Persists. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The item. </param>
        /// <param name="where"> The where. </param>
        /// <returns> . </returns>
        public List<Persist> Persist<T>(object item, Expression<Func<T, bool>> @where) where T : class, new()
        {
            string name = typeof (T).Name;
            string sql = GenerateUpdate(item, name, @where);

            Persist persist = new Persist(sql, item, name);
            return new List<Persist> { persist };
        }

        /// <summary> Persists. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The item. </param>
        /// <param name="where"> The where. </param>
        /// <returns> . </returns>
        public List<Persist> Persist<T>(object item, string tablename, Expression<Func<T, bool>> @where) where T : class, new()
        {
            string sql = GenerateUpdate(item, tablename, @where);

            var persist = new Persist(sql, item, tablename);
            return new List<Persist> { persist };
        }

        /// <summary> Persists. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The item. </param>
        /// <param name="where"> The where. </param>
        /// <returns> . </returns>
        public List<Persist> Persist(object item, string tablename, string @where)
        {
            string sql = GenerateUpdate(item, tablename, @where);

            var persist = new Persist(sql, item, tablename);
            return new List<Persist> { persist };
        }

        /// <summary> Persists. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <param name="where">     The where. </param>
        /// <returns> . </returns>
        public List<Persist> Persist<T>(ICollection collection, Expression<Func<T, bool>> @where) where T : class, new()
        {
            return (from object o in collection
                    let name = o.GetType().Name
                    let sql = GenerateUpdate(o, name, @where)
                    select new Persist(sql, o, name)).ToList();
        }

        /// <summary> Generates an update. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The item. </param>
        /// <param name="name">
        /// (optional) the name. This overrides the table name. Which is gotten from the class name. </param>
        /// <param name="where"> The where. </param>
        /// <returns> The update&lt; t&gt; </returns>
        private static string GenerateUpdate<T>(object item, string name, Expression<Func<T, bool>> @where)
        {
            SqlGenerator generator = new SqlGenerator();
            ExpressionProcessor processor = new ExpressionProcessor();

            var sql = generator.Update(item, name);
            sql += " WHERE " + processor.Process(@where);

            return sql;
        }

        /// <summary> Generates an update. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The item. </param>
        /// <param name="name">
        /// (optional) the name. This overrides the table name. Which is gotten from the class name. </param>
        /// <param name="where"> The where. </param>
        /// <returns> The update&lt; t&gt; </returns>
        private static string GenerateUpdate(object item, string name, string @where)
        {
            SqlGenerator generator = new SqlGenerator();

            var sql = generator.Update(item, name);
            sql += " WHERE " + @where;

            return sql;
        }
    }
}
