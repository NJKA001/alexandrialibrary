using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Document;
using Gnosis.Core.Document.Xml;
using Gnosis.Core.Document.Xml.Xhtml;

namespace Gnosis.Core.Spiders.LyricsWikia
{
    public class ArtistsCategorySpider
        : ISpider
    {
        public ArtistsCategorySpider(IRepresentationFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            this.factory = factory;
        }


        private readonly IRepresentationFactory factory;
        private readonly Uri defaultLocation = new Uri("http://lyrics.wikia.com/Category:Artists");

        public void Crawl(IRepresentationGraph graph)
        {
            Crawl(graph, defaultLocation);
        }

        public void Crawl(IRepresentationGraph graph, Uri location)
        {
            if (graph == null)
                throw new ArgumentNullException("graph");
            if (location == null)
                throw new ArgumentNullException("location");

            var document = factory.Create(location) as IXmlDocument;
            if (document == null)
                return;

            graph.AddRepresentation(document);

            foreach (var anchor in document.Xml.Root.Where<IHtmlAnchor>(x => x != null))
            {
                System.Diagnostics.Debug.WriteLine("anchor. href=" + anchor.Href.ToString());
                //TODO: filter anchors for the Aritst letter links
            }
        }
    }
}
