using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Tags;
using Gnosis.Utilities;
using Gnosis.Data;
using Gnosis.Data.SQLite;

namespace Gnosis.Tests.Performance
{
    public abstract class TagPerformanceTestBase
    {
        protected TagPerformanceTestBase()
        {
            connection = new SQLiteConnectionFactory().Create("Data Source=:memory:;Version=3;");
            connection.Open();
            repository = new SQLiteTagRepository(logger, typeFactory, connection);
            repository.Initialize();
        }

        private readonly ILogger logger = new DebugLogger();
        private readonly IConnectionFactory connectionFactory = new SQLiteConnectionFactory();
        private readonly IDbConnection connection;
        private readonly ITagTypeFactory typeFactory = new TagTypeFactory();
        protected readonly SQLiteTagRepository repository;

        protected void Cleanup()
        {
            connection.Close();
        }
    }

    [TestFixture]
    public class LargeNumbersOfTags
        : TagPerformanceTestBase
    {
        [TestFixtureTearDown]
        public void TearDown()
        {
            Cleanup();
        }

        [Test]
        public void CanBeSavedQuickly()
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
                        //System.Diagnostics.Debug.WriteLine("tag for word: " + word);
                        tags.Add(new Gnosis.Tags.Tag(new Uri(urlBase + Guid.NewGuid().ToString()), TagType.DefaultString, word));
                    }

                    var start = DateTime.Now;
                    repository.Save(tags);
                    var elapsed = DateTime.Now.Subtract(start);
                    
                    Assert.IsTrue(elapsed.TotalMilliseconds < 150);
                }

                //var tagsA = repository.GetByAlgorithm(Algorithms.Algorithm.Default, TagDomain.String, "a%");

                //var count = tagsA.Count();
                //Assert.IsTrue(count > 0);
            }
        }
    }
}
