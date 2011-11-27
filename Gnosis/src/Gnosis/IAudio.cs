using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IAudio
        : IMedia
    {
        IArtist GetArtist(ISecurityContext securityContext, IMediaItemRepository<IArtist> artistRepository);
        IAlbum GetAlbum(ISecurityContext securityContext, IMediaItemRepository<IAlbum> albumRepository, IArtist artist);
        ITrack GetTrack(ISecurityContext securityContext, IMediaItemRepository<ITrack> trackRepository, IArtist artist, IAlbum album);
    }
}
