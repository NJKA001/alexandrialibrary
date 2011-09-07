using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Spiders.LyricsWikia;

using NUnit.Framework;

namespace Gnosis.Tests.Core.Spiders
{
    [TestFixture]
    public class LyricsWikiaSpiderTests
    {
        private readonly IRepresentationFactory factory = new RepresentationFactory();

        [Test]
        public void TestArtistsCategorySpider()
        {
            var spider = new ArtistsCategorySpider(factory);
            var graph = new RepresentationGraph();
            spider.Crawl(graph);

            Assert.AreEqual(1, graph.GetRepresentations().Count());
        }
    }
}
