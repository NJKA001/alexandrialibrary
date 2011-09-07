using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Document;
using Gnosis.Core.Document.Xml;
using Gnosis.Core.Document.Xml.Xhtml;

namespace Gnosis.Core.Spiders.LyricsWikia
{
    public class ArtistsMainCategorySpider
        : ISpider
    {
        public ArtistsMainCategorySpider(IRepresentationFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            this.factory = factory;
        }


        private readonly IRepresentationFactory factory;
        private readonly Uri defaultLocation = new Uri("http://lyrics.wikia.com/Category:Artists");

        public void Crawl(ILinkGraph graph, Uri location)
        {
            if (graph == null)
                throw new ArgumentNullException("graph");
            if (location == null)
                throw new ArgumentNullException("location");

            var document = factory.Create(location) as IXmlDocument;
            if (document == null)
                return;

            foreach (var anchor in document.Xml.Root.Where<IHtmlAnchor>(x => x != null && x.Href.ToString().StartsWith("/Category:Artists_")))
            {
                System.Diagnostics.Debug.WriteLine("anchor. href=" + anchor.Href.ToString());
                graph.AddChild(new LinkGraph(anchor.Href, anchor.Content, anchor.Rel, anchor.Rev));
                //var subCategory = factory.Create(anchor.Href);
                //var link = new RepresentationLink(anchor.Content, anchor.Rel, anchor.Rev, document, subCategory);
                //graph.AddLink(link);
                //System.Threading.Thread.Sleep(2000);
            }
        }
    }
}
