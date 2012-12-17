using System;
using System.Collections;
using System.Data;
using System.Linq.Expressions;

namespace Hypersonic
{
    public interface ISession : IDisposable
    {
        IDatabase Database { get; }

        /// <summary> Begins a transaction. </summary>
        /// <param name="isolationLevel"> The isolation level. </param>
        /// <returns> instance of ITransaction. </returns>
        ITransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary> Begins a transaction. </summary>
        /// <returns> instance of ITransaction. </returns>
        ITransaction BeginTransaction();

        /// <summary> Gets. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="where"> The where. </param>
        /// <returns> instance of type of T. </returns>
        T Get<T>(Expression<Func<T, bool>> @where) where T : class, new();

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The name. </param>
        /// <param name="where"> The where. </param>
        /// <returns> instance of type of T. </returns>
        T Save<T>(T item, Expression<Func<T, bool>> @where) where T : class, new();

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="collection"> The ICollection to save. </param>
        /// <param name="where">     The where. </param>
        void Save<T>(ICollection collection, Expression<Func<T, bool>> @where) where T : class, new();

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item"> The where. </param>
        /// <returns> instance of type of T. </returns>
        T SaveAnonymous<T>(object item) where T : class, new();

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The name. </param>
        /// <param name="where"> The where. </param>
        /// <returns> instance of type of T. </returns>
        T SaveAnonymous<T>(object item, Expression<Func<T, bool>> @where) where T : class, new();

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">   The name. </param>
        /// <param name="where"> The where. </param>
        /// <returns> instance of type of T. </returns>
        T SaveAnonymous<T>(object item, string tableName, Expression<Func<T, bool>> @where) where T : class, new();

        void SaveAnonymous(object item, string tableName, string @where);


        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item"> The where. </param>
        /// <returns> instance of type of T. </returns>
        T Save<T>(T item) where T : class, new();

        /// <summary> Saves the item. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="item">      The where. </param>
        /// <param name="tableName"> The name. </param>
        /// <returns> instance of type of T. </returns>
        T Save<T>(T item, string tableName);

        /// <summary> Saves. </summary>
        /// <param name="collection"> The ICollection to save. </param>
        void Save(ICollection collection);

        void Save(ICollection collection, string tableName);

        /// <summary> Gets the query. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <returns> instance of IQuery of type T. </returns>
        IQuery<T> Query<T>() where T : class, new();

        /// <summary> Gets the query. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <returns> . </returns>
        IQuery<T> Query<T>(string[] columns) where T : class, new();

        /// <summary> Gets the query. </summary>
        /// <typeparam name="TReturn"> Type of the return. </typeparam>
        /// <typeparam name="TTable">  Type of the table. </typeparam>
        /// <returns> . </returns>
        IQuery<TReturn> Query<TReturn, TTable>()
            where TReturn : class, new()
            where TTable : new();
        
        /// <summary> Gets the select. </summary>
        /// <returns> . </returns>
        IQuery<T> Query<T>(object instance, string name) where T : class, new();
        
        /// <summary> Gets the select. </summary>
        /// <returns> . </returns>
        IQuery<T> Query<T>(string name, string[] columns) where T : class, new();
        
        /// <summary> Gets the query. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <returns> . </returns>
        IQuery<T> Query<T>(object columns) where T : class, new();

        IQuery<TReturn> Query<TReturn>(string tableName) where TReturn : class, new();

    }
}