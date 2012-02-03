using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Audio;
using Gnosis.Audio.Fmod;
using Gnosis.Data;
using Gnosis.Data.SQLite;
using Gnosis.Links;
using Gnosis.Utilities;
using Gnosis.Spiders;
using Gnosis.Tags;
using Gnosis.Tasks;

namespace Gnosis.Tests.Unit.Spiders
{
    [TestFixture]
    public class CatalogSpiderTests
    {
        public CatalogSpiderTests()
        {
            logger = new DebugLogger();
            contentTypeFactory = new ContentTypeFactory(logger);
            securityContext = new SecurityContext(contentTypeFactory);
        }

        private const string connectionString = "Data Source=:memory:;Version=3;";

        private IConnectionFactory connectionFactory = new SQLiteConnectionFactory();
        private ILogger logger;
        private IContentTypeFactory contentTypeFactory;
        private ISecurityContext securityContext;
        private ITagRepository tagRepository;
        private ILinkRepository linkRepository;
        private IMediaRepository mediaRepository;
        private IMediaItemRepository mediaItemRepository;
        private IAudioStreamFactory audioStreamFactory;
        private ITagTypeFactory tagTypeFactory = new TagTypeFactory();
        private IDbConnection linkConnection;
        private IDbConnection tagConnection;
        private IDbConnection mediaConnection;
        private IDbConnection itemConnection;
        private CatalogSpider spider;

        [TestFixtureSetUp]
        public void SetUp()
        {
            linkConnection = connectionFactory.Create(connectionString);
            linkConnection.Open();
            linkRepository = new SQLiteLinkRepository(logger, linkConnection);
            linkRepository.Initialize();

            tagConnection = connectionFactory.Create(connectionString);
            tagConnection.Open();
            tagRepository = new SQLiteTagRepository(logger, tagTypeFactory, tagConnection);
            tagRepository.Initialize();

            mediaConnection = connectionFactory.Create(connectionString);
            mediaConnection.Open();
            mediaRepository = new SQLiteMediaRepository(logger, contentTypeFactory, mediaConnection);
            mediaRepository.Initialize();

            itemConnection = connectionFactory.Create(connectionString);
            itemConnection.Open();
            mediaItemRepository = new SQLiteMediaItemRepository(logger, securityContext, contentTypeFactory, itemConnection);
            mediaItemRepository.Initialize();

            audioStreamFactory = new AudioStreamFactory();

            spider = new CatalogSpider(logger, securityContext, contentTypeFactory, linkRepository, tagRepository, mediaRepository, mediaItemRepository, audioStreamFactory);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            linkConnection.Close();
            tagConnection.Close();
            mediaConnection.Close();
            itemConnection.Close();
        }

        [Test]
        public void CanCrawl()
        {
            const string path = @".\Files";
            const int timeoutSeconds = 60;
            const int mediaCount = 39;
            const int fileCount = 28;

            var directory = new DirectoryInfo(path);
            Assert.IsTrue(directory.Exists);
            System.Diagnostics.Debug.WriteLine("CanCrawl() directory path=" + directory.FullName);

            var fileTally = 0;
            foreach (var file in System.IO.Directory.GetFiles(path))
                fileTally++;

            Assert.AreEqual(fileCount, fileTally);

            var target = new Uri(directory.FullName);
            Assert.IsTrue(target.IsFile);

            var media = new List<IMedia>();

            var task = spider.Crawl(target);
            task.AddResultsCallback(results => media.AddRange(results));
            task.AddProgressCallback(progress => logger.Info(string.Format("PROGRESS: {0} {1}", progress.Count, progress.Description)));
            //task.AddFailedCallback(

            task.StartSynchronously(TimeSpan.FromSeconds(timeoutSeconds));

            Assert.AreEqual(mediaCount, media.Count);

            foreach (var medium in media)
                logger.Info(medium.Type.ToString() + " " + medium.Location.ToString());
        }
    }
}
