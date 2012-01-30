﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Gnosis.Tests.Unit.Media
{
    [TestFixture]
    public class MediaFactories
    {
        public MediaFactories()
        {
            logger = new Gnosis.Utilities.DebugLogger();
            characterSetFactory = new CharacterSetFactory();
            mediaTypeFactory = new MediaTypeFactory(logger, characterSetFactory);
            contentTypeFactory = new ContentTypeFactory(logger, mediaTypeFactory, characterSetFactory);
        }

        private readonly ILogger logger;
        private readonly ICharacterSetFactory characterSetFactory;
        private readonly IMediaTypeFactory mediaTypeFactory;
        private readonly IContentTypeFactory contentTypeFactory;

        const string pathBearAtom = @".\Files\bearbrarian.xml";
        const string pathArsRss = @".\Files\arstechnica.xml";
        const string pathArsHtml = @".\Files\arstechnica.html";
        const string pathPostroadRdf = @".\Files\postroad.xml";
        const string pathGif = @".\Files\nin.gif";
        const string pathJpg = @".\Files\Undertow.jpg";
        const string pathPng = @".\Files\radiohead.png";

        private IMedia CreateMedia(Uri location)
        {
            var type = mediaTypeFactory.GetByLocation(location, contentTypeFactory);

            return type != null ?
                type.CreateMedia(location)
                : null;
        }

        [Test]
        public void CanCreateApplicationAtomMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathBearAtom).FullName);
            var doc = CreateMedia(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual("application/atom+xml", doc.Type.ToString());
        }

        [Test]
        public void CanCreateApplicationRssMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathArsRss).FullName);
            var doc = CreateMedia(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual("application/rss+xml", doc.Type.ToString());
        }

        [Test]
        public void CanCreateTextXhtmlMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathArsHtml).FullName);
            var doc = CreateMedia(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual("text/html", doc.Type.ToString());
        }

        [Test]
        public void CanCreateApplicationRdfAtomMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathPostroadRdf).FullName);
            var doc = CreateMedia(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual("application/atom+xml", doc.Type.ToString());
        }

        [Test]
        public void CanCreateImageGifMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathGif).FullName);
            var doc = CreateMedia(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual("image/gif", doc.Type.ToString());
        }

        [Test]
        public void CanCreateImageJpgMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathJpg).FullName);
            var doc = CreateMedia(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual("image/jpeg", doc.Type.ToString());
        }

        [Test]
        public void CanCreateImagePngMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathPng).FullName);
            var doc = CreateMedia(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual("image/png", doc.Type.ToString());
        }
    }
}
