using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Hypersonic.Core;

namespace Hypersonic
{
    /// <summary>
    /// Interface IDatabase
    /// </summary>
    /// <typeparam name="TConnection">The type of the t connection.</typeparam>
    public interface IDatabase<TConnection> where TConnection : DbConnection, new()
    {
        /// <summary>
        /// Begins the session and opens the database connection.
        /// </summary>
        /// <returns>HypersonicDbConnection&lt;SqlConnection&gt;.</returns>
        HypersonicDbConnection<TConnection> BeginSession();

        /// <summary>
        /// Automatics the mapper.
        /// </summary>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>TReturn.</returns>
        TReturn AutoMapper<TReturn>(IHypersonicDbReader reader) where TReturn : class, new();

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
        /// Readers the specified stored procedure.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <returns>HypersonicDbDataReader.</returns>
        HypersonicDbDataReader Reader(string storedProcedureOrRawSql);

        /// <summary>
        /// Populate and returns the NullableReader.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        HypersonicDbDataReader Reader<N>(string storedProcedureOrRawSql, N parameters) where N : class;


        /// <summary>
        /// Readers the specified stored procedure.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>HypersonicDbDataReader.</returns>
        HypersonicDbDataReader Reader(string storedProcedureOrRawSql, List<DbParameter> parameters);

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
        int NonQuery(string storedProcedureOrRawSql, List<DbParameter> parameters);

        /// <summary>
        /// Executes the procedure and passes the parameters to the stored procedure.
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <param name="storedProc">The stored proc.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        int NonQuery<N>(string storedProcedureOrRawSql, N parameters) where N : class;

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