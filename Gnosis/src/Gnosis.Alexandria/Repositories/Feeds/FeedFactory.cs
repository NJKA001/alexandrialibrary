using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Feeds;
using Gnosis.Core;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class FeedFactory
        : FactoryBase
    {
        public FeedFactory(IContext context, ILogger logger)
            : base(context, logger)
        {
            MapCreateEntityFunction(typeof(IFeed), (record) => CreateFeed(record));
            MapCreateChildFunction(typeof(IFeedItem), (record, parent) => CreateItem(record, parent));
            MapCreateValueFunction(typeof(IFeedCategory), (record) => CreateCategory(record));
            MapCreateValueFunction(typeof(IFeedLink), (record) => CreateLink(record));
            MapCreateValueFunction(typeof(IFeedMetadata), (record) => CreateMetadata(record));
            MapAddChildAction(typeof(IFeed), typeof(IFeedItem), (name, parents, children) => AddFeedItems(name, parents, children));
            MapAddValueAction(typeof(IFeed), typeof(IFeedCategory), (name, parents, children) => AddFeedCategories(name, parents, children));
            MapAddValueAction(typeof(IFeed), typeof(IFeedLink), (name, parents, children) => AddFeedLinks(name, parents, children));
            MapAddValueAction(typeof(IFeed), typeof(IFeedMetadata), (name, parents, children) => AddFeedMetadata(name, parents, children));
            MapAddValueAction(typeof(IFeedItem), typeof(IFeedCategory), (name, parents, children) => AddFeedItemCategories(name, parents, children));
            MapAddValueAction(typeof(IFeedItem), typeof(IFeedLink), (name, parents, children) => AddFeedItemLinks(name, parents, children));
            MapAddValueAction(typeof(IFeedItem), typeof(IFeedMetadata), (name, parents, children) => AddFeedItemMetadata(name, parents, children));
        }

        private IEntity CreateFeed(IDataRecord record)
        {
            var feed = new Feed();
            if (record != null)
                feed.Initialize(new EntityInitialState(Context, Logger, record));
            else feed.Initialize(new EntityInitialState(Context, Logger));
            return feed;

            /*
            if (record == null)
                feed.Initialize(new EntityInitialState(Context, Logger, 
            else
            {
                var id = record.GetGuid("Id");
                var timeStamp = record.GetDateTime("TimeStamp");
                var location = record.GetUri("Location");
                var mediaType = record.GetString("MediaType");
                var title = record.GetString("Title");
                var authors = record.GetString("Authors");
                var contributors = record.GetString("Contributors");
                var description = record.GetString("Description");
                var language = record.GetString("Language");
                var originalLocation = record.GetUri("OriginalLocation");
                var copyright = record.GetString("Copyright");
                var publishedDate = record.GetDateTime("PublishedDate");
                var updatedDate = record.GetDateTime("UpdatedDate");
                var generator = record.GetString("Generator");
                var imagePath = record.GetUri("ImagePath");
                var iconPath = record.GetUri("IconPath");
                var feedIdentifier = record.GetString("FeedIdentifier");
                
                return new Feed(Context, id, timeStamp, location, mediaType, title, authors, contributors, description, language, originalLocation, copyright, publishedDate, updatedDate, generator, imagePath, iconPath, feedIdentifier);
            }
             */
        }

        private IChild CreateItem(IDataRecord record, Guid parent)
        {
            var item = new FeedItem();
            if (record != null)
                item.Initialize(new EntityInitialState(Context, Logger, record));
            else item.Initialize(new EntityInitialState(Context, Logger));

            return item;

            /*
            if (record == null)
                return new FeedItem(Context, parent);
            else
            {
                var id = record.GetGuid("Id");
                parent = record.GetGuid("Parent");
                var timeStamp = record.GetDateTime("TimeStamp");
                var title = record.GetString("Title");
                var titleMediaType = record.GetString("TitleMediaType");
                var authors = record.GetString("Authors");
                var contributors = record.GetString("Contributors");
                var publishedDate = record.GetDateTime("PublishedDate");
                var copyright = record.GetString("Copyright");
                var summary = record.GetString("Summary");
                var content = record.GetString("Content");
                var contentMediaType = record.GetString("ContentMediaType");
                var contentLocation = record.GetUri("ContentLocation");
                var updatedDate = record.GetDateTime("UpdatedDate");
                var feedItemIdentifier = record.GetString("FeedItemIdentifier");

                return new FeedItem(Context, id, parent, timeStamp, title, titleMediaType, authors, contributors, publishedDate, copyright, summary, content, contentMediaType, contentLocation, updatedDate, feedItemIdentifier);
            }
            */
        }

        private IValue CreateCategory(IDataRecord record)
        {
            var category = new FeedCategory();
            category.Initialize(new ValueInitialState(record));
            return category;

            /*
            var id = record.GetGuid("Id");
            var parent = record.GetGuid("Parent");
            var sequence = record.GetUInt32("Sequence");
            var scheme = record.GetUri("Scheme");
            var name = record.GetString("Name");
            var label = record.GetString("Label");
            return new FeedCategory(id, parent, sequence, scheme, name, label);
            */
        }

        private IValue CreateLink(IDataRecord record)
        {
            var link = new FeedLink();
            link.Initialize(new ValueInitialState(record));
            return link;

            /*
            var id = record.GetGuid("Id");
            var parent = record.GetGuid("Parent");
            var sequence = record.GetUInt32("Sequence");
            var relationship = record.GetString("Relationship");
            var location = record.GetUri("Location");
            var mediaType = record.GetString("MediaType");
            var length = record.GetUInt32("Length");
            var language = record.GetString("Language");
            return new FeedLink(id, parent, sequence, relationship, location, mediaType, length, language);
            */
        }

        private IValue CreateMetadata(IDataRecord record)
        {
            var metadata = new FeedMetadata();
            metadata.Initialize(new ValueInitialState(record));
            return metadata;

            /*
            var id = record.GetGuid("Id");
            var parent = record.GetGuid("Parent");
            var sequence = record.GetUInt32("Sequence");
            var mediaType = record.GetString("MediaType");
            var scheme = record.GetUri("Scheme");
            var name = record.GetString("Name");
            var content = record.GetString("Content");
            return new FeedMetadata(id, parent, sequence, mediaType, scheme, name, content);
            */
        }

        private void AddFeedCategories(string valueName, IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            foreach (var feed in parents.Cast<IFeed>())
            {
                foreach (var child in children.Where(x => x.Parent == feed.Id).OrderBy(x => x.Sequence))
                {
                    feed.AddCategory(child as IFeedCategory);
                }
            }
        }

        private void AddFeedLinks(string valueName, IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            foreach (var feed in parents.Cast<IFeed>())
            {
                foreach (var child in children.Where(x => x.Parent == feed.Id).OrderBy(x => x.Sequence))
                {
                    feed.AddLink(child as IFeedLink);
                }
            }
        }

        private void AddFeedMetadata(string valueName, IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            foreach (var feed in parents.Cast<IFeed>())
            {
                foreach (var child in children.Where(x => x.Parent == feed.Id).OrderBy(x => x.Sequence))
                {
                    feed.AddMetadatum(child as IFeedMetadata);
                }
            }
        }

        private void AddFeedItems(string childName, IEnumerable<IEntity> parents, IEnumerable<IChild> children)
        {
            foreach (var feed in parents.Cast<IFeed>())
            {
                foreach (var child in children.Where(x => x.Parent == feed.Id).OrderBy(x => x.Sequence))
                {
                    feed.AddItem(child as IFeedItem);
                }
            }
        }

        private void AddFeedItemCategories(string valueName, IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            //FeedItem_FeedCategory
            foreach (var feedItem in parents.Cast<IFeedItem>())
            {
                foreach (var child in children.Where(x => x.Parent == feedItem.Id).OrderBy(x => x.Sequence))
                {
                    feedItem.AddCategory(child as IFeedCategory);
                }
            }
        }

        private void AddFeedItemLinks(string valueName, IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            foreach (var feedItem in parents.Cast<IFeedItem>())
            {
                foreach (var child in children.Where(x => x.Parent == feedItem.Id).OrderBy(x => x.Sequence))
                {
                    feedItem.AddLink(child as IFeedLink);
                }
            }
        }

        private void AddFeedItemMetadata(string valueName, IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            foreach (var feedItem in parents.Cast<IFeedItem>())
            {
                foreach (var child in children.Where(x => x.Parent == feedItem.Id).OrderBy(x => x.Sequence))
                {
                    feedItem.AddMetadatum(child as IFeedMetadata);
                }
            }
        }
    }
}
