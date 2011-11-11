using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public class MediaDetail
        : IMediaDetail
    {
        public MediaDetail(ITag tag, IImage artistThumbnail, IImage collectionThumbnail)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            this.tag = tag;
            this.artistThumbnail = artistThumbnail;
            this.collectionThumbnail = collectionThumbnail;
        }

        private readonly ITag tag;
        private readonly IImage artistThumbnail;
        private readonly IImage collectionThumbnail;

        public ITag Tag
        {
            get { return tag; }
        }

        public IImage ArtistThumbnail
        {
            get { return artistThumbnail; }
        }

        public IImage CollectionThumbnail
        {
            get { return collectionThumbnail; }
        }
    }
}
