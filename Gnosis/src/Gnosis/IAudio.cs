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
        IArtist GetArtist(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository mediaItemRepository);
        IAlbum GetAlbum(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository mediaItemRepository, IArtist artist);
        ITrack GetTrack(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository mediaItemRepository, IAudioStreamFactory audioStreamFactory, IArtist artist, IAlbum album);
    }
}
