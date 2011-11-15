using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis;
using Gnosis.Data;
using Gnosis.Data.SQLite;
using Gnosis.Utilities;

namespace Gnosis.Tests.Unit.Data.SQLite
{
    [TestFixture]
    public class SavedAlbums
    {
        public SavedAlbums()
        {
            connection = connectionFactory.Create(connectionString);
            connection.Open();
            repository = new SQLiteAlbumRepository(logger, connection);
            repository.Initialize();
            repository.Save(new List<IAlbum> { album1, album2, album5 });
        }

        private const string connectionString = "Data Source=:memory:;Version=3;";
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();

        protected readonly ILogger logger = new DebugLogger();
        protected readonly IDbConnection connection;
        protected readonly IAlbumRepository repository;

        private const string radioheadUrn = "urn:uuid:27A19456-E6E9-463F-951D-98BB44356C65";
        private IAlbum album1 = new Album("OK Computer", new DateTime(1997, 9, 22), new Uri(radioheadUrn), "Radiohead", new Uri("http://example.com/image.jpg"));
        private IAlbum album2 = new Album("Undertow", new DateTime(1992, 3, 28), Guid.NewGuid().ToUrn(), "Tool", null);
        private IAlbum album3 = new Album("Free", new DateTime(2002, 7, 9), Guid.NewGuid().ToUrn(), "Cat Power", null);
        private IAlbum album4 = new Album("White Chalk", new DateTime(2008, 4, 30), Guid.NewGuid().ToUrn(), "PJ Harvey", new Uri("http://other.org/blah.png"));
        private IAlbum album5 = new Album("Pablo Honey", new DateTime(1993, 4, 4), new Uri(radioheadUrn), "Radiohead", new Uri("file:///some-stuff/blah/ph.jpg"));

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
        public void CanBeReadByLocation()
        {
            var check1 = repository.GetByLocation(album1.Location);
            Assert.IsNotNull(check1);
            Assert.AreEqual(album1.Title, check1.Title);
            Assert.AreEqual(album1.Released, check1.Released);
            Assert.AreEqual(album1.Artist, check1.Artist);
            Assert.AreEqual(album1.ArtistName, album1.ArtistName);
            Assert.AreEqual(album1.Thumbnail, check1.Thumbnail);
            var check2 = repository.GetByLocation(album2.Location);
            Assert.IsNotNull(check2);
            Assert.AreEqual(album2.Title, check2.Title);
            Assert.AreEqual(album2.Released, check2.Released);
            Assert.AreEqual(album2.Artist, check2.Artist);
            Assert.AreEqual(album2.ArtistName, check2.ArtistName);
            Assert.AreEqual(album2.Thumbnail, check2.Thumbnail);
        }

        [Test]
        public void CanBeReadByTitle()
        {
            var checks1 = repository.GetByTitle("OK%");
            Assert.AreEqual(1, checks1.Count());
            var check1 = checks1.FirstOrDefault();
            Assert.IsNotNull(check1);
            Assert.AreEqual(album1.Title, check1.Title);
            Assert.AreEqual(album1.Released, check1.Released);
            Assert.AreEqual(album1.Artist, check1.Artist);
            Assert.AreEqual(album1.ArtistName, album1.ArtistName);
            Assert.AreEqual(album1.Thumbnail, check1.Thumbnail);
            var checks2 = repository.GetByTitle(album2.Title);
            Assert.AreEqual(1, checks2.Count());
            var check2 = checks2.FirstOrDefault();
            Assert.IsNotNull(check2);
            Assert.AreEqual(album2.Title, check2.Title);
            Assert.AreEqual(album2.Released, check2.Released);
            Assert.AreEqual(album2.Artist, check2.Artist);
            Assert.AreEqual(album2.ArtistName, check2.ArtistName);
            Assert.AreEqual(album2.Thumbnail, check2.Thumbnail);
        }

        [Test]
        public void CanBeReadByArtistSortedByReleaseDate()
        {
            var check = repository.GetByArtist(new Uri(radioheadUrn));
            Assert.AreEqual(2, check.Count());
            Assert.AreEqual(album5.Title, check.First().Title);
            Assert.AreEqual(album1.Title, check.Last().Title);
        }

        [Test]
        public void AreUniqueByNameArtistTitleAndReleasDate()
        {
            repository.Save(new List<IAlbum> { album2 });

            var check = repository.GetByTitle("Undertow");
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
