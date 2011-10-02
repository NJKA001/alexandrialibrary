using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Loggers;
using Gnosis.Core;
using Gnosis.Core.Tags.Generic;
using Gnosis.Core.Tags.Id3;
using Gnosis.Data;
using Gnosis.Data.SQLite;

using NUnit.Framework;

namespace Gnosis.Tests.Data.SQLite
{
    [TestFixture]
    public class SQLiteTests
    {
        public SQLiteTests()
        {
            connection = connectionFactory.Create("Data Source=:memory:;Version=3;");
            connection.Open();
            tagRepository = new SQLiteTagRepository(logger, connection);
            mediaRepository = new SQLiteMediaRepository(logger, connection);
            mediaFactory = new MediaFactory();
        }

        private readonly ILogger logger = new DebugLogger();
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();
        private readonly IDbConnection connection;
        private readonly SQLiteTagRepository tagRepository;
        private readonly SQLiteMediaRepository mediaRepository;
        private readonly MediaFactory mediaFactory;

        private readonly Uri uri1 = new Uri("http://arstechnica.com/index.ars");
        private readonly Uri uri2 = new Uri(@"C:\Users\dpoage\Music\Tool\Undertown\Bottom.mp3");
        private readonly Uri uri3 = new Uri("http://flickr.com/users/dpoage/example.jpg");
        private readonly Uri uri4 = new Uri(@"C:\Users\dpoage\Pictures\icon.gif");

        [TestFixtureSetUp]
        public void SetUp()
        {
            tagRepository.Initialize();
            mediaRepository.Initialize();
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
            var tag1 = new Tag(uri1, Algorithm.Default, GenericTagTypes.Generic1Default.ToUri(), "Some Tag #1");
            var tag2 = new Tag(uri2, Algorithm.Default, Id3v1Schema.Id3v1Artist.ToUri(), "Tool");
            var tag3 = new Tag(new Uri("http://blah.com/1234"), Algorithm.Americanized, Id3v1Schema.Id3v1Artist.ToUri(), "Tool".ToAmericanizedString());
            var tag4 = new Tag(new Uri("http://blah.com/4567"), Algorithm.Americanized, Id3v1Schema.Id3v1Title.ToUri(), "Oil & Water 1".ToAmericanizedString());


            var tags = new List<ITag> { tag1, tag2, tag3, tag4 };
            tagRepository.Save(tags);

            var all = tagRepository.All();
            var americanized = tagRepository.Search(Algorithm.Americanized, Id3v1Schema.Id3v1.ToUri());

            Assert.IsNotNull(all);
            Assert.AreEqual(tags.Count, all.Count());
            Assert.IsNotNull(americanized);
            Assert.AreEqual(tags.Where(x => x.Algorithm == Algorithm.Americanized).Count(), americanized.Count());
        }

        [Test]
        public void MediaRepositorySaveTest()
        {
            var media1 = mediaFactory.Create(uri1, MediaType.TextHtml);
            var media2 = mediaFactory.Create(uri2, MediaType.AudioMpeg);
            var media3 = mediaFactory.Create(uri3, MediaType.ImageJpeg);

            mediaRepository.Save(new List<IMedia> { media1, media2, media3 });

            var all = mediaRepository.All();

            Assert.IsNotNull(all);
            Assert.IsTrue(all.Count() == 3);
        }
    }
}
