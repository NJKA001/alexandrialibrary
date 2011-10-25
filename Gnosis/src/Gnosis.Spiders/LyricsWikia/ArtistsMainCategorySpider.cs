using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Document;
using Gnosis.Core.Document.Xml;
using Gnosis.Core.Document.Xml.Xhtml;

namespace Gnosis.Spiders.LyricsWikia
{
    public class ArtistsMainCategorySpider
    {
        public ArtistsMainCategorySpider(IMediaFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            this.factory = factory;
        }


        private readonly IMediaFactory factory;
        //private readonly Uri defaultLocation = new Uri("http://lyrics.wikia.com/Category:Artists");

        public void Crawl()
        {
            /*
            if (graph == null)
                throw new ArgumentNullException("graph");
            
            var document = factory.Create(graph.Source) as IXmlDocument;
            if (document == null)
                return;

            document.Load();
            foreach (var anchor in document.Xml.Root.Where<IHtmlAnchor>(x => x != null && x.Href.ToString().StartsWith("/Category:Artists_")))
            {
                System.Diagnostics.Debug.WriteLine("anchor. href=" + anchor.Href.ToString());
                //graph.AddChild(new LinkGraph(anchor.Href, anchor.Content, anchor.Rel, anchor.Rev));
            }
            */
        }
    }
}
