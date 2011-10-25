using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Document;
using Gnosis.Document.Xml;
using Gnosis.Document.Xml.Xspf;

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

            var mediaFactory = new MediaFactory();

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var contentType = ContentType.GetContentType(location); //.ToContentType();
            Assert.AreEqual(MediaType.ApplicationXspfXml, contentType.Type);

            var document = mediaFactory.Create(location, contentType.Type) as IXmlDocument;
            Assert.IsNotNull(document);
            Assert.IsNull(document.Xml);
            
            document.Load();
            Assert.IsNotNull(document.Xml);
            Assert.IsNotNull(document.Xml.Root);

            var playlist = document.Xml.Root as IXspfPlaylist;
            Assert.IsNotNull(playlist);

            var tracks = playlist.Where<IXspfTrack>(x => x != null);
            Assert.AreEqual(trackCount, tracks.Count());
        }
    }
}
