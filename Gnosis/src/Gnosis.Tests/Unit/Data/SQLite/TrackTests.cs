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
    public class SavedTracks
    {
        public SavedTracks()
        {
            connection = connectionFactory.Create(connectionString);
            connection.Open();
            repository = new SQLiteTrackRepository(logger, connection);
            repository.Initialize();
            repository.Save(new List<ITrack> { track1, track2, track5 });
        }

        private const string connectionString = "Data Source=:memory:;Version=3;";
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();

        protected readonly ILogger logger = new DebugLogger();
        protected readonly IDbConnection connection;
        protected readonly IMediaItemRepository<ITrack> repository;

        private const string radioheadUrn = "urn:uuid:27A19456-E6E9-463F-951D-98BB44356C65";
        private const string okComputerUrn = "urn:uuid:FA6A7FD0-74A3-4D83-A363-13733C04BB85";
        private ITrack track1 = new GnosisTrack("Paranoid Android", DateTime.MinValue, DateTime.MaxValue, 2, TimeSpan.FromSeconds(220), new Uri(radioheadUrn), "Radiohead", new Uri(okComputerUrn), "OK Computer", new Uri("file:///audio/radiohead/paranoid_android.mp3"), MediaType.AudioMpeg, Guid.Empty.ToUrn(), "Administrator", new Uri("http://example.com/image1.jpg"));
        private ITrack track2 = new GnosisTrack("Sober", DateTime.MinValue, DateTime.MaxValue, 4, TimeSpan.FromSeconds(306), Guid.NewGuid().ToUrn(), "Tool", Guid.NewGuid().ToUrn(), "Undertow", new Uri("file:///audio/tool/sober.mp3"), MediaType.AudioMpeg, Guid.Empty.ToUrn(), "Administrator", new Uri("http://example.com/image2.jpg"));
        private ITrack track3 = new GnosisTrack("Maybe Not", DateTime.MinValue, DateTime.MaxValue, 7, TimeSpan.FromSeconds(189), Guid.NewGuid().ToUrn(), "Cat Power", Guid.NewGuid().ToUrn(), "Free", new Uri("file:///audio/cat_power/maybe_not.mp3"), MediaType.AudioMpeg, Guid.Empty.ToUrn(), "Administrator", new Uri("http://example.com/image3.jpg"));
        private ITrack track4 = new GnosisTrack("Silence", DateTime.MinValue, DateTime.MaxValue, 5, TimeSpan.FromSeconds(423), Guid.NewGuid().ToUrn(), "PJ Harvey", Guid.NewGuid().ToUrn(), "White Chalk", new Uri("file:///audio/pj_harvey/paranoid_android.mp3"), MediaType.AudioMpeg, Guid.Empty.ToUrn(), "Administrator", new Uri("http://other.org/blah.png"));
        private ITrack track5 = new GnosisTrack("Airbag", DateTime.MinValue, DateTime.MaxValue, 1, TimeSpan.FromSeconds(291), new Uri(radioheadUrn), "Radiohead", new Uri(okComputerUrn), "OK Computer", new Uri("file:///audio/radiohead/airbag.mp3"), MediaType.AudioMpeg, Guid.Empty.ToUrn(), "Administrator", new Uri("file:///some-stuff/blah/ph.jpg"));

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
        public void DefaultTrackCannotBeDeleted()
        {
            repository.Delete(new List<Uri> { GnosisTrack.Unknown.Location });
            var check = repository.GetByLocation(GnosisTrack.Unknown.Location);
            Assert.IsNotNull(check);
            Assert.AreEqual(check, GnosisTrack.Unknown);
        }

        [Test]
        public void CanBeReadByLocation()
        {
            var check1 = repository.GetByLocation(track1.Location);
            Assert.IsNotNull(check1);
            Assert.AreEqual(track1.Name, check1.Name);
            Assert.AreEqual(track1.Number, check1.Number);
            Assert.AreEqual(track1.Duration, check1.Duration);
            Assert.AreEqual(track1.Creator, check1.Creator);
            Assert.AreEqual(track1.CreatorName, track1.CreatorName);
            Assert.AreEqual(track1.Catalog, check1.Catalog);
            Assert.AreEqual(track1.CatalogName, check1.CatalogName);
            Assert.AreEqual(track1.Target, check1.Target);
            Assert.AreEqual(track1.TargetType, check1.TargetType);
            Assert.AreEqual(track1.Thumbnail, check1.Thumbnail);
            var check2 = repository.GetByLocation(track2.Location);
            Assert.IsNotNull(check2);
            Assert.AreEqual(track2.Name, check2.Name);
            Assert.AreEqual(track2.Number, check2.Number);
            Assert.AreEqual(track2.Duration, check2.Duration);
            Assert.AreEqual(track2.Creator, check2.Creator);
            Assert.AreEqual(track2.CreatorName, check2.CreatorName);
            Assert.AreEqual(track2.Catalog, check2.Catalog);
            Assert.AreEqual(track2.CatalogName, check2.CatalogName);
            Assert.AreEqual(track2.Target, check2.Target);
            Assert.AreEqual(track2.TargetType, check2.TargetType);
            Assert.AreEqual(track2.Thumbnail, check2.Thumbnail);
        }

        [Test]
        public void CanBeReadByTitle()
        {
            var checks1 = repository.GetByName("Paranoid%");
            Assert.AreEqual(1, checks1.Count());
            var check1 = checks1.FirstOrDefault();
            Assert.AreEqual(track1.Name, check1.Name);
            Assert.AreEqual(track1.Number, check1.Number);
            Assert.AreEqual(track1.Duration, check1.Duration);
            Assert.AreEqual(track1.Creator, check1.Creator);
            Assert.AreEqual(track1.CreatorName, track1.CreatorName);
            Assert.AreEqual(track1.Catalog, check1.Catalog);
            Assert.AreEqual(track1.CatalogName, check1.CatalogName);
            Assert.AreEqual(track1.Target, check1.Target);
            Assert.AreEqual(track1.TargetType, check1.TargetType);
            Assert.AreEqual(track1.Thumbnail, check1.Thumbnail);
            var checks2 = repository.GetByName(track2.Name);
            Assert.AreEqual(1, checks2.Count());
            var check2 = checks2.FirstOrDefault();
            Assert.IsNotNull(check2);
            Assert.AreEqual(track2.Name, check2.Name);
            Assert.AreEqual(track2.Number, check2.Number);
            Assert.AreEqual(track2.Duration, check2.Duration);
            Assert.AreEqual(track2.Creator, check2.Creator);
            Assert.AreEqual(track2.CreatorName, check2.CreatorName);
            Assert.AreEqual(track2.Catalog, check2.Catalog);
            Assert.AreEqual(track2.CatalogName, check2.CatalogName);
            Assert.AreEqual(track2.Target, check2.Target);
            Assert.AreEqual(track2.TargetType, check2.TargetType);
            Assert.AreEqual(track2.Thumbnail, check2.Thumbnail);
        }

        [Test]
        public void CanBeReadBytrackSortedByNumber()
        {
            var check = repository.GetByCatalog(new Uri(okComputerUrn));
            Assert.AreEqual(2, check.Count());
            Assert.AreEqual(track5.Name, check.First().Name);
            Assert.AreEqual(track5.Number, check.First().Number);
            Assert.AreEqual(track1.Name, check.Last().Name);
            Assert.AreEqual(track1.Number, check.Last().Number);
        }

        [Test]
        public void AreUniqueByAudioLocation()
        {
            repository.Save(new List<ITrack> { track2 });

            var check = repository.GetByName("Sober");
            Assert.AreEqual(1, check.Count());
        }

        [Test]
        public void CanBeReadByTarget()
        {
            var check = repository.GetByTarget(track1.Target).FirstOrDefault();
            Assert.IsNotNull(check);
            Assert.AreEqual(track1.Name, check.Name);
        }

        [Test]
        public void CanBeDeleted()
        {
            repository.Save(new List<ITrack> { track3, track4 });
            Assert.IsNotNull(repository.GetByLocation(track3.Location));
            Assert.IsNotNull(repository.GetByLocation(track4.Location));

            repository.Delete(new List<Uri> { track3.Location, track4.Location });
            Assert.IsNull(repository.GetByLocation(track3.Location));
            Assert.IsNull(repository.GetByLocation(track4.Location));
        }
    }
}
