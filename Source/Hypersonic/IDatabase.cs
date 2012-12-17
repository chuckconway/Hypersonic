using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Hypersonic.Core;

namespace Hypersonic
{
    public interface IDatabase: IParameterBuilder 
    {
        /// <summary>
        /// Populates the specified reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        T AutoPopulate<T>(INullableReader reader) where T : class, new();

        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        /// <value>The type of the command.</value>
        CommandType CommandType { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        string ConnectionStringName { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets the parameter delimiter.
        /// </summary>
        /// <value>The parameter delimiter.</value>
        string ParameterDelimiter { get; }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns></returns>
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <returns></returns>
        T Scalar<T>(string cmdText);

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        T Scalar<T>(string cmdText, IDbTransaction transaction);

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        object Scalar<N>(string cmdText, N parameters) where N : class;

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        object Scalar<N>(string cmdText, N parameters, IDbTransaction transaction) where N : class;

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        T Scalar<T>(string cmdText, List<DbParameter> parameters);

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        T Scalar<T>(string cmdText, List<DbParameter> parameters, IDbTransaction transaction);

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <returns>Result Set from procedure execution</returns>
        DbDataReader Reader(string storedProcedure);

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>Result Set from procedure execution</returns>
        DbDataReader Reader(string storedProcedure, IDbTransaction transaction);

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Result Set from procedure execution</returns>
        DbDataReader Reader<N>(string storedProcedure, N parameters) where N : class;

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>Result Set from procedure execution</returns>
        DbDataReader Reader<N>(string storedProcedure, N parameters, IDbTransaction transaction) where N : class;

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Result Set from procedure execution</returns>
        DbDataReader Reader(string storedProcedure, List<DbParameter> parameters);

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>Result Set from procedure execution</returns>
        DbDataReader Reader(string storedProcedure, List<DbParameter> parameters, IDbTransaction transaction);

        /// <summary>
        /// Nullables the reader.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <returns></returns>
        NullableDataReader NullableReader(string storedProcedure);

        /// <summary>
        /// Nullables the reader.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        NullableDataReader NullableReader(string storedProcedure, IDbTransaction transaction);

        /// <summary>
        /// Populate and returns the NullableReader.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        NullableDataReader NullableReader<N>(string storedProcedure, N parameters) where N : class;

        /// <summary>
        /// Populate and returns the NullableReader.
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        NullableDataReader NullableReader<N>(string storedProcedure, N parameters, IDbTransaction transaction) where N : class;

        /// <summary>
        /// Nullables the reader.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        NullableDataReader NullableReader(string storedProcedure, List<DbParameter> parameters);

        /// <summary>
        /// Nullables the reader.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        NullableDataReader NullableReader(string storedProcedure, List<DbParameter> parameters, IDbTransaction transaction);

        /// <summary>
        /// Executes the procedure
        /// </summary>
        /// <param name="storedProc">name of stored procedure</param>
        /// <returns>rows affected</returns>
        int NonQuery(string storedProc);

        /// <summary>
        /// Executes the procedure
        /// </summary>
        /// <param name="storedProc">name of stored procedure</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>rows affected</returns>
        int NonQuery(string storedProc, IDbTransaction transaction);

        /// <summary>
        /// Nons the query.
        /// </summary>
        /// <param name="storedProc">The stored proc.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        int NonQuery(string storedProc, List<DbParameter> parameters);

        /// <summary>
        /// Executes the procedure and passes the parameters to the stored procedure
        /// </summary>
        /// <param name="storedProc">name of the stored procedure to be executed</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>rows affected</returns>
        int NonQuery(string storedProc, List<DbParameter> parameters, IDbTransaction transaction);

        /// <summary>
        /// Executes the procedure and passes the parameters to the stored procedure.
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <param name="storedProc">The stored proc.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        int NonQuery<N>(string storedProc, N parameters) where N : class;

        /// <summary>
        /// Executes the procedure and passes the parameters to the stored procedure.
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <param name="storedProc">The stored proc.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        int NonQuery<N>(string storedProc, N parameters, IDbTransaction transaction) where N : class;

        /// <summary>
        /// Makes the parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        DbParameter MakeParameter<T>(string parameterName, T value);

        /// <summary>
        /// Makes the parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <param name="parameterDirection">The parameter direction.</param>
        /// <returns></returns>
        DbParameter MakeParameter<T>(string parameterName, T value, ParameterDirection parameterDirection);

        /// <summary>
        /// Makes the parameter
        /// </summary>
        /// <param name="parameterName">name of parameter in Stored Procedure</param>
        /// <param name="dbType">Type of SqlDbType (column type)</param>
        /// <param name="size">size of the SQL column</param>
        /// <param name="parmValue">value of the parameter</param>
        /// <returns>Returns a fully populated parameter</returns>
        DbParameter MakeParameter(string parameterName, DbType dbType, int size, object parmValue);

        /// <summary>
        /// Makes the parameter
        /// </summary>
        /// <param name="parameterName">name of parameter in Stored Procedure</param>
        /// <param name="dbType">Type of SqlDbType (column type)</param>
        /// <param name="size">size of the SQL column</param>
        /// <param name="parmValue">value of the parameter</param>
        /// <param name="direction">What direction the parameter is...</param>
        /// <returns>Returns a fully populated parameter</returns>
        DbParameter MakeParameter(string parameterName, DbType dbType, int size, object parmValue, ParameterDirection direction);

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        IList<T> List<T>(string storedProcedure, List<DbParameter> parameters, Func<INullableReader, T> getItem, IDbTransaction transaction);

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns></returns>
        
        IList<T> List<T>(string storedProcedure, Func<INullableReader, T> getItem);

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="getItem">The get item.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        IList<T> List<T>(string storedProcedure, Func<INullableReader, T> getItem, IDbTransaction transaction);

        IList<TReturn> List<TReturn>(string storedProcedure) where TReturn : class, new();

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="N"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns></returns>
        IList<T> List<T, N>(string storedProcedure, N parameters, Func<INullableReader, T> getItem) where N : class;

        IList<TReturn> List<TReturn, TN>(string storedProcedure, TN parameters)
            where TN : class
            where TReturn : class, new();

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="N"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        IList<T> List<T, N>(string storedProcedure, N parameters, Func<INullableReader, T> getItem, IDbTransaction transaction) where N : class;

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns></returns>
        IList<T> List<T>(string storedProcedure, List<DbParameter> parameters, Func<INullableReader, T> getItem);
        
        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns></returns>
        T Single<T>(string storedProcedure, List<DbParameter> parameters, Func<INullableReader, T> getItem);

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        T Single<T>(string storedProcedure, List<DbParameter> parameters, Func<INullableReader, T> getItem, IDbTransaction transaction);

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="N"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        T Single<T, N>(string storedProcedure, N parameters, Func<INullableReader, T> getItem, IDbTransaction transaction) where N : class;

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="N"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns></returns>
        T Single<T, N>(string storedProcedure, N parameters, Func<INullableReader, T> getItem) where N : class;

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="getItem">The get item.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        T Single<T>(string storedProcedure, Func<INullableReader, T> getItem, IDbTransaction transaction);

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns></returns>
        T Single<T>(string storedProcedure, Func<INullableReader, T> getItem);

        /// <summary>
        /// Singles the specified stored procedure.
        /// </summary>
        /// <typeparam name="TReturn">The type of the T return.</typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <returns>``0.</returns>
        TReturn Single<TReturn>(string storedProcedure) where TReturn : class, new();

        TReturn Single<TReturn, TN>(string storedProcedure, TN parameters)
            where TN : class
            where TReturn : class, new();

        /// <summary> Converts an entity to the parameters. </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> The given data converted to the parameters. </returns>
        List<DbParameter> ConvertToParameters(object entity);

    }
}