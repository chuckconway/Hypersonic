using System;
using FakeItEasy;
using Hypersonic.Session;
using NUnit.Framework;

namespace Hypersonic.Tests.Unit
{
    [TestFixture]
    public class SessionTests
    {
        [Test]
        public void Session_SingleFilter_ValidSqlIsGeneratedThatRepresentsTheLambdaExpression()
        {
            IDatabase database = A.Fake<IDatabase>();
            string sql = string.Empty;
            //A.CallTo(() => database.List(sql, A<Func<INullableReader, DummyClass>>.Ignored)).Returns<string>(sql);

            using (ISession session = new SqlServerSession(database))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Query<DummyClass>()
                    .Where(u => u.Name == "Chuck");
                    
                    transaction.Commit();
                }
            }

        }

        public class DummyClass
        {
            public DummyClass()
            {
                Name = "Chuck";
                Age = 21;
            }

            public string Name { get; set; }

            public int Age { get; set; }
        }
    }
}
