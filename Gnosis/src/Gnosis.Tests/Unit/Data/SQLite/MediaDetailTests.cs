using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Links;
using Gnosis.Tags;
using Gnosis.Tags.Id3.Id3v2;
using Gnosis.Utilities;
using Gnosis.Data.SQLite;

namespace Gnosis.Tests.Unit.Data.SQLite
{
    public abstract class MediaDetailTestBase
    {
        protected MediaDetailTestBase()
        {
            connection = new SQLiteConnectionFactory().Create("Data Source=:memory:;Version=3;");
            connection.Open();
            tagRepository = new SQLiteTagRepository(logger, tagTypeFactory, connection);
            linkRepository = new SQLiteLinkRepository(logger, linkTypeFactory, connection);
            detailRepository = new SQLiteMediaDetailRepository(logger, tagRepository, linkRepository);

            tagRepository.Initialize();
            linkRepository.Initialize();

            InitializeData();
        }

        private readonly IDbConnection connection;
        private readonly ILinkTypeFactory linkTypeFactory = new LinkTypeFactory();
        private readonly ITagTypeFactory tagTypeFactory = new TagTypeFactory();
        protected readonly ILogger logger = new DebugLogger();
        protected readonly ITagRepository tagRepository;
        protected readonly ILinkRepository linkRepository;
        protected readonly IMediaDetailRepository detailRepository;

        protected void Cleanup()
        {
            connection.Close();
        }

        #region Test Values

        protected void InitializeData()
        {
            link1 = new Link(trackUri1, thumbUri1, LinkType.AlbumThumbnail, "Album Cover Image");
            linkRepository.Save(new List<ILink> { link1 });

            tag1 = new Tag(trackUri1, Id3v2TagType.Artist, "TPE1", 0, "Björk");
            tag2 = new Tag(trackUri1, TagType.AmericanizedString, "Artist", 0 ,"Björk".ToAmericanizedString());
            tag3 = new Tag(trackUri1, TagType.DefaultString, "Default", 0, "Bjork is awesome!");
            tagRepository.Save(new List<ITag> { tag1, tag2, tag3 });
        }

        protected Uri trackUri1 = new Uri("http://example.com/bands/nin/broken/pinion.mp3");
        protected Uri thumbUri1 = new Uri("http://example.com/images/nin/broken/cover.jpg");
        protected ILink link1;
        protected ITag tag1;
        protected ITag tag2;
        protected ITag tag3;

        #endregion
    }

    [TestFixture]
    public class SavedTagsAndLinks
        : MediaDetailTestBase
    {
        [Test]
        public void CanBeReadAsDetails()
        {
            var details = new List<IMediaDetail>();
            
            var task = detailRepository.Search("Bjork%");
            task.AddResultsCallback(x => details.AddRange(x));
            task.StartSynchronously();

            Assert.AreEqual(2, details.Count);
            var first = details.FirstOrDefault();
            Assert.IsNotNull(first);
            Assert.AreEqual(tag3.Value, first.Tag.Value);
            Assert.AreEqual(link1.Target, first.CollectionThumbnail.Location);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Cleanup();
        }
    }
}
