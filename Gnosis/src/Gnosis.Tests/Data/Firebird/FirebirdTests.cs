using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Loggers;
using Gnosis.Core;
using Gnosis.Core.Tags.Id3;
using Gnosis.Data;
using Gnosis.Data.Firebird;

using NUnit.Framework;

namespace Gnosis.Tests.Data.Firebird
{
    [TestFixture]
    public class FirebirdTests
    {
        public FirebirdTests()
        {
            tagRepository = new FirebirdTagRepository(logger);
        }

        private readonly ILogger logger = new DebugLogger();
        private readonly FirebirdTagRepository tagRepository;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            tagRepository.Initialize();
        }

        [Test]
        public void TagRepositoryInitializeTest()
        {
            tagRepository.Initialize();
        }

        [Test]
        public void TagRepositorySaveTest()
        {
            var tag1 = new Tag(new Uri("http://arstechnica.com/index.ars"), Algorithm.Default, Schema.Default.Identifier, "Sample Tag #1");
            var tag2 = new Tag(new Uri(@"C:\Users\dpoage\Music\Queen\bicycle.mp3"), Algorithm.Default, Id3v1Schema.Id3v1Artist.ToUri(), "Queen");

            tagRepository.Save(new List<ITag> { tag1, tag2 });
            var count = tagRepository.Count();
            Assert.IsTrue(count > 0);

            var tags = tagRepository.Search();
            Assert.IsNotNull(tags);
            Assert.IsTrue(tags.Count() > 0);
            Assert.IsTrue(tags.All(x => x.Id > 0));
        }
    }
}
