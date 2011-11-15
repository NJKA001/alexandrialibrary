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
        [Test]
        public void CanBeParsedForRssFeedsWithInvalidContentType()
        {
            var location = new Uri("http://feeds.arstechnica.com/arstechnica/index");
            var contentType = ContentType.GetContentType(location); //location.ToContentType();
            Assert.IsNotNull(contentType);
            Assert.AreNotEqual(ContentType.Empty, contentType);
            Assert.AreEqual(MediaType.ApplicationRssXml, contentType.Type);
            Assert.AreEqual(CharacterSet.Utf8, contentType.CharSet);
            Assert.IsNull(contentType.Boundary);
        }

        [Test]
        public void CanBeParsedForAtomFeeds()
        {
            var location = new Uri("http://www.blogger.com/feeds/8677504/posts/default");
            var contentType = ContentType.GetContentType(location);
            Assert.IsNotNull(contentType);
            Assert.AreNotEqual(ContentType.Empty, contentType);
            Assert.AreEqual(MediaType.ApplicationAtomXml, contentType.Type);
            Assert.AreEqual(CharacterSet.Utf8, contentType.CharSet);
            Assert.IsNull(contentType.Boundary);
        }
    }
}
