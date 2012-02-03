using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Application;
using Gnosis.Application.Xml;
using Gnosis.Application.Xml.Xspf;

using NUnit.Framework;

namespace Gnosis.Tests.Unit.Application.Xml
{
    [TestFixture]
    public class XspfDocuments
    {
        public XspfDocuments()
        {
            logger = new Gnosis.Utilities.DebugLogger();
            mediaFactory = new MediaFactory(logger);
        }

        private ILogger logger;
        private IMediaFactory mediaFactory;

        [Test]
        public void CanBeParsedFromLocalXspfFile()
        {
            const string path = @".\Files\playlist.xspf";
            const int trackCount = 3;

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);

            var document = mediaFactory.Create(location) as IXmlDocument;
            Assert.IsNotNull(document);
            Assert.IsNull(document.Xml);
            Assert.AreEqual("application/xspf+xml", document.Type.Name);

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
