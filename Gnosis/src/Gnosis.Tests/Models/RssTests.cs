using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.Ietf;
using Gnosis.Core.Iso;
using Gnosis.Core.Rss;
using Gnosis.Core.W3c;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class RssTests
    {
        [Test]
        public void CreateRssFeedFromLocalXml()
        {
            const string path = @".\Files\arstechnica.xml";
            const string version = "2.0";
            const string title = "Ars Technica";
            const string link = "http://arstechnica.com/index.php";
            const string description = "The Art of Technology";
            var language = LanguageTag.Parse("en");
            var lastBuildDate = new DateTime(2011, 6, 29, 18, 45, 05); //Wed, 29 Jun 2011 18:45:05 +0000
            var generator = "http://www.sixapart.com/movabletype/";
            var docs = "http://www.rssboard.org/rss-specification";
            var copyright = "Copyright 2011 Conde Nast Digital. The contents of this feed are available for non-commercial use only.";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);
            
            var location = new Uri(fileInfo.FullName);
            var contentType = location.ToContentType();
            Assert.AreEqual(MediaType.ApplicationRssXml, contentType.Type);
            
            var factory = new RssFeedFactory();
            var feed = factory.Create(location);

            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Channel);
            Assert.AreEqual(version, feed.Version);
            Assert.AreEqual(2, feed.StyleSheets.Count());
            Assert.AreEqual(2, feed.Namespaces.Count());

            Assert.AreEqual(title, feed.Channel.Title);
            Assert.AreEqual(link, feed.Channel.Link.ToString());
            Assert.AreEqual(description, feed.Channel.Description);
            Assert.AreEqual(language.PrimaryLanguage, feed.Channel.Language.PrimaryLanguage);
            Assert.AreEqual(lastBuildDate, feed.Channel.LastBuildDate);
            Assert.AreEqual(generator, feed.Channel.Generator);
            Assert.AreEqual(docs, feed.Channel.Docs.ToString());
            Assert.AreEqual(copyright, feed.Channel.Copyright);
            Assert.AreEqual(DateTime.MinValue, feed.Channel.PubDate);
            Assert.IsNull(feed.Channel.ManagingEditor);
            Assert.IsNull(feed.Channel.WebMaster);
            Assert.IsNull(feed.Channel.Cloud);
            Assert.AreEqual(TimeSpan.Zero, feed.Channel.Ttl);
            Assert.IsNull(feed.Channel.Image);
            Assert.IsNull(feed.Channel.Rating);
            Assert.IsNull(feed.Channel.TextInput);
            Assert.AreEqual(0, feed.Channel.SkipHours.Count());
            Assert.AreEqual(0, feed.Channel.SkipDays.Count());
        }
    }
}
