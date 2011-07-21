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
        [Test]
        public void CreateXmlDocumentFromLocalFile()
        {
            const string path = @".\Files\arstechnica.xml";

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var xml = location.ToXmlDocument();
            Assert.IsNotNull(xml);
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
            Assert.IsNotNull(xml);
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
