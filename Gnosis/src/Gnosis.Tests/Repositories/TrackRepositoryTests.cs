using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NUnit.Framework;
using System.Data.SQLite;

using Gnosis.Core;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Tracks;
using Gnosis.Alexandria.Repositories.Tracks;

namespace Gnosis.Tests.Repositories
{
    [TestFixture]
    public class TrackRepositoryTests
    {
        private IContext context;
        private ILogger logger;
        private ITrackRepository repository;
        private IDbConnection connection;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            context = new SingleThreadedContext();
            logger = new DebugLogger();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
        }

        [SetUp]
        public void SetUp()
        {
            connection = new SQLiteConnection("Data Source=:memory:;Version=3;");
            connection.Open();

            repository = new TrackRepository(context, logger, connection);
            repository.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }
        }

        [Test]
        public void TestCreateTrack()
        {
            var initialTracks = repository.Search();
            Assert.AreEqual(0, initialTracks.Count());

        }
    }
}
