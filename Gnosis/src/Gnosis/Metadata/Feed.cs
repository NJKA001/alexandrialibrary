using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public class Feed
        : MediaItemBase, IFeed
    {
        public Feed(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
            : base(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo)
        {
        }
        
        public static readonly IFeed Unknown = new Feed(IdentityInfo.GetDefault(MediaType.ApplicationGnosisFeed), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, ThumbnailInfo.Default);
    }
}
