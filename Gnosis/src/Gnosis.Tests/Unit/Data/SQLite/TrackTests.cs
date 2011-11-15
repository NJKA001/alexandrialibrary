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
        protected readonly ITrackRepository repository;

        private const string radioheadUrn = "urn:uuid:27A19456-E6E9-463F-951D-98BB44356C65";
        private const string okComputerUrn = "urn:uuid:FA6A7FD0-74A3-4D83-A363-13733C04BB85";
        private ITrack track1 = new Track("Paranoid Android", 2, TimeSpan.FromSeconds(220), new Uri(radioheadUrn), "Radiohead", new Uri(okComputerUrn), "OK Computer", new Uri("file:///audio/radiohead/paranoid_android.mp3"), MediaType.AudioMpeg, new Uri("http://example.com/image.jpg"));
        private ITrack track2 = new Track("Sober", 4, TimeSpan.FromSeconds(306),  Guid.NewGuid().ToUrn(), "Tool", Guid.NewGuid().ToUrn(), "Undertow", new Uri("file:///audio/tool/sober.mp3"), MediaType.AudioMpeg, null);
        private ITrack track3 = new Track("Maybe Not", 7, TimeSpan.FromSeconds(189), Guid.NewGuid().ToUrn(), "Cat Power", Guid.NewGuid().ToUrn(), "Free", new Uri("file:///audio/cat_power/maybe_not.mp3"), MediaType.AudioMpeg, null);
        private ITrack track4 = new Track("Silence", 5, TimeSpan.FromSeconds(423), Guid.NewGuid().ToUrn(), "PJ Harvey", Guid.NewGuid().ToUrn(), "White Chalk", new Uri("file:///audio/pj_harvey/paranoid_android.mp3"), MediaType.AudioMpeg, new Uri("http://other.org/blah.png"));
        private ITrack track5 = new Track("Airbag", 1, TimeSpan.FromSeconds(291), new Uri(radioheadUrn), "Radiohead", new Uri(okComputerUrn), "OK Computer", new Uri("file:///audio/radiohead/airbag.mp3"), MediaType.AudioMpeg, new Uri("file:///some-stuff/blah/ph.jpg"));

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
            var check1 = repository.GetByLocation(track1.Location);
            Assert.IsNotNull(check1);
            Assert.AreEqual(track1.Title, check1.Title);
            Assert.AreEqual(track1.Number, check1.Number);
            Assert.AreEqual(track1.Duration, check1.Duration);
            Assert.AreEqual(track1.Artist, check1.Artist);
            Assert.AreEqual(track1.ArtistName, track1.ArtistName);
            Assert.AreEqual(track1.Album, check1.Album);
            Assert.AreEqual(track1.AlbumTitle, check1.AlbumTitle);
            Assert.AreEqual(track1.AudioLocation, check1.AudioLocation);
            Assert.AreEqual(track1.AudioType, check1.AudioType);
            Assert.AreEqual(track1.Thumbnail, check1.Thumbnail);
            var check2 = repository.GetByLocation(track2.Location);
            Assert.IsNotNull(check2);
            Assert.AreEqual(track2.Title, check2.Title);
            Assert.AreEqual(track2.Number, check2.Number);
            Assert.AreEqual(track2.Duration, check2.Duration);
            Assert.AreEqual(track2.Artist, check2.Artist);
            Assert.AreEqual(track2.ArtistName, check2.ArtistName);
            Assert.AreEqual(track2.Album, check2.Album);
            Assert.AreEqual(track2.AlbumTitle, check2.AlbumTitle);
            Assert.AreEqual(track2.AudioLocation, check2.AudioLocation);
            Assert.AreEqual(track2.AudioType, check2.AudioType);
            Assert.AreEqual(track2.Thumbnail, check2.Thumbnail);
        }

        [Test]
        public void CanBeReadByTitle()
        {
            var checks1 = repository.GetByTitle("Paranoid%");
            Assert.AreEqual(1, checks1.Count());
            var check1 = checks1.FirstOrDefault();
            Assert.AreEqual(track1.Title, check1.Title);
            Assert.AreEqual(track1.Number, check1.Number);
            Assert.AreEqual(track1.Duration, check1.Duration);
            Assert.AreEqual(track1.Artist, check1.Artist);
            Assert.AreEqual(track1.ArtistName, track1.ArtistName);
            Assert.AreEqual(track1.Album, check1.Album);
            Assert.AreEqual(track1.AlbumTitle, check1.AlbumTitle);
            Assert.AreEqual(track1.AudioLocation, check1.AudioLocation);
            Assert.AreEqual(track1.AudioType, check1.AudioType);
            Assert.AreEqual(track1.Thumbnail, check1.Thumbnail);
            var checks2 = repository.GetByTitle(track2.Title);
            Assert.AreEqual(1, checks2.Count());
            var check2 = checks2.FirstOrDefault();
            Assert.IsNotNull(check2);
            Assert.AreEqual(track2.Title, check2.Title);
            Assert.AreEqual(track2.Number, check2.Number);
            Assert.AreEqual(track2.Duration, check2.Duration);
            Assert.AreEqual(track2.Artist, check2.Artist);
            Assert.AreEqual(track2.ArtistName, check2.ArtistName);
            Assert.AreEqual(track2.Album, check2.Album);
            Assert.AreEqual(track2.AlbumTitle, check2.AlbumTitle);
            Assert.AreEqual(track2.AudioLocation, check2.AudioLocation);
            Assert.AreEqual(track2.AudioType, check2.AudioType);
            Assert.AreEqual(track2.Thumbnail, check2.Thumbnail);
        }

        [Test]
        public void CanBeReadBytrackSortedByNumber()
        {
            var check = repository.GetByAlbum(new Uri(okComputerUrn));
            Assert.AreEqual(2, check.Count());
            Assert.AreEqual(track5.Title, check.First().Title);
            Assert.AreEqual(track1.Title, check.Last().Title);
        }

        [Test]
        public void AreUniqueByAudioLocation()
        {
            repository.Save(new List<ITrack> { track2 });

            var check = repository.GetByTitle("Sober");
            Assert.AreEqual(1, check.Count());
        }

        [Test]
        public void CanBeReadByAudioLocation()
        {
            var check = repository.GetByAudioLocation(track1.AudioLocation);
            Assert.IsNotNull(check);
            Assert.AreEqual(track1.Title, check.Title);
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
