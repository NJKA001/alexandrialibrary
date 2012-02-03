using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Application.Xml;
using Gnosis.Application.Xml.Rss;
using Gnosis.Culture;

namespace Gnosis.Tests.Network
{
    [TestFixture]
    public class RemoteRssTests
    {
        public RemoteRssTests()
        {
            logger = new Gnosis.Utilities.DebugLogger();
            contentTypeFactory = new ContentTypeFactory(logger);
        }

        private ILogger logger;
        private IContentTypeFactory contentTypeFactory;

        [Test]
        public void CanBeCreatedFromRemoteCustomSource()
        {
            const string generator = "ESPN Inc. http://espn.go.com/";
            var language = LanguageTag.Parse("en-us");

            var location = new Uri("http://search.espn.go.com/rss/bill-simmons/");

            var document = XmlElement.Parse(location);
            Assert.IsNotNull(document);

            var feed = document.Root as IRssFeed;
            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Channel);
            Assert.AreEqual(generator, feed.Channel.Generator);
            Assert.AreEqual(language, feed.Channel.Language);
            Assert.IsTrue(feed.Channel.Items.Count() > 1);
        }

        [Test]
        public void CanBeCreatedFromRemoteWordPressSource()
        {
            const string generator = "http://wordpress.org/?v=";

            var location = new Uri("http://www.nerdist.com/category/podcast/feed/");
            var document = XmlElement.Parse(location);
            Assert.IsNotNull(document);

            var feed = document.Root as IRssFeed;
            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Channel);
            Assert.IsNotNull(feed.Channel.Generator);
            Assert.IsTrue(feed.Channel.Generator.Contains(generator));
        }

        [Test]
        public void CanBeCreatedFromRemoteUnspecifiedSourceWithXmlBase()
        {
            const string baseId = "http://www.thisamericanlife.org";

            var location = new Uri("http://feeds.thisamericanlife.org/talpodcast");

            var document = XmlElement.Parse(location);
            Assert.IsNotNull(document);

            var feed = document.Root as IRssFeed;
            Assert.IsNotNull(feed);

            var xmlBase = feed.Attributes.Where(x => x.Name.ToString() == "xml:base").FirstOrDefault();
            Assert.IsNotNull(xmlBase);
            Assert.AreEqual(baseId, xmlBase.Value);
            Assert.IsNotNull(feed.Channel);
        }
    }
}
