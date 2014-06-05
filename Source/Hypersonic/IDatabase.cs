using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Hypersonic.Core;

namespace Hypersonic
{
    public interface IDatabase
    {

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        HypersonicSettings Settings { get; set; }

        /// <summary>
        /// Gets the parameter delimiter.
        /// </summary>
        /// <value>The parameter delimiter.</value>
        string ParameterDelimiter { get; }

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
        /// <typeparam name="N"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        object Scalar<N>(string cmdText, N parameters) where N : class;

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        T Scalar<T>(string cmdText, List<DbParameter> parameters);

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
        /// <param name="parameters">The parameters.</param>
        /// <returns>Result Set from procedure execution</returns>
        DbDataReader Reader<N>(string storedProcedure, N parameters) where N : class;

        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Result Set from procedure execution</returns>
        DbDataReader Reader(string storedProcedure, List<DbParameter> parameters);

        /// <summary>
        /// Nullables the reader.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <returns></returns>
        HypersonicDbDataReader NullableReader(string storedProcedure);


        /// <summary>
        /// Populate and returns the NullableReader.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        HypersonicDbDataReader NullableReader<N>(string storedProcedure, N parameters) where N : class;

        /// <summary>
        /// Nullables the reader.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        HypersonicDbDataReader NullableReader(string storedProcedure, List<DbParameter> parameters);

        /// <summary>
        /// Executes the procedure
        /// </summary>
        /// <param name="storedProc">name of stored procedure</param>
        /// <returns>rows affected</returns>
        int NonQuery(string storedProc);

        /// <summary>
        /// Nons the query.
        /// </summary>
        /// <param name="storedProc">The stored proc.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        int NonQuery(string storedProc, List<DbParameter> parameters);

        /// <summary>
        /// Executes the procedure and passes the parameters to the stored procedure.
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <param name="storedProc">The stored proc.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        int NonQuery<N>(string storedProc, N parameters) where N : class;

        /// <summary>
        /// Makes the parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        DbParameter MakeParameter<T>(string parameterName, T value);

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
        

        /// <summary> Converts an entity to the parameters. </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> The given data converted to the parameters. </returns>
        List<DbParameter> ConvertToParameters(object entity);

    }
}