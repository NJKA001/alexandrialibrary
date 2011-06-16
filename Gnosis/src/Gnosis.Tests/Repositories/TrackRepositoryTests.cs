using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;
using System.Data.SQLite;

using Gnosis.Core;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Tracks;
using Gnosis.Alexandria.Repositories.Tracks;

namespace Gnosis.Tests.Repositories
{
    [TestFixture]
    public class TrackRepositoryTests
    {
        private IContext context;
        private ILogger logger;
        private ITrackRepository repository;
        private IDbConnection connection;

        private const string location = @"C:\Users\Dan\Music\Tool\Undertow\01 Intolerance.mp3";
        private const string mediaType = "audio/mpeg";
        private const string album = "Undertow";
        private const string artists = "Tool";
        private const string title = "Intolerance";
        private const string comment = "First track on their first LP";
        private const int discCount = 1;
        private const int discNumber = 1;
        private const int durationSeconds = 294;
        private DateTime releaseDate = new DateTime(1993, 4, 6);
        private DateTime recodingDate = new DateTime(1992, 1, 1);
        private DateTime encodingDate = new DateTime(2000, 5, 17);
        private const string genres = "Rock; Metal; Alternative Metal; Heavy Metal; Progressive Metal";
        private const string languages = "en-us";
        private const string moods = "Angry; Angst-Rodden; Bleak; Harsh; Menacing; Suffocating; Confrontational; Eerie; Fierce; Gloomy; Intense";
        private ulong playCount = 397;
        private const int playlistDelaySeconds = 2;
        private const int trackCount = 69;
        private const int trackNumber = 1;

        private const string coverImageDescription = "Undertow";
        private const string coverImageTextEncoding = "UTF-8";
        private const string coverImageLocation = @"Files\Undertow.jpg";
        private const string coverImageMediaType = "image/jpg";
        private Image coverImage = Image.FromFile(coverImageLocation);

        private Uri allMusicScheme = new Uri("http://allmusic.com");
        private string allMusicId = "R   169347";

        private Uri musicBrainzReleaseScheme = new Uri("http://musicbrainz.org/trackId");
        private string musicBrainzReleaseId = "9afffc81-c4b9-46aa-baef-232a45817158";

        private Uri ratingUser = new Uri("mailto:bob.smith@yahoo.com");
        private byte ratingScore = 9;
        private ulong ratingPlayCount = 138;

        #region Helpers Methods

