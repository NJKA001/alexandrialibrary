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
            functionMap.Add(typeof(IFeedItem), (record, parent) => CreateFeedItem(record, parent));
            functionMap.Add(typeof(IFeedCategory), (record, parent) => CreateFeedCategory(record, parent));
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

        private object CreateFeedItem(IDataRecord record, Guid parent)
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

        private object CreateFeedCategory(IDataRecord record, Guid parent)
        {
            if (record != null)
            {
                var id = record.GetGuid("Id");
                parent = record.GetGuid("Parent");
                var sequence = record.GetUInt32("Sequence");
                var scheme = record.GetUri("Scheme");
                var name = record.GetString("Name");
                var label = record.GetString("Label");
                return new FeedCategory(id, parent, sequence, scheme, name, label);
            }
            else return null;
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
            throw new NotImplementedException();
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
        }

        public void AddValues(Type parentType, Type valueType, string valueName, IEnumerable<IEntity> parents, IEnumerable<IValue> values)
        {
        }
    }
}
