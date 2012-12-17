using System;
using System.Linq.Expressions;
using System.Text;

namespace Hypersonic.Session.Query.Expressions
{
    public class WhereExpressionVisitor : ExpressionVisitorBase<StringBuilder>
    {
        /// <summary> Visit binary. </summary>
        /// <param name="context"> The context. </param>
        /// <param name="node">    The node. </param>
        protected override void VisitBinary(Context context, BinaryExpression node)
        {
            context.State.Append("(");
            var builder = Visit(node.Left, context.State);

            VisitBinaryExpressions binaryExpressions = new VisitBinaryExpressions();
            string s = binaryExpressions.Visit(node);
            builder.Append(s);

            builder = Visit(node.Right, context.State);
            builder.Append(")");
        }

        /// <summary> Visit unary. </summary>
        /// <param name="context"> The context. </param>
        /// <param name="node">    The node. </param>
        protected override void VisitUnary(Context context, UnaryExpression node)
        {
            if(node.NodeType == ExpressionType.Not)
            {
                context.State.Append(" NOT ");
            }

            base.VisitUnary(context, node);
        }

        /// <summary> Visit member. </summary>
        /// <param name="context"> The context. </param>
        /// <param name="node">    The node. </param>
        protected override void VisitMember(Context context, MemberExpression node)
        {
            VisitMemberExpressions visitMember = new VisitMemberExpressions();
            string val = visitMember.Visit(context.State, node);

            context.State.Append(val);
        }

        /// <summary> Visit constant. </summary>
        /// <param name="context"> The context. </param>
        /// <param name="node">    The node. </param>
        protected override void VisitConstant(Context context, ConstantExpression node)
        {
            VisitConstantExpression visitConstant = new VisitConstantExpression();
            string val = visitConstant.Visit(context.State, node);
            context.State.Append(val);
        }
    }
}
