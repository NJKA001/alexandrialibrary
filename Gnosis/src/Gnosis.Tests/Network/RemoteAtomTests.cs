using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Application.Xml;
using Gnosis.Application.Xml.Atom;
using Gnosis.Culture;

namespace Gnosis.Tests.Network
{
    [TestFixture]
    public class RemoteAtomTests
    {
        public RemoteAtomTests()
        {
            logger = new Gnosis.Utilities.DebugLogger();
            mediaFactory = new MediaFactory(logger);
        }

        private ILogger logger;
        private IMediaFactory mediaFactory;

        [Test]
        public void CanBeCreatedFromRemoteFeedBurnerSource()
        {
            #region Constants

            var location = new Uri("http://feeds2.feedburner.com/oreilly/radar/atom");
            var document = XmlElement.Parse(location);
            Assert.IsNotNull(document);

            var feed = document.Root as IAtomFeed;
            const string generatorName = "Movable Type Pro 4.21-en";
            const string generatorUri = "http://www.sixapart.com/movabletype/";

            var entry1ContentLang = LanguageTag.Parse("en");
            var entry1ContentBase = "http://radar.oreilly.com/";
            var entry1ContentType = AtomTextType.html;

            #endregion

            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Id);
            Assert.IsNotNull(feed.Title);

            Assert.IsNotNull(feed.Generator);
            Assert.AreEqual(generatorName, feed.Generator.GeneratorName);
            Assert.AreEqual(generatorUri, feed.Generator.Uri.ToString());

            var entryCount = feed.Entries.Count();
            Assert.IsTrue(entryCount > 0);
            Assert.IsNotNull(feed.Entries.First().Content);
            Assert.AreEqual(entry1ContentLang, feed.Entries.First().Content.Lang);
            Assert.AreEqual(entry1ContentBase, feed.Entries.First().Content.BaseId.ToString());
            Assert.AreEqual(entry1ContentType, feed.Entries.First().Content.Type);
            Assert.IsTrue(feed.Links.Count() > 0);
        }

        [Test]
        public void CanBeCreatedFromRemoteBlogEdSource()
        {
            const string generatorName = "BlogEd 008";
            const string generatorUri = "https://bloged.dev.java.net/";

            var location = new Uri("http://bblfish.net/blog/blog.atom");
            //System.Diagnostics.Debug.WriteLine("before ToAtomFeed");
            var document = XmlElement.Parse(location);
            var feed = document.Root as IAtomFeed;
            //System.Diagnostics.Debug.WriteLine("after ToAtomFeed");

            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Generator);
            Assert.AreEqual(generatorName, feed.Generator.GeneratorName);
            Assert.AreEqual(generatorUri, feed.Generator.Uri.ToString());
        }

        [Test]
        public void CanBeParsedFromRemoteAtomResource()
        {
            var location = new Uri("http://bblfish.net/blog/blog.atom");

            var xml = XmlElement.Parse(location);
            Assert.IsNotNull(xml);
        }
    }
}
