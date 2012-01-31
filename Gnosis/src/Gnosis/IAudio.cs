using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Audio;

namespace Gnosis
{
    public interface IAudio
        : IMedia
    {
        IArtist GetArtist(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaItemRepository mediaItemRepository);
        IAlbum GetAlbum(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaItemRepository mediaItemRepository, IArtist artist);
        ITrack GetTrack(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaItemRepository mediaItemRepository, IAudioStreamFactory audioStreamFactory, IArtist artist, IAlbum album);
    }
}
