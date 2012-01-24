using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IVideo
        : IMedia
    {
        IArtist GetArtist(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository<IClip> clipRepository, IMediaItemRepository<IArtist> artistRepository);
        IAlbum GetAlbum(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository<IClip> clipRepository, IMediaItemRepository<IAlbum> albumRepository, IArtist artist);
        IClip GetClip(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository<IClip> clipRepository, IArtist artist, IAlbum album);
    }
}
