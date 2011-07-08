using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.Atom;
using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class AtomTests
    {
        [Test]
        public void CreateAtomFeedFromLocalXml()
        {
            #region Constants

            const string path = @".\Files\bearbrarian.xml";
            const string title = "A Bear Librarians's Cave";
            const string authorName = "David";
            const string authorUri = "http://www.blogger.com/profile/06751101786776663258";
            const string authorEmail = "noreply@blogger.com";

            #endregion

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var contentType = location.ToContentType();
            Assert.AreEqual(MediaType.ApplicationAtomXml, contentType.Type);

            var feed = location.ToAtomFeed();

            Assert.IsNotNull(feed);
            Assert.IsNotNull(feed.Title);
            Assert.AreEqual(title, feed.Title.Text);
            Assert.AreEqual(1, feed.StyleSheets.Count());
            Assert.AreEqual(4, feed.Namespaces.Count());
            Assert.AreEqual(1, feed.Authors.Count());
            Assert.AreEqual(authorName, feed.Authors.First().Name);
            Assert.AreEqual(authorUri, feed.Authors.First().Uri.ToString());
            Assert.AreEqual(authorEmail, feed.Authors.First().Email);
        }
    }
}
