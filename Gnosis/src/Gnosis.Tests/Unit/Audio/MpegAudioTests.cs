using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Audio;

using NUnit.Framework;

namespace Gnosis.Tests.Unit.Audio
{
    [TestFixture]
    public class MpegAudio
    {
        public MpegAudio()
        {
            logger = new Gnosis.Utilities.DebugLogger();
            characterSetFactory = new CharacterSetFactory();
            mediaTypeFactory = new MediaTypeFactory(logger, characterSetFactory);
            contentTypeFactory = new ContentTypeFactory(logger, mediaTypeFactory, characterSetFactory);
        }

        private ILogger logger;
        private ICharacterSetFactory characterSetFactory;
        private IMediaTypeFactory mediaTypeFactory;
        private IContentTypeFactory contentTypeFactory;

        private const string location1 = @"Files\03 - Antes De Las Seis.mp3";

        [Test]
        public void CanBeLoaded()
        {
            var file = new System.IO.FileInfo(location1);
            var location = new Uri(file.FullName);
            Assert.IsTrue(file.Exists);
            var type = contentTypeFactory.GetByLocation(location);
            Assert.IsNotNull(type);
            var audio = type.CreateMedia(location) as IAudio;
            Assert.IsNotNull(audio);
            audio.Load();
        }
    }
}
