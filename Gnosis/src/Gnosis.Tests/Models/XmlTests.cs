using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.Xml;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class XmlTests
    {
        private void MakeArsXmlAssertions(IXmlDocument xml)
        {
            #region Constants

            const int attribCount = 5;
            const int linksCount = 30;
            const int atomLinksCount = 2;
            const int commentsCount = 3;
            const int mediaCreditCount = 22;
            const int escapedCount = 364;
            const int rootNamespaceCount = 2;
            const int namespaceCount = 3;
            const string xmlBaseUri = "http://arstechnica.com/";
            const string xmlLang = "en-US";
            const int channelChildCount = 47;

            #endregion

            Assert.IsNotNull(xml);

            foreach (var child in xml.Root.Children)
                Assert.IsNotNull(child.Parent);

            Assert.AreEqual(linksCount, xml.Root.Where<IXmlElement>(elem => elem.Name.LocalPart == "link").Count());
            Assert.AreEqual(atomLinksCount, xml.Root.Where<IXmlElement>(elem => elem.Name.Prefix == "atom10" && elem.Name.LocalPart == "link").Count());
            Assert.AreEqual(commentsCount, xml.Root.Where<IXmlComment>(comment => comment != null).Count());
            Assert.AreEqual(mediaCreditCount, xml.Root.Where<IXmlElement>(elem => elem.Name.ToString() == "media:credit").Count());
            Assert.AreEqual(escapedCount, xml.Root.Where<IXmlEscapedSection>(esc => esc != null).Count());
            Assert.AreEqual(rootNamespaceCount, xml.Root.Namespaces.Count());
            Assert.AreEqual(namespaceCount, xml.Root.Where<IXmlNamespace>(ns => ns != null).DistinctBy(ns => ns.Name.ToString()).Count());
            Assert.AreEqual(attribCount, xml.Root.Attributes.Count());
            Assert.IsTrue(xml.Root.Attributes.All(attrib => attrib != null && attrib.Parent == xml.Root));
            Assert.IsNotNull(xml.Root.Attributes.Where(x => x.Name.ToString() == "xml:base").FirstOrDefault() as XmlBaseAttribute);
            Assert.IsNotNull(xml.Root.Attributes.Where(x => x.Name.Prefix == "xmlns").FirstOrDefault() as IXmlNamespace);
            
            var xmlBaseAttrib = xml.Root.Where<IXmlAttribute>(attrib => attrib != null && attrib.Name.ToString() == "xml:base").FirstOrDefault() as IXmlBaseAttribute;
            Assert.IsNotNull(xmlBaseAttrib);
            Assert.IsNotNull(xmlBaseAttrib.Value);
            Assert.AreEqual(xmlBaseUri, xmlBaseAttrib.Value.ToString());

            var xmlLangAttrib = xml.Root.Where<IXmlAttribute>(attrib => attrib != null && attrib.Name.ToString() == "xml:lang").FirstOrDefault() as IXmlLangAttribute;
            Assert.IsNotNull(xmlLangAttrib);
            Assert.IsNotNull(xmlLangAttrib.Value);
            Assert.AreEqual(xmlLang, xmlLangAttrib.Value.ToString());

            var channel = xml.Root.Where<IXmlElement>(elem => elem.Name.ToString() == "channel").FirstOrDefault();
            Assert.IsNotNull(channel);
            Assert.AreEqual(channelChildCount, channel.ChildElements.Count());
            var link = channel.Where<IXmlElement>(elem => elem.Name.ToString() == "link").FirstOrDefault();
            Assert.IsNotNull(link);
            Assert.IsNotNull(link.Children.FirstOrDefault());
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

            var xml = XmlDocument.Parse(output);
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
