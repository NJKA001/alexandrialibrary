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

        private readonly LookupByLocation byLocation = new LookupByLocation();

        private readonly SearchAll all = new SearchAll();
        private readonly SearchByAuthors byAuthors = new SearchByAuthors();
        private readonly SearchByTitle byTitle = new SearchByTitle();
        private readonly SearchCategoriesByParent categoriesByParent = new SearchCategoriesByParent();
        private readonly SearchItemsByParent itemsByParent = new SearchItemsByParent();
        private readonly SearchLinksByParent linksByParent = new SearchLinksByParent();
        private readonly SearchMetadataByParent metadataByParent = new SearchMetadataByParent();
        private readonly SearchItemCategoriesByParent itemCategoriesByParent = new SearchItemCategoriesByParent();
        private readonly SearchItemLinksByParent itemLinksByParent = new SearchItemLinksByParent();
        private readonly SearchItemMetadataByParent itemMetadataByParent = new SearchItemMetadataByParent();

        private readonly SearchByKeyword byKeyword = new SearchByKeyword();

        public IFeed LookupByLocation(Uri location)
        {
            return Select(byLocation.GetFilter(location))
                .FirstOrDefault();
        }

        public IEnumerable<IFeed> SearchByAuthors(string authors)
        {
            return Select(byAuthors.GetFilter(authors));
        }

        public IEnumerable<IFeed> SearchByKeyword(string keyword)
        {
            return Select(byKeyword.GetFilter(keyword));
        }

        public IEnumerable<IFeed> SearchByTitle(string title)
        {
            return Select(byTitle.GetFilter(title));
        }
    }
}
