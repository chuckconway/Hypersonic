using System;
using Hypersonic.Core;
using NUnit.Framework;

namespace Hypersonic.Tests.Integration.Core
{
    [TestFixture]
    public class CoreTests
    {
        [Test]
        public void Test()
        {
            IDbContext context = new DbContext();
            context.Settings.CommandType = HypersonicCommandType.Sql;

            var photos = context.List<Photo>("Select * From Photos");

            Assert.Greater(photos.Count, 0);
        }

        [Test]
        public void UsingConnectionString()
        {
            IDbContext context = new DbContext();

            int count = 0;

            using (context.BeginSession())
            {
                context.Settings.CommandType = HypersonicCommandType.Sql;
                var photos = context.List<Photo>("Select * From Photos");

                count = photos.Count;
            }

            Assert.Greater(count, 0);
        }

        public class Photo
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>The identifier.</value>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>The description.</value>
            public string Description { get; set; }

            /// <summary>
            /// Gets or sets the create date.
            /// </summary>
            /// <value>The create date.</value>
            public DateTime CreateDate { get; set; }
        }
    }
}
