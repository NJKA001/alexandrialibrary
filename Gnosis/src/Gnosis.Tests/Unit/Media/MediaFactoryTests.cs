using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Gnosis.Tests.Unit.Media
{
    [TestFixture]
    public class MediaFactories
    {
        private readonly IMediaFactory factory = new MediaFactory();

        const string pathBearAtom = @".\Files\bearbrarian.xml";
        const string pathArsRss = @".\Files\arstechnica.xml";
        const string pathArsHtml = @".\Files\arstechnica.html";
        const string pathPostroadRdf = @".\Files\postroad.xml";
        const string pathGif = @".\Files\nin.gif";
        const string pathJpg = @".\Files\Undertow.jpg";
        const string pathPng = @".\Files\radiohead.png";

        [Test]
        public void CanCreateApplicationAtomMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathBearAtom).FullName);
            var doc = factory.Create(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual(MediaType.ApplicationAtomXml, doc.Type);
        }

        [Test]
        public void CanCreateApplicationRssMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathArsRss).FullName);
            var doc = factory.Create(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual(MediaType.ApplicationRssXml, doc.Type);
        }

        [Test]
        public void CanCreateTextXhtmlMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathArsHtml).FullName);
            var doc = factory.Create(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual(MediaType.TextHtml, doc.Type);
        }

        [Test]
        public void CanCreateApplicationRdfAtomMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathPostroadRdf).FullName);
            var doc = factory.Create(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual(MediaType.ApplicationAtomXml, doc.Type);
        }

        [Test]
        public void CanCreateImageGifMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathGif).FullName);
            var doc = factory.Create(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual(MediaType.ImageGif, doc.Type);
        }

        [Test]
        public void CanCreateImageJpgMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathJpg).FullName);
            var doc = factory.Create(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual(MediaType.ImageJpeg, doc.Type);
        }

        [Test]
        public void CanCreateImagePngMedia()
        {
            var url = new Uri(new System.IO.FileInfo(pathPng).FullName);
            var doc = factory.Create(url);
            Assert.IsNotNull(doc);
            Assert.IsNotNull(doc.Location);
            Assert.IsNotNull(doc.Type);
            Assert.AreEqual(url.ToString(), doc.Location.ToString());
            Assert.AreEqual(MediaType.ImagePng, doc.Type);
        }
    }
}
