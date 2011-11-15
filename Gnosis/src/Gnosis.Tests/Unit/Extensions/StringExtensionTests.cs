using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Gnosis.Tests.Unit.Extensions
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test]
        public void StringToLevenshteinDistanceTest()
        {
            Assert.AreEqual(3, "kitten".ToLevenshteinDistance("sitting"));
            Assert.AreEqual(2, "CHROMEO".ToLevenshteinDistance("ROMEO"));
            Assert.AreEqual(5, "adz".ToLevenshteinDistance("hazard"));
            Assert.AreEqual(0, "apples".ToLevenshteinDistance("apples"));
            Assert.AreEqual(0, "".ToLevenshteinDistance(""));
            Assert.AreEqual(1, "a".ToLevenshteinDistance(""));
        }

        [Test]
        public void StringToHammingDistanceTest()
        {
            Assert.AreEqual(-1, "apples".ToHammingDistance("oranges"));
            Assert.AreEqual(0, "oranges".ToHammingDistance("oranges"));
            Assert.AreEqual(0, "".ToHammingDistance(""));
            Assert.AreEqual(1, "cat".ToHammingDistance("bat"));
            Assert.AreEqual(1, "x".ToHammingDistance("y"));
            Assert.AreEqual(2, "prism".ToHammingDistance("pried"));
            Assert.AreEqual(3, "abc".ToHammingDistance("xyz"));
            Assert.AreEqual(4, "black".ToHammingDistance("brown"));
        }
    }
}
