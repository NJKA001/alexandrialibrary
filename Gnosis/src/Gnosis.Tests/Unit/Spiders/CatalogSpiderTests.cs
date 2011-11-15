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

namespace Gnosis.Tests.Unit.Spiders
{
    [TestFixture]
    public class CatalogSpiderTests
    {
        public CatalogSpiderTests()
        {
        }

        private const string connectionString = "Data Source=:memory:;Version=3;";

        private IConnectionFactory connectionFactory = new SQLiteConnectionFactory();
        private ILogger logger = new DebugLogger();
        private ITagRepository tagRepository;
        private ILinkRepository linkRepository;
        private IMediaRepository mediaRepository;
        private IArtistRepository artistRepository;
        private IAlbumRepository albumRepository;
        private ITrackRepository trackRepository;
        private IMediaFactory mediaFactory = new MediaFactory();
        private ILinkTypeFactory linkTypeFactory = new LinkTypeFactory();
        private ITagTypeFactory tagTypeFactory = new TagTypeFactory();
        private IDbConnection linkConnection;
        private IDbConnection tagConnection;
        private IDbConnection mediaConnection;
        private IDbConnection artistConnection;
        private IDbConnection albumConnection;
        private IDbConnection trackConnection;
        private CatalogSpider spider;

        [TestFixtureSetUp]
        public void SetUp()
        {
            linkConnection = connectionFactory.Create(connectionString);
            linkConnection.Open();
            linkRepository = new SQLiteLinkRepository(logger, linkTypeFactory, linkConnection);
            linkRepository.Initialize();

            tagConnection = connectionFactory.Create(connectionString);
            tagConnection.Open();
            tagRepository = new SQLiteTagRepository(logger, tagTypeFactory, tagConnection);
            tagRepository.Initialize();

            mediaConnection = connectionFactory.Create(connectionString);
            mediaConnection.Open();
            mediaRepository = new SQLiteMediaRepository(logger, mediaConnection);
            mediaRepository.Initialize();

            artistConnection = connectionFactory.Create(connectionString);
            artistConnection.Open();
            artistRepository = new SQLiteArtistRepository(logger, artistConnection);
            artistRepository.Initialize();

            albumConnection = connectionFactory.Create(connectionString);
            albumConnection.Open();
            albumRepository = new SQLiteAlbumRepository(logger, albumConnection);
            albumRepository.Initialize();

            trackConnection = connectionFactory.Create(connectionString);
            trackConnection.Open();
            trackRepository = new SQLiteTrackRepository(logger, trackConnection);
            trackRepository.Initialize();

            spider = new CatalogSpider(logger, mediaFactory, linkRepository, tagRepository, mediaRepository, artistRepository, albumRepository, trackRepository);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            linkConnection.Close();
            tagConnection.Close();
            mediaConnection.Close();
            artistConnection.Close();
            albumConnection.Close();
            trackConnection.Close();
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
            task.AddProgressCallback(progress => logger.Info(string.Format("PROGRESS: {0} {1}", progress.Count, progress.Description)));
            //task.AddFailedCallback(

            task.StartSynchronously(TimeSpan.FromSeconds(timeoutSeconds));

            Assert.AreEqual(mediaCount, media.Count);

            foreach (var medium in media)
                logger.Info(medium.Type.ToString() + " " + medium.Location.ToString());
        }
    }
}
