using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IVideo
        : IMedia
    {
        IArtist GetArtist(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaItemRepository mediaItemRepository);
        IAlbum GetAlbum(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaItemRepository mediaItemRepository, IArtist artist);
        IClip GetClip(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaItemRepository mediaItemRepository, IArtist artist, IAlbum album);
    }
}
