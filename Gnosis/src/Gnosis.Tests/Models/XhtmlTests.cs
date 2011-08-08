using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Xml;

using NUnit.Framework;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class XhtmlTests
    {
        [Test]
        public void TestArsTechnicaXhtml()
        {
            const string path = @".\Files\arstechnica.html";

            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);

            var xhtml = location.ToXhtmlDocument();
            Assert.IsNotNull(xhtml);
            Assert.AreEqual(2, xhtml.Children.OfType<IComment>().Count());
            //Assert.IsNotNull(xhtml.Document);
            //Assert.IsNotNull(xhtml.Document.DocumentNode);

            //Gnosis.Core.Xml.Document.ParseHtml(location);
        }

        [Test]
        public void TestArsTechnicaXhtmlAsXml()
        {
            const string path = @".\Files\arstechnica.html";

            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);

            var xml = location.ToXmlDocument();
            Assert.IsNotNull(xml);
            Assert.IsNotNull(xml.DocumentType);
            Assert.AreEqual(EntityVisibility.Public, xml.DocumentType.Visibility);
        }

    }
}
