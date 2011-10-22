using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Loggers;
using Gnosis.Core;
using Gnosis.Core.Tags.Id3;
using Gnosis.Core.Tags.Id3.Id3v1;
using Gnosis.Core.Tags.Id3.Id3v2;
using Gnosis.Data;
using Gnosis.Data.SQLite;

using NUnit.Framework;

namespace Gnosis.Tests.Data.SQLite
{
    [TestFixture]
    public class SQLiteTests
    {
        public SQLiteTests()
        {
            connection = connectionFactory.Create("Data Source=:memory:;Version=3;");
            connection.Open();
            typeFactory = new TagTypeFactory();

            tagRepository = new SQLiteTagRepository(logger, typeFactory, connection);
            mediaRepository = new SQLiteMediaRepository(logger, connection);
            mediaFactory = new MediaFactory();
        }

        private readonly ILogger logger = new DebugLogger();
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();
        private readonly IDbConnection connection;
        private readonly ITagTypeFactory typeFactory;
        private readonly SQLiteTagRepository tagRepository;
        private readonly SQLiteMediaRepository mediaRepository;
        private readonly MediaFactory mediaFactory;

        private readonly Uri uri1 = new Uri("http://arstechnica.com/index.ars");
        private readonly Uri uri2 = new Uri(@"C:\Users\dpoage\Music\Tool\Undertown\Bottom.mp3");
        private readonly Uri uri3 = new Uri("http://flickr.com/users/dpoage/example.jpg");
        private readonly Uri uri4 = new Uri(@"C:\Users\dpoage\Pictures\icon.gif");

        [TestFixtureSetUp]
        public void SetUp()
        {
            tagRepository.Initialize();
            mediaRepository.Initialize();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }

        [Test]
        public void BulkTagTest()
        {
            var random = new Random(57);

            using (var reader = new System.IO.StreamReader(@".\Files\words1.txt"))
            {
                const string urlBase = "http://example.com/media/random/";
                var line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    var tags = new List<ITag>();
                    //var words = new Stack<string>(line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                    //var num = random.Next(7) + 1;
                    //while (words.Count > num)
                    //{

                    //}

                    //if (words.Count > 0)
                    //{
                    //    //tags.Add(new Tag(new Uri(urlBase + Guid.NewGuid().ToString()), TagType.
                    //}

                    foreach (var word in line.Split(' '))
                    {
                        System.Diagnostics.Debug.WriteLine("tag for word: " + word);
                        tags.Add(new Tag(new Uri(urlBase + Guid.NewGuid().ToString()), TagType.DefaultString, word));
                    }
                    tagRepository.Save(tags);
                }

                System.Diagnostics.Debug.WriteLine("Before: " + DateTime.Now.Millisecond);
                var tagsA = tagRepository.GetByAlgorithm(Algorithm.Default, TagDomain.String, "a%");
                System.Diagnostics.Debug.WriteLine("After: " + DateTime.Now.Millisecond);

                var count = tagsA.Count();
                System.Diagnostics.Debug.WriteLine("count=" + count);
                Assert.IsTrue(count > 0);
            }
        }

