using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Document.Xml.Xspf;

using NUnit.Framework;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class XspfTests
    {
        [Test]
        public void LoadLocalXspfDocument()
        {
            const string path = @".\Files\playlist.xspf";
            const int trackCount = 3;

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var contentType = location.ToContentType();
            Assert.AreEqual(MediaType.ApplicationXspfXml, contentType.Type);

            var document = location.ToXmlDocument();
            Assert.IsNotNull(document);
            Assert.IsNotNull(document.Root);

            var playlist = document.Root as IXspfPlaylist;
            Assert.IsNotNull(playlist);

            var tracks = playlist.Where<IXspfTrack>(x => x != null);
            Assert.AreEqual(trackCount, tracks.Count());
        }
    }
}
