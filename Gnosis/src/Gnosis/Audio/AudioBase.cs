using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Audio
{
    public abstract class AudioBase
        : IAudio
    {
        protected AudioBase(Uri location, IMediaType type)
        {
            this.location = location;
            this.type = type;
        }

        private readonly Uri location;
        private readonly IMediaType type;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
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

        public virtual IArtist GetArtist(ISecurityContext securityContext, IMediaFactory mediaFactory, IMediaItemRepository mediaItemRepository)
        {
            return null;
        }

        public virtual IAlbum GetAlbum(ISecurityContext securityContext, IMediaFactory mediaFactory, IMediaItemRepository mediaItemRepository, IArtist artist)
        {
            return null;
        }

        public virtual ITrack GetTrack(ISecurityContext securityContext, IMediaFactory mediaFactory, IMediaItemRepository mediaItemRepository, IAudioStreamFactory audioStreamFactory, IArtist artist, IAlbum album)
        {
            return null;
        }
    }
}
