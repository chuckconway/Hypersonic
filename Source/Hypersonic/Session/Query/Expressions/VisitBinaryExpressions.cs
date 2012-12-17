using System.Linq;
using System.Linq.Expressions;

namespace Hypersonic.Session.Query.Expressions
{
    internal class VisitBinaryExpressions
    {
        public string Visit(BinaryExpression expression)
        {
            string operatorExpression = string.Empty;
            var @operator = _operators.SingleOrDefault(o => o.NodeType == expression.NodeType);

            if (@operator != null)
            {
                operatorExpression = @operator.Sql;
            }

            return operatorExpression;
        }


        readonly Operator[] _operators = new[]
                                {
                                    new Operator{ NodeType = ExpressionType.Negate, Sql = " NOT " },
                                    new Operator{ NodeType = ExpressionType.Equal, Sql = " = " },
                                    new Operator{ NodeType = ExpressionType.And, Sql = " AND " },
                                    new Operator{ NodeType = ExpressionType.AndAlso, Sql = " AND " },
                                    new Operator{ NodeType = ExpressionType.Or, Sql = " OR " },
                                    new Operator{ NodeType = ExpressionType.OrElse, Sql = " OR " },
                                    new Operator{ NodeType = ExpressionType.NotEqual, Sql = " <> " },
                                    new Operator{ NodeType = ExpressionType.Not, Sql = " NOT " },
                                    new Operator{ NodeType = ExpressionType.GreaterThan, Sql = " > " },
                                    new Operator{ NodeType = ExpressionType.GreaterThanOrEqual, Sql = " >= " },
                                    new Operator{ NodeType = ExpressionType.LessThan, Sql = " < " },
                                    new Operator{ NodeType = ExpressionType.LessThanOrEqual, Sql = " <= "},
                                };

        internal class Operator
        {
            public ExpressionType NodeType { get; set; }

            public string Sql { get; set; }
        }
    }
}