        [Test]
        public void TagRepositorySaveTest()
        {
            var uri3 = new Uri("http://blah.com/music/Ticks-And-Leeches");
            var uri4 = new Uri("http://meh.org/index/Tool/The+Bottom.mp3");
            var uri5 = new Uri("http://meh.org/index/Blah/abcdefghi.mp3");

            var image = System.Drawing.Image.FromFile(@".\Files\Undertow.jpg");
            Assert.IsNotNull(image);
            var imageData = image.ToBytes();
            Assert.IsNotNull(imageData);
            var releaseDate = new DateTime(2011, 2, 19);
            var artists = new string[] { "Aa", "Bb", "Cc", "Dd", "Ee", "Ff", "Gg", "Hi", "Ii" };


            var tag1 = new Tag(uri1, TagType.DefaultString, "Tool Kicks Ass!");
            var tag2 = new Tag(uri2, Id3v1TagType.Artist, "Tool");
            var tag3 = new Tag(uri3, Id3v1TagType.Artist, "Tool");
            var tag4 = new Tag(uri3, Id3v1TagType.Title, "Ticks & Leeches 1".ToAmericanizedString());
            var tag5 = new Tag(uri4, Id3v2TagType.Artist, new string[] { "Tool" });
            var tag6 = new Tag(uri4, Id3v2TagType.Title, "The Bottom");
            var tag7 = new Tag(uri4, Id3v2TagType.Album, "Undertow");
            var tag8 = new Tag(uri4, Id3v2TagType.AttachedPicture, imageData);
            var tag9 = new Tag(uri4, Id3v2TagType.ReleaseTime, releaseDate);
            var tag10 = new Tag(uri5, Id3v2TagType.Artist, artists);
            var tag11 = new Tag(uri5, Id3v1TagType.Genre, Id3v1Genre.Rock_and_Roll);

            var tags = new List<ITag> { tag1, tag2, tag3, tag4, tag5, tag6, tag7, tag8, tag9, tag10, tag11 };
            tagRepository.Save(tags);

            var tool = tagRepository.GetByAlgorithm(Algorithm.Default, TagDomain.String, "Tool%");
            Assert.IsNotNull(tool);
            Assert.AreEqual(3, tool.Count());

            var pic = tagRepository.GetByTarget(uri4, Id3v2TagType.AttachedPicture).FirstOrDefault();
            Assert.IsNotNull(pic);
            Assert.AreEqual(imageData, pic.Value);

            var dateTag = tagRepository.GetByTarget(uri4, Id3v2TagType.ReleaseTime).FirstOrDefault();
            Assert.IsNotNull(dateTag);
            Assert.AreEqual(releaseDate, dateTag.Value);

            var arrayTag = tagRepository.GetByTarget(uri5, Id3v2TagType.Artist).FirstOrDefault();
            Assert.IsNotNull(arrayTag);
            Assert.AreEqual(artists, arrayTag.Value);

            var genreTag = tagRepository.GetByTarget(uri5, Id3v1TagType.Genre).FirstOrDefault();
            Assert.IsNotNull(genreTag);
            Assert.AreEqual(Id3v1Genre.Rock_and_Roll, genreTag.Value);

            var taskResults = new List<ITag>();
            //var completed = false;
            var task1 = tagRepository.Search(Algorithm.Default, "Tool%");
            task1.AddResultsCallback(t => taskResults.AddRange(t));
            task1.StartSynchronously();

            //t => taskResults.AddRange(t), () => completed = true);
            
            //while(!completed)
                //System.Threading.Thread.Sleep(100);
            
            Assert.AreEqual(4, taskResults.Count);

            var genreResults = new List<ITag>();
            //var genreCompleted = false;
            var genreTask = tagRepository.Search(Algorithm.Default, "Rock%");
            genreTask.AddResultsCallback(x => genreResults.AddRange(x));
            genreTask.StartSynchronously();
                //t => genreResults.AddRange(t), () => genreCompleted = true);

            //while (!genreCompleted)
                //System.Threading.Thread.Sleep(100);

            var filteredGenreResults = genreResults.Where(x => x.Type == Id3v1TagType.Genre);
            Assert.AreEqual(1, filteredGenreResults.Count());
            Assert.IsNotNull(genreResults.First());
            Assert.AreEqual(Id3v1Genre.Rock_and_Roll, filteredGenreResults.First().Value);

            //var americanized = tagRepository.Search(Algorithm.Americanized, TagSchema.Id3v1);
            //var v2 = tagRepository.Search(Algorithm.Default, TagSchema.Id3v2);
            //var apic = tagRepository.Search(Algorithm.Default, Id3v2TagType.AttachedPicture).FirstOrDefault();

            //Assert.IsNotNull(all);
            //Assert.AreEqual(tags.Where(x => x.Type.Schema == TagSchema.Id3v2).Count(), ;
            //Assert.IsNotNull(americanized);
            //Assert.AreEqual(tags.Where(x => x.Algorithm == Algorithm.Americanized).Count(), americanized.Count());
            //Assert.IsTrue(americanized.All(x => x.Type.Schema == TagSchema.Id3v1));
            //Assert.IsNotNull(v2);
            //Assert.AreEqual(tags.Where(x => x.Type.Schema == TagSchema.Id3v2).Count(), v2.Count());
            //Assert.IsTrue(v2.All(x => x != null && x.Type.Schema == TagSchema.Id3v2));
            //Assert.IsNotNull(apic);
            //Assert.AreEqual(imageData, apic.Value);
        }

        [Test]
        public void MediaRepositorySaveTest()
        {
            var media1 = mediaFactory.Create(uri1, MediaType.TextHtml);
            var media2 = mediaFactory.Create(uri2, MediaType.AudioMpeg);
            var media3 = mediaFactory.Create(uri3, MediaType.ImageJpeg);

            mediaRepository.Save(new List<IMedia> { media1, media2, media3 });

            var all = mediaRepository.All();

            Assert.IsNotNull(all);
            Assert.IsTrue(all.Count() == 3);
        }
    }
}
