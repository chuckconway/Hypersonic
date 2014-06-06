//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Hypersonic.Attributes;
//using Hypersonic.Core;
//using Hypersonic.Session.Query.Expressions;
//using NUnit.Framework;

//namespace Hypersonic.Tests.Unit
//{
//    [TestFixture]
//    public class PropertiesToSqlTest
//    {
//        [Test]
//        public void Sql()
//        {
//            var toSql = new PrimaryKeysToSql();
//            string sql = toSql.ConvertToSql(new User {Id = Guid.NewGuid()});
//            Console.WriteLine(sql);

//            Assert.IsFalse(string.IsNullOrEmpty(sql));
//        }

//        public class User
//        {
//            /// <summary> Gets or sets the identifier. </summary>
//            /// <value> The identifier. </value>
//            [PrimaryKey]
//            public Guid Id { get; set; }

//            /// <summary> Gets or sets the name of the first. </summary>
//            /// <value> The name of the first. </value>
//            public string FirstName { get; set; }

//            [PrimaryKey]
//            public string LastName { get; set; }

//            [PrimaryKey]
//            public DateTime CreateDate { get; set; }
//        }
//    }
//}
