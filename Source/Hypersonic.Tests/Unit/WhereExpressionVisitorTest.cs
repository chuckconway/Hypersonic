using System;
using System.Linq.Expressions;
using System.Text;
using Hypersonic.Session.Query.Expressions;
using Hypersonic.Tests.Integration;
using NUnit.Framework;

namespace Hypersonic.Tests.Unit
{
    [TestFixture]
    public class WhereExpressionVisitorTest
    {
        [Test]
        public void ExpressionVisitor_ValueTypeOfNumber_ValueIsCorrectlyExtracted()
        {
            string name = new Random().Next().ToString();

            Expression<Func<User, bool>> li = u => (u.FirstName == name);
            var s = ProcessExpression(li);

            Assert.IsTrue(s.Contains(name), string.Format("Expected to find {0} in the generated where clause. It was not found.", name));
        }

        [Test]
        public void ExpressionVisitor_ValueTypeOfNumberWithNot_NotIsCorrectlyPlaced()
        {
            string name = new Random().Next().ToString();

            Expression<Func<User, bool>> li = u => !(u.FirstName == name);
            var s = ProcessExpression(li);

            Assert.IsTrue(s.Contains("NOT ("), string.Format("Expected to find {0} in the generated where clause. It was not found.", name));
        }
        
        [Test]
        public void ExpressionVisitor_VariableType_ValueIsCorrectlyExtracted()
        {
            var name = "Chuck" + new Random().Next();

            Expression<Func<User, bool>> li = u => (u.FirstName == name);
            var s = ProcessExpression(li);

            Assert.IsTrue(s.Contains("'" + name + "'"), string.Format("Expected to find {0} in the generated where clause. It was not found.", name));
        }

        [Test]
        public void ExpressionVisitor_ConstantType_ValueIsCorrectlyExtracted()
        {
            const string name = "chuck";

            Expression<Func<User, bool>> li = u => (u.FirstName == name);
            var s = ProcessExpression(li);

            Assert.IsTrue(s.Contains("'" + name + "'"), string.Format("Expected to find {0} in the generated where clause. It was not found.", name));
        }

        [Test]
        public void ExpressionVisitor_ConstantTypeWithNotEqual_ValueIsCorrectlyExtracted()
        {
            const string name = "chuck";

            Expression<Func<User, bool>> li = u => (u.FirstName != name);
            var s = ProcessExpression(li);

            Assert.IsTrue(s.Contains("<> '" + name + "'"), string.Format("Expected to find {0} in the generated where clause. It was not found.", name));
        }

        [Test]
        public void ExpressionVisitor_VariableTypeWithNullValue_EqualIsConvertedToIsNull()
        {
            string name = null;

            Expression<Func<User, bool>> li = u => (u.FirstName == name);
            var s = ProcessExpression(li);

            Assert.IsTrue(s.Contains("IS NULL"), "Expected to find IS NULL");
        }

        [Test]
        public void ExpressionVisitor_ClassWithProertySet_ProperyValueIsCorrectlyExtracted()
        {
            var p = new {Person = "Dude"};

            Expression<Func<User, bool>> li =
                u =>
                ((u.FirstName == p.Person && u.LastName == "conway2") &&
                 (u.FirstName == "chuck3" && u.LastName == "conway4")) || u.FirstName == "cat" ||
                (u.FirstName == "chuck5" | u.LastName == "conway6");

            var s = ProcessExpression(li);

            Assert.IsTrue(s.Contains("= 'Dude'"), string.Format("Expected to find {0} in the generated where clause. It was not found.", p.Person));
        }

        private static string ProcessExpression(Expression<Func<User, bool>> li)
        {
            WhereExpressionVisitor whereExpression = new WhereExpressionVisitor();
            var builder = whereExpression.Visit(li.Body, new StringBuilder());
            var s = builder.ToString();

            Console.WriteLine(s);

            return s;
        }

    }
}
