using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Data;
using Gnosis.Data.SQLite;
using Gnosis.Links;
using Gnosis.Utilities;
using Gnosis.Spiders;
using Gnosis.Tags;
using Gnosis.Tasks;

namespace Gnosis.Tests.Spiders
{
    [TestFixture]
    public class CatalogMediaSpiderTests
    {
        public CatalogMediaSpiderTests()
        {
        }

        private IConnectionFactory connectionFactory = new SQLiteConnectionFactory();
        private ILogger logger = new DebugLogger();
        private ITagRepository tagRepository;
        private ILinkRepository linkRepository;
        private IMediaRepository mediaRepository;
        private IMediaFactory mediaFactory = new MediaFactory();
        private ILinkTypeFactory linkTypeFactory = new LinkTypeFactory();
        private ITagTypeFactory tagTypeFactory = new TagTypeFactory();
        private IDbConnection linkConnection;
        private IDbConnection tagConnection;
        private IDbConnection mediaConnection;
        private CatalogMediaSpider spider;

        [TestFixtureSetUp]
        public void SetUp()
        {
            linkConnection = connectionFactory.Create("Data Source=:memory:;Version=3;");
            linkConnection.Open();
            linkRepository = new SQLiteLinkRepository(logger, linkTypeFactory, linkConnection);
            linkRepository.Initialize();

            tagConnection = connectionFactory.Create("Data Source=:memory:;Version=3;");
            tagConnection.Open();
            tagRepository = new SQLiteTagRepository(logger, tagTypeFactory, tagConnection);
            tagRepository.Initialize();

            mediaConnection = connectionFactory.Create("Data Source=:memory:;Version=3;");
            mediaConnection.Open();
            mediaRepository = new SQLiteMediaRepository(logger, mediaConnection);
            mediaRepository.Initialize();

            spider = new CatalogMediaSpider(logger, mediaFactory, linkRepository, tagRepository, mediaRepository);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            linkConnection.Close();
            tagConnection.Close();
            mediaConnection.Close();
        }

        [Test]
        public void CanCrawl()
        {
            const string path = @".\Files";
            const int timeoutSeconds = 60;
            const int mediaCount = 39;

            var directory = new DirectoryInfo(path);
            Assert.IsTrue(directory.Exists);


            var target = new Uri(directory.FullName);
            Assert.IsTrue(target.IsFile);

            var media = new List<IMedia>();

            var task = spider.Crawl(target);
            task.AddResultsCallback(results => media.AddRange(results));
            task.AddProgressCallback(progress => logger.Info(string.Format("PROGRESS: {0} {1}", progress.Number, progress.Description)));
            //task.AddFailedCallback(

            task.StartSynchronously(TimeSpan.FromSeconds(timeoutSeconds));

            Assert.AreEqual(mediaCount, media.Count);

            foreach (var medium in media)
                logger.Info(medium.Type.ToString() + " " + medium.Location.ToString());
        }
    }
}
