using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IVideo
        : IMedia
    {
        IArtist GetArtist(ISecurityContext securityContext, IMediaFactory mediaFactory, IMetadataRepository mediaItemRepository);
        IAlbum GetAlbum(ISecurityContext securityContext, IMediaFactory mediaFactory, IMetadataRepository mediaItemRepository, IArtist artist);
        IClip GetClip(ISecurityContext securityContext, IMediaFactory mediaFactory, IMetadataRepository mediaItemRepository, IArtist artist, IAlbum album);
    }
}
