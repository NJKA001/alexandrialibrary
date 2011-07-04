using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.Rss;
using Gnosis.Core.W3c;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class RssTests
    {
        [Test]
        public void CreateChannel()
        {
            const string path = @".\Files\arstechnica.xml";
            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);
            
            var location = new Uri(fileInfo.FullName);
            var contentType = location.ToContentType();
            Assert.AreEqual(MediaType.RssFeed, contentType.Type);
            
            var factory = new RssChannelFactory();
            var channel = factory.Create(location);
            

            Assert.IsNotNull(channel);
        }
    }
}
