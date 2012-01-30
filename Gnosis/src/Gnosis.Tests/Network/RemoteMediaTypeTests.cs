using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Gnosis.Tests.Network
{
    [TestFixture]
    public class RemoteMediaTypeTests
    {
        public RemoteMediaTypeTests()
        {
            logger = new Gnosis.Utilities.DebugLogger();
            characterSetFactory = new CharacterSetFactory();
            mediaTypeFactory = new MediaTypeFactory(logger, characterSetFactory);
            contentTypeFactory = new ContentTypeFactory(logger, mediaTypeFactory, characterSetFactory);
        }

        private ILogger logger;
        private ICharacterSetFactory characterSetFactory;
        private IMediaTypeFactory mediaTypeFactory;
        private IContentTypeFactory contentTypeFactory;

        [Test]
        public void CanGetMediaTypeForRemoteRssFeedWithInvalidContentType()
        {
            var location = new Uri("http://feeds.arstechnica.com/arstechnica/index");
            var mediaType = mediaTypeFactory.GetByLocation(location, contentTypeFactory);
            Assert.AreEqual("application/rss+xml", mediaType.ToString());
        }

        [Test]
        public void CanGetMediaTypeForRemoteAtomFeed()
        {
            var location = new Uri("http://www.blogger.com/feeds/8677504/posts/default");
            var mediaType = mediaTypeFactory.GetByLocation(location, contentTypeFactory);
            Assert.AreEqual("application/atom+xml", mediaType.ToString());
        }

        [Test]
        public void CanGetMediaTypeForRemoteGif()
        {
            var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Rotating_earth_%28large%29.gif/200px-Rotating_earth_%28large%29.gif");
            var mediaType = mediaTypeFactory.GetByLocation(location, contentTypeFactory);
            Assert.AreEqual("image/gif", mediaType.ToString());
        }

        [Test]
        public void CanGetMediaTypeForRemoteJpg()
        {
            var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/b/b4/JPEG_example_JPG_RIP_100.jpg");
            var mediaType = mediaTypeFactory.GetByLocation(location, contentTypeFactory);
            Assert.AreEqual("image/jpeg", mediaType.ToString());
        }

        [Test]
        public void CanGetMediaTypeForRemoteHtml()
        {
            var location = new Uri("http://arstechnica.com/");
            var mediaType = mediaTypeFactory.GetByLocation(location, contentTypeFactory);
            Assert.AreEqual("text/html", mediaType.ToString());
        }
    }
}
