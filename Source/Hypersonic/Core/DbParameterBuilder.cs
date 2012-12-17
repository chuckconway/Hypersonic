﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Hypersonic.Core
{
    public class DbParameterBuilder<TParameter> where TParameter : DbParameter, new()
    {
        private readonly string _parameterDelimiter;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbParameterBuilder&lt;TParameter&gt;"/> class.
        /// </summary>
        /// <param name="parameterDelimiter">The parameter delimiter.</param>
        public DbParameterBuilder(string parameterDelimiter)
        {
            _parameterDelimiter = parameterDelimiter;
        }

        /// <summary>
        /// Gets the values from anonymous types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public List<DbParameter> GetValuesFromType<T>(T parameters)
        {
            Type type = typeof(T);

            CodeContract.Requires(parameters != null, "Parameters are null.");
            CodeContract.Requires(type != typeof(string), "The type of string is not supported. Strings must be encapsulated in a property.");
            CodeContract.Requires(type != typeof(DataSet), "DataSets are not supported.");

            Flattener flattener = new Flattener();
            var values = flattener.GetNamesAndValues(parameters);

            List<DbParameter> dbParameter = GetDbParameters(values);
            return dbParameter;
        }

        /// <summary> Gets the db parameters. </summary>
        /// <param name="values"> The instance and properties. </param>
        /// <returns> The database parameters. </returns>
        private List<DbParameter> GetDbParameters(IEnumerable<Property> values)
        {
            MakeParameterService<TParameter> parameters = new MakeParameterService<TParameter>();
           return values.Select(v => parameters.MakeParameter(_parameterDelimiter + v.Name, v.Value)).ToList();
        }

    }
}
