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
            
            const int linksCount = 30;
            const int atomLinksCount = 2;
            const int commentsCount = 3;
            const int mediaCreditCount = 22;
            const int escapedCount = 364;
            const int rootNamespaceCount = 2;
            const int namespaceCount = 3;

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
