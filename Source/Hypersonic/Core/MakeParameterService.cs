using System.Data;
using System.Data.Common;

namespace Hypersonic.Core
{
    internal class MakeParameterService<TParameter> 
        where TParameter : DbParameter, new()
    {
        /// <summary>
        /// Makes the parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public DbParameter MakeParameter<T>(string parameterName, T value)
        {
            DbParameter parameter = new TParameter { ParameterName = parameterName, Value = value };
            return parameter;
        }

        /// <summary>
        /// Makes the parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <param name="parameterDirection">The parameter direction.</param>
        /// <returns></returns>
        public DbParameter MakeParameter<T>(string parameterName, T value, ParameterDirection parameterDirection)
        {
            DbParameter parameter = new TParameter { ParameterName = parameterName, Value = value, Direction = parameterDirection };
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
        public DbParameter MakeParameter(string parameterName, DbType dbType, int size, object parmValue,
                                         ParameterDirection direction)
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
    }
}
