using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IVideo
        : IMedia
    {
        IArtist GetArtist(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository mediaItemRepository);
        IAlbum GetAlbum(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository mediaItemRepository, IArtist artist);
        IClip GetClip(ISecurityContext securityContext, IMediaTypeFactory mediaTypeFactory, IMediaItemRepository mediaItemRepository, IArtist artist, IAlbum album);
    }
}
