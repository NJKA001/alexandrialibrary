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
            const int categoryCount = 44;

            var spider = new ArtistsMainCategorySpider(factory);
            var root = new Uri("http://lyrics.wikia.com/Category:Artists");
            var graph = new LinkGraph(root, "Artists Categories", null, null);
            spider.Crawl(graph);

            Assert.AreEqual(categoryCount, graph.Children.Count());
        }
    }
}
