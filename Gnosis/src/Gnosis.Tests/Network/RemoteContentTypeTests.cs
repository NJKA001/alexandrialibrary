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
            contentTypeFactory = new ContentTypeFactory(logger);
        }

        private ILogger logger;
        private IContentTypeFactory contentTypeFactory;

        [Test]
        public void CanBeParsedForRssFeedsWithInvalidContentType()
        {
            var location = new Uri("http://feeds.arstechnica.com/arstechnica/index");
            var contentType = contentTypeFactory.GetByLocation(location);
            Assert.IsNotNull(contentType);
            Assert.AreNotEqual(contentTypeFactory.Default, contentType);
            Assert.AreEqual("application/rss+xml", contentType.Name);
            Assert.AreEqual("UTF-8", contentType.CharSet);
            Assert.IsNull(contentType.Boundary);
        }

        [Test]
        public void CanBeParsedForAtomFeeds()
        {
            var location = new Uri("http://www.blogger.com/feeds/8677504/posts/default");
            var contentType = contentTypeFactory.GetByLocation(location);
            Assert.IsNotNull(contentType);
            Assert.AreNotEqual(contentTypeFactory.Default, contentType);
            Assert.AreEqual("application/atom+xml", contentType.Name);
            Assert.AreEqual("UTF-8", contentType.CharSet);
            Assert.IsNull(contentType.Boundary);
        }
    }
}
