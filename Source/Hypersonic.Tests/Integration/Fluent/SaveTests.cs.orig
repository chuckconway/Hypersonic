using System;
using System.IO;
using Hypersonic.Attributes;
using Hypersonic.Session;
using NUnit.Framework;

namespace Hypersonic.Tests.Integration.Fluent
{
    [TestFixture]
    public class SaveTests
    {
        const string _connection = "Data Source=localhost;Initial Catalog=Hypersonic;User Id=sa;Password=folsom_1;";

        [Test, NUnit.Framework.Ignore]
        public void Session_SaveAccount_AccountIsSavedWithoutExceptionsThrown()
        {
            Account account = new Account(){Birthday = DateTime.Now, Name = Path.GetRandomFileName()};

            using (ISession session = SessionFactory.SqlServer(connection:_connection))
            {
                var save = session.Save(account);

            }
        }

        [Test, NUnit.Framework.Ignore]
        public void Session_SaveAccountInTransaction_AccountIsSavedWithoutExceptionsThrown()
        {
            Account account = new Account() { Birthday = DateTime.Now, Name = Path.GetRandomFileName() };

            using (ISession session = SessionFactory.SqlServer(connection: _connection))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(account);
                    transaction.Commit();
                }
            }
        }

        public class Account
        {
            [PrimaryKey(Generator.Guid)]
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime Birthday { get; set; }
        }
    }
}