        private int GetCount(string sql)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                return int.Parse(command.ExecuteScalar().ToString());
            }
        }

        private ITrack GetTestTrack()
        {
            var track = new Track();
            track.Initialize(new EntityInitialState(context, logger));

            track.Album = album;
            track.AlbumSort = album;
            track.Artists = artists;
            track.ArtistsSort = artists;
            track.Comment = comment;
            track.DiscCount = discCount;
            track.DiscNumber = discNumber;
            track.Duration = new TimeSpan(0, 0, durationSeconds);
            track.EncodingDate = encodingDate;
            track.Genres = genres;
            track.Languages = languages;
            track.Location = new Uri(location);
            track.MediaType = mediaType;
            track.Moods = moods;
            track.PlayCount = playCount;
            track.PlaylistDelay = new TimeSpan(0, 0, playlistDelaySeconds);
            track.TrackCount = trackCount;
            track.TrackNumber = trackNumber;
            track.Title = title;
            track.TitleSort = title;

            track.AddIdentifier(allMusicScheme, allMusicId);
            track.AddIdentifier(musicBrainzReleaseScheme, musicBrainzReleaseId);
            track.AddPicture(coverImageTextEncoding, coverImageMediaType, TrackPictureType.FrontCover, coverImageDescription, coverImage.ToBytes());
            track.AddRating(ratingScore, ratingUser, ratingPlayCount);
            track.AddLink("UTF-8", "alt", new Uri("http://random.org/some-random-path/example.html"));

            return track;
        }

        #endregion

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            context = new SingleThreadedContext();
            logger = new DebugLogger();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
        }

        [SetUp]
        public void SetUp()
        {
            connection = new SQLiteConnection("Data Source=:memory:;Version=3;");
            connection.Open();

            repository = new TrackRepository(context, logger, connection);
            repository.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }
        }

        [Test]
        public void CreateTrack()
        {
            var initialTracks = repository.Search();
            Assert.AreEqual(0, initialTracks.Count());
            Assert.IsTrue(File.Exists(coverImageLocation));

            var track = GetTestTrack();
            var id = track.Id;
            Assert.IsTrue(track.IsNew());
            Assert.IsNotNull(track.Pictures.FirstOrDefault());
            Assert.IsNotNull(track.Identifiers.FirstOrDefault());

            repository.Save(new List<ITrack> { track });

            Assert.IsNotNull(repository.Lookup(id));

            var lookupTrack = repository.LookupByLocation(new Uri(location));
            Assert.IsNotNull(lookupTrack);
            Assert.IsFalse(lookupTrack.IsChanged());
            Assert.AreEqual(id, lookupTrack.Id);

            Assert.AreEqual(album, track.Album);
            Assert.AreEqual(album, lookupTrack.AlbumSort);
            Assert.AreEqual(artists, lookupTrack.Artists);
            Assert.AreEqual(artists, lookupTrack.ArtistsSort);
            Assert.AreEqual(comment, lookupTrack.Comment);
            Assert.AreEqual(discCount, lookupTrack.DiscCount);
            Assert.AreEqual(discNumber, lookupTrack.DiscNumber);
            Assert.AreEqual(durationSeconds, lookupTrack.Duration.TotalSeconds);
            Assert.AreEqual(encodingDate, lookupTrack.EncodingDate);
            Assert.AreEqual(genres, lookupTrack.Genres = genres);
            Assert.AreEqual(languages, lookupTrack.Languages);
            Assert.AreEqual(location, lookupTrack.Location.LocalPath);
            Assert.AreEqual(mediaType, lookupTrack.MediaType);
            Assert.AreEqual(moods, lookupTrack.Moods);
            Assert.AreEqual(playCount, lookupTrack.PlayCount);
            Assert.AreEqual(playlistDelaySeconds, lookupTrack.PlaylistDelay.TotalSeconds);
            Assert.AreEqual(trackCount, lookupTrack.TrackCount);
            Assert.AreEqual(trackNumber, lookupTrack.TrackNumber);
            Assert.AreEqual(title, lookupTrack.Title);
            Assert.AreEqual(title, lookupTrack.TitleSort);

            Assert.IsNotNull(lookupTrack.Identifiers.FirstOrDefault());
            Assert.AreEqual(2, lookupTrack.Identifiers.Count());

            Assert.IsNotNull(lookupTrack.Pictures.FirstOrDefault());
            Assert.IsNotNull(lookupTrack.Ratings.FirstOrDefault());

            var firstId = lookupTrack.Identifiers.FirstOrDefault();
            Assert.IsNotNull(firstId);
            Assert.AreEqual(allMusicScheme, firstId.Scheme);
            Assert.AreEqual(allMusicId, firstId.Identifier);

            var secondId = lookupTrack.Identifiers.LastOrDefault();
            Assert.IsNotNull(secondId);
            Assert.AreEqual(musicBrainzReleaseScheme, secondId.Scheme);
            Assert.AreEqual(musicBrainzReleaseId, secondId.Identifier);

            var firstPicture = lookupTrack.Pictures.FirstOrDefault();
            Assert.IsNotNull(firstPicture.Data);
            Assert.IsTrue(firstPicture.Data.Length == coverImage.ToBytes().Length);

            var firstRating = lookupTrack.Ratings.FirstOrDefault();
            Assert.AreEqual(ratingScore, firstRating.Score);
            Assert.AreEqual(ratingUser, firstRating.User);
            Assert.AreEqual(ratingPlayCount, firstRating.PlayCount);
        }

        [Test]
        public void UpdateTrack()
        {
            const int trackNumber = 4;
            const string title = "Bottom";
            const string artists = "Tool; Henry Rollins";
            const string composers = "Tool; Henry Rollins";
            const string comment = "Guest vocals by Henry Rollins";
            const int durationSeconds = 433;
            
            var musicBrainzScheme = new Uri("http://musicbrainz.org/trackId");
            const string musicBrainzTrackId = "002e6458-fee5-4bf1-b602-2037d14fd3ce";

            var ratingUser = new Uri("mailto:sally_jones@hotmail.com");
            const byte ratingScore = 7;
            const ulong ratingPlayCount = 50;

            var track = GetTestTrack();
            var id = track.Id;
            repository.Save(new List<ITrack> { track });
            Assert.IsFalse(track.IsChanged());

            track.TrackNumber = trackNumber;
            Assert.IsTrue(track.IsChanged());

            track.Artists = artists;
            track.Composers = composers;
            track.Title = title;
            track.TitleSort = title;
            track.Comment = comment;
            track.Duration = new TimeSpan(0, 0, durationSeconds);

            track.AddIdentifier(musicBrainzScheme, musicBrainzTrackId);
            track.AddRating(ratingScore, ratingUser, ratingPlayCount);
            track.RemovePicture(track.Pictures.FirstOrDefault());

            repository.Save(new List<ITrack> { track });
            var changedTrack = repository.Lookup(id);
            Assert.IsNotNull(changedTrack);
            Assert.AreEqual(2, changedTrack.Ratings.Count());
            Assert.AreEqual(3, changedTrack.Identifiers.Count());
            Assert.AreEqual(0, changedTrack.Pictures.Count());
        }

        [Test]
        public void DeleteTrack()
        {
            var track = GetTestTrack();
            var id = track.Id;
            var linkCountSql = string.Format("select count() from Track_Links where Parent = '{0}';", id);
            repository.Save(new List<ITrack> { track });

            var track2 = new Track();
            track2.Initialize(new EntityInitialState(context, logger));
            var id2 = track2.Id;
            var linkCount2Sql = string.Format("select count() from Track_Links where Parent = '{0}';", id2);
            track2.Location = new Uri("http://example.com/feeds/45235235/some-podcast.mp3");
            track2.AddLink("UTF-8", "alt", new Uri("http://example.com/feeds/2525235/summary.html"));
            track2.AddLink("UTF-8", "self", new Uri("http://example.com/feeds/45235235/some-podcast.mp3"));
            track2.AddLink("UTF-8", "alt", new Uri("http://nowhere.com/editorials/index"));
            repository.Save(new List<ITrack> { track2 });
            Assert.AreEqual(2, repository.Search().Count());
            Assert.AreEqual(1, GetCount(linkCountSql));
            Assert.AreEqual(3, GetCount(linkCount2Sql));

            repository.Delete(new List<ITrack> {track});
            Assert.AreEqual(1, repository.Search().Count());
            Assert.IsNull(repository.Lookup(id));
            Assert.IsNotNull(repository.Lookup(id2));
            Assert.AreEqual(0, GetCount(linkCountSql));
            Assert.AreEqual(3, GetCount(linkCount2Sql));
        }
    }
}
