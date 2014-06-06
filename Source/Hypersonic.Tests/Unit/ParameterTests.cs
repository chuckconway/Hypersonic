using System;
using System.Data;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Hypersonic.Tests.Unit
{
    [TestFixture]
    public class ParameterTests
    {
        [Test]
        public void Parameters_SingleLevelClass_PropertiesAreConvertedToDbParameters()
        {
            Fixture fixture = new Fixture();
            var user = fixture.CreateAnonymous<SingleLevelClass>();
            var propertyNames = user.GetType().GetProperties().Select(p => "@" + p.Name).ToArray();
            var database = new MsSqlDatabase();

            var parameters = database.ConvertToParameters(user);
            var parameterNames = parameters.Select(d => d.ParameterName).ToArray();

            Assert.That(parameterNames, Is.EquivalentTo(propertyNames));
            Console.WriteLine(string.Join(", ", parameterNames));
        }

        [Test]
        public void Parameters_SecondLevelClass_PropertiesAreConvertedToDbParameters()
        {
            Fixture fixture = new Fixture();
            var database = new MsSqlDatabase();
            var user = fixture.CreateAnonymous<SecondLevelClass>();
            var propertyNames = new[] { "@Id", "@FirstName", "@LastName", "@Email", "@Password", "@CreateTime" };

            var parameters = database.ConvertToParameters(user);
            var parameterNames = parameters.Select(d => d.ParameterName).ToArray();

            Assert.That(parameterNames, Is.EquivalentTo(propertyNames));
            Console.WriteLine(string.Join(", ", parameterNames));
        }

        [Test]
        public void Parameters_ThirdLevelClass_PropertiesAreConvertedToDbParameters()
        {
            var database = new MsSqlDatabase();
            Fixture fixture = new Fixture();
            var user = fixture.CreateAnonymous<ThirdLevelClass>();
            var propertyNames = new[] { "@Id", "@FirstName", "@LastName", "@Email", "@Password", "@CreateTime", "@UpdateDatetime" };

            var parameters = database.ConvertToParameters(user);
            var parameterNames = parameters.Select(d => d.ParameterName).ToArray();

            Assert.That(parameterNames, Is.EquivalentTo(propertyNames));
            Console.WriteLine(string.Join(", ", parameterNames));
        }

        [Test, ExpectedException(typeof(DuplicateNameException))]
        public void Parameters_DuplicateParameters_AnExceptionIsThrownWhenDuplicateParametersAreFound()
        {
            var database = new MsSqlDatabase();
            
            Fixture fixture = new Fixture();
            var duplicate = fixture.CreateAnonymous<DuplicateParameters>();

            database.ConvertToParameters(duplicate);
        }

        public class DuplicateParameters
        {
            public string FirstName { get; set; }
            public SingleLevelClass SingleLevelClass { get; set; }
        }

        public class SingleLevelClass
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class SecondLevelClass
        {
            public SingleLevelClass NestedClass { get; set; }
            public DateTime CreateTime { get; set; }
        }

        public class ThirdLevelClass
        {
            public SecondLevelClass NestedClassOne { get; set; }
            public DateTime UpdateDatetime { get; set; }
        }
    }

}
