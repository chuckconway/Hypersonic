using System.Collections.Generic;
using Hypersonic.Session;
using NUnit.Framework;

namespace Hypersonic.Tests.Integration
{
    public class QueryTest
    { 
        ///// <summary>
        ///// Databases the connection string test.
        ///// </summary>
        //[Test]
        //public void DatabaseConnectionStringTest()
        //{
        //    const string connection = "Data Source=localhost;Initial Catalog=Development.Momntz;User Id=momntz;Password=folsom_1;";
        //    IDatabase database = new MsSqlDatabase(new ConnectionString(connection));
        //    ISession session = new SqlServerSession(database);

        //    List<User> list = session.Query<User>(where: u => ((u.FirstName == "chuck" && u.LastName == "conway") && (u.FirstName == "chuck" && u.LastName == "conway")) || u.FirstName == "cat" || (u.FirstName == "chuck" && u.LastName == "conway"), orderBy: (u) => new { u.FirstName, OrderBy.Asc });

        //    bool hasCount = list.Count > 0;
        //    Assert.True(hasCount);
        //}

        //[Test]
        //public void Session_Save_ObjectIsSavedToDatabase()
        //{
        //    const string connection = "Data Source=localhost;Initial Catalog=Development.Momntz;User Id=momntz;Password=folsom_1;";
        //    IDatabase database = new MsSqlDatabase(new ConnectionString(connection));
        //    ISession session = new SqlServerSession(database);

        //    User user = session.Save<User>(u => u.FirstName == "chuck");
        //}

        //[Test]
        //public void Session_Get_ObjectIsRetrieved()
        //{
        //    const string connection = "Data Source=localhost;Initial Catalog=Development.Momntz;User Id=momntz;Password=folsom_1;";
        //    IDatabase database = new MsSqlDatabase(new ConnectionString(connection));
        //    ISession session = new SqlServerSession(database);

        //    User user = session.Get<User>(u => u.FirstName == "chuck");
        //}
    }

    public class User
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
    }
}
