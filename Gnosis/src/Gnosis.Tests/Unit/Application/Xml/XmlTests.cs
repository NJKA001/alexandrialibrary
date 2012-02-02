using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Application.Xml;
using Gnosis.Application.Xml.Atom;
using Gnosis.Application.Xml.Namespaces.DublinCore;
using Gnosis.Application.Xml.Namespaces.FeedBurner;
using Gnosis.Application.Xml.Namespaces.Google.Data;
using Gnosis.Application.Xml.Namespaces.MediaRss;
using Gnosis.Application.Xml.Namespaces.OpenSearch;
using Gnosis.Application.Xml.Namespaces.YouTube;
using Gnosis.Application.Xml.Rss;

namespace Gnosis.Tests.Unit.Application.Xml
{
    [TestFixture]
    public class XmlDocuments
    {
        public XmlDocuments()
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

        private void MakeArsXmlAssertions(IXmlElement xml)
        {
            #region Constants

            const int attribCount = 6;
            const int linksCount = 30;
            const int rssLinksCount = 26;
            const int rootRssLinkCount = 1;
            const int atomLinksCount = 2;
            const int commentsCount = 3;
            const int mediaCreditCount = 22;
            const int escapedCount = 365;
            const int rootNamespaceCount = 3;
            const int namespaceCount = 4;
            const string xmlBaseUri = "http://arstechnica.com/";
            const string xmlLang = "en-US";
            const int channelChildCount = 48;
            const string mediaRssNamespace = "http://search.yahoo.com/mrss/";
            const int mediaRssContentCount = 20;
            const string feedBurnerInfoUri = "arstechnica/index";
            const string dcTitleContent = "Ars Technica";

            #endregion

            Assert.IsNotNull(xml);

            foreach (var child in xml.Root.Children)
                Assert.IsNotNull(child.Parent);

            Assert.AreEqual(linksCount, xml.Root.Where<IElement>(elem => elem.Name.LocalPart == "link").Count());
            Assert.AreEqual(atomLinksCount, xml.Root.Where<IElement>(elem => elem.Name.Prefix == "atom10" && elem.Name.LocalPart == "link").Count());
            Assert.AreEqual(commentsCount, xml.Root.Where<IComment>(comment => comment != null).Count());
            Assert.AreEqual(mediaCreditCount, xml.Root.Where<IElement>(elem => elem.Name.ToString() == "media:credit").Count());
            Assert.AreEqual(escapedCount, xml.Root.Where<IEscapedSection>(esc => esc != null).Count());
            Assert.AreEqual(rootNamespaceCount, xml.Root.Namespaces.Count());
            Assert.AreEqual(namespaceCount, xml.Root.Where<INamespaceDeclaration>(ns => ns != null).DistinctBy(ns => ns.Name.ToString()).Count());
            Assert.AreEqual(attribCount, xml.Root.Attributes.Count());
            Assert.IsTrue(xml.Root.Attributes.All(attrib => attrib != null && attrib.Parent == xml.Root));
            Assert.IsNotNull(xml.Root.Attributes.Where(x => x.Name.ToString() == "xml:base").FirstOrDefault() as BaseAttribute);
            Assert.IsNotNull(xml.Root.Attributes.Where(x => x.Name.Prefix == "xmlns").FirstOrDefault() as INamespaceDeclaration);

            var xmlBaseAttrib = xml.Root.Where<IAttribute>(attrib => attrib != null && attrib.Name.ToString() == "xml:base").FirstOrDefault() as IBaseAttribute;
            Assert.IsNotNull(xmlBaseAttrib);
            Assert.IsNotNull(xmlBaseAttrib.Value);
            Assert.AreEqual(xmlBaseUri, xmlBaseAttrib.Value.ToString());

            var xmlLangAttrib = xml.Root.Where<IAttribute>(attrib => attrib != null && attrib.Name.ToString() == "xml:lang").FirstOrDefault() as ILangAttribute;
            Assert.IsNotNull(xmlLangAttrib);
            Assert.IsNotNull(xmlLangAttrib.Value);
            Assert.AreEqual(xmlLang, xmlLangAttrib.Value.ToString());

            var channelElem = xml.Root.Where<IElement>(elem => elem.Name.ToString() == "channel").FirstOrDefault();
            Assert.IsNotNull(channelElem);
            Assert.AreEqual(channelChildCount, channelElem.ChildElements.Count());
            var link = channelElem.Where<IElement>(elem => elem.Name.ToString() == "link").FirstOrDefault();
            Assert.IsNotNull(link);
            Assert.IsNotNull(link.Children.FirstOrDefault());

            var rss = xml.Root as IRssFeed;
            Assert.IsNotNull(rss);

            var channel = rss.Children.FirstOrDefault() as IRssChannel;
            Assert.IsNotNull(channel);
            Assert.IsNull(channel.CurrentNamespace);

            var links = rss.Where<IRssLink>(x => x != null);
            Assert.AreEqual(rssLinksCount, links.Count());

            var rootLinks = rss.Where<IRssLink>(x => x != null && x.Parent is IRssChannel);
            Assert.AreEqual(rootRssLinkCount, rootLinks.Count());

            var atomLinks = rss.Where<IAtomLink>(x => x != null);
            Assert.AreEqual(atomLinksCount, atomLinks.Count());

            var allLinks = rss.Where<IElement>(x => x != null && x.Name.LocalPart == "link");
            Assert.AreEqual(linksCount, allLinks.Count());

            var mediaRssContents = rss.Where<IMediaContent>(x => x != null && x.Name.LocalPart == "content");
            Assert.AreEqual(mediaRssContentCount, mediaRssContents.Count());
            Assert.IsNotNull(mediaRssContents.FirstOrDefault());
            Assert.AreEqual("image/jpeg", mediaRssContents.FirstOrDefault().MediaType);
            var firstContents = mediaRssContents.FirstOrDefault();
            Assert.IsNotNull(firstContents.CurrentNamespace);
            Assert.AreEqual(mediaRssNamespace, firstContents.CurrentNamespace.Identifier.ToString());

            var feedBurnerInfo = rss.Where<IFeedBurnerInfo>(x => x != null).FirstOrDefault();
            Assert.IsNotNull(feedBurnerInfo);
            Assert.AreEqual(feedBurnerInfoUri, feedBurnerInfo.Uri.ToString());

            var dcTitle = rss.Where<IDcTitle>(x => x != null).FirstOrDefault();
            Assert.IsNotNull(dcTitle);
            Assert.AreEqual(dcTitleContent, dcTitle.Content);
        }

