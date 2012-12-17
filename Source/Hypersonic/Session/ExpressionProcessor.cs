using System;
using System.Linq.Expressions;
using System.Text;
using Hypersonic.Session.Query.Expressions;

namespace Hypersonic.Session
{
    public class ExpressionProcessor
    {
        /// <summary>   Processes the Expression, converting it to a SQL string.</summary>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="expression">   The expression. </param>
        ///
        /// <returns>   . </returns>
        public string Process<T>(Expression<Func<T, bool>> expression)
        {
            WhereExpressionVisitor expressionVisitor = new WhereExpressionVisitor();
            var filter = expressionVisitor.Visit(expression, new StringBuilder()).ToString();
            return filter;
        }
    }
}
