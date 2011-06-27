using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Feeds;
using Gnosis.Core;
using Gnosis.Data.Commands;
using Gnosis.Data.Queries;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class FeedRepository
        : RepositoryBase<IFeed>, IFeedRepository
    {
        public FeedRepository()
            : this(null)
        {
        }

        public FeedRepository(IDbConnection defaultConnection)
            : base(new FeedFactory(), defaultConnection)
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
        private readonly SearchTitleTagsBySchemeAndValue titleTagsBySchemeAndValue = new SearchTitleTagsBySchemeAndValue();

        private readonly SearchByKeyword byKeyword = new SearchByKeyword();

        public IFeed LookupByLocation(Uri location)
        {
            var query = new Query<IFeed>(Factory, byLocation.GetFilter(location));
            return Lookup(query);
        }

        public IEnumerable<IFeed> SearchByAuthors(string authors)
        {
            var query = new Query<IFeed>(Factory, byAuthors.GetFilter(authors));
            return Search(query);
        }

        public IEnumerable<IFeed> SearchByKeyword(string keyword)
        {
            //return SelectForward(byKeyword.GetFilter(keyword));
            var query = new ForwardQuery<IFeed>(Factory, byKeyword.GetFilter(keyword));
            return Search(query);
        }

        public IEnumerable<IFeedItem> SearchFeedItemsByKeyword(string keyword)
        {
            var query = new ForwardQuery<IFeedItem>(Factory, itemsByKeyword.GetFilter(keyword));
            return SelectEntities<IFeedItem>(query);
            //return SelectForward<IFeedItem>(itemsByKeyword.GetFilter(keyword));
        }

        public IEnumerable<IFeed> SearchByTitle(string title)
        {
            var query = new Query<IFeed>(Factory, byTitle.GetFilter(title));
            return Search(query);
        }

        public IEnumerable<ITag> SearchForTitleTags(Uri scheme, string value)
        {
            var query = new ValueQuery<IFeed, ITag>(Factory, titleTagsBySchemeAndValue.GetFilter(scheme, value), feed => feed.TitleTags);
            return SelectValues<ITag>(query);
            //return Select<ITag>(titleTagsBySchemeAndValue.GetFilter(scheme, value), feed => feed.TitleTags);
        }

        public IEnumerable<IFeedItem> SearchFeedItemsByParent(Guid parent)
        {
            var query = new Query<IFeedItem>(Factory, itemsByParent.GetFilter(parent));
            return SelectEntities<IFeedItem>(query);
            //return SelectChild<IFeedItem>(itemsByParent.GetFilter(new Dictionary<string, object> { { "@Parent", parent.ToString() } }));
        }

        public IEnumerable<IFeed> SearchByTitleTags(Uri scheme, string value)
        {
            var filter = titleTagsBySchemeAndValue.GetFilter(scheme, value);
            var query = new ReverseQuery<IFeed, ITag>(Factory, filter, feed => feed.TitleTags);
            return Search(query);
            //return SelectReverse<ITag>(titleTagsBySchemeAndValue.GetFilter(scheme, value), "Feed.Authors ASC, Feed.PublishedDate ASC, Feed.Title ASC", feed => feed.TitleTags);
        }

        public IEnumerable<IFeedOutline> SearchOutlinesByTitle(string title)
        {
            var query = new OutlineQuery<IFeed, IFeedOutline>(() => new FeedOutline(), byTitle.GetFilter(title));
            return SelectOutlines<IFeed, IFeedOutline>(query);
        }
    }
}
