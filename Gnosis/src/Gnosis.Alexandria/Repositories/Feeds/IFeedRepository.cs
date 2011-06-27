using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public interface IFeedRepository
        : IRepository<IFeed>
    {
        IFeed LookupByLocation(Uri location);
        IEnumerable<IFeed> SearchByAuthors(string authors);
        IEnumerable<IFeed> SearchByKeyword(string keyword);
        IEnumerable<IFeed> SearchByTitle(string title);

        IEnumerable<IFeedItem> SearchFeedItemsByParent(Guid parent);
        IEnumerable<IFeedItem> SearchFeedItemsByKeyword(string keyword);

        IEnumerable<ITag> SearchForTitleTags(Uri scheme, string value);
        IEnumerable<IFeed> SearchByTitleTags(Uri scheme, string value);

        IEnumerable<IFeedOutline> SearchOutlinesByTitle(string title);
    }
}
