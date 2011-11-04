using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchMetadataByParent
        : ValueSearchBase<IFeed, IFeedMetadatum>
    {
        public SearchMetadataByParent()
            : base("Feed_Metadata.Parent = @Parent", 
            parent => parent.Metadata, 
            metadata => metadata.Parent, metadata => metadata.Sequence)
        {
        }
    }
}
