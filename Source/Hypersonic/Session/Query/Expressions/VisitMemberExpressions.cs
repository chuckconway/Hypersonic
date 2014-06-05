using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Hypersonic.Session.Query.Expressions
{
    internal class VisitMemberExpressions : ExpressionBase
    {
        public string Visit(StringBuilder state, MemberExpression expression)
        {
            var val = GetRightHandValue(state, i => (i != '=' ? expression.Member.Name : GetValue(expression)));
            return val;
        }

        private static string GetValue(MemberExpression node)
        {
            object val = string.Empty;

            if (node.Member is PropertyInfo)
            {
                val = GetPropertyInfoValue(node);
            }

            if (node.Member is FieldInfo)
            {
                val = GetFieldInfoValue(node);
            }

            var quotify = new QuotifyValues();
            return quotify.Quotify(val);
        }

        /// <summary> Gets a property information value. </summary>
        /// <param name="node"> The node. </param>
        /// <returns> The property information value. </returns>
        private static object GetPropertyInfoValue(Expression node)
        {
            var valueExpression = Expression.Lambda(node).Compile();
            object value = valueExpression.DynamicInvoke();

            return value;
        }

        /// <summary> Gets a field information value. </summary>
        /// <param name="node"> The node. </param>
        /// <returns> The field information value. </returns>
        private static object GetFieldInfoValue(MemberExpression node)
        {
            object container = ((ConstantExpression)node.Expression).Value;
            object value = ((FieldInfo)node.Member).GetValue(container);

            return value;
        }
    }
}
