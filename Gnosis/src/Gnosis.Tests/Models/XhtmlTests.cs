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
            //Assert.IsNotNull(xhtml.Document);
            //Assert.IsNotNull(xhtml.Document.DocumentNode);

            //Gnosis.Core.Xml.Document.ParseHtml(location);
        }
    }
}
