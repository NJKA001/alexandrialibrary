using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NUnit.Framework;
using System.Data.SQLite;

using Gnosis.Core;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Feeds;
using Gnosis.Alexandria.Repositories.Feeds;

namespace Gnosis.Tests.Repositories
{
    [TestFixture]
    public class FeedRepositoryTests
    {
        private IContext context;
        private ILogger logger;
        private IRepository<IFeed> repository;
        private IDbConnection connection;

        [TestFixtureSetUp]
        public void Setup()
        {
            context = new SingleThreadedContext();
            logger = new DummyLogger();
            
            connection = new SQLiteConnection(":memory:");
            connection.Open();

            repository = new FeedRepository(context, logger, connection);
            repository.Initialize();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            if (connection != null)
                connection.Close();
        }

        [Test]
        public void TestInsert()
        {
        }
    }
}