        private void MakeAtomXmlAssertions(IXmlElement xml)
        {
            const string atomNamespace = "http://www.w3.org/2005/Atom";
            const string openSearchNamespace = "http://a9.com/-/spec/opensearchrss/1.0/";

            Assert.IsNotNull(xml);
            var feed = xml.Where<IElement>(x => x.Name.LocalPart == "feed").FirstOrDefault();
            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.CurrentNamespace);
            Assert.AreEqual(atomNamespace, feed.CurrentNamespace.Identifier.ToString());
            var totalResults = feed.Where<IOpenSearchTotalResults>(x => true);
            Assert.AreEqual(2, totalResults.Count());
            Assert.IsTrue(totalResults.All(x => x.CurrentNamespace != null && x.CurrentNamespace.Identifier.ToString() == openSearchNamespace && x.Content > 0));
        }

        [Test]
        public void CanBeParsedFromLocalRssFile()
        {
            const string path = @".\Files\arstechnica.xml";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var xml = XmlElement.Parse(location, characterSetFactory);
            MakeArsXmlAssertions(xml);
        }

        [Test]
        public void CanBeParsedFromLocalAtomFile()
        {
            const string path = @".\Files\bearbrarian.xml";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var xml = XmlElement.Parse(location, characterSetFactory);
            MakeAtomXmlAssertions(xml);
        }

        [Test]
        public void CanBeParsedFromLocalYouTubePlaylistsFile()
        {
            const string path = @".\Files\youtube_playlists.xml";
            const string feedLinkHref = "http://gdata.youtube.com/feeds/api/playlists/5615F5EBE2BC72C2";
            const int feedLinkCountHint = 195;
            const string playlistIdContent = "5615F5EBE2BC72C2";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var xml = XmlElement.Parse(location, characterSetFactory);

            Assert.IsNotNull(xml);
            
            var feedLink = xml.Where<IGoogleDataFeedLink>(x => x != null).FirstOrDefault();
            Assert.IsNotNull(feedLink);
            Assert.AreEqual(feedLinkHref, feedLink.Href.ToString());
            Assert.AreEqual(feedLinkCountHint, feedLink.CountHint);

            var playlistId = xml.Where<IYouTubePlaylistId>(x => x != null).FirstOrDefault();
            Assert.IsNotNull(playlistId);
            Assert.AreEqual(playlistIdContent, playlistId.Content);
        }

        [Test]
        public void CanBeParsedFromLocalRssXmlOutput()
        {
            const string path = @".\Files\arstechnica.xml";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var original = XmlElement.Parse(location, characterSetFactory);
            Assert.IsNotNull(original);

            var output = original.ToString();
            System.Diagnostics.Debug.WriteLine(output);

            var xml = XmlElement.Parse(output, characterSetFactory);
            MakeArsXmlAssertions(xml);
        }
    }
}
