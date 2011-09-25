using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

using NUnit.Framework;

namespace Gnosis.Tests.Core
{
    [TestFixture]
    public class ExtensionTests
    {
        [Test]
        public void StringToLevenshteinDistanceTest()
        {
            Assert.AreEqual(3, "kitten".ToLevenshteinDistance("sitting"));
            Assert.AreEqual(2, "CHROMEO".ToLevenshteinDistance("ROMEO"));
            Assert.AreEqual(5, "adz".ToLevenshteinDistance("hazard"));
        }
    }
}
