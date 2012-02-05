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
        IArtist GetArtist(ISecurityContext securityContext, IMediaFactory mediaFactory, IMetadataRepository mediaItemRepository);
        IAlbum GetAlbum(ISecurityContext securityContext, IMediaFactory mediaFactory, IMetadataRepository mediaItemRepository, IArtist artist);
        ITrack GetTrack(ISecurityContext securityContext, IMediaFactory mediaFactory, IMetadataRepository mediaItemRepository, IAudioStreamFactory audioStreamFactory, IArtist artist, IAlbum album);
    }
}
