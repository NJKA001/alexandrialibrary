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
            contentTypeFactory = new ContentTypeFactory(logger);
            securityContext = new SecurityContext(contentTypeFactory);
            contentType = contentTypeFactory.GetByCode("application/vnd.gnosis.album");

            album1 = new Album(new IdentityInfo(Guid.NewGuid().ToUrn(), contentType, "OK Computer", string.Empty, new DateTime(1997, 9, 22), new DateTime(1997, 9, 22), 0), SizeInfo.Default, new CreatorInfo(new Uri(radioheadUrn), "Radiohead"), CatalogInfo.Default, TargetInfo.GetDefault(contentTypeFactory), UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image1.jpg"), new byte[0]));
            album2 = new Album(new IdentityInfo(Guid.NewGuid().ToUrn(), contentType, "Undertow", string.Empty, new DateTime(1992, 3, 28), new DateTime(1992, 3, 28), 0), SizeInfo.Default, new CreatorInfo(Guid.NewGuid().ToUrn(), "Tool"), CatalogInfo.Default, TargetInfo.GetDefault(contentTypeFactory), UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image2.jpg"), new byte[0]));
            album3 = new Album(new IdentityInfo(Guid.NewGuid().ToUrn(), contentType, "Free", string.Empty, new DateTime(2002, 7, 9), new DateTime(2002, 7, 9), 0), SizeInfo.Default, new CreatorInfo(Guid.NewGuid().ToUrn(), "Cat Power"), CatalogInfo.Default, TargetInfo.GetDefault(contentTypeFactory), UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image3.jpg"), new byte[0]));
            album4 = new Album(new IdentityInfo(Guid.NewGuid().ToUrn(), contentType, "White Chalk", string.Empty, new DateTime(2008, 4, 30), new DateTime(2008, 4, 30), 0), SizeInfo.Default, new CreatorInfo(Guid.NewGuid().ToUrn(), "PJ Harvey"), CatalogInfo.Default, TargetInfo.GetDefault(contentTypeFactory), UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image4.jpg"), new byte[0]));
            album5 = new Album(new IdentityInfo(Guid.NewGuid().ToUrn(), contentType, "Pablo Honey", string.Empty, new DateTime(1993, 4, 4), new DateTime(1993, 4, 4), 0), SizeInfo.Default, new CreatorInfo(new Uri(radioheadUrn), "Radiohead"), CatalogInfo.Default, TargetInfo.GetDefault(contentTypeFactory), UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image5.jpg"), new byte[0]));

            connection = connectionFactory.Create(connectionString);
            connection.Open();
            repository = new SQLiteMediaItemRepository(logger, securityContext, contentTypeFactory, connection);
            repository.Initialize();
            repository.Save(new List<IAlbum> { album1, album2, album5 });
        }

        private const string connectionString = "Data Source=:memory:;Version=3;";
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();

        protected readonly ILogger logger;
        protected readonly ICharacterSetFactory characterSetFactory;
        protected readonly ISecurityContext securityContext;
        protected readonly IContentTypeFactory contentTypeFactory;
        protected readonly IDbConnection connection;
        protected readonly IMediaItemRepository repository;
        protected readonly IContentType contentType;
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
            repository.Delete<IAlbum>(new List<Uri> { unknownLocation });
            var check = repository.GetByLocation<IAlbum>(unknownLocation);
            Assert.IsNotNull(check);
            Assert.AreEqual(check.Name, "Unknown");
        }

        [Test]
        public void CanBeReadByLocation()
        {
            var check1 = repository.GetByLocation<IAlbum>(album1.Location);
            Assert.IsNotNull(check1);
            Assert.AreEqual(album1.Name, check1.Name);
            Assert.AreEqual(album1.FromDate, check1.FromDate);
            Assert.AreEqual(album1.Creator, check1.Creator);
            Assert.AreEqual(album1.CreatorName, album1.CreatorName);
            Assert.AreEqual(album1.Thumbnail, check1.Thumbnail);
            var check2 = repository.GetByLocation<IAlbum>(album2.Location);
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
            var checks1 = repository.GetByName<IAlbum>("OK%");
            Assert.AreEqual(1, checks1.Count());
            var check1 = checks1.FirstOrDefault();
            Assert.IsNotNull(check1);
            Assert.AreEqual(album1.Name, check1.Name);
            Assert.AreEqual(album1.FromDate, check1.FromDate);
            Assert.AreEqual(album1.Creator, check1.Creator);
            Assert.AreEqual(album1.CreatorName, album1.CreatorName);
            Assert.AreEqual(album1.Thumbnail, check1.Thumbnail);
            var checks2 = repository.GetByName<IAlbum>(album2.Name);
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
            var check = repository.GetByCreator<IAlbum>(new Uri(radioheadUrn));
            Assert.AreEqual(2, check.Count());
            Assert.AreEqual(album5.Name, check.First().Name);
            Assert.AreEqual(album1.Name, check.Last().Name);
        }

        [Test]
        public void AreUniqueByNameArtistTitleAndReleasDate()
        {
            repository.Save(new List<IAlbum> { album2 });

            var check = repository.GetByName<IAlbum>("Undertow");
            Assert.AreEqual(1, check.Count());
        }

        [Test]
        public void CanBeDeleted()
        {
            repository.Save(new List<IAlbum> { album3, album4 });
            Assert.IsNotNull(repository.GetByLocation<IAlbum>(album3.Location));
            Assert.IsNotNull(repository.GetByLocation<IAlbum>(album4.Location));

            repository.Delete<IAlbum>(new List<Uri> { album3.Location, album4.Location });
            Assert.IsNull(repository.GetByLocation<IAlbum>(album3.Location));
            Assert.IsNull(repository.GetByLocation<IAlbum>(album4.Location));
        }
    }
}
