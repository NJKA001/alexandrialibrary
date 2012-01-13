using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Metadata;

namespace Gnosis.Application.Vendor
{
    public class GnosisAlbum
        : GnosisMediaItemBase, IAlbum
    {
        public GnosisAlbum(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
            : base(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo)
        {
        }
        
        public static readonly IAlbum Unknown = new GnosisAlbum(IdentityInfo.GetDefault(MediaType.ApplicationGnosisAlbum), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, ThumbnailInfo.Default);
    }
}
