using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis;
using Gnosis.Application.Vendor;
using Gnosis.Data;
using Gnosis.Data.SQLite;
using Gnosis.Metadata;
using Gnosis.Utilities;

namespace Gnosis.Tests.Unit.Data.SQLite
{
    [TestFixture]
    public class SavedAlbums
    {
        public SavedAlbums()
        {
            logger = new DebugLogger();
            mediaTypeFactory = new MediaTypeFactory(logger);
            contentTypeFactory = new ContentTypeFactory(logger, mediaTypeFactory);
            mediaType = mediaTypeFactory.GetByCode("application/vnd.gnosis.album");

            album1 = new Album(new IdentityInfo(Guid.NewGuid().ToUrn(), mediaType, "OK Computer", string.Empty, new DateTime(1997, 9, 22), new DateTime(1997, 9, 22), 0), SizeInfo.Default, new CreatorInfo(new Uri(radioheadUrn), "Radiohead"), CatalogInfo.Default, TargetInfo.GetDefault(mediaTypeFactory), UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image1.jpg"), new byte[0]));
            album2 = new Album(new IdentityInfo(Guid.NewGuid().ToUrn(), mediaType, "Undertow", string.Empty, new DateTime(1992, 3, 28), new DateTime(1992, 3, 28), 0), SizeInfo.Default, new CreatorInfo(Guid.NewGuid().ToUrn(), "Tool"), CatalogInfo.Default, TargetInfo.GetDefault(mediaTypeFactory), UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image2.jpg"), new byte[0]));
            album3 = new Album(new IdentityInfo(Guid.NewGuid().ToUrn(), mediaType, "Free", string.Empty, new DateTime(2002, 7, 9), new DateTime(2002, 7, 9), 0), SizeInfo.Default, new CreatorInfo(Guid.NewGuid().ToUrn(), "Cat Power"), CatalogInfo.Default, TargetInfo.GetDefault(mediaTypeFactory), UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image3.jpg"), new byte[0]));
            album4 = new Album(new IdentityInfo(Guid.NewGuid().ToUrn(), mediaType, "White Chalk", string.Empty, new DateTime(2008, 4, 30), new DateTime(2008, 4, 30), 0), SizeInfo.Default, new CreatorInfo(Guid.NewGuid().ToUrn(), "PJ Harvey"), CatalogInfo.Default, TargetInfo.GetDefault(mediaTypeFactory), UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image4.jpg"), new byte[0]));
            album5 = new Album(new IdentityInfo(Guid.NewGuid().ToUrn(), mediaType, "Pablo Honey", string.Empty, new DateTime(1993, 4, 4), new DateTime(1993, 4, 4), 0), SizeInfo.Default, new CreatorInfo(new Uri(radioheadUrn), "Radiohead"), CatalogInfo.Default, TargetInfo.GetDefault(mediaTypeFactory), UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image5.jpg"), new byte[0]));

            connection = connectionFactory.Create(connectionString);
            connection.Open();
            repository = new SQLiteAlbumRepository(logger, mediaTypeFactory, connection);
            repository.Initialize();
            repository.Save(new List<IAlbum> { album1, album2, album5 });
        }

        private const string connectionString = "Data Source=:memory:;Version=3;";
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();

        protected readonly ILogger logger;
        protected readonly IMediaTypeFactory mediaTypeFactory;
        protected readonly IContentTypeFactory contentTypeFactory;
        protected readonly IDbConnection connection;
        protected readonly IMediaItemRepository<IAlbum> repository;
        protected readonly IMediaType mediaType;
        protected readonly Uri unknownLocation = Guid.Empty.ToUrn();

        private const string radioheadUrn = "urn:uuid:27A19456-E6E9-463F-951D-98BB44356C65";
        private IAlbum album1;
        private IAlbum album2;
        private IAlbum album3;
        private IAlbum album4;
        private IAlbum album5;

        [TestFixtureSetUp]
        public void Setup()
        {
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            connection.Close();
        }

        [Test]
        public void DefaultAlbumCannotBeDeleted()
        {
            repository.Delete(new List<Uri> { unknownLocation });
            var check = repository.GetByLocation(unknownLocation);
            Assert.IsNotNull(check);
            Assert.AreEqual(check.Name, "Unknown");
        }

        [Test]
        public void CanBeReadByLocation()
        {
            var check1 = repository.GetByLocation(album1.Location);
            Assert.IsNotNull(check1);
            Assert.AreEqual(album1.Name, check1.Name);
            Assert.AreEqual(album1.FromDate, check1.FromDate);
            Assert.AreEqual(album1.Creator, check1.Creator);
            Assert.AreEqual(album1.CreatorName, album1.CreatorName);
            Assert.AreEqual(album1.Thumbnail, check1.Thumbnail);
            var check2 = repository.GetByLocation(album2.Location);
            Assert.IsNotNull(check2);
            Assert.AreEqual(album2.Name, check2.Name);
            Assert.AreEqual(album2.FromDate, check2.FromDate);
            Assert.AreEqual(album2.Creator, check2.Creator);
            Assert.AreEqual(album2.CreatorName, check2.CreatorName);
            Assert.AreEqual(album2.Thumbnail, check2.Thumbnail);
        }

        [Test]
        public void CanBeReadByTitle()
        {
            var checks1 = repository.GetByName("OK%");
            Assert.AreEqual(1, checks1.Count());
            var check1 = checks1.FirstOrDefault();
            Assert.IsNotNull(check1);
            Assert.AreEqual(album1.Name, check1.Name);
            Assert.AreEqual(album1.FromDate, check1.FromDate);
            Assert.AreEqual(album1.Creator, check1.Creator);
            Assert.AreEqual(album1.CreatorName, album1.CreatorName);
            Assert.AreEqual(album1.Thumbnail, check1.Thumbnail);
            var checks2 = repository.GetByName(album2.Name);
            Assert.AreEqual(1, checks2.Count());
            var check2 = checks2.FirstOrDefault();
            Assert.IsNotNull(check2);
            Assert.AreEqual(album2.Name, check2.Name);
            Assert.AreEqual(album2.FromDate, check2.FromDate);
            Assert.AreEqual(album2.Creator, check2.Creator);
            Assert.AreEqual(album2.CreatorName, check2.CreatorName);
            Assert.AreEqual(album2.Thumbnail, check2.Thumbnail);
        }

        [Test]
        public void CanBeReadByArtistSortedByReleaseDate()
        {
            var check = repository.GetByCreator(new Uri(radioheadUrn));
            Assert.AreEqual(2, check.Count());
            Assert.AreEqual(album5.Name, check.First().Name);
            Assert.AreEqual(album1.Name, check.Last().Name);
        }

        [Test]
        public void AreUniqueByNameArtistTitleAndReleasDate()
        {
            repository.Save(new List<IAlbum> { album2 });

            var check = repository.GetByName("Undertow");
            Assert.AreEqual(1, check.Count());
        }

        [Test]
        public void CanBeDeleted()
        {
            repository.Save(new List<IAlbum> { album3, album4 });
            Assert.IsNotNull(repository.GetByLocation(album3.Location));
            Assert.IsNotNull(repository.GetByLocation(album4.Location));

            repository.Delete(new List<Uri> { album3.Location, album4.Location });
            Assert.IsNull(repository.GetByLocation(album3.Location));
            Assert.IsNull(repository.GetByLocation(album4.Location));
        }
    }
}
