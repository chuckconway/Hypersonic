using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Hypersonic.Core
{
    internal class DbService<TConnection, TCommand>
        where TConnection : DbConnection, new()
        where TCommand : DbCommand, new()
    {
        private readonly HypersonicDbConnection<TConnection> _connection;
        private readonly HypersonicSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbService&lt;TConnection, TCommand&gt;" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="settings">The settings.</param>
        public DbService(HypersonicDbConnection<TConnection> connection, HypersonicSettings settings)
        {
            _connection = connection;
            _settings = settings;
        }

        /// <summary>
        /// Scalars the specified CMD text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="parms">The parms.</param>
        /// <returns>T.</returns>
        public T Scalar<T>(string cmdText, List<DbParameter> parms)
        {
            var result = default(T);

            using (DbCommand command = GetCommand(cmdText, parms))
            {
                var executeCommand = new ExecuteCommand();
                result = executeCommand.ExecuteScalar(command, result); 
            }

            return result;
        }


        /// <summary>
        /// Returns reader from database call. **!# MUST be CLOSED and DISPOSED! Suggest using a "using" block
        /// </summary>
        /// <param name="storedProcedure">Procedure to be executed</param>
        /// <param name="values">values to be passed into procedure</param>
        /// <returns>Result Set from procedure execution</returns>
        public DbDataReader Reader(string storedProcedure, List<DbParameter> values)
        {
            DbDataReader reader;

            using (DbCommand command = GetCommand(storedProcedure, values))
            {
                var executeCommand = new ExecuteCommand();
                reader = executeCommand.ExecuteReader(command);
            }

            return reader;
        }


        /// <summary>
        /// Nullables the reader.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="values">The values.</param>
        /// <returns>NullableDataReader.</returns>
        public HypersonicDbDataReader NullableReader(string storedProcedure, List<DbParameter> values)
        {
            DbDataReader reader = Reader(storedProcedure, values);
            var nullableDataReader = new HypersonicDbDataReader(reader);
            return nullableDataReader;
        }

        /// <summary>
        /// Executes the procedure and passes the parameters to the stored procedure
        /// </summary>
        /// <param name="storedProc">name of the stored procedure to be executed</param>
        /// <param name="values">The values.</param>
        /// <returns>rows affected</returns>
        public int NonQuery(string storedProc, List<DbParameter> values)
        {
            int returnValue;

            //Get command
            using (DbCommand command = GetCommand(storedProc, values))
            {
                var executeCommand = new ExecuteCommand();
                returnValue = executeCommand.ExecuteNonQuery(command);
            }

            return returnValue;
        }

        /// <summary>
        /// Populates the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public List<T> PopulateCollection<T>(string storedProcedure, List<DbParameter> parameters, Func<IHypersonicDbReader, T> getItem)
        {
            HypersonicDbDataReader reader = NullableReader(storedProcedure, parameters);
            var lineitems = new List<T>();

            using (reader)
            {
                while (reader.Read())
                {
                    T items = getItem(reader);
                    lineitems.Add(items);
                }
            }

            return lineitems;
        }

        /// <summary>
        /// Populates the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="getItem">The get item.</param>
        /// <returns>T.</returns>
        public T PopulateItem<T>(string storedProcedure, List<DbParameter> parameters, Func<IHypersonicDbReader, T> getItem)
        {
            var reader = NullableReader(storedProcedure, parameters);
            var lineitem = default(T);

            using (reader)
            {
                while (reader.Read())
                {
                    lineitem = getItem(reader);
                }
            }

            return lineitem;
        }

        /// <summary>
        /// Populates the Command with the commandText and array of parameters
        /// </summary>
        /// <param name="commandText">Inline Sql or stored procedure name</param>
        /// <param name="parameters">parameters to be passed into procedure call</param>
        /// <returns>A SqlCommand with associated commandText and SqlParameters</returns>
        private DbCommand GetCommand(string commandText, List<DbParameter> parameters)
        {
            DbCommand cmd = GetCommand(commandText);
            cmd.Parameters.AddRange(parameters.ToArray());

            return cmd;
        }

        /// <summary>
        /// Creates the SqlCommand and sets the command Text
        /// </summary>
        /// <param name="commandText">Inline Sql or stored procedure name</param>
        /// <returns>A SqlCommand with associated commandText</returns>
        private DbCommand GetCommand(string commandText)
        {
            DbCommand cmd = new TCommand
                                {
                                    CommandType = (_settings.CommandType == HypersonicCommandType.StoredProcedures ? CommandType.StoredProcedure : CommandType.Text),
                                    CommandText = commandText,
                                    Connection = _connection.DbConnection
                                };
            return cmd;
        }
    }
}
