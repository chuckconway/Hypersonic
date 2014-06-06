using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Hypersonic.Core
{
    public class DatabaseBase<TConnection, TCommand, TParameter> : IDatabase<TConnection>
        where TConnection : DbConnection, new()
        where TCommand : DbCommand, new()
        where TParameter : DbParameter, new()
    {
        private HypersonicDbConnection<TConnection> _connection;


        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseBase&lt;TConnection, TCommand, TParameter&gt;" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected DatabaseBase(HypersonicSettings settings)
        {
            Settings = settings;
            _connection = new HypersonicDbConnection<TConnection>(Settings);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseBase&lt;TConnection, TCommand, TParameter&gt;"/> class.
        /// </summary>
        protected DatabaseBase()
        {
            Settings = new HypersonicSettings();
            _connection = new HypersonicDbConnection<TConnection>(Settings);
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public HypersonicSettings Settings { get; set; }

        /// <summary>
        /// Begins the session and opens the database connection.
        /// </summary>
        /// <returns>HypersonicDbConnection&lt;SqlConnection&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public HypersonicDbConnection<TConnection> BeginSession()
        {
            _connection = new HypersonicDbConnection<TConnection>(Settings) { IsManual = false };
            return _connection;
        }

        /// <summary>
        /// Populates the specified reader.
        /// </summary>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>TReturn.</returns>
        public TReturn AutoMapper<TReturn>(IHypersonicDbReader reader) where TReturn : class, new()
        {
            var discoveryService = new ObjectBuilder(Settings);
            var instance = discoveryService.HydrateType<TReturn>(reader);

            return instance;
        }

        /// <summary>
        /// Gets the parameter delimiter.
        /// </summary>
        /// <value>The parameter delimiter.</value>
        public virtual string ParameterDelimiter
        {
            get { return "@"; }
        }

        /// <summary>
        /// Gets the db service.
        /// </summary>
        /// <returns></returns>
        internal DbService<TConnection, TCommand> GetDbService()
        {
            return new DbService<TConnection, TCommand>(_connection, Settings);
        }

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <returns></returns>
        public T Scalar<T>(string cmdText)
        {
            return Scalar<T>(cmdText, new List<DbParameter>());
        }

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="TN"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public object Scalar<TN>(string cmdText, TN parameters) where TN : class
        {
            var dbParameters = GetValuesFromType(parameters);
            return Scalar<object>(cmdText, dbParameters);
        }

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T Scalar<T>(string cmdText, List<DbParameter> parameters)
        {
            var dbService = GetDbService();
            return dbService.Scalar<T>(cmdText, parameters);
        }


        /// <summary>
        /// Readers the specified stored procedure.
        /// </summary>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <returns>HypersonicDbDataReader.</returns>
        public HypersonicDbDataReader Reader(string storedProcedureOrRawSql)
        {
            var dbService = GetDbService();
            var hypersonicDbDataReader = dbService.HypersonicReader(storedProcedureOrRawSql, new List<DbParameter>());

            return hypersonicDbDataReader;
        }


        /// <summary>
        /// Populate and returns the NullableReader.
        /// </summary>
        /// <typeparam name="TN">The type of the tn.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>HypersonicDbDataReader.</returns>
        public HypersonicDbDataReader Reader<TN>(string storedProcedureOrRawSql, TN parameters) where TN : class
        {
            List<DbParameter> dbParameters = GetValuesFromType(parameters);
            DbService<TConnection, TCommand> dbService = GetDbService();

            HypersonicDbDataReader hypersonicDbDataReader = dbService.HypersonicReader(storedProcedureOrRawSql, dbParameters);
            return hypersonicDbDataReader;
        }


        /// <summary>
        /// Nullables the reader.
        /// </summary>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>HypersonicDbDataReader.</returns>
        public HypersonicDbDataReader Reader(string storedProcedureOrRawSql, List<DbParameter> parameters)
        {
            DbService<TConnection, TCommand> dbService = GetDbService();

            HypersonicDbDataReader hypersonicDbDataReader = dbService.HypersonicReader(storedProcedureOrRawSql, parameters);
            return hypersonicDbDataReader;
        }

        /// <summary>
        /// Executes the procedure
        /// </summary>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <returns>rows affected</returns>
        public int NonQuery(string storedProcedureOrRawSql)
        {
            return NonQuery(storedProcedureOrRawSql, new List<DbParameter>());
        }

        /// <summary>
        /// Nons the query.
        /// </summary>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>System.Int32.</returns>
        public int NonQuery(string storedProcedureOrRawSql, List<DbParameter> parameters)
        {
            DbService<TConnection, TCommand> dbService = GetDbService();
            return dbService.NonQuery(storedProcedureOrRawSql, parameters);
        }

        /// <summary>
        /// Executes the procedure and passes the parameters to the stored procedure.
        /// </summary>
        /// <typeparam name="TN">The type of the tn.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>System.Int32.</returns>
        public int NonQuery<TN>(string storedProcedureOrRawSql, TN parameters) where TN : class
        {
            List<DbParameter> dbParameters = GetValuesFromType(parameters);
            return NonQuery(storedProcedureOrRawSql, dbParameters);
        }

        /// <summary>
        /// Makes the parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public DbParameter MakeParameter<T>(string parameterName, T value)
        {
            DbParameter parameter = new TParameter {ParameterName = parameterName, Value = value};
            return parameter;
        }
        
        /// <summary>
        /// Makes the parameter
        /// </summary>
        /// <param name="parameterName">name of parameter in Stored Procedure</param>
        /// <param name="dbType">Type of SqlDbType (column type)</param>
        /// <param name="size">size of the SQL column</param>
        /// <param name="parmValue">value of the parameter</param>
        /// <returns>Returns a fully populated parameter</returns>
        public DbParameter MakeParameter(string parameterName, DbType dbType, int size, object parmValue)
        {
            DbParameter parameter = MakeParameter(parameterName, dbType, size, parmValue, ParameterDirection.Input);
            return parameter;
        }

        /// <summary>
        /// Makes the parameter
        /// </summary>
        /// <param name="parameterName">name of parameter in Stored Procedure</param>
        /// <param name="dbType">Type of SqlDbType (column type)</param>
        /// <param name="size">size of the SQL column</param>
        /// <param name="parmValue">value of the parameter</param>
        /// <param name="direction">What direction the parameter is...</param>
        /// <returns>Returns a fully populated parameter</returns>
        public DbParameter MakeParameter(string parameterName, DbType dbType, int size, object parmValue, ParameterDirection direction)
        {
            DbParameter parm = new TParameter
                                   {
                                       Value = parmValue,
                                       Direction = direction,
                                       ParameterName = parameterName,
                                       Size = size,
                                       DbType = dbType
                                   };
            return parm;
        }


        /// <summary> Converts an entity to the parameters. </summary>
        /// <exception cref="DuplicateNameException">
        /// Thrown when a duplicate name error condition occurs. </exception>
        /// <param name="entity"> The entity. </param>
        /// <returns> The given data converted to the parameters. </returns>
        public List<DbParameter> ConvertToParameters(object entity)
        {
            var parameterBuilder = new DbParameterBuilder<SqlParameter>("@", Settings);
            var parameters = parameterBuilder.GetValuesFromType(entity);

            var hasDuplicateParameterNames = parameters.GroupBy(i => i.ParameterName).Where(g => g.Count() > 1).Select(g => g.Key).Any();

            if (hasDuplicateParameterNames)
            {
                throw new DuplicateNameException("Parameter Names must be distinct across a class hierarchy. Use the ColumnAttribute on properties to assign unique names.");
            }

            return parameters.ToList();
        }

        /// <summary>
        /// Gets the type of the values from.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        private List<DbParameter> GetValuesFromType<T>(T instance)
        {
            var extractColumnNamesAndValuesService = new DbParameterBuilder<TParameter>(ParameterDelimiter, Settings);
            List<DbParameter> parameters = extractColumnNamesAndValuesService.GetValuesFromType(instance);
            return parameters;
        }
    }
}