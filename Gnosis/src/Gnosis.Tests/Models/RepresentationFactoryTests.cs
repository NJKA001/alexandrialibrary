using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

using NUnit.Framework;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class RepresentationFactoryTests
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
        public void CreateApplicationAtom()
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
        public void CreateApplicationRss()
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
        public void CreateTextXhtml()
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
        public void CreateApplicationRdfAtom()
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
        public void CreateImageGif()
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
        public void CreateImageJpg()
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
        public void CreateImagePng()
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
