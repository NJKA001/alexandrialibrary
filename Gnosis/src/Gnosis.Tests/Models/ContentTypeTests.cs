using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.W3c;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class ContentTypeTests
    {
        [Test]
        public void GetContentTypeForRssFeedWithInvalidContentType()
        {
            var location = new Uri("http://feeds.arstechnica.com/arstechnica/index");
            var contentType = location.ToContentType();
            Assert.IsNotNull(contentType);
            Assert.AreNotEqual(ContentType.Empty, contentType);
            Assert.AreEqual(MediaType.RssFeed, contentType.Type);
            Assert.AreEqual(CharacterSet.Utf8, contentType.CharSet);
            Assert.IsNull(contentType.Boundary);
        }
    }
}
