using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.Xml;
using Gnosis.Core.Xml.Atom;
using Gnosis.Core.Xml.Rss;
using Gnosis.Core.Xml.Yahoo;
using Gnosis.Core.W3c;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class XmlTests
    {
        private void MakeArsXmlAssertions(IDocument xml)
        {
            #region Constants

            const int attribCount = 5;
            const int linksCount = 30;
            const int rssLinksCount = 26;
            const int rootRssLinkCount = 1;
            const int atomLinksCount = 2;
            const int commentsCount = 3;
            const int mediaCreditCount = 22;
            const int escapedCount = 364;
            const int rootNamespaceCount = 2;
            const int namespaceCount = 3;
            const string xmlBaseUri = "http://arstechnica.com/";
            const string xmlLang = "en-US";
            const int channelChildCount = 47;
            const int mediaRssContentCount = 20;

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
            Assert.AreEqual(namespaceCount, xml.Root.Where<INamespace>(ns => ns != null).DistinctBy(ns => ns.Name.ToString()).Count());
            Assert.AreEqual(attribCount, xml.Root.Attributes.Count());
            Assert.IsTrue(xml.Root.Attributes.All(attrib => attrib != null && attrib.Parent == xml.Root));
            Assert.IsNotNull(xml.Root.Attributes.Where(x => x.Name.ToString() == "xml:base").FirstOrDefault() as BaseAttribute);
            Assert.IsNotNull(xml.Root.Attributes.Where(x => x.Name.Prefix == "xmlns").FirstOrDefault() as INamespace);

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

            var rss = xml.Root as IRssRoot;
            Assert.IsNotNull(rss);

            var channel = rss.Children.FirstOrDefault() as IRssChannel;
            Assert.IsNotNull(channel);

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
            Assert.AreEqual(MediaType.ImageJpeg, mediaRssContents.FirstOrDefault().Type);
        }

        [Test]
        public void CreateXmlDocumentFromLocalFile()
        {
            const string path = @".\Files\arstechnica.xml";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var xml = location.ToXmlDocument();
            MakeArsXmlAssertions(xml);
        }

        [Test]
        public void CreateXmlDocumentFromXmlOutput()
        {
            const string path = @".\Files\arstechnica.xml";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var original = location.ToXmlDocument();
            Assert.IsNotNull(original);

            var output = original.ToString();
            System.Diagnostics.Debug.WriteLine(output);

            var xml = Document.Parse(output);
            MakeArsXmlAssertions(xml);
        }

        [Test]
        public void CreateXmlDocumentFromRemoteLocation()
        {
            var location = new Uri("http://bblfish.net/blog/blog.atom");

            var xml = location.ToXmlDocument();
            Assert.IsNotNull(xml);
        }
    }
}
