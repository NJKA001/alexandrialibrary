using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Metadata;

namespace Gnosis.Application.Vendor
{
    public class GnosisClip
        : GnosisMediaItemBase, IClip
    {
        public GnosisClip(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
            : base(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo)
        {
        }
        
        public static readonly IClip Unknown = new GnosisClip(IdentityInfo.GetDefault(MediaType.ApplicationGnosisClip), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, ThumbnailInfo.Default);
    }
}
