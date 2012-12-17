using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Hypersonic
{
    public static class ParameterExtensions
    {
        /// <summary>
        /// Selects the identity.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static List<DbParameter> SelectIdentity(this List<DbParameter> parameters)
        {
            if (parameters.Count > 1)
            {
                Type type = parameters[0].GetType();

                if (type != typeof(SqlParameter))
                {
                    throw new InvalidCastException(string.Format("{0} is incompatible with {1}. Only MSSQL server is supported.", typeof(SqlParameter).FullName, type.FullName));
                }
            }

            SqlParameter parameter = new SqlParameter {Direction = ParameterDirection.Output, ParameterName = "@Identity"};
            parameters.Add(parameter);

            return parameters;
        }

        /// <summary>
        /// Gets the identity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static T Identity<T>(this List<DbParameter> parameters)
        {
            var parameter = parameters.SingleOrDefault(d => d.ParameterName == "@Identity" && d.Direction == ParameterDirection.Output);

            if (parameter == null)
            {
                throw new InvalidOperationException("@Identity not found. Expected ParameterReturn Value of '@Identity', SelectIdentity() needs to be called");
            }

            return (T)parameter.Value;
        }
    }
}
