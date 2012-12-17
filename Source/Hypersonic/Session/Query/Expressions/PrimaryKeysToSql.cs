using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Hypersonic.Attributes;
using Hypersonic.Core;

namespace Hypersonic.Session.Query.Expressions
{
    public class PrimaryKeysToSql
    {
        /// <summary> Converts the properties to sql. </summary>
        /// <param name="instance"> The properties. </param>
        /// <returns> The given data converted to a sql. </returns>
        public string ConvertToSql(object instance)
        {
            Flattener flattener = new Flattener();
            var properties = flattener.GetPropertiesWithDefaultValues<PrimaryKeyAttribute>(instance);
            return ConvertToSql(properties);
        }

        /// <summary> Converts the properties to sql. </summary>
        /// <param name="properties"> The properties. </param>
        /// <returns> The given data converted to a sql. </returns>
        public string ConvertToSql(IEnumerable<Property> properties)
        {
            var expression = DynamicLambdaExpressions(properties);

            StringBuilder builder = new StringBuilder();
            WhereExpressionVisitor whereExpressions = new WhereExpressionVisitor();
            whereExpressions.Visit(expression, builder);

            return builder.ToString();
        }

        /// <summary> Generates lambda expressions from this collection. </summary>
        /// <param name="properties"> The properties. </param>
        /// <returns>
        /// An enumerator that allows foreach to be used to process generate lambda expressions in this
        /// collection.
        /// </returns>
        private static Expression DynamicLambdaExpressions(IEnumerable<Property> properties)
        {
            var array = properties.ToArray();

            BinaryExpression expression = null;

            for (int index = 0; index < array.Length; index++)
            {
                var param = Expression.Parameter(array[index].Instance.GetType(), "x");
                var value = Expression.Constant(array[index].Value);
                var prop = Expression.PropertyOrField(param, array[index].Name);
                var body = Expression.Equal(prop, value);

                expression = (expression == null ? body : Expression.And(expression, body));
            }

            return expression;
        }
    }
}
