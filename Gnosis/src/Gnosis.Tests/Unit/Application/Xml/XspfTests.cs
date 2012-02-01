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
            characterSetFactory = new CharacterSetFactory();
            mediaFactory = new MediaFactory();
            contentTypeFactory = new ContentTypeFactory(logger, characterSetFactory);
        }

        private ILogger logger;
        private ICharacterSetFactory characterSetFactory;
        private IMediaFactory mediaFactory;
        private IContentTypeFactory contentTypeFactory;

        [Test]
        public void CanBeParsedFromLocalXspfFile()
        {
            const string path = @".\Files\playlist.xspf";
            const int trackCount = 3;

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var contentType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("application/xspf+xml", contentType.Name);

            var document = mediaFactory.Create(location, contentTypeFactory.GetByLocation(location)) as IXmlDocument;
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
