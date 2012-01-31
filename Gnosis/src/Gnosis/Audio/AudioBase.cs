using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Audio
{
    public abstract class AudioBase
        : IAudio
    {
        protected AudioBase(Uri location, IContentType type)
        {
            this.location = location;
            this.type = type;
        }

        private readonly Uri location;
        private readonly IContentType type;

        public Uri Location
        {
            get { return location; }
        }

        public IContentType Type
        {
            get { return type; }
        }

        public virtual void Load()
        {
        }

        public virtual IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public virtual IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }

        public virtual IArtist GetArtist(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaItemRepository mediaItemRepository)
        {
            return null;
        }

        public virtual IAlbum GetAlbum(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaItemRepository mediaItemRepository, IArtist artist)
        {
            return null;
        }

        public virtual ITrack GetTrack(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaItemRepository mediaItemRepository, IAudioStreamFactory audioStreamFactory, IArtist artist, IAlbum album)
        {
            return null;
        }
    }
}
