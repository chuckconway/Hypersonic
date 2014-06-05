using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hypersonic.Attributes;
using Hypersonic.Session.Persistance;
using Hypersonic.Session.Persistence;
using Hypersonic.Tests.Integration;
using NUnit.Framework;
using NUnitIgnore = NUnit.Framework.IgnoreAttribute;

namespace Hypersonic.Tests.Unit
{
    [TestFixture]
    public class PersistTests
    {
        [Test]
        public void Persist_SingleIntegerPrimaryKeyNewRow_RecordIsInserted()
        {
            IPersistence persistence = new Persistence();
            SingleIntegerPrimaryKeyNewRow user = new SingleIntegerPrimaryKeyNewRow { FirstName = "Chuck", LastName = "Conway" };
            List<Persist> persists = persistence.Persist<SingleIntegerPrimaryKeyNewRow>(user);
            Console.WriteLine(persists.Single().Sql);

            StringAssert.AreEqualIgnoringCase("Insert INTO [SingleIntegerPrimaryKeyNewRow] ([FirstName], [LastName]) Values ('Chuck', 'Conway')", persists.Single().Sql);
        }

        public class SingleIntegerPrimaryKeyNewRow
        {
            [PrimaryKey]
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_SingleIntgerPrimaryKeyChangedRow_RecordIsUpdated()
        {
            IPersistence persistence = new Persistence();
            SingleIntegerPrimaryKeyChangeRow user = new SingleIntegerPrimaryKeyChangeRow { Id = 123432, FirstName = "Chuck", LastName = "Conway" };
            List<Persist> persists = persistence.Persist<SingleIntegerPrimaryKeyChangeRow>(user);
            Console.WriteLine(persists.Single().Sql);

            StringAssert.AreEqualIgnoringCase("UPDATE [SingleIntegerPrimaryKeyChangeRow] SET [FirstName] = 'Chuck', [LastName] = 'Conway' WHERE (Id = 123432)", persists.Single().Sql);
        }

        public class SingleIntegerPrimaryKeyChangeRow
        {
            [PrimaryKey]
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_SingleGuidPrimaryKeyNewRowGuidGenerator_RecordIsInserted()
        {
            IPersistence persistence = new Persistence();
            SingleGuidPrimaryKeyNewRowGuidGenerator user = new SingleGuidPrimaryKeyNewRowGuidGenerator { FirstName = "Chuck", LastName = "Conway" };
            List<Persist> persists = persistence.Persist<SingleGuidPrimaryKeyNewRow>(user);
            Console.WriteLine(persists.Single().Sql);

        }

        public class SingleGuidPrimaryKeyNewRowGuidGenerator
        {
            [PrimaryKey(Generator.Guid)]
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_SingleGuidPrimaryKeyNewRow_RecordIsInserted()
        {
            IPersistence persistence = new Persistence();
            SingleGuidPrimaryKeyNewRow user = new SingleGuidPrimaryKeyNewRow { FirstName = "Chuck", LastName = "Conway" };
            List<Persist> persists = persistence.Persist<SingleGuidPrimaryKeyNewRow>(user);
            Console.WriteLine(persists.Single().Sql);

            StringAssert.AreEqualIgnoringCase("Insert INTO [SingleGuidPrimaryKeyNewRow] ([FirstName], [LastName]) Values ('Chuck', 'Conway')", persists.Single().Sql);
        }

