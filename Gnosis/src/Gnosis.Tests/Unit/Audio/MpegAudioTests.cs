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
        private const string location1 = @"Files\03 - Antes De Las Seis.mp3";

        private readonly IMediaFactory mediaFactory = new MediaFactory();

        [Test]
        public void CanBeLoaded()
        {
            var file = new System.IO.FileInfo(location1);
            Assert.IsTrue(file.Exists);
            var audio = mediaFactory.Create(new Uri(file.FullName)) as IMpegAudio;
            Assert.IsNotNull(audio);
            audio.Load();
        }
    }
}
