using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Hypersonic.Core;

namespace Hypersonic
{
    public interface IDbContext
    {
        /// <summary>
        /// Begins the session and opens the database connection.
        /// </summary>
        /// <returns>HypersonicDbConnection&lt;SqlConnection&gt;.</returns>
        HypersonicDbConnection<SqlConnection> BeginSession();

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>The database.</value>
        IDatabase<SqlConnection> Database { get; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        HypersonicSettings Settings { get; }

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>T.</returns>
        TReturn Single<TReturn>(string storedProcedureOrRawSql, List<DbParameter> parameters, Func<IHypersonicDbReader, TReturn> mapper);

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>T.</returns>
        TReturn Single<TParameters, TReturn>(string storedProcedureOrRawSql, TParameters parameters, Func<IHypersonicDbReader, TReturn> mapper) where TParameters : class;

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>T.</returns>
        TReturn Single<TReturn>(string storedProcedureOrRawSql, Func<IHypersonicDbReader, TReturn> mapper);

        /// <summary>
        /// Singles the specified stored procedure.
        /// </summary>
        /// <typeparam name="TReturn">The type of the T return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <returns>``0.</returns>
        TReturn Single<TReturn>(string storedProcedureOrRawSql) where TReturn : class, new();

        /// <summary>
        /// Singles the specified stored procedure.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>TReturn.</returns>
        TReturn Single<TParameters, TReturn>(string storedProcedureOrRawSql, TParameters parameters)
            where TParameters : class
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
        /// <param name="mapper">The mapper.</param>
        /// <returns>IList&lt;T&gt;.</returns>
        IList<T> List<T>(string storedProcedureOrRawSql, List<DbParameter> parameters, Func<IHypersonicDbReader, T> mapper);
    }
}
