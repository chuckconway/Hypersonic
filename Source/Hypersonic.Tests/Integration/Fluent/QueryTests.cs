using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hypersonic.Session;
using NUnit.Framework;

namespace Hypersonic.Tests.Integration.Fluent
{
    [TestFixture]
    public class QueryTests
    {
        const string connection = "Data Source=localhost;Initial Catalog=Development.Momntz;User Id=momntz;Password=folsom_1;";

        public void Query_Test()
        {
            using (ISession session = SessionFactory.SqlServer(connection:connection))
            {

            }
        }

        public class User
        {
            public int Id { get; set; }
            public string  FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime CreateDate { get; set; }
        }
    }
}
