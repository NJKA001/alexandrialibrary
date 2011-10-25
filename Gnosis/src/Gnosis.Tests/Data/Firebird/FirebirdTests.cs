using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Tags.Id3;
using Gnosis.Core.Tags.Id3.Id3v1;
using Gnosis.Core.Utilities;
using Gnosis.Data;

using NUnit.Framework;

#if FIREBIRD
using Gnosis.Data.Firebird;

namespace Gnosis.Tests.Data.Firebird
{
    [TestFixture]
    public class FirebirdTests
    {
        public FirebirdTests()
        {
            typeFactory = new TagTypeFactory();
            tagRepository = new FirebirdTagRepository(logger, typeFactory);
        }

        private readonly ILogger logger = new DebugLogger();
        private readonly ITagTypeFactory typeFactory;
        private readonly FirebirdTagRepository tagRepository;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            const string databaseFile = "ALEXANDRIA.FDB";
            if (System.IO.File.Exists(databaseFile))
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Deleting Firebird test database");
                    System.IO.File.Delete(databaseFile);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Could not delete Firebird test database: file in use");
                }
            }

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
            var tag1 = new Tag(new Uri("http://arstechnica.com/index.ars"), TagType.Default, "Sample Tag #1");
            var tag2 = new Tag(new Uri(@"C:\Users\dpoage\Music\Queen\bicycle.mp3"), Id3v1TagType.Artist, "Queen");

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

#endif