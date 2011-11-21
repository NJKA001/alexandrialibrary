using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IAudio
        : IMedia
    {
        IArtist GetArtist(IMediaItemRepository<IArtist> artistRepository);
        IAlbum GetAlbum(IMediaItemRepository<IAlbum> albumRepository, IArtist artist);
        ITrack GetTrack(IMediaItemRepository<ITrack> trackRepository, IArtist artist, IAlbum album);
    }
}
