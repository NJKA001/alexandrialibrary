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
    public class SavedArtists
    {
        public SavedArtists()
        {
            logger = new DebugLogger();
            mediaFactory = new MediaFactory(logger);
            securityContext = new SecurityContext(mediaFactory);
            mediaType = mediaFactory.GetMediaType("application/vnd.gnosis.artist");

            artist1 = new Artist(new IdentityInfo(Guid.NewGuid().ToUrn(), mediaType, "Radiohead", string.Empty, new DateTime(1985, 1, 2), DateTime.MaxValue, 0), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image.jpg"), new byte[0]));
            artist2 = new Artist(new IdentityInfo(Guid.NewGuid().ToUrn(), mediaType, "Tool", string.Empty, new DateTime(1991, 2, 28), DateTime.MaxValue, 0), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image2.jpg"), new byte[0]));
            artist3 = new Artist(new IdentityInfo(Guid.NewGuid().ToUrn(), mediaType, "Cat Power", string.Empty, new DateTime(1997, 10, 15), DateTime.MaxValue, 0), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image3.jpg"), new byte[0]));
            artist4 = new Artist(new IdentityInfo(Guid.NewGuid().ToUrn(), mediaType, "PJ Harvey", string.Empty, new DateTime(2011, 11, 11), DateTime.MaxValue, 0), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, new ThumbnailInfo(new Uri("http://example.com/image4.jpg"), new byte[0]));

            connection = connectionFactory.Create(connectionString);
            connection.Open();
            repository = new SQLiteMetadataRepository(logger, securityContext, mediaFactory, connection);
            repository.Initialize();
            repository.Save(new List<IArtist> { artist1, artist2 });
        }

        private const string connectionString = "Data Source=:memory:;Version=3;";
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();

        protected readonly ILogger logger = new DebugLogger();
        protected readonly ICharacterSetFactory characterSetFactory;
        protected readonly ISecurityContext securityContext;
        protected readonly IMediaFactory mediaFactory;
        protected readonly IDbConnection connection;
        protected readonly IMetadataRepository repository;
        protected readonly IMediaType mediaType;
        protected readonly Uri unknownLocation = Guid.Empty.ToUrn();

        private IArtist artist1;
        private IArtist artist2;
        private IArtist artist3;
        private IArtist artist4;

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
        public void DefaultArtistCannotBeDeleted()
        {
            repository.Delete<IArtist>(new List<Uri> { unknownLocation });
            var check = repository.GetByLocation<IArtist>(unknownLocation);
            Assert.IsNotNull(check);
            Assert.AreEqual(check.Name, "Unknown");
        }

        [Test]
        public void CanBeReadByLocation()
        {
            var check1 = repository.GetByLocation<IArtist>(artist1.Location);
            Assert.IsNotNull(check1);
            Assert.AreEqual(artist1.Name, check1.Name);
            Assert.AreEqual(artist1.FromDate, check1.FromDate);
            Assert.AreEqual(artist1.ToDate, check1.ToDate);
            Assert.AreEqual(artist1.Thumbnail, check1.Thumbnail);
            var check2 = repository.GetByLocation<IArtist>(artist2.Location);
            Assert.IsNotNull(check2);
            Assert.AreEqual(artist2.Name, check2.Name);
            Assert.AreEqual(artist2.FromDate, check2.FromDate);
            Assert.AreEqual(artist2.ToDate, check2.ToDate);
            Assert.AreEqual(artist2.Thumbnail, check2.Thumbnail);
        }

        [Test]
        public void CanBeReadByName()
        {
            var checks1 = repository.GetByName<IArtist>("Radio%");
            Assert.AreEqual(1, checks1.Count());
            var check1 = checks1.FirstOrDefault();
            Assert.IsNotNull(check1);
            Assert.AreEqual(artist1.Name, check1.Name);
            Assert.AreEqual(artist1.FromDate, check1.FromDate);
            Assert.AreEqual(artist1.ToDate, check1.ToDate);
            Assert.AreEqual(artist1.Thumbnail, check1.Thumbnail);
            var checks2 = repository.GetByName<IArtist>(artist2.Name);
            Assert.AreEqual(1, checks2.Count());
            var check2 = checks2.FirstOrDefault();
            Assert.IsNotNull(check2);
            Assert.AreEqual(artist2.Name, check2.Name);
            Assert.AreEqual(artist2.FromDate, check2.FromDate);
            Assert.AreEqual(artist2.ToDate, check2.ToDate);
            Assert.AreEqual(artist2.Thumbnail, check2.Thumbnail);
        }

        [Test]
        public void AreUniqueByName()
        {
            repository.Save(new List<IArtist> { artist2 });

            var check = repository.GetByName<IArtist>("Tool");
            Assert.AreEqual(1, check.Count());
        }

        [Test]
        public void CanBeDeleted()
        {
            repository.Save(new List<IArtist> { artist3, artist4 });
            Assert.IsNotNull(repository.GetByLocation<IArtist>(artist3.Location));
            Assert.IsNotNull(repository.GetByLocation<IArtist>(artist4.Location));

            repository.Delete<IArtist>(new List<Uri> { artist3.Location, artist4.Location });
            Assert.IsNull(repository.GetByLocation<IArtist>(artist3.Location));
            Assert.IsNull(repository.GetByLocation<IArtist>(artist4.Location));
        }
    }
}
