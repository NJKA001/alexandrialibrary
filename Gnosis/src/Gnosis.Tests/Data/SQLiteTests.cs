using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Loggers;
using Gnosis.Core;
using Gnosis.Data;
using Gnosis.Data.SQLite;

using NUnit.Framework;

namespace Gnosis.Tests.Data
{
    [TestFixture]
    public class SQLiteTests
    {
        public SQLiteTests()
        {
            connection = connectionFactory.Create("Data Source=:memory:;Version=3;");
            connection.Open();
            tagRepository = new SQLiteTagRepository(logger, connection);
        }

        private readonly ILogger logger = new DebugLogger();
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();
        private readonly IDbConnection connection;
        private readonly SQLiteTagRepository tagRepository;

        [TestFixtureSetUp]
        public void SetUp()
        {
            tagRepository.Initialize();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }

        [Test]
        public void TagRepositorySaveTest()
        {
            var tag1 = new Tag(new Uri("http://arstechnica.com/index.ars"), Algorithm.Default, TagType.GeneralTagType, "Some Tag #1");
            var tag2 = new Tag(new Uri(@"C:\Users\dpoage\Music\Tool\Undertown\Bottom.mp3"), Algorithm.Default, TagType.Id3v2ArtistTagType, "Tool");

            tagRepository.Save(new List<ITag> { tag1, tag2 });

            var tags = tagRepository.All();

            Assert.IsNotNull(tags);
            Assert.IsTrue(tags.Count() > 0);
        }

    }
}
