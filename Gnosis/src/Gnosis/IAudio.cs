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
        IArtist GetArtist(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository<ITrack> trackRepository, IMediaItemRepository<IArtist> artistRepository);
        IAlbum GetAlbum(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository<ITrack> trackRepository, IMediaItemRepository<IAlbum> albumRepository, IArtist artist);
        ITrack GetTrack(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository<ITrack> trackRepository, IAudioStreamFactory audioStreamFactory, IArtist artist, IAlbum album);
    }
}
