using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public class FeedItem
        : MediaItemBase, IFeedItem
    {
        public FeedItem(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
            : base(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo)
        {
        }
        
        public static readonly IFeedItem Unknown = new FeedItem(IdentityInfo.GetDefault(MediaType.ApplicationGnosisFeedItem), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, ThumbnailInfo.Default);
    }
}
