using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchLinksByParent
        : ValueSearchBase<IFeed, IFeedLink>
    {
        public SearchLinksByParent()
            : base("Feed_Links.Parent = @Parent", 
            parent => parent.Links, 
            link => link.Parent, link => link.Sequence)
        {
        }
    }
}
