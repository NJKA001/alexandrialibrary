using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.Iso;
using Gnosis.Core.Rss;
using Gnosis.Core.W3c;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class RssTests
    {
        private void MakeRssFeedAssertions(IRssFeed feed)
        {
            #region Constants

            const string version = "2.0";
            const string title = "Ars Technica";
            const string link = "http://arstechnica.com/index.php";
            const string description = "The Art of Technology";
            var language = LanguageTag.Parse("en");
            var lastBuildDate = new DateTime(2011, 6, 29, 18, 45, 05); //Wed, 29 Jun 2011 18:45:05 +0000
            const string generator = "http://www.sixapart.com/movabletype/";
            const string docs = "http://www.rssboard.org/rss-specification";
            const string copyright = "Copyright 2011 Conde Nast Digital. The contents of this feed are available for non-commercial use only.";
            var pubDate = new DateTime(2011, 6, 4, 9, 14, 58); //Sat, 4 Jun 2011 09:14:58 +0000
            const string managingEditor = "kfisher@arstechnica.com (Ken Fisher)";
            const string webMaster = "cecker@arstechnica.com (Clint Ecker)";

            const string categoryDomain = "http://www.sixapart.com/ns/types#category";
            const string categoryName = "News";

            const string cloudDomain = "radio.xmlstoragesystem.com";
            const int cloudPort = 80;
            const string cloudPath = "/RPC2";
            const string cloudProc = "xmlStorageSystem.rssPleaseNotify";
            var cloudProcotol = RssCloudProtocol.XmlRpc;

            const int ttl = 45;

            const string imageUrl = "http://static.arstechnica.net//public/v6/styles/light/images/masthead/logo.png?1309476728";
            const string imageTitle = "Ars Technica";
            const string imageLink = "http://arstechnica.com/index.php";
            const int imageHeight = 169;
            const int imageWidth = 300;
            const string imageDescription = "The Art of Technology";

            const string ratingExcerpt = "'http://www.gcf.org/v2.5' labels";

            const string textInputTitle = "Contact Us";
            const string textInputDescription = "Email us with feedback";
            const string textInputName = "Submit";
            const string textInputLink = "http://arstechnica.com/contact-us.php";

            var skipHours = new List<RssHour> { RssHour.Zero, RssHour.One, RssHour.Two, RssHour.Twelve, RssHour.Thirteen, RssHour.Nineteen, RssHour.TwentyThree };
            var skipDays = new List<RssDay> { RssDay.Sunday, RssDay.Thursday, RssDay.Saturday };

            const string ext1Prefix = "atom10";
            const string ext1Name = "link";
            const string ext1Namespace = "http://www.w3.org/2005/Atom";

            const string item1Title = "Impressions from Uncharted 3: Drake's Deception's open beta ";
            const string item1Author = "joshmcillwain@gmail.com (Josh McIllwain)";
            const string item1MediaContentUrl = "http://static.arstechnica.net/assets/2011/06/uncharted-3-thumb-300x169-23017-f.jpg";
            const string item1Guid = "http://arstechnica.com/gaming/news/2011/06/impressions-from-uncharted-3-drakes-deceptions-open-beta.ars?utm_source=rss&utm_medium=rss&utm_campaign=rss";
            var item1PubDate = new DateTime(2011, 6, 29, 16, 47, 00); //Wed, 29 Jun 2011 11:47:00 -0500

            #endregion

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
            Assert.AreEqual(pubDate, feed.Channel.PubDate);
            Assert.AreEqual(managingEditor, feed.Channel.ManagingEditor);
            Assert.AreEqual(webMaster, feed.Channel.WebMaster);
            Assert.IsNotNull(feed.Channel.Cloud);
            Assert.AreEqual(cloudDomain, feed.Channel.Cloud.Domain);
            Assert.AreEqual(cloudPort, feed.Channel.Cloud.Port);
            Assert.AreEqual(cloudPath, feed.Channel.Cloud.Path);
            Assert.AreEqual(cloudProc, feed.Channel.Cloud.RegisterProcedure);
            Assert.AreEqual(cloudProcotol, feed.Channel.Cloud.Protocol);
            Assert.AreEqual(ttl, feed.Channel.Ttl.TotalMinutes);
            Assert.IsNotNull(feed.Channel.Image);
            Assert.AreEqual(imageUrl, feed.Channel.Image.Url.ToString());
            Assert.AreEqual(imageTitle, feed.Channel.Image.Title);
            Assert.AreEqual(imageLink, feed.Channel.Image.Link.ToString());
            Assert.AreEqual(imageHeight, feed.Channel.Image.Height);
            Assert.AreEqual(imageWidth, feed.Channel.Image.Width);
            Assert.AreEqual(imageDescription, feed.Channel.Image.Description);
            Assert.IsNotNull(feed.Channel.Rating);
            Assert.IsNotNull(feed.Channel.Rating.Value);
            Assert.IsTrue(feed.Channel.Rating.Value.Contains(ratingExcerpt));
            Assert.IsNotNull(feed.Channel.TextInput);
            Assert.AreEqual(textInputTitle, feed.Channel.TextInput.Title);
            Assert.AreEqual(textInputDescription, feed.Channel.TextInput.Description);
            Assert.AreEqual(textInputName, feed.Channel.TextInput.Name);
            Assert.AreEqual(textInputLink, feed.Channel.TextInput.Link.ToString());
            Assert.IsTrue(skipHours.SequenceEqual(feed.Channel.SkipHours));
            Assert.IsTrue(skipDays.SequenceEqual(feed.Channel.SkipDays));
            Assert.AreEqual(1, feed.Channel.Categories.Count());
            Assert.AreEqual(categoryDomain, feed.Channel.Categories.First().Domain.ToString());
            Assert.AreEqual(categoryName, feed.Channel.Categories.First().Name);
            Assert.AreEqual(3, feed.Channel.Extensions.Count());
            Assert.AreEqual(ext1Name, feed.Channel.Extensions.First().Name);
            Assert.AreEqual(ext1Prefix, feed.Channel.Extensions.First().Prefix);
            Assert.IsNotNull(feed.Channel.Extensions.First().PrimaryNamespace);
            Assert.AreEqual(ext1Namespace, feed.Channel.Extensions.First().PrimaryNamespace.Identifier.ToString());
            Assert.AreEqual(25, feed.Channel.Items.Count());
            var firstItem = feed.Channel.Items.First();
            Assert.IsNotNull(firstItem);
            Assert.AreEqual(item1Title, firstItem.Title);
            Assert.AreEqual(item1Author, firstItem.Author);
            Assert.IsNotNull(firstItem.Guid);
            Assert.AreEqual(item1Guid, firstItem.Guid.Value);
            Assert.IsFalse(firstItem.Guid.IsPermaLink);
            Assert.AreEqual(item1PubDate, firstItem.PubDate);
            Assert.AreEqual(5, firstItem.Categories.Count());
            var firstMediaContentExt = firstItem.Extensions.Where(x => x.Prefix == "media" && x.Name == "content").FirstOrDefault();
            Assert.IsNotNull(firstMediaContentExt);
            Assert.IsTrue(firstMediaContentExt.ToString().Contains(item1MediaContentUrl));
            var lastItem = feed.Channel.Items.Last();
            Assert.IsNotNull(lastItem);
        }

        [Test]
        public void CreateExampleRss091FeedFromLocalXml()
        {
            const string path = @".\Files\example0.91.rss";
            const string version = "0.91";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var contentType = location.ToContentType();
            Assert.AreEqual(MediaType.ApplicationRssXml, contentType.Type);

            var feed = location.ToRssFeed();

            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Channel);
            Assert.AreEqual(version, feed.Version);
            Assert.AreEqual(CharacterSet.Latin1, feed.Encoding);
        }

        [Test]
        public void CreateExampleRss092FeedFromLocalXml()
        {
            const string path = @".\Files\example0.92.rss";
            const string version = "0.92";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var contentType = location.ToContentType();
            Assert.AreEqual(MediaType.ApplicationRssXml, contentType.Type);

            var feed = location.ToRssFeed();

            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Channel);
            Assert.AreEqual(version, feed.Version);
        }

        [Test]
        public void CreateExampleRss20FeedFromLocalXml()
        {
            const string path = @".\Files\example2.0.rss";
            const string version = "2.0";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var contentType = location.ToContentType();
            Assert.AreEqual(MediaType.ApplicationRssXml, contentType.Type);

            var feed = location.ToRssFeed();

            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Channel);
            Assert.AreEqual(version, feed.Version);
        }

        [Test]
        public void CreateRssFeedFromLocalXml()
        {
            const string path = @".\Files\arstechnica.xml";
            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);
            
            var location = new Uri(fileInfo.FullName);
            var contentType = location.ToContentType();
            Assert.AreEqual(MediaType.ApplicationRssXml, contentType.Type);
            
            var feed = location.ToRssFeed();

            MakeRssFeedAssertions(feed);
        }

        [Test]
        public void CreateRssFeedFromRemoteCustomSource()
        {
            const string generator = "ESPN Inc. http://espn.go.com/";
            var language = LanguageTag.Parse("en-us");

            var location = new Uri("http://search.espn.go.com/rss/bill-simmons/");
            var feed = location.ToRssFeed();
            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Channel);
            Assert.AreEqual(generator, feed.Channel.Generator);
            Assert.AreEqual(language, feed.Channel.Language);
            Assert.IsTrue(feed.Channel.Items.Count() > 1);
        }

        [Test]
        public void CreateRssFeedFromRemoteWordPressSource()
        {
            const string generator = "http://wordpress.org/?v=3.2.1";

            var location = new Uri("http://www.nerdist.com/category/podcast/feed/");
            var feed = location.ToRssFeed();
            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Channel);
            Assert.AreEqual(generator, feed.Channel.Generator);
        }

        [Test]
        public void CreateRssFeedFromRemoteUnspecifiedSourceWithXmlBase()
        {
            const string baseId = "http://www.thisamericanlife.org/";

            var location = new Uri("http://feeds.thisamericanlife.org/talpodcast");
            var feed = location.ToRssFeed();
            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.BaseId);
            Assert.AreEqual(baseId, feed.BaseId.ToString());
            Assert.IsNotNull(feed.Channel);
        }

        [Test]
        public void CreateRssFeedFromXmlOutput()
        {
            const string path = @".\Files\arstechnica.xml";
            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var contentType = location.ToContentType();
            Assert.AreEqual(MediaType.ApplicationRssXml, contentType.Type);

            var original = location.ToRssFeed();

            IRssFeed feed = null;
            var encoding = CharacterSet.Utf8;
            IEnumerable<IXmlNamespace> namespaces = new List<IXmlNamespace>();
            var styleSheets = new List<IXmlStyleSheet>();
            var xmlString = original.ToString();
            var xml = new XmlDocument();
            xml.LoadXml(xmlString);

            foreach (var child in xml.ChildNodes.Cast<XmlNode>().Where(node => node != null))
            {
                switch (child.NodeType)
                {
                    case XmlNodeType.XmlDeclaration:
                        encoding = child.ToEncoding();
                        break;
                    case XmlNodeType.ProcessingInstruction:
                        styleSheets.AddIfNotNull(child.ToXmlStyleSheet());
                        break;
                    case XmlNodeType.Element:
                        if (child.Name != "rss")
                            break;

                        namespaces = child.ToXmlNamespaces();
                        feed = child.ToRssFeed(encoding, namespaces, styleSheets);
                        break;
                    default:
                        break;
                }
            }

            MakeRssFeedAssertions(feed);
        }
    }
}
