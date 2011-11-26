using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Algorithms;
using Gnosis.Tags;
using Gnosis.Tags.Id3;
using Gnosis.Tags.Id3.Id3v1;
using Gnosis.Tags.Id3.Id3v2;
using Gnosis.Utilities;
using Gnosis.Data;
using Gnosis.Data.SQLite;

using NUnit.Framework;

namespace Gnosis.Tests.Unit.Data.SQLite
{
    public abstract class TagTestBase
    {
        protected TagTestBase()
        {
            connection = connectionFactory.Create("Data Source=:memory:;Version=3;");
            connection.Open();
            typeFactory = new TagTypeFactory();
            repository = new SQLiteTagRepository(logger, typeFactory, connection);
            repository.Initialize();
        }

        private readonly ILogger logger = new DebugLogger();
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();
        private readonly IDbConnection connection;
        private readonly ITagTypeFactory typeFactory = new TagTypeFactory();
        protected readonly SQLiteTagRepository repository;

        #region Test Values

        protected readonly Uri uri1 = new Uri("http://arstechnica.com/index.ars");
        protected readonly Uri uri2 = new Uri(@"C:\Users\dpoage\Music\Tool\Undertown\Bottom.mp3");
        protected readonly Uri uri3 = new Uri("http://flickr.com/users/dpoage/example.jpg");
        protected readonly Uri uri4 = new Uri(@"C:\Users\dpoage\Pictures\icon.gif");
        protected readonly Uri uri5 = new Uri("http://meh.org/index/Blah/abcdefghi.mp3");
        protected readonly Uri uri6 = new Uri("http://blah.com/music/Ticks-And-Leeches");
        protected readonly Uri uri7 = new Uri("http://meh.org/index/Tool/The+Bottom.mp3");

        #endregion

        protected void Cleanup()
        {
            connection.Close();
        }
    }

    [TestFixture]
    public class SavedTags
        : TagTestBase
    {
        public SavedTags()
        {
            var image = System.Drawing.Image.FromFile(@".\Files\Undertow.jpg");
            //Assert.IsNotNull(image);
            imageData = image.ToBytes();
            //Assert.IsNotNull(imageData);

            var tag1 = new Tag(uri1, TagType.DefaultString, "Default", 0, "Tool Kicks Ass!");
            var tag2 = new Tag(uri2, Id3v1TagType.Artist, "TPE1", 0, "Tool");
            var tag3 = new Tag(uri3, Id3v1TagType.Artist, "TPE1", 0, "Tool");
            var tag4 = new Tag(uri3, Id3v1TagType.Title, "Title", 0, "Ticks & Leeches 1".ToAmericanizedString());
            var tag5 = new Tag(uri4, Id3v2TagType.Artist, "TPE1", 0, "Tool");
            var tag6 = new Tag(uri4, Id3v2TagType.Title, "TIT1", 0, "The Bottom");
            var tag7 = new Tag(uri4, Id3v2TagType.Album, "TALB", 0, "Undertow");
            var tag8 = new Tag(uri4, Id3v2TagType.AttachedPicture, "Album Cover", 0, imageData);
            var tag9 = new Tag(uri4, Id3v2TagType.ReleaseTime, "TDRL", 0, releaseDate);
            var tag10 = new Tag(uri5, Id3v2TagType.Artist, "TPE1", 0, artist);
            var tag11 = new Tag(uri5, Id3v1TagType.Genre, "Genre", 0, Id3v1Genre.Rock_and_Roll);

            var tags = new List<ITag> { tag1, tag2, tag3, tag4, tag5, tag6, tag7, tag8, tag9, tag10, tag11 };
            repository.Save(tags);
        }

        //protected string[] artists = new string[] { "Aa", "Bb", "Cc", "Dd", "Ee", "Ff", "Gg", "Hi", "Ii" };
        protected string artist = "Some Example Artist Name";
        protected byte[] imageData;
        protected DateTime releaseDate = new DateTime(2011, 2, 19);

        [TestFixtureSetUp]
        public void SetUp()
        {
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Cleanup();
        }

        [Test]
        public void CanBeReadByAlgorithm()
        {
            var tool = repository.GetByAlgorithm(Algorithm.Default, TagDomain.String, "Tool%");
            Assert.IsNotNull(tool);
            Assert.AreEqual(3, tool.Count());
        }

        [Test]
        public void CanBeReadByTarget()
        {
            var pic = repository.GetByTarget(uri4, Id3v2TagType.AttachedPicture).FirstOrDefault();
            Assert.IsNotNull(pic);
            Assert.AreEqual(imageData, pic.Value);

            var dateTag = repository.GetByTarget(uri4, Id3v2TagType.ReleaseTime).FirstOrDefault();
            Assert.IsNotNull(dateTag);
            Assert.AreEqual(releaseDate, dateTag.Value);

            var arrayTag = repository.GetByTarget(uri5, Id3v2TagType.Artist).FirstOrDefault();
            Assert.IsNotNull(arrayTag);
            Assert.AreEqual(artist, arrayTag.Value);

            var genreTag = repository.GetByTarget(uri5, Id3v1TagType.Genre).FirstOrDefault();
            Assert.IsNotNull(genreTag);
            Assert.AreEqual(Id3v1Genre.Rock_and_Roll, genreTag.Value);
        }

        [Test]
        public void CanBeSearchedForArtistsAndOtherStrings()
        {
            var results = new List<ITag>();

            var task = repository.Search(Algorithm.Default, "Tool%");
            task.AddResultsCallback(t => results.AddRange(t));
            task.StartSynchronously();

            Assert.AreEqual(4, results.Count);
        }

        [Test]
        public void CanBeSearchedForGenres()
        {
            var results = new List<ITag>();
            var task = repository.Search(Algorithm.Default, "Rock%");
            task.AddResultsCallback(x => results.AddRange(x));
            task.StartSynchronously();

            var filteredResults = results.Where(x => x.Type == Id3v1TagType.Genre);
            Assert.AreEqual(1, filteredResults.Count());
            Assert.IsNotNull(results.First());
            Assert.AreEqual(Id3v1Genre.Rock_and_Roll, filteredResults.First().Value);
        }
    }
}
