using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Helpers;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Feeds;
using Gnosis.Core;
using Gnosis.Core.Commands;
using Gnosis.Core.Queries;

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
        private readonly SearchItemsByKeyword itemsByKeyword = new SearchItemsByKeyword();
        private readonly SearchLinksByParent linksByParent = new SearchLinksByParent();
        private readonly SearchMetadataByParent metadataByParent = new SearchMetadataByParent();
        private readonly SearchItemCategoriesByParent itemCategoriesByParent = new SearchItemCategoriesByParent();
        private readonly SearchItemLinksByParent itemLinksByParent = new SearchItemLinksByParent();
        private readonly SearchItemMetadataByParent itemMetadataByParent = new SearchItemMetadataByParent();
        private readonly SearchTitleHashCodesBySchemeAndValue titleHashCodesBySchemeAndValue = new SearchTitleHashCodesBySchemeAndValue();

        private readonly SearchByKeyword byKeyword = new SearchByKeyword();

        public IFeed LookupByLocation(Uri location)
        {
            var query = new Query<IFeed>(Logger, Factory, byLocation.GetFilter(location));
            return Lookup(query);
        }

        public IEnumerable<IFeed> SearchByAuthors(string authors)
        {
            var query = new Query<IFeed>(Logger, Factory, byAuthors.GetFilter(authors));
            return Search(query);
        }

        public IEnumerable<IFeed> SearchByKeyword(string keyword)
        {
            //return SelectForward(byKeyword.GetFilter(keyword));
            var query = new ForwardQuery<IFeed>(Logger, Factory, byKeyword.GetFilter(keyword));
            return Search(query);
        }

        public IEnumerable<IFeedItem> SearchFeedItemsByKeyword(string keyword)
        {
            var query = new ForwardQuery<IFeedItem>(Logger, Factory, itemsByKeyword.GetFilter(keyword));
            return SelectEntities<IFeedItem>(query);
            //return SelectForward<IFeedItem>(itemsByKeyword.GetFilter(keyword));
        }

        public IEnumerable<IFeed> SearchByTitle(string title)
        {
            var query = new Query<IFeed>(Logger, Factory, byTitle.GetFilter(title));
            return Search(query);
        }

        public IEnumerable<IHashCode> SearchTitleHashCodesBySchemeAndValue(Uri scheme, string value)
        {
            var query = new ValueQuery<IFeed, IHashCode>(Logger, Factory, titleHashCodesBySchemeAndValue.GetFilter(scheme, value), feed => feed.TitleHashCodes);
            return SelectValues<IHashCode>(query);
            //return Select<IHashCode>(titleHashCodesBySchemeAndValue.GetFilter(scheme, value), feed => feed.TitleHashCodes);
        }

        public IEnumerable<IFeedItem> SearchFeedItemsByParent(Guid parent)
        {
            var query = new Query<IFeedItem>(Logger, Factory, itemsByParent.GetFilter(parent));
            return SelectEntities<IFeedItem>(query);
            //return SelectChild<IFeedItem>(itemsByParent.GetFilter(new Dictionary<string, object> { { "@Parent", parent.ToString() } }));
        }

        public IEnumerable<IFeed> SearchByTitleHashCodes(Uri scheme, string value)
        {
            var filter = titleHashCodesBySchemeAndValue.GetFilter(scheme, value);
            var query = new ReverseQuery<IFeed, IHashCode>(Logger, Factory, filter, feed => feed.TitleHashCodes);
            return Search(query);
            //return SelectReverse<IHashCode>(titleHashCodesBySchemeAndValue.GetFilter(scheme, value), "Feed.Authors ASC, Feed.PublishedDate ASC, Feed.Title ASC", feed => feed.TitleHashCodes);
        }

        public IEnumerable<IFeedOutline> SearchOutlinesByTitle(string title)
        {
            var query = new OutlineQuery<IFeed, IFeedOutline>(Logger, () => new FeedOutline(), byTitle.GetFilter(title));
            return SelectOutlines<IFeed, IFeedOutline>(query);
        }
    }
}
