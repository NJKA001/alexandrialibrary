using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Metadata;

namespace Gnosis.Application.Vendor
{
    public class GnosisPlaylistItem
        : GnosisMediaItemBase, IPlaylistItem
    {
        public GnosisPlaylistItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
            : base(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo)
        {
        }
        
        public static readonly IPlaylistItem Unknown = new GnosisPlaylistItem(IdentityInfo.GetDefault(MediaType.ApplicationGnosisPlaylistItem), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, ThumbnailInfo.Default);
    }
}
