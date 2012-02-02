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
            mediaFactory = new MediaFactory(characterSetFactory);
            contentTypeFactory = new ContentTypeFactory(logger, characterSetFactory);
        }

        private ILogger logger;
        private ICharacterSetFactory characterSetFactory;
        private IMediaFactory mediaFactory;
        private IContentTypeFactory contentTypeFactory;

        [Test]
        public void CanGetMediaTypeForRemoteRssFeedWithInvalidContentType()
        {
            var location = new Uri("http://feeds.arstechnica.com/arstechnica/index");
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("application/rss+xml", mediaType.Name);
        }

        [Test]
        public void CanGetMediaTypeForRemoteAtomFeed()
        {
            var location = new Uri("http://www.blogger.com/feeds/8677504/posts/default");
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("application/atom+xml", mediaType.Name);
        }

        [Test]
        public void CanGetMediaTypeForRemoteGif()
        {
            var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Rotating_earth_%28large%29.gif/200px-Rotating_earth_%28large%29.gif");
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("image/gif", mediaType.Name);
        }

        [Test]
        public void CanGetMediaTypeForRemoteJpg()
        {
            var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/b/b4/JPEG_example_JPG_RIP_100.jpg");
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("image/jpeg", mediaType.Name);
        }

        [Test]
        public void CanGetMediaTypeForRemoteHtml()
        {
            var location = new Uri("http://arstechnica.com/");
            var mediaType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("text/html", mediaType.Name);
        }
    }
}
