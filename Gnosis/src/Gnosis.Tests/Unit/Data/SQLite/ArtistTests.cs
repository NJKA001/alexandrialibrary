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
using Gnosis.Utilities;

namespace Gnosis.Tests.Unit.Data.SQLite
{
    [TestFixture]
    public class SavedArtists
    {
        public SavedArtists()
        {
            connection = connectionFactory.Create(connectionString);
            connection.Open();
            repository = new SQLiteArtistRepository(logger, connection);
            repository.Initialize();
            repository.Save(new List<IArtist> { artist1, artist2 });
        }

        private const string connectionString = "Data Source=:memory:;Version=3;";
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();

        protected readonly ILogger logger = new DebugLogger();
        protected readonly IDbConnection connection;
        protected readonly IMediaItemRepository<IArtist> repository;

        private IArtist artist1 = new GnosisArtist("Radiohead", string.Empty, new DateTime(1985, 1, 2), DateTime.MaxValue, Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, GnosisUser.Administrator.Location, GnosisUser.Administrator.Name, new Uri("http://example.com/image.jpg"), new byte[0]);
        private IArtist artist2 = new GnosisArtist("Tool", string.Empty, new DateTime(1991, 2, 28), DateTime.MaxValue, Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, GnosisUser.Administrator.Location, GnosisUser.Administrator.Name, new Uri("http://example.com/image2.jpg"), new byte[0]);
        private IArtist artist3 = new GnosisArtist("Cat Power", string.Empty, new DateTime(1997, 10, 15), DateTime.MaxValue, Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, GnosisUser.Administrator.Location, GnosisUser.Administrator.Name, new Uri("http://example.com/image3.jpg"), new byte[0]);
        private IArtist artist4 = new GnosisArtist("PJ Harvey", string.Empty, new DateTime(2011, 11, 11), DateTime.MaxValue, Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, GnosisUser.Administrator.Location, GnosisUser.Administrator.Name, new Uri("http://example.com/image4.jpg"), new byte[0]);

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
            repository.Delete(new List<Uri> { GnosisArtist.Unknown.Location });
            var check = repository.GetByLocation(GnosisArtist.Unknown.Location);
            Assert.IsNotNull(check);
            Assert.AreEqual(check, GnosisArtist.Unknown);
        }

        [Test]
        public void CanBeReadByLocation()
        {
            var check1 = repository.GetByLocation(artist1.Location);
            Assert.IsNotNull(check1);
            Assert.AreEqual(artist1.Name, check1.Name);
            Assert.AreEqual(artist1.FromDate, check1.FromDate);
            Assert.AreEqual(artist1.ToDate, check1.ToDate);
            Assert.AreEqual(artist1.Thumbnail, check1.Thumbnail);
            var check2 = repository.GetByLocation(artist2.Location);
            Assert.IsNotNull(check2);
            Assert.AreEqual(artist2.Name, check2.Name);
            Assert.AreEqual(artist2.FromDate, check2.FromDate);
            Assert.AreEqual(artist2.ToDate, check2.ToDate);
            Assert.AreEqual(artist2.Thumbnail, check2.Thumbnail);
        }

        [Test]
        public void CanBeReadByName()
        {
            var checks1 = repository.GetByName("Radio%");
            Assert.AreEqual(1, checks1.Count());
            var check1 = checks1.FirstOrDefault();
            Assert.IsNotNull(check1);
            Assert.AreEqual(artist1.Name, check1.Name);
            Assert.AreEqual(artist1.FromDate, check1.FromDate);
            Assert.AreEqual(artist1.ToDate, check1.ToDate);
            Assert.AreEqual(artist1.Thumbnail, check1.Thumbnail);
            var checks2 = repository.GetByName(artist2.Name);
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

            var check = repository.GetByName("Tool");
            Assert.AreEqual(1, check.Count());
        }

        [Test]
        public void CanBeDeleted()
        {
            repository.Save(new List<IArtist> { artist3, artist4 });
            Assert.IsNotNull(repository.GetByLocation(artist3.Location));
            Assert.IsNotNull(repository.GetByLocation(artist4.Location));

            repository.Delete(new List<Uri> { artist3.Location, artist4.Location });
            Assert.IsNull(repository.GetByLocation(artist3.Location));
            Assert.IsNull(repository.GetByLocation(artist4.Location));
        }
    }
}
