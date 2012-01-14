using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Metadata
{
    public class Prog
        : MediaItemBase, IProgram
    {
        public Prog(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
            : base(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo)
        {
        }
        
        public static readonly IProgram Unknown = new Prog(IdentityInfo.GetDefault(MediaType.ApplicationGnosisProgram), SizeInfo.Default, CreatorInfo.Default, CatalogInfo.Default, TargetInfo.Default, UserInfo.Default, ThumbnailInfo.Default); 
    }
}
