using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;
using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class FeedRepository
        : RepositoryBase<IFeed>, IFeedRepository
    {
        public FeedRepository(IContext context)
            : base(context)
        {
        }

        protected override IFeed CreateDefault()
        {
            return Create(UriExtensions.EmptyUri);
        }

        private IDictionary<Guid, IFeed> GetFeeds(IDataReader reader)
        {
            var feeds = new Dictionary<Guid, IFeed>();

            do
            {
                var timeStamp = GetTimeStamp(reader);
                var id = new Guid(reader["Id"].ToString());
                var location = new Uri(reader["Location"].ToString());
                var mediaType = reader["MediaType"].ToString();
                var title = reader["Title"].ToString();
                var authors = reader["Authors"].ToString();
                var contributors = reader["Contributors"].ToString();
                var description = reader["Description"].ToString();
                var language = reader["Language"].ToString();
                var originalLocation = new Uri(reader["OriginalLocation"].ToString());
                var copyright = reader["Copyright"].ToString();
                var publishedDate = DateTime.Parse(reader["PublishedDate"].ToString());
                var updatedDate = DateTime.Parse(reader["UpdatedDate"].ToString());
                var generator = reader["Generator"].ToString();
                var imagePath = new Uri(reader["ImagePath"].ToString());
                var iconPath = new Uri(reader["IconPath"].ToString());
                var feedIdentifier = reader["FeedIdentifier"].ToString();

                var feed = new Feed(Context, id, timeStamp, location, mediaType, title, authors, contributors, description, language, originalLocation, copyright, publishedDate, updatedDate, generator, imagePath, iconPath, feedIdentifier);
                feeds.Add(feed.Id, feed);
            }
            while (reader.Read());

            return feeds;
        }

        private IDictionary<Guid, IList<IFeedCategory>> GetFeedCategories(IDataReader reader)
        {
            var categories = new Dictionary<Guid, IList<IFeedCategory>>();

            do
            {
                var parent = new Guid(reader["Parent"].ToString());
                var scheme = new Uri(reader["Scheme"].ToString());
                var name = reader["Name"].ToString();
                var label = reader["Label"].ToString();

                var category = new FeedCategory(scheme, name, label);

                if (!categories.ContainsKey(parent))
                    categories.Add(parent, new List<IFeedCategory>());

                categories[parent].Add(category);
            }
            while (reader.Read());

            return categories;
        }

        private IDictionary<Guid, IList<IFeedLink>> GetFeedLinks(IDataReader reader)
        {
            var links = new Dictionary<Guid, IList<IFeedLink>>();

            return links;
        }

        private IDictionary<Guid, IList<IFeedMetadata>> GetFeedMetadata(IDataReader reader)
        {
            var metadata = new Dictionary<Guid, IList<IFeedMetadata>>();

            return metadata;
        }

        private IDictionary<Guid, IList<IFeedItem>> GetFeedItems(IDataReader reader)
        {
            var items = new Dictionary<Guid, IList<IFeedItem>>();

            return items;
        }

        private IDictionary<Guid, IList<IFeedCategory>> GetFeedItemCategories(IDataReader reader)
        {
            var categories = new Dictionary<Guid, IList<IFeedCategory>>();

            do
            {
                var parent = new Guid(reader["Parent"].ToString());
                var scheme = new Uri(reader["Scheme"].ToString());
                var name = reader["Name"].ToString();
                var label = reader["Label"].ToString();

                var category = new FeedCategory(scheme, name, label);

                if (!categories.ContainsKey(parent))
                    categories.Add(parent, new List<IFeedCategory>());

                categories[parent].Add(category);
            }
            while (reader.Read());

            return categories;
        }

        private IDictionary<Guid, IList<IFeedLink>> GetFeedItemLinks(IDataReader reader)
        {
            var links = new Dictionary<Guid, IList<IFeedLink>>();

            return links;
        }

        private IDictionary<Guid, IList<IFeedMetadata>> GetFeedItemMetadata(IDataReader reader)
        {
            var metadata = new Dictionary<Guid, IList<IFeedMetadata>>();

            return metadata;
        }

        protected override IEnumerable<IFeed> Create(IDataReader reader)
        {
            IDictionary<Guid, IFeed> feeds = null;
            IDictionary<Guid, IList<IFeedCategory>> categories = null;
            IDictionary<Guid, IList<IFeedLink>> links = null;
            IDictionary<Guid, IList<IFeedMetadata>> metadata = null;
            IDictionary<Guid, IList<IFeedItem>> items = null;
            IDictionary<Guid, IList<IFeedCategory>> itemCategories = null;
            IDictionary<Guid, IList<IFeedLink>> itemLinks = null;
            IDictionary<Guid, IList<IFeedMetadata>> itemMetadata = null;

            do
            {
                if (reader.Read())
                {
                    var table = reader[0].ToString();
                    switch (table)
                    {
                        case "Feed":
                            feeds = GetFeeds(reader);
                            break;
                        case "FeedCategory":
                            categories = GetFeedCategories(reader);
                            break;
                        case "FeedLink":
                            links = GetFeedLinks(reader);
                            break;
                        case "FeedMetadata":
                            metadata = GetFeedMetadata(reader);
                            break;
                        case "FeedItem":
                            items = GetFeedItems(reader);
                            break;
                        case "FeedItemCategory":
                            itemCategories = GetFeedCategories(reader);
                            break;
                        case "FeedItemLink":
                            itemLinks = GetFeedLinks(reader);
                            break;
                        case "FeedItemMetadata":
                            itemMetadata = GetFeedMetadata(reader);
                            break;
                        default:
                            break;
                    }
                }
            }
            while (reader.NextResult());

            if (feeds != null)
            {
                if (categories != null)
                {
                    foreach (var pair in categories)
                    {
                        if (feeds.ContainsKey(pair.Key))
                        {
                            foreach (var item in categories[pair.Key])
                                feeds[pair.Key].Categories.Add(item);

                            feeds[pair.Key].Categories.ResetState();
                        }
                    }
                }
                if (links != null)
                {
                    foreach (var pair in links)
                    {
                        if (feeds.ContainsKey(pair.Key))
                        {
                            foreach (var item in links[pair.Key])
                                feeds[pair.Key].Links.Add(item);

                            feeds[pair.Key].Links.ResetState();
                        }
                    }
                }
                if (metadata != null)
                {
                    foreach (var pair in metadata)
                    {
                        if (feeds.ContainsKey(pair.Key))
                        {
                            foreach (var item in metadata[pair.Key])
                                feeds[pair.Key].Metadata.Add(item);

                            feeds[pair.Key].Metadata.ResetState();
                        }
                    }
                }
                if (items != null)
                {
                    foreach (var pair in items)
                    {
                        if (feeds.ContainsKey(pair.Key))
                        {
                            foreach (var item in items[pair.Key])
                            {
                                feeds[pair.Key].Items.Add(item);

                                if (itemCategories != null && itemCategories.ContainsKey(item.Id))
                                {
                                    foreach (var itemCategory in itemCategories[item.Id])
                                        item.Categories.Add(itemCategory);

                                    item.Categories.ResetState();
                                }
                                if (itemLinks != null && itemLinks.ContainsKey(item.Id))
                                {
                                    foreach (var itemLink in itemLinks[item.Id])
                                        item.Links.Add(itemLink);

                                    item.Links.ResetState();
                                }
                                if (itemMetadata != null && itemMetadata.ContainsKey(item.Id))
                                {
                                    foreach (var itemDatum in itemMetadata[item.Id])
                                        item.Metadata.Add(itemDatum);

                                    item.Metadata.ResetState();
                                }
                            }

                            feeds[pair.Key].Items.ResetState();
                        }
                    }
                }

                return feeds.Values;
            }
            else
                return new List<IFeed>();
        }

        protected IFeed Create(Uri location)
        {
            return new Feed(Context, location);
        }

        public IFeed New(Uri location)
        {
            throw new NotImplementedException();
        }

        public IFeed GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public IFeed GetOne(Uri location)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFeed> GetAll()
        {
            return Select();
        }

        public IEnumerable<IFeed> GetAny(IFeedSearch search)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<IFeed> tracks)
        {
            throw new NotImplementedException();
        }
    }
}
