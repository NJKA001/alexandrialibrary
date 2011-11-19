﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Gnosis.Tests.Network
{
    [TestFixture]
    public class RemoteMediaTypeTests
    {
        [Test]
        public void CanGetMediaTypeForRemoteRssFeedWithInvalidContentType()
        {
            var location = new Uri("http://feeds.arstechnica.com/arstechnica/index");
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ApplicationRssXml, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForRemoteAtomFeed()
        {
            var location = new Uri("http://www.blogger.com/feeds/8677504/posts/default");
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ApplicationAtomXml, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForRemoteGif()
        {
            var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Rotating_earth_%28large%29.gif/200px-Rotating_earth_%28large%29.gif");
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ImageGif, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForRemoteJpg()
        {
            var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/b/b4/JPEG_example_JPG_RIP_100.jpg");
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ImageJpeg, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForRemoteHtml()
        {
            var location = new Uri("http://arstechnica.com/");
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.TextHtml, mediaType);
        }
    }
}