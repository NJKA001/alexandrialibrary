using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Gnosis.Tests.Network
{
    [TestFixture]
    public class RemoteContentTypeItems
    {
        public RemoteContentTypeItems()
        {
            logger = new Gnosis.Utilities.DebugLogger();
            mediaFactory = new MediaFactory(logger);
        }

        private ILogger logger;
        private IMediaFactory mediaFactory;

        [Test]
        public void CanBeParsedForRssFeedsWithInvalidContentType()
        {
            var location = new Uri("http://feeds.arstechnica.com/arstechnica/index");
            var media = mediaFactory.Create(location);
            Assert.IsNotNull(media);
            Assert.AreNotEqual(mediaFactory.Default.Type, media.Type);
            Assert.AreEqual("application/rss+xml", media.Type.Name);
            Assert.AreEqual("UTF-8", media.Type.CharSet);
            Assert.IsNull(media.Type.Boundary);
        }

        [Test]
        public void CanBeParsedForAtomFeeds()
        {
            var location = new Uri("http://www.blogger.com/feeds/8677504/posts/default");
            var media = mediaFactory.Create(location);
            Assert.IsNotNull(media);
            Assert.AreNotEqual(mediaFactory.Default.Type, media.Type);
            Assert.AreEqual("application/atom+xml", media.Type.Name);
            Assert.AreEqual("UTF-8", media.Type.CharSet);
            Assert.IsNull(media.Type.Boundary);
        }
    }
}
