using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Helpers;
using Gnosis.Alexandria.Models.Feeds;
using Gnosis.Core;
using Gnosis.Core.Commands;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class FeedRepository
        : RepositoryBase<IFeed>, IFeedRepository
    {
        public FeedRepository(IContext context, ILogger logger)
            : this(context, logger, null)
        {
        }

        public FeedRepository(IContext context, ILogger logger, IDbConnection defaultConnection)
            : base(context, logger, new FeedFactory(context, logger), defaultConnection)
        {
            AddLookup(byLocation);
            AddSearch(all);
            AddSearch(byAuthors);
            AddSearch(byTitle);
            AddSearch(categoriesByParent);
            AddSearch(itemsByParent);
            AddSearch(linksByParent);
            AddSearch(metadataByParent);
            AddSearch(itemCategoriesByParent);
            AddSearch(itemLinksByParent);
            AddSearch(itemMetadataByParent);
        }

        private readonly ILookup byLocation = new LookupByLocation();

        private readonly ISearch all = new SearchAll();
        private readonly ISearch byAuthors = new SearchByAuthors();
        private readonly ISearch byTitle = new SearchByTitle();
        private readonly ISearch categoriesByParent = new SearchCategoriesByParent();
        private readonly ISearch itemsByParent = new SearchItemsByParent();
        private readonly ISearch linksByParent = new SearchLinksByParent();
        private readonly ISearch metadataByParent = new SearchMetadataByParent();
        private readonly ISearch itemCategoriesByParent = new SearchItemCategoriesByParent();
        private readonly ISearch itemLinksByParent = new SearchItemLinksByParent();
        private readonly ISearch itemMetadataByParent = new SearchItemMetadataByParent();

        public IFeed LookupByLocation(Uri location)
        {
            return Lookup(byLocation, new Dictionary<string, object> { { "@Location", location } });
        }

        public IEnumerable<IFeed> SearchByAuthors(string authors)
        {
            return Search(byAuthors, new Dictionary<string, object> { { "@Authors", authors } });
        }

        public IEnumerable<IFeed> SearchByTitle(string title)
        {
            return Search(byTitle, new Dictionary<string, object> { { "@Title", title } });
        }
    }
}
