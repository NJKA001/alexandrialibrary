using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public class Album
        : MediaItemBase, IAlbum
    {
        public Album(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
            : base(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo)
        {
        }
        
        public static readonly IAlbum Unknown = new Album(IdentityInfo.GetDefault(MediaType.ApplicationGnosisAlbum), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, ThumbnailInfo.Default);
    }
}
