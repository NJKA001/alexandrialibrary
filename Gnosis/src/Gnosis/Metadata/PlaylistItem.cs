using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public class PlaylistItem
        : MediaItemBase, IPlaylistItem
    {
        public PlaylistItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
            : base(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo)
        {
        }
        
        public static readonly IPlaylistItem Unknown = new PlaylistItem(IdentityInfo.GetDefault(MediaType.ApplicationGnosisPlaylistItem), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, ThumbnailInfo.Default);
    }
}
