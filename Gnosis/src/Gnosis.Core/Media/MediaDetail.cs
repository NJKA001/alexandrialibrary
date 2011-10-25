using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Image;

namespace Gnosis.Core.Media
{
    public class MediaDetail
        : IMediaDetail
    {
        public MediaDetail(ITag tag, IImage thumbnail)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            this.tag = tag;
            this.thumbnail = thumbnail;
        }

        private readonly ITag tag;
        private readonly IImage thumbnail;

        public ITag Tag
        {
            get { return tag; }
        }

        public IImage Thumbnail
        {
            get { return thumbnail; }
        }
    }
}
