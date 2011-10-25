using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Document;
using Gnosis.Document.Xml;
using Gnosis.Document.Xml.Xhtml;

using NUnit.Framework;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class XhtmlTests
    {
        private static void MakeDocumentAssertions(IXmlElement xhtml)
        {
            const string paragraphContent = "How stable will the West Antarctic Ice sheet be as";

            Assert.IsNotNull(xhtml);
            Assert.AreEqual(2, xhtml.Children.OfType<IComment>().Count());

            var declaration = xhtml.Children.OfType<IDeclaration>().FirstOrDefault();
            Assert.IsNotNull(declaration);
            Assert.AreEqual(CharacterSet.Utf8, declaration.Encoding);
            Assert.AreEqual("1.0", declaration.Version);

            var root = xhtml.Children.OfType<IElement>().FirstOrDefault();
            Assert.IsNotNull(root);
            Assert.AreEqual("html", root.Name.ToString());
            Assert.AreEqual(8, xhtml.Where<IElement>(x => x.Name.ToString() == "link").Count());
            Assert.AreEqual(277, xhtml.Where<IElement>(x => x.Name.ToString() == "div").Count());
            var p = xhtml.Where<IElement>(x => x.Name.ToString() == "p" && x.ToString().Contains(paragraphContent)).FirstOrDefault();
            Assert.IsNotNull(p);
        }

        [Test]
        public void TestArsTechnicaXhtml()
        {
            const string path = @".\Files\arstechnica.html";

            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);

            var xhtml = location.ToXhtmlDocument();
            MakeDocumentAssertions(xhtml);
        }

        [Test]
        public void TestArsTechnicaXhtmlFromXhtmlOutput()
        {
            const string path = @".\Files\arstechnica.html";

            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);

            var original = location.ToXhtmlDocument();
            var xhtml = XhtmlElement.Parse(original.ToString());
            MakeDocumentAssertions(xhtml);
        }

        [Test]
        public void TestArsTechnicaXhtmlAsXml()
        {
            const string path = @".\Files\arstechnica.html";

            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);

            var xml = location.ToXmlDocument();
            MakeDocumentAssertions(xml);
            //Assert.IsNotNull(xml);
            //Assert.IsNotNull(xml.DocumentType);
            //Assert.AreEqual(EntityVisibility.Public, xml.DocumentType.Visibility);
        }

    }
}
