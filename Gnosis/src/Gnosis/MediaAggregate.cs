using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class MediaAggregate
        : IMediaAggregate
    {
        public MediaAggregate(IMedia media, IEnumerable<ILink> links, IEnumerable<ITag> tags)
        {
            if (media == null)
                throw new ArgumentNullException("media");
            if (links == null)
                throw new ArgumentNullException("links");
            if (tags == null)
                throw new ArgumentNullException("tags");

            this.media = media;
            this.links = links;
            this.tags = tags;
        }

        private readonly IMedia media;
        private readonly IEnumerable<ILink> links;
        private readonly IEnumerable<ITag> tags;

        public IMedia Media
        {
            get { return media; }
        }

        public IEnumerable<ILink> Links
        {
            get { return links; }
        }

        public IEnumerable<ITag> Tags
        {
            get { return tags; }
        }
    }
}
