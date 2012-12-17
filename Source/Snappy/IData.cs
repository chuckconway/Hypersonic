using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snappy
{
    public interface IData
    {
        /// <summary>
        /// Saves the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        void Save<T>(T item);

        /// <summary>
        /// Saves the specified item, with a filter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <param name="filter">The filter.</param>
        void Save<T>(T item, Expression<Action<T>> filter);

        /// <summary>
        /// Deletes item with a filter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">The filter.</param>
        void Delete<T>(Expression<Action<T>> filter);

        /// <summary>
        /// Selects the specified filter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        List<T> Select<T>(Expression<Action<T>> filter);

        /// <summary>
        /// Selects this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> Select<T>();

        /// <summary>
        /// Selects the specified string filter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        List<T> Select<T>(string filter);

        /// <summary>
        /// Queries this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> Query<T>();
    }
}
