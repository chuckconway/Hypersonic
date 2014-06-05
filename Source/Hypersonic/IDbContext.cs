using System;
using System.Collections.Generic;
using System.Data.Common;
using Hypersonic.Core;

namespace Hypersonic
{
    public interface IDbContext
    {
        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns></returns>
        T Single<T>(string storedProcedure, List<DbParameter> parameters, Func<IHypersonicDbReader, T> getItem);

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="N"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns></returns>
        T Single<T, N>(string storedProcedure, N parameters, Func<IHypersonicDbReader, T> getItem) where N : class;

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns></returns>
        T Single<T>(string storedProcedure, Func<IHypersonicDbReader, T> getItem);

        /// <summary>
        /// Singles the specified stored procedure.
        /// </summary>
        /// <typeparam name="TReturn">The type of the T return.</typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <returns>``0.</returns>
        TReturn Single<TReturn>(string storedProcedure) where TReturn : class, new();

        /// <summary>
        /// Singles the specified stored procedure.
        /// </summary>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <typeparam name="TN">The type of the tn.</typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>TReturn.</returns>
        TReturn Single<TReturn, TN>(string storedProcedure, TN parameters)
            where TN : class
            where TReturn : class, new();

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>IList&lt;T&gt;.</returns>

        IList<TReturn> List<TReturn>(string storedProcedureOrRawSql, Func<IHypersonicDbReader, TReturn> mapper);


        /// <summary>
        /// Lists the specified stored procedure.
        /// </summary>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure.</param>
        /// <returns>IList&lt;TReturn&gt;.</returns>
        IList<TReturn> List<TReturn>(string storedProcedureOrRawSql) where TReturn : class, new();

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>IList&lt;TReturn&gt;.</returns>
        IList<TReturn> List<TParameters, TReturn>(string storedProcedureOrRawSql, TParameters parameters, Func<IHypersonicDbReader, TReturn> mapper) where TParameters : class;

        /// <summary>
        /// Lists the specified stored procedure.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IList&lt;TReturn&gt;.</returns>
        IList<TReturn> List<TParameters, TReturn>(string storedProcedureOrRawSql, TParameters parameters)
            where TParameters : class
            where TReturn : class, new();


        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns></returns>
        IList<T> List<T>(string storedProcedureOrRawSql, List<DbParameter> parameters, Func<IHypersonicDbReader, T> mapper);
    }
}
