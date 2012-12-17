using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Hypersonic.Tests.Integration
{
    public class InjectDatabaseConnectionStringTest
    {
        /// <summary>
        /// Databases the connection string test.
        /// </summary>
        [Test, Ignore]
        public void DatabaseConnectionStringTest()
        {
            const string connection = "Data Source=localhost;Initial Catalog=Development.Momntz;User Id=momntz;Password=folsom_1;";
            IDatabase database = new MsSqlDatabase(connectionString: connection);
            database.CommandType = CommandType.Text;

            const string sql = "Select UserId From [User]";
            IList<int> list = database.List(sql, Populate);

            bool hasCount = list.Count > 0;
            Assert.True(hasCount);
        }

        /// <summary>
        /// Populates the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static int Populate(INullableReader reader)
        {
            int userId = reader.GetInt32("UserId");
            return userId;
        }
    }
}
