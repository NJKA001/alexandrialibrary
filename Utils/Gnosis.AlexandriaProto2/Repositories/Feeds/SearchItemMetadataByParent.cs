﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchItemMetadataByParent
        : ValueSearchBase<IFeedItem, IFeedMetadatum>
    {
        public SearchItemMetadataByParent()
            : base("FeedItem_Metadata.Parent = @Parent",
            parent => parent.Metadata,
            metadata => metadata.Parent, metadata => metadata.Sequence)
        {
        }
    }
}
