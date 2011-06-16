using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class TrackTests
    {
        private const string location = @"Files\03 - Antes De Las Seis.mp3";

        [Test]
        public void TestLyrics()
        {
            Assert.IsTrue(File.Exists(location));
        }
    }
}
