using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

namespace Hypersonic.Core
{
    public class ExecuteCommand
    {
        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbCommand command)
        {
          return ExecuteNonQueryResult(command);
        }

        /// <summary>
        /// Executes the non query result.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>System.Int32.</returns>
        private static int ExecuteNonQueryResult(DbCommand command)
        {
            GenerateDebugInformation(command);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int returnValue = command.ExecuteNonQuery();

            stopwatch.Stop();
            Debug.WriteLine("Elapsed Time: {0}ms", stopwatch.ElapsedMilliseconds);

            return returnValue;
        }

        /// <summary>
        /// Generates the debug information.
        /// </summary>
        /// <param name="command">The command.</param>
        private static void GenerateDebugInformation(DbCommand command)
        {
            Debug.WriteLine(string.Empty);
            Debug.WriteLine("-----------------------------------------------------------------");
            Debug.WriteLine(string.Format("Connection String: {0}", command.Connection.ConnectionString));
            Debug.WriteLine(string.Format("Procedure/CommandText: {0}", command.CommandText));
            Debug.WriteLine(string.Format("CommandType: {0}", command.CommandType));

            if (command.Parameters != null && command.Parameters.Count > 0)
            {
                Debug.WriteLine(string.Format("{0}", GetParameters(command)));
            }
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        private static string GetParameters(DbCommand command)
        {
            var builder = new StringBuilder();
            builder.AppendLine("[Parameters]");

            if (command.Parameters.Count > 0)
            {
                foreach (DbParameter parameter in command.Parameters)
                {
                    const string format = "Name: {0}, Value: {1}, Type: {2}, Direction: {3},";
                    builder.AppendLine(string.Format(format, parameter.ParameterName, parameter.Value, parameter.DbType, parameter.Direction));
                }
            }

            builder.Append(Environment.NewLine +"[/Parameters]");
            return builder.ToString();
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public T ExecuteScalar<T>(DbCommand command, T result)
        {
            GenerateDebugInformation(command);
            result = ScalarResult(command, result);

            return result;
        }

        /// <summary> Scalar result. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="command"> The command. </param>
        /// <param name="result">  The result. </param>
        /// <returns> . </returns>
        private static T ScalarResult<T>(IDbCommand command, T result)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var val = command.ExecuteScalar();

            stopwatch.Stop();
            Debug.WriteLine("Elapsed Time: {0}ms", stopwatch.ElapsedMilliseconds);

            //catch DBNull return value
            if (!(val.GetType() == DBNull.Value.GetType()))
            {
                result = (T) val;
            }

            return result;
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(DbCommand command)
        {
            return ExecuteReaderResult(command, CommandBehavior.CloseConnection);
        }
        
        /// <summary> Executes the reader result operation. </summary>
        /// <param name="command">         The command. </param>
        /// <param name="commandBehavior"> The command behavior. </param>
        /// <returns> . </returns>
        private static DbDataReader ExecuteReaderResult(DbCommand command, CommandBehavior commandBehavior)
        {
            GenerateDebugInformation(command);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var reader = command.ExecuteReader(commandBehavior);

            stopwatch.Stop();
            Debug.WriteLine("Elapsed Time: {0}ms", stopwatch.ElapsedMilliseconds);

            return reader;
        }
    }
}
