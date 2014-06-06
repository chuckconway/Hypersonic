//using System;
//using System.Linq.Expressions;
//using System.Text;
//using Hypersonic.Session.Query;
//using Hypersonic.Session.Query.Expressions;
//using Hypersonic.Session.Query.Filters;
//using NUnit.Framework;

//namespace Hypersonic.Tests.Unit
//{
//    [TestFixture]
//    public class QueryWriterTests
//    {
//        [Test]
//        public void QueryWriter_AddFilter_WhereClauseIsRenderedWithoutErrors()
//        {
//            Expression<Func<User, bool>> li = u => u.Name == "Chuck";
//            string v = GetValue(li);
//            Console.WriteLine(v);

//            Expression<Func<User, bool>> li1 = u => u.Name != "Mack";
//            string v1 = GetValue(li1);
//            Console.WriteLine(v1);

//            Expression<Func<User, bool>> li2 = u => u.Age == 12;
//            string v2 = GetValue(li2);
//            Console.WriteLine(v2);

//            IQueryWriter queryWriter = new QueryWriter();
//            queryWriter.AddFilter(new WhereFilter(v));
//            queryWriter.AddFilter(new AndFilter(v1));
//            queryWriter.AddFilter(new OrFilter(v2));

//            string render = queryWriter.Query<User>(new User());

//            Console.WriteLine(render);
//            StringAssert.AreEqualIgnoringCase("SELECT [Name], [Age] FROM [User] Where (Name = 'Chuck') And (Name <> 'Mack') Or (Age = 12) ", render);
//        }

//        [Test]
//        public void QueryWriter_WhereFilter_ClauseMatchesTheFormat()
//        {
//            WhereFilter filter = new WhereFilter("Name = 'chuck'");

//            string query = filter.Query();
//            Console.WriteLine(query);

//            StringAssert.AreEqualIgnoringCase("Name = 'chuck'", query);
//        }

//        [Test]
//        public void QueryWriter_OrFilter_ClauseMatchesTheFormat()
//        {
//            OrFilter filter = new OrFilter("Name = 'chuck'");

//            string query = filter.Query();
//            Console.WriteLine(query);

//            StringAssert.AreEqualIgnoringCase("Name = 'chuck'", query);
//        }

//        [Test]
//        public void QueryWriter_AndFilter_ClauseMatchesTheFormat()
//        {
//            AndFilter filter = new AndFilter("Name = 'chuck'");

//            string query = filter.Query();
//            Console.WriteLine(query);

//            StringAssert.AreEqualIgnoringCase("Name = 'chuck'", query);
//        }

//        [Test]
//        public void QueryWriter_LikeFilter_ClauseMatchesTheFormat()
//        {
//            LikeFilter filter = new LikeFilter("Name = 'chuck'");

//            string query = filter.Query();
//            Console.WriteLine(query);

//            StringAssert.AreEqualIgnoringCase("Name LIKE 'chuck'", query);
//        }

//        [Test]
//        public void QueryWriter_NotLikeFilter_ClauseMatchesTheFormat()
//        {
//            LikeFilter filter = new LikeFilter("Name <> 'chuck'");

//            string query = filter.Query();
//            Console.WriteLine(query);

//            StringAssert.AreEqualIgnoringCase("Name NOT LIKE 'chuck'", query);
//        }

//        private static string GetValue(Expression<Func<User, bool>> li)
//        {
//            var expression = new WhereExpressionVisitor();
//            return expression.Visit(li, new StringBuilder()).ToString();
//        }

//        public class User
//        {
//            public string Name { get; set; }

//            public int Age { get; set; }
//        }
//    }
//}
