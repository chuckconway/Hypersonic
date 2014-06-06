using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Hypersonic.Core;

namespace Hypersonic
{
    public class DbContext : IDbContext
    {
        private HypersonicDbConnection<SqlConnection> _connection;

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public HypersonicSettings Settings { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContext"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public DbContext(HypersonicSettings settings)
        {
            Settings = settings;
            Database = new MsSqlDatabase(settings);
            _connection = new HypersonicDbConnection<SqlConnection>(settings);
        }

        /// <summary>
        /// Begins the session.
        /// </summary>
        /// <returns>HypersonicDbConnection&lt;SqlConnection&gt;.</returns>
        public HypersonicDbConnection<SqlConnection> BeginSession()
        {
            _connection = new HypersonicDbConnection<SqlConnection>(Settings) { IsManual = true };
            _connection.DbConnection.Open();
            return _connection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContext"/> class.
        /// </summary>
        public DbContext()
        {
            Database = new MsSqlDatabase();
            Settings = new HypersonicSettings();
            _connection = new HypersonicDbConnection<SqlConnection>(Settings) { IsManual = false };
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>The database.</value>
        public IDatabase<SqlConnection> Database { get; private set; }

        /// <summary>
        /// Singles the specified stored procedure.
        /// </summary>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns>TReturn.</returns>
        public TReturn Single<TReturn>(string storedProcedure, List<DbParameter> parameters, Func<IHypersonicDbReader, TReturn> getItem)
        {
            var dbService = GetDbService();
            return dbService.PopulateItem(storedProcedure, parameters, getItem);
        }

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>T.</returns>
        public TReturn Single<TParameters, TReturn>(string storedProcedureOrRawSql, TParameters parameters, Func<IHypersonicDbReader, TReturn> mapper) where TParameters : class
        {
            List<DbParameter> dbParameters = GetValuesFromType(parameters);
            return Single(storedProcedureOrRawSql, dbParameters, mapper);
        }

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>T.</returns>
        public TReturn Single<TReturn>(string storedProcedureOrRawSql, Func<IHypersonicDbReader, TReturn> mapper)
        {
            return Single(storedProcedureOrRawSql, new List<DbParameter>(), mapper);
        }

        /// <summary>
        /// Singles the specified stored procedure.
        /// </summary>
        /// <typeparam name="TReturn">The type of the T return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <returns>``0.</returns>
        public TReturn Single<TReturn>(string storedProcedureOrRawSql) where TReturn : class, new()
        {
            return Single(storedProcedureOrRawSql, Database.AutoMapper<TReturn>);
        }

        /// <summary>
        /// Singles the specified stored procedure.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure or raw SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>TReturn.</returns>
        public TReturn Single<TParameters, TReturn>(string storedProcedureOrRawSql, TParameters parameters) where TParameters : class where TReturn : class, new()
        {
            return Single(storedProcedureOrRawSql, parameters, Database.AutoMapper<TReturn>);
        }

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>IList&lt;T&gt;.</returns>
        public IList<TReturn> List<TReturn>(string storedProcedureOrRawSql, Func<IHypersonicDbReader, TReturn> mapper)
        {
            var dbService = GetDbService();
            return dbService.PopulateCollection(storedProcedureOrRawSql, new List<DbParameter>(), mapper);
        }

        /// <summary>
        /// Lists the specified stored procedure.
        /// </summary>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure.</param>
        /// <returns>IList&lt;TReturn&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<TReturn> List<TReturn>(string storedProcedureOrRawSql) where TReturn : class, new()
        {
            var dbService = GetDbService();
            return dbService.PopulateCollection(storedProcedureOrRawSql, new List<DbParameter>(), Database.AutoMapper<TReturn>);
        }

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>IList&lt;TReturn&gt;.</returns>
        public IList<TReturn> List<TParameters, TReturn>(string storedProcedureOrRawSql, TParameters parameters, Func<IHypersonicDbReader, TReturn> mapper) where TParameters : class
        {
            var dbService = GetDbService();
            return dbService.PopulateCollection(storedProcedureOrRawSql, GetValuesFromType(parameters), mapper);
        }

        /// <summary>
        /// Lists the specified stored procedure.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IList&lt;TReturn&gt;.</returns>
        public IList<TReturn> List<TParameters, TReturn>(string storedProcedureOrRawSql, TParameters parameters) where TParameters : class where TReturn : class, new()
        {
            var dbService = GetDbService();
            return dbService.PopulateCollection(storedProcedureOrRawSql, GetValuesFromType(parameters), Database.AutoMapper<TReturn>);
        }

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedureOrRawSql">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>IList&lt;T&gt;.</returns>
        public IList<T> List<T>(string storedProcedureOrRawSql, List<DbParameter> parameters, Func<IHypersonicDbReader, T> mapper)
        {
            var dbService = GetDbService();
            return dbService.PopulateCollection(storedProcedureOrRawSql, parameters, mapper);
        }


        /// <summary>
        /// Gets the database service.
        /// </summary>
        /// <returns>DbService&lt;SqlConnection, SqlCommand&gt;.</returns>
        private DbService<SqlConnection, SqlCommand> GetDbService()
        {
            return new DbService<SqlConnection, SqlCommand>(_connection, Settings);
        }

        /// <summary>
        /// Gets the type of the values from.
        /// </summary>
        /// <typeparam name="TParameter">The type of the t parameter.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>List&lt;DbParameter&gt;.</returns>
        private List<DbParameter> GetValuesFromType<TParameter>(TParameter instance)
        {
            var extractColumnNamesAndValuesService = new DbParameterBuilder<SqlParameter>("@", Settings);
            var parameters = extractColumnNamesAndValuesService.GetValuesFromType(instance);
            return parameters;
        }

    }
}