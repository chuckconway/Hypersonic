using System;
using System.Linq.Expressions;
using System.Text;

namespace Hypersonic.Session.Query.Expressions
{
    public class VisitConstantExpression : ExpressionBase
    {
        public string Visit(StringBuilder state, ConstantExpression expression)
        {
            QuotifyValues values = new QuotifyValues();
            return GetRightHandValue(state, i => expression.Value == null ? "null" : values.QuotifyText(Convert.ToString(expression.Value)));
        }
    }
}
