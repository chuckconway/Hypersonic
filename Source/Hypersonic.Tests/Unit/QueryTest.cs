using NUnit.Framework;

namespace Hypersonic.Tests.Unit
{
    [TestFixture]
    public class QueryTest
    {
        //[Test]
        //public void Query()
        //{
        //    QueryWriter writer = new QueryWriter();

        //    IQuery<User> query = new Query<User>(new MsSqlDatabase(), writer);
        //    query
        //        .OrderBy(u => u.Name).Asc()
        //        .OrderBy(u => u.Name).Asc()
        //        .OrderBy(u => u.Name).Desc()
        //        .OrderBy(u => u.Age).Desc();

        //    Console.WriteLine(writer.Query<User>());
        //}

        internal class User
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }
    }
}
