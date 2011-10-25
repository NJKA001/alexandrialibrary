using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Spiders;
using Gnosis.Spiders.LyricsWikia;

using NUnit.Framework;

namespace Gnosis.Tests.Network.Spiders
{
    [TestFixture]
    public class LyricsWikiaSpiderTests
    {
        private readonly IMediaFactory factory = new MediaFactory();

        [Test]
        public void TestArtistsCategorySpider()
        {
            var spider = new ArtistsMainCategorySpider(factory);
            var root = new Uri("http://lyrics.wikia.com/Category:Artists");
            //var graph = new LinkGraph(root, "Artists Categories", null, null);
            spider.Crawl(); //graph);

            //Assert.IsTrue(graph.Children.Count() > 1);
        }
    }
}
