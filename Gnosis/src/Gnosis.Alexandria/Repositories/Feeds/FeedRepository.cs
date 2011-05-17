using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;
using Gnosis.Core;
using Gnosis.Core.Commands;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class FeedRepository
        : RepositoryBase<IFeed>, IFeedRepository
    {
        public FeedRepository(IContext context)
            : base(context)
        {
        }

        #region Private Helpers Methods

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

            do
            {
                var parent = new Guid(reader["Parent"].ToString());
                var relationship = reader["Relationship"].ToString();
                var location = new Uri(reader["Location"].ToString());
                var mediaType = reader["MediaType"].ToString();
                var length = uint.Parse(reader["Length"].ToString());
                var language = reader["Language"].ToString();

                var link = new FeedLink(relationship, location, mediaType, length, language);

                if (!links.ContainsKey(parent))
                    links.Add(parent, new List<IFeedLink>());

                links[parent].Add(link);
            }
            while (reader.Read());

            return links;
        }

        private IDictionary<Guid, IList<IFeedMetadata>> GetFeedMetadata(IDataReader reader)
        {
            var metadata = new Dictionary<Guid, IList<IFeedMetadata>>();

            do
            {
                var parent = new Guid(reader["Parent"].ToString());
                var mediaType = reader["MediaType"].ToString();
                var scheme = new Uri(reader["Scheme"].ToString());
                var name = reader["Name"].ToString();
                var content = reader["Content"].ToString();

                var metadatum = new FeedMetadata(mediaType, scheme, name, content);

                if (!metadata.ContainsKey(parent))
                    metadata.Add(parent, new List<IFeedMetadata>());

                metadata[parent].Add(metadatum);
            }
            while (reader.Read());

            return metadata;
        }

        private IDictionary<Guid, IList<IFeedItem>> GetFeedItems(IDataReader reader)
        {
            var items = new Dictionary<Guid, IList<IFeedItem>>();

            do
            {
                var timeStamp = GetTimeStamp(reader);
                var id = new Guid(reader["Id"].ToString());
                var parent = new Guid(reader["Parent"].ToString());
                var title = reader["Title"].ToString();
                var titleMediaType = reader["TitleMediaType"].ToString();
                var authors = reader["Authors"].ToString();
                var contributors = reader["Contributors"].ToString();
                var publishedDate = DateTime.Parse(reader["PublishedDate"].ToString());
                var copyright = reader["Copyright"].ToString();
                var summary = reader["Summary"].ToString();
                var content = reader["Content"].ToString();
                var contentMediaType = reader["ContentMediaType"].ToString();
                var contentLocation = new Uri(reader["ContentLocation"].ToString());
                var updatedDate = DateTime.Parse(reader["UpdatedDate"].ToString());
                var feedItemIdentifier = reader["FeedItemIdentifier"].ToString();

                var item = new FeedItem(Context, id, timeStamp, title, titleMediaType, authors, contributors, publishedDate, copyright, summary, content, contentMediaType, contentLocation, updatedDate, feedItemIdentifier);

                if (!items.ContainsKey(parent))
                    items.Add(parent, new List<IFeedItem>());

                items[parent].Add(item);
            }
            while (reader.Read());

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

            do
            {
                var parent = new Guid(reader["Parent"].ToString());
                var relationship = reader["Relationship"].ToString();
                var location = new Uri(reader["Location"].ToString());
                var mediaType = reader["MediaType"].ToString();
                var length = uint.Parse(reader["Length"].ToString());
                var language = reader["Language"].ToString();

                var link = new FeedLink(relationship, location, mediaType, length, language);

                if (!links.ContainsKey(parent))
                    links.Add(parent, new List<IFeedLink>());

                links[parent].Add(link);
            }
            while (reader.Read());

            return links;
        }

        private IDictionary<Guid, IList<IFeedMetadata>> GetFeedItemMetadata(IDataReader reader)
        {
            var metadata = new Dictionary<Guid, IList<IFeedMetadata>>();

            do
            {
                var parent = new Guid(reader["Parent"].ToString());
                var mediaType = reader["MediaType"].ToString();
                var scheme = new Uri(reader["Scheme"].ToString());
                var name = reader["Name"].ToString();
                var content = reader["Content"].ToString();

                var metadatum = new FeedMetadata(mediaType, scheme, name, content);

                if (!metadata.ContainsKey(parent))
                    metadata.Add(parent, new List<IFeedMetadata>());

                metadata[parent].Add(metadatum);
            }
            while (reader.Read());

            return metadata;
        }

        #region Update Statements

        private void AddFeedUpdateStatment(CommandBuilder builder, IFeed feed)
        {

        }

        #endregion

        #region Delete Statements

        private void AddFeedDeleteStatement(CommandBuilder builder, IFeed feed)
        {
            var idParameterName = builder.GetParameterName();

            builder.AddStatement(new DeleteStatement("Feed", "Feed.Id = " + idParameterName));
            builder.AddParameter(idParameterName, feed.Id);

            foreach (var category in feed.Categories)
            {
                AddFeedCategoryDeleteStatement(builder, feed.Id, category, "FeedCategory");
            }

            foreach (var link in feed.Links)
            {
                AddFeedLinkDeleteStatement(builder, feed.Id, link, "FeedLink");
            }

            foreach (var metadata in feed.Metadata)
            {
                AddFeedMetadataDeleteStatement(builder, feed.Id, metadata, "FeedMetadata");
            }

            foreach (var item in feed.Items)
            {
                AddFeedItemDeleteStatement(builder, feed.Id, item);
            }
        }

        private void AddFeedCategoryDeleteStatement(CommandBuilder builder, Guid parentId, IFeedCategory category, string tableName)
        {
            var parentParameterName = builder.GetParameterName();
            var schemeParameterName = builder.GetParameterName();
            var nameParameterName = builder.GetParameterName();
            var whereClause = string.Format("{0}.Parent = {1} and {0}.Scheme = {2} and {0}.Name = {3}", tableName, parentParameterName, schemeParameterName, nameParameterName);

            builder.AddStatement(new DeleteStatement(tableName, whereClause));
            builder.AddParameter(parentParameterName, parentId);
            builder.AddParameter(schemeParameterName, category.Scheme);
            builder.AddParameter(nameParameterName, category.Name);
        }

        private void AddFeedLinkDeleteStatement(CommandBuilder builder, Guid parentId, IFeedLink link, string tableName)
        {
            var parentParameterName = builder.GetParameterName();
            var relationshipParameterName = builder.GetParameterName();
            var mediaTypeParameterName = builder.GetParameterName();
            var languageParameterName = builder.GetParameterName();
            var whereClause = string.Format("{0}.Parent = {1} and {0}.Relationship = {2} and {0}.MediaType = {3} and {0}.Language = {4}", tableName, parentParameterName, relationshipParameterName, mediaTypeParameterName, languageParameterName);

            builder.AddStatement(new DeleteStatement(tableName, whereClause));
            builder.AddParameter(parentParameterName, parentId);
            builder.AddParameter(relationshipParameterName, link.Relationship);
            builder.AddParameter(mediaTypeParameterName, link.MediaType);
            builder.AddParameter(languageParameterName, link.Language);
        }

        private void AddFeedMetadataDeleteStatement(CommandBuilder builder, Guid parentId, IFeedMetadata metadata, string tableName)
        {
            var parentParameterName = builder.GetParameterName();
            var schemeParameterName = builder.GetParameterName();
            var nameParameterName = builder.GetParameterName();
            var whereClause = string.Format("{0}.Parent = {1} and {0}.Scheme = {2} and {0}.Name = {3}", tableName, parentParameterName, schemeParameterName, nameParameterName);

            builder.AddStatement(new DeleteStatement(tableName, whereClause));
            builder.AddParameter(parentParameterName, parentId);
            builder.AddParameter(schemeParameterName, metadata.Scheme);
            builder.AddParameter(nameParameterName, metadata.Name);
        }

        private void AddFeedItemDeleteStatement(CommandBuilder builder, Guid parentId, IFeedItem item)
        {
            var parentParameterName = builder.GetParameterName();
            var whereClause = string.Format("FeedItem.Parent = {0}", parentParameterName);

            builder.AddStatement(new DeleteStatement("FeedItem", whereClause));
            builder.AddParameter(parentParameterName, parentId);

            foreach (var category in item.Categories)
            {
                AddFeedCategoryDeleteStatement(builder, item.Id, category, "FeedItemCategory");
            }

            foreach (var link in item.Links)
            {
                AddFeedLinkDeleteStatement(builder, item.Id, link, "FeedItemLink");
            }

            foreach (var metadata in item.Metadata)
            {
                AddFeedMetadataDeleteStatement(builder, item.Id, metadata, "FeedItemMetadata");
            }
        }

#endregion

        #endregion

        protected override IFeed CreateDefault()
        {
            return Create(UriExtensions.EmptyUri);
        }

        protected override CommandBuilder GetSaveCommandBuilder(IEnumerable<IFeed> items)
        {
            var builder = new SaveCommandBuilder();

            foreach (var feed in items)
            {
                feed.AddEntitySaveStatement<IFeed>(builder);
            }

            return builder;
        }

        protected override CommandBuilder GetDeleteCommandBuilder(IEnumerable<IFeed> items)
        {
            var builder = new SaveCommandBuilder();

            foreach (var feed in items)
            {
                AddFeedDeleteStatement(builder, feed);
            }

            return builder;
        }

        protected override IEnumerable<IFeed> Read(IDataReader reader)
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
            return Create(location);
        }

        public IFeed GetOne(Guid id)
        {
            return Select("Feed.Id = @Id", "@Id", id).FirstOrDefault();
        }

        public IFeed GetOne(Uri location)
        {
            return Select("Feed.Location = @Location", "@Location", location).FirstOrDefault();
        }
    }
}