        public class SingleGuidPrimaryKeyNewRow
        {
            [PrimaryKey]
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_SingleGuidPrimaryKeyChangedRow_RecordIsUpdated()
        {
            IPersistence persistence = new Persistence();
            SingleGuidPrimaryKeyNewRow user = new SingleGuidPrimaryKeyNewRow {Id = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D"), FirstName = "Chuck", LastName = "Conway" };
            List<Persist> persists = persistence.Persist<SingleGuidPrimaryKeyNewRow>(user);
            Console.WriteLine(persists.Single().Sql);

            StringAssert.AreEqualIgnoringCase("UPDATE [SingleGuidPrimaryKeyNewRow] SET [FirstName] = 'Chuck', [LastName] = 'Conway' WHERE (Id = '21ec2020-3aea-1069-a2dd-08002b30309d')", persists.Single().Sql);
        }

        public class SingleGuidPrimaryKeyChangedRow
        {
            [PrimaryKey]
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_CustomTableNameWithAnoymousType_RecordIsInserted()
        {
            var insertClass = new
                                  {
                                     FirstName = "Chuck",
                                     LastName = "Conway",
                                     CreateDate = DateTime.MinValue
                                  };

            IPersistence persistence = new Persistence();
            var sql = persistence.Persist<User>(insertClass);


            Console.WriteLine(sql.First().Sql);

            StringAssert.AreEqualIgnoringCase("Insert INTO [User] ([FirstName], [LastName], [CreateDate]) Values ('Chuck', 'Conway', '1/1/0001 12:00:00 AM')", sql.First().Sql);
        }

        [Test]
        public void Persist_MultipleGuidPrimaryKeyNewRow_RecordIsInserted()
        {
            IPersistence persistence = new Persistence();
            MultipleGuidPrimaryKeyNewRow user = new MultipleGuidPrimaryKeyNewRow {FirstName = "Chuck", LastName = "Conway" };
            List<Persist> persists = persistence.Persist<MultipleGuidPrimaryKeyNewRow>(user);
            Console.WriteLine(persists.First().Sql);

            StringAssert.AreEqualIgnoringCase("Insert INTO [MultipleGuidPrimaryKeyNewRow] ([FirstName], [LastName]) Values ('Chuck', 'Conway')", persists.Single().Sql);

        }

        public class MultipleGuidPrimaryKeyNewRow
        {
            [PrimaryKey]
            public Guid FirstId { get; set; }
            [PrimaryKey]
            public Guid SecondId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_MultipleGuidPrimaryKeyChangedRow_RecordIsUpdated()
        {
            IPersistence persistence = new Persistence();
            MultipleGuidPrimaryKeyChangedRow user = new MultipleGuidPrimaryKeyChangedRow { FirstId = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D"), SecondId = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D"), FirstName = "Chuck", LastName = "Conway" };
            List<Persist> persists = persistence.Persist<MultipleGuidPrimaryKeyChangedRow>(user);
            Console.WriteLine(persists.First().Sql);

            StringAssert.AreEqualIgnoringCase("UPDATE [MultipleGuidPrimaryKeyChangedRow] SET [FirstName] = 'Chuck', [LastName] = 'Conway' WHERE ((FirstId = '21ec2020-3aea-1069-a2dd-08002b30309d') AND (SecondId = '21ec2020-3aea-1069-a2dd-08002b30309d'))", persists.First().Sql);
        }

        public class MultipleGuidPrimaryKeyChangedRow
        {
            [PrimaryKey]
            public Guid FirstId { get; set; }
            [PrimaryKey]
            public Guid SecondId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_MultipleIntPrimaryKeyNewRow_RecordIsInserted()
        {
            IPersistence persistence = new Persistence();
            MultipleIntPrimaryKeyNewRow user = new MultipleIntPrimaryKeyNewRow { FirstName = "Chuck", LastName = "Conway" };
            List<Persist> persists = persistence.Persist<MultipleIntPrimaryKeyNewRow>(user);
            Console.WriteLine(persists.Single().Sql);

            StringAssert.AreEqualIgnoringCase("INSERT INTO [MultipleIntPrimaryKeyNewRow] ([FirstName], [LastName]) Values ('Chuck', 'Conway')", persists.First().Sql);
 
        }

        public class MultipleIntPrimaryKeyNewRow
        {
            [PrimaryKey]
            public int FirstId { get; set; }
            [PrimaryKey]
            public int SecondId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_MultipleIntPrimaryKeyChangedRow_RecordIsInserted()
        {
            IPersistence persistence = new Persistence();
            MultipleIntPrimaryKeyChangedRow user = new MultipleIntPrimaryKeyChangedRow { FirstId = 4324234, SecondId = 3243243, FirstName = "Chuck", LastName = "Conway" };
            List<Persist> persists = persistence.Persist<MultipleIntPrimaryKeyChangedRow>(user);
            Console.WriteLine(persists.First().Sql);

            StringAssert.AreEqualIgnoringCase("UPDATE [MultipleIntPrimaryKeyChangedRow] SET [FirstName] = 'Chuck', [LastName] = 'Conway' WHERE ((FirstId = 4324234) AND (SecondId = 3243243))", persists.Single().Sql);
        }

        public class MultipleIntPrimaryKeyChangedRow
        {
            [PrimaryKey]
            public int FirstId { get; set; }
            [PrimaryKey]
            public int SecondId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_MultipleMixedPrimaryKeyNewRow_RecordIsInserted()
        {
            IPersistence persistence = new Persistence();
            MultipleMixedPrimaryKeyNewRow user = new MultipleMixedPrimaryKeyNewRow { FirstName = "Chuck", LastName = "Conway" };
            List<Persist> persists = persistence.Persist<MultipleMixedPrimaryKeyNewRow>(user);
            Console.WriteLine(persists.Single().Sql);

            StringAssert.AreEqualIgnoringCase("Insert INTO [MultipleMixedPrimaryKeyNewRow] ([FirstName], [LastName]) Values ('Chuck', 'Conway')", persists.Single().Sql);
        }

        public class MultipleMixedPrimaryKeyNewRow
        {
            [PrimaryKey]
            public int FirstId { get; set; }
            [PrimaryKey]
            public Guid SecondId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_MultipleMixedPrimaryKeyChangedRow_RecordIsUpdated()
        {
            IPersistence persistence = new Persistence();
            MultipleMixedPrimaryKeyNewRow user = new MultipleMixedPrimaryKeyNewRow { FirstId = 41234234, SecondId = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D"), FirstName = "Chuck", LastName = "Conway" };
            List<Persist> persists = persistence.Persist<MultipleMixedPrimaryKeyNewRow>(user);
            Console.WriteLine(persists.Single().Sql);

            StringAssert.AreEqualIgnoringCase("UPDATE [MultipleMixedPrimaryKeyNewRow] SET [FirstName] = 'Chuck', [LastName] = 'Conway' WHERE ((FirstId = 41234234) AND (SecondId = '21ec2020-3aea-1069-a2dd-08002b30309d'))", persists.Single().Sql);
        }

        public class MultipleMixedPrimaryKeyChangedRow
        {
            [PrimaryKey]
            public int FirstId { get; set; }
            [PrimaryKey]
            public Guid SecondId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_NoPrimayKeysWithInt32_RecordIsInserted()
        {
            IPersistence persistence = new Persistence();
            UserWithInt32Id user = new UserWithInt32Id { FirstName = "Chuck", LastName = "Conway" };
            var sql = persistence.Persist<UserWithInt32Id>(user);
            Console.WriteLine(sql.Single().Sql);

            StringAssert.AreEqualIgnoringCase("Insert INTO [UserWithInt32Id] ([FirstName], [LastName]) Values ('Chuck', 'Conway')", sql.Single().Sql);
        }

        public class UserWithInt32Id
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_NoPrimayKeysWithGuid_RecordIsInserted()
        {
            IPersistence persistence = new Persistence();
            UserWithGuidId user = new UserWithGuidId { FirstName = "Chuck", LastName = "Conway" };
            var sql = persistence.Persist<UserWithGuidId>(user);
            Console.WriteLine(sql.Single().Sql);

            StringAssert.AreEqualIgnoringCase("Insert INTO [UserWithGuidId] ([FirstName], [LastName]) Values ('Chuck', 'Conway')", sql.Single().Sql);
        }

        [Test]
        public void Persist_NoPrimayKeysGuidWithValue_RecordIsInserted()
        {
            IPersistence persistence = new Persistence();
            UserWithInt32Id user = new UserWithInt32Id { Id = 67890, FirstName = "Chuck", LastName = "Conway" };
            var sql = persistence.Persist<UserWithInt32Id>(user);
            Console.WriteLine(sql.Single().Sql);

            StringAssert.AreEqualIgnoringCase("Insert INTO [UserWithInt32Id] ([Id], [FirstName], [LastName]) Values (67890, 'Chuck', 'Conway')", sql.Single().Sql);
        }

        [Test]
        public void Persist_NoPrimayKeysIntWithValue_RecordIsInserted()
        {
            IPersistence persistence = new Persistence();
            UserWithGuidId user = new UserWithGuidId { Id = new Guid("b57a2175-4ec9-4f1d-84af-fb5e44da8bc9"), FirstName = "Chuck", LastName = "Conway" };
            var sql = persistence.Persist<UserWithGuidId>(user);
            Console.WriteLine(sql.Single().Sql);

            StringAssert.AreEqualIgnoringCase("Insert INTO [UserWithGuidId] ([Id], [FirstName], [LastName]) Values ('b57a2175-4ec9-4f1d-84af-fb5e44da8bc9', 'Chuck', 'Conway')", sql.Single().Sql);
        }

        public class UserWithGuidId
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_CollectionOfNewItems_ItemsAreInserted()
        {
            List<object> collection = new List<object>()
                                          {
                                              new CollectionOfNewItems{FirstName = Path.GetRandomFileName(), LastName = Path.GetRandomFileName()},
                                              new CollectionOfNewItems{FirstName = Path.GetRandomFileName(), LastName = Path.GetRandomFileName()},
                                              new CollectionOfNewItems{FirstName = Path.GetRandomFileName(), Id = 23, LastName = Path.GetRandomFileName()},
                                              new CollectionOfNewItems{FirstName = Path.GetRandomFileName(), LastName = Path.GetRandomFileName()},
                                              new CollectionOfNewItems{FirstName = Path.GetRandomFileName(), Id = 23234, LastName = Path.GetRandomFileName()},
                                          };

            IPersistence persistence = new Persistence();
            var sql = persistence.Persist(collection);
            Console.WriteLine(sql.First().Sql);
        }

        public class CollectionOfNewItems
        {
            [PrimaryKey]
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Persist_CollectionOfMixedItems_ItemsAreUpdatedInserted()
        {
            List<object> collection = new List<object>
                                          {
                                              new CollectionOfNewItems{FirstName = Path.GetRandomFileName(), LastName = Path.GetRandomFileName()},
                                              new CollectionOfNewItems{FirstName = Path.GetRandomFileName(), LastName = Path.GetRandomFileName()},
                                              new CollectionOfMixedItems{FirstName = Path.GetRandomFileName(), Hash = new Guid("b57a2175-4ec9-4f1d-84af-fb5e44da8bc9"), LastName = Path.GetRandomFileName()},
                                              new CollectionOfNewItems{ FirstName = Path.GetRandomFileName(), LastName = Path.GetRandomFileName()},
                                              new CollectionOfMixedItems{FirstName = Path.GetRandomFileName(), Hash = new Guid("b57a2175-4ec9-4f1d-84af-fb5e44da8bc9"), Id = new Guid("b57a2175-4ec9-4f1d-84af-fb5e44da8bc9"), LastName = Path.GetRandomFileName()},
                                              new CollectionOfMixedItemsTwo{CreateDate = DateTime.MinValue}
                                          };

            IPersistence persistence = new Persistence();
            var sql = persistence.Persist(collection);
            Console.WriteLine(sql.First().Sql);
        }

        public class CollectionOfMixedItems
        {
            [PrimaryKey]
            public Guid Id { get; set; }
            public Guid Hash { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class CollectionOfMixedItemsTwo
        {
            [PrimaryKey]
            public Guid Id { get; set; }
            public DateTime CreateDate { get; set; }
        }
    }
}
