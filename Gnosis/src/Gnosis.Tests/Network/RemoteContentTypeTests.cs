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
            var contentType = mediaFactory.GetTypeByLocation(location);
            Assert.IsNotNull(contentType);
            Assert.AreNotEqual(mediaFactory.DefaultType, contentType);
            Assert.AreEqual("application/rss+xml", contentType.Name);
            Assert.AreEqual("UTF-8", contentType.CharSet);
            Assert.IsNull(contentType.Boundary);
        }

        [Test]
        public void CanBeParsedForAtomFeeds()
        {
            var location = new Uri("http://www.blogger.com/feeds/8677504/posts/default");
            var contentType = mediaFactory.GetTypeByLocation(location);
            Assert.IsNotNull(contentType);
            Assert.AreNotEqual(mediaFactory.DefaultType, contentType);
            Assert.AreEqual("application/atom+xml", contentType.Name);
            Assert.AreEqual("UTF-8", contentType.CharSet);
            Assert.IsNull(contentType.Boundary);
        }
    }
}
