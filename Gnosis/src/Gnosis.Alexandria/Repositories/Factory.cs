using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories
{
    public class Factory : IFactory
    {
        public Factory(IContext context)
        {
            this.context = context;

            functionMap.Add(typeof(IFeed), (record, parent) => CreateFeed(record, parent));
            functionMap.Add(typeof(IFeedItem), (record, parent) => CreateItem(record, parent));
            functionMap.Add(typeof(IFeedCategory), (record, parent) => CreateCategory(record, parent));
            functionMap.Add(typeof(IFeedLink), (record, parent) => CreateLink(record, parent));
            functionMap.Add(typeof(IFeedMetadata), (record, parent) => CreateMetadata(record, parent));
        }

        private readonly IContext context;
        private readonly IDictionary<Type, Func<IDataRecord, Guid, object>> functionMap = new Dictionary<Type, Func<IDataRecord, Guid, object>>();

        private object CreateFeed(IDataRecord record, Guid parent)
        {
            if (record == null)
                return new Feed(context);
            else
            {
                var id =  record.GetGuid("Id");
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
                return new Feed(context, id, timeStamp, location, mediaType, title, authors, contributors, description, language, originalLocation, copyright, publishedDate, updatedDate, generator, imagePath, iconPath, feedIdentifier);
            }
        }

        private object CreateItem(IDataRecord record, Guid parent)
        {
            if (record == null)
                return new FeedItem(context, parent);
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

                return new FeedItem(context, id, parent, timeStamp, title, titleMediaType, authors, contributors, publishedDate, copyright, summary, content, contentMediaType, contentLocation, updatedDate, feedItemIdentifier);
            }
        }

        private object CreateCategory(IDataRecord record, Guid parent)
        {
            var id = record.GetGuid("Id");
            parent = record.GetGuid("Parent");
            var sequence = record.GetUInt32("Sequence");
            var scheme = record.GetUri("Scheme");
            var name = record.GetString("Name");
            var label = record.GetString("Label");
            return new FeedCategory(id, parent, sequence, scheme, name, label);
        }

        private object CreateLink(IDataRecord record, Guid parent)
        {
            var id = record.GetGuid("Id");
            parent = record.GetGuid("Parent");
            var sequence = record.GetUInt32("Sequence");
            var relationship = record.GetString("Relationship");
            var location = record.GetUri("Location");
            var mediaType = record.GetString("MediaType");
            var length = record.GetUInt32("Length");
            var language = record.GetString("Language");
            return new FeedLink(id, parent, sequence, relationship, location, mediaType, length, language);
        }

        private object CreateMetadata(IDataRecord record, Guid parent)
        {
            var id = record.GetGuid("Id");
            parent = record.GetGuid("Parent");
            var sequence = record.GetUInt32("Sequence");
            var mediaType = record.GetString("MediaType");
            var scheme = record.GetUri("Scheme");
            var name = record.GetString("Name");
            var content = record.GetString("Content");
            return new FeedMetadata(id, parent, sequence, mediaType, scheme, name, content);
        }

        private void AddFeedCategories(IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            foreach (var feed in parents.Cast<IFeed>())
            {
                foreach (var child in children.Where(x => x.Parent == feed.Id).OrderBy(x => x.Sequence))
                {
                    feed.AddCategory(child as IFeedCategory);
                }
            }
        }

        private void AddFeedLinks(IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            foreach (var feed in parents.Cast<IFeed>())
            {
                foreach (var child in children.Where(x => x.Parent == feed.Id).OrderBy(x => x.Sequence))
                {
                    feed.AddLink(child as IFeedLink);
                }
            }
        }

        private void AddFeedMetadata(IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            foreach (var feed in parents.Cast<IFeed>())
            {
                foreach (var child in children.Where(x => x.Parent == feed.Id).OrderBy(x => x.Sequence))
                {
                    feed.AddMetadatum(child as IFeedMetadata);
                }
            }
        }

        private void AddFeedItems(IEnumerable<IEntity> parents, IEnumerable<IChild> children)
        {
            foreach (var feed in parents.Cast<IFeed>())
            {
                foreach (var child in children.Where(x => x.Parent == feed.Id).OrderBy(x => x.Sequence))
                {
                    feed.AddItem(child as IFeedItem);
                }
            }
        }

        private void AddFeedItemCategories(IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            foreach (var feedItem in parents.Cast<IFeedItem>())
            {
                foreach (var child in children.Where(x => x.Parent == feedItem.Id).OrderBy(x => x.Sequence))
                {
                    feedItem.AddCategory(child as IFeedCategory);
                }
            }
        }

        private void AddFeedItemLinks(IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            foreach (var feedItem in parents.Cast<IFeedItem>())
            {
                foreach (var child in children.Where(x => x.Parent == feedItem.Id).OrderBy(x => x.Sequence))
                {
                    feedItem.AddLink(child as IFeedLink);
                }
            }
        }

        private void AddFeedItemMetadata(IEnumerable<IEntity> parents, IEnumerable<IValue> children)
        {
            foreach (var feedItem in parents.Cast<IFeedItem>())
            {
                foreach (var child in children.Where(x => x.Parent == feedItem.Id).OrderBy(x => x.Sequence))
                {
                    feedItem.AddMetadatum(child as IFeedMetadata);
                }
            }
        }

        public IEntity CreateEntity(Type type)
        {
            if (functionMap.ContainsKey(type))
            {
                return functionMap[type](null, Guid.Empty) as IEntity;
            }

            return null;
        }

        public IEntity CreateEntity(Type type, IDataRecord record)
        {
            if (functionMap.ContainsKey(type))
            {
                return functionMap[type](record, Guid.Empty) as IEntity;
            }

            return null;
        }

        public IChild CreateChild(Type type, Guid parent)
        {
            if (functionMap.ContainsKey(type))
            {
                return functionMap[type](null, parent) as IChild;
            }

            return null;
        }

        public IChild CreateChild(Type type, IDataRecord record)
        {
            if (functionMap.ContainsKey(type))
            {
                return functionMap[type](record, Guid.Empty) as IChild;
            }

            return null;
        }

        public IValue CreateValue(Type type, IDataRecord record)
        {
            if (functionMap.ContainsKey(type))
            {
                return functionMap[type](record, Guid.Empty) as IValue;
            }

            return null;
        }

        public T CreateEntity<T>()
            where T : IEntity
        {
            return (T)CreateEntity(typeof(T));
        }

        public T CreateEntity<T>(IDataRecord record)
            where T : IEntity
        {
            return (T)CreateEntity(typeof(T), record);
        }

        public T CreateChild<T>(Guid parent)
            where T : IChild
        {
            return (T)CreateChild(typeof(T), parent);
        }

        public T CreateChild<T>(IDataRecord record) where T : IChild
        {
            return (T)CreateChild(typeof(T), record);
        }

        public T CreateValue<T>(IDataRecord record) where T : IValue
        {
            return (T)CreateValue(typeof(T), record);
        }

        public void AddChildren(Type parentType, Type childType, string childName, IEnumerable<IEntity> parents, IEnumerable<IChild> children)
        {
            if (parentType == typeof(IFeed))
            {
                if (childType == typeof(IFeedItem))
                    AddFeedItems(parents, children);
            }
        }

        public void AddValues(Type parentType, Type valueType, string valueName, IEnumerable<IEntity> parents, IEnumerable<IValue> values)
        {
            if (parentType == typeof(IFeed))
            {
                if (valueType == typeof(IFeedCategory))
                    AddFeedCategories(parents, values);
                else if (valueType == typeof(IFeedLink))
                    AddFeedLinks(parents, values);
                else if (valueType == typeof(IFeedMetadata))
                    AddFeedMetadata(parents, values);
            }
            else if (parentType == typeof(IFeedItem))
            {
                if (valueType == typeof(IFeedCategory))
                    AddFeedItemCategories(parents, values);
                else if (valueType == typeof(IFeedLink))
                    AddFeedItemLinks(parents, values);
                else if (valueType == typeof(IFeedMetadata))
                    AddFeedItemMetadata(parents, values);
            }
        }
    }
}
