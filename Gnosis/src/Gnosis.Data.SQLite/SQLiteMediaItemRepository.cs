using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLiteMediaItemRepository
        : SQLiteRepositoryBase, IMediaItemRepository
    {
        public SQLiteMediaItemRepository(ILogger logger, ISecurityContext securityContext, IContentTypeFactory contentTypeFactory)
            : this(logger, securityContext, contentTypeFactory, null)
        {
        }

        public SQLiteMediaItemRepository(ILogger logger, ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IDbConnection defaultConnection)
            : base(logger, defaultConnection)
        {
            if (securityContext == null)
                throw new ArgumentNullException("securityContext");
            if (contentTypeFactory == null)
                throw new ArgumentNullException("contentTypeFactory");

            this.securityContext = securityContext;
            this.contentTypeFactory = contentTypeFactory;

            InitializeTableNames();
        }

        private readonly ISecurityContext securityContext;
        private readonly IContentTypeFactory contentTypeFactory;
        private readonly IDictionary<Type, string> tables = new Dictionary<Type, string>();

        private void InitializeTableNames()
        {
            tables.Add(typeof(IArtist), "Artist");
            tables.Add(typeof(IAlbum), "Album");
            tables.Add(typeof(IClip), "Clip");
            tables.Add(typeof(IDoc), "Doc");
            tables.Add(typeof(IFeed), "Feed");
            tables.Add(typeof(IFeedItem), "FeedItem");
            tables.Add(typeof(IPic), "Pic");
            tables.Add(typeof(IProgram), "Program");
            tables.Add(typeof(IPlaylist), "Playlist");
            tables.Add(typeof(IPlaylistItem), "PlaylistItem");
            tables.Add(typeof(ITrack), "Track");
        }

        private string GetTableName<T>()
            where T : class, IMediaItem
        {
            return GetTableName(typeof(T));
        }

        private string GetTableName(Type type)
        {
            if (tables.ContainsKey(type))
                return tables[type];

            throw new InvalidOperationException("Unknown MediaItem type: " + type.Name);
        }

        private T BuildItem<T>(IdentityInfo identityInfo, SizeInfo sizeInfo, CreatorInfo creatorInfo, CatalogInfo catalogInfo, TargetInfo targetInfo, UserInfo userInfo, ThumbnailInfo thumbnailInfo)
            where T : class, IMediaItem
        {
            var builder = new MediaItemBuilder<T>(securityContext, contentTypeFactory)
                .Identity(identityInfo.Name, identityInfo.Summary, identityInfo.FromDate, identityInfo.ToDate, identityInfo.Number, identityInfo.Location)
                .Size(sizeInfo.Duration, sizeInfo.Height, sizeInfo.Width)
                .Creator(creatorInfo.Location, creatorInfo.Name)
                .Catalog(catalogInfo.Location, catalogInfo.Name)
                .Target(targetInfo.Location, targetInfo.Type)
                .User(userInfo.Location, userInfo.Name)
                .Thumbnail(thumbnailInfo.Location, thumbnailInfo.Data);

            return builder.ToMediaItem();
        }

        private ICommandBuilder GetInitializeBuilder(Type type)
        {
            var tableName = GetTableName(type);
            
            var builder = new CommandBuilder();
            builder.AppendFormat("create table if not exists {0} (", tableName);
            builder.Append("Location text primary key not null, Name text not null, ");
            builder.Append("Summary text not null, FromDate text not null, ToDate text not null, ");
            builder.Append("Number integer not null, Duration integer not null, ");
            builder.Append("Height integer not null, Width integer not null, ");
            builder.Append("Creator text not null, CreatorName text not null, ");
            builder.Append("Catalog text not null, CatalogName text not null, ");
            builder.Append("Target text not null, TargetType text not null, ");
            builder.Append("User text not null, UserName text not null, ");
            builder.AppendLine("Thumbnail text not null, ThumbnailData blob not null);");

            builder.AppendFormatLine("create index if not exists {0}_Name on {0} (Name asc);", tableName);
            builder.AppendFormatLine("create index if not exists {0}_Catalog on {0} (Catalog asc);", tableName);
            builder.AppendFormatLine("create index if not exists {0}_Creator on {0} (Creator asc);", tableName);
            builder.AppendFormatLine("create index if not exists {0}_Target on {0} (Target asc);", tableName);
            builder.AppendFormatLine("create unique index if not exists {0}_Creator_Name on {0} (Creator asc, Name asc);", tableName);

            return builder;
        }

        private IMediaItem GetDefaultItemByType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (!tables.ContainsKey(type))
                throw new InvalidOperationException("Invalid Media Item type: " + type.Name);

            switch (tables[type])
            {
                case "Album":
                    return GetDefaultItem<IAlbum>();
                case "Artist":
                    return GetDefaultItem<IArtist>();
                case "Clip":
                    return GetDefaultItem<IClip>();
                case "Doc":
                    return GetDefaultItem<IDoc>();
                case "Feed":
                    return GetDefaultItem<IFeed>();
                case "FeedItem":
                    return GetDefaultItem<IFeedItem>();
                case "Pic":
                    return GetDefaultItem<IPic>();
                case "Playlist":
                    return GetDefaultItem<IPlaylist>();
                case "PlaylistItem":
                    return GetDefaultItem<IPlaylistItem>();
                case "Program":
                    return GetDefaultItem<IProgram>();
                case "Track":
                    return GetDefaultItem<ITrack>();
                default:
                    throw new InvalidOperationException("Invalid Media Item type: " + type.Name);
            }

            //var method = this.GetType().GetMethod("GetDefaultItem", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            //var generic = method.MakeGenericMethod(type);
            //return generic.Invoke(this, null) as IMediaItem;
        }

        private T GetDefaultItem<T>()
            where T : class, IMediaItem
        {
            return new MediaItemBuilder<T>(securityContext, contentTypeFactory).GetDefault();
        }

        private ICommandBuilder GetDeleteBuilder<T>(Uri location)
            where T : class, IMediaItem
        {
            var defaultItem = GetDefaultItem<T>();

            if (defaultItem != null && defaultItem.Location.ToString() == location.ToString())
                return null;

            var tableName = GetTableName<T>();

            var builder = new CommandBuilder();
            builder.AppendFormatLine("delete from {0} where Location = @Location;", tableName);
            builder.AddParameter("@Location", location.ToString());
            return builder;
        }

        private ICommandBuilder GetSaveBuilderByType(IMediaItem item, Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (!tables.ContainsKey(type))
                throw new InvalidOperationException("Invalid Media Item type: " + type.Name);

            switch (tables[type])
            {
                case "Album":
                    return GetSaveBuilder<IAlbum>((IAlbum)item);
                case "Artist":
                    return GetSaveBuilder<IArtist>((IArtist)item);
                case "Clip":
                    return GetSaveBuilder<IClip>((IClip)item);
                case "Doc":
                    return GetSaveBuilder<IDoc>((IDoc)item);
                case "Feed":
                    return GetSaveBuilder<IFeed>((IFeed)item);
                case "FeedItem":
                    return GetSaveBuilder<IFeedItem>((IFeedItem)item);
                case "Pic":
                    return GetSaveBuilder<IPic>((IPic)item);
                case "Playlist":
                    return GetSaveBuilder<IPlaylist>((IPlaylist)item);
                case "PlaylistItem":
                    return GetSaveBuilder<IPlaylistItem>((IPlaylistItem)item);
                case "Program":
                    return GetSaveBuilder<IProgram>((IProgram)item);
                case "Track":
                    return GetSaveBuilder<ITrack>((ITrack)item);
                default:
                    throw new InvalidOperationException("Invalid Media Item type: " + type.Name);
            }

            //var method = this.GetType().GetMethod("GetSaveBuilder", System.Reflection.BindingFlags.NonPublic);
            //var generic = method.MakeGenericMethod(type);
            //return generic.Invoke(this, new object[] { item }) as ICommandBuilder;
        }

        private ICommandBuilder GetSaveBuilder<T>(T item)
            where T : class, IMediaItem
        {
            var tableName = GetTableName<T>();

            var builder = new CommandBuilder();
            builder.AppendFormat("replace into {0} (", tableName);
            builder.Append("Location, Name, Summary, FromDate, ToDate, Number, Duration, Height, Width, ");
            builder.Append("Creator, CreatorName, Catalog, CatalogName, Target, TargetType, ");
            builder.Append("User, UserName, Thumbnail, ThumbnailData) values (");
            builder.Append("@Location, @Name, @Summary, @FromDate, @ToDate, @Number, @Duration, @Height, @Width, ");
            builder.Append("@Creator, @CreatorName, @Catalog, @CatalogName, @Target, @TargetType, ");
            builder.Append("@User, @UserName, @Thumbnail, @ThumbnailData);");
            
            builder.AddParameter("@Location", item.Location.ToString());
            builder.AddParameter("@Name", item.Name);
            builder.AddParameter("@Summary", item.Summary);
            builder.AddParameter("@FromDate", item.FromDate.ToString("o"));
            builder.AddParameter("@ToDate", item.ToDate.ToString("o"));
            builder.AddParameter("@Number", item.Number);
            builder.AddParameter("@Duration", item.Duration.Ticks);
            builder.AddParameter("@Height", item.Height);
            builder.AddParameter("@Width", item.Width);
            builder.AddParameter("@Creator", item.Creator.ToString());
            builder.AddParameter("@CreatorName", item.CreatorName);
            builder.AddParameter("@Catalog", item.Catalog.ToString());
            builder.AddParameter("@CatalogName", item.CatalogName);
            builder.AddParameter("@Target", item.Target.ToString());
            builder.AddParameter("@TargetType", item.TargetType.ToString());
            builder.AddParameter("@User", item.User.ToString());
            builder.AddParameter("@UserName", item.UserName);
            builder.AddParameter("@Thumbnail", item.Thumbnail.ToString());
            builder.AddParameter("@ThumbnailData", item.ThumbnailData);

            return builder;
        }

        private ICommandBuilder GetSelectByLocationBuilder<T>(Uri location)
            where T : class, IMediaItem
        {
            var tableName = GetTableName<T>();

            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Location = @Location;", tableName);
            builder.AddParameter("@Location", location.ToString());
            return builder;
        }

        private ICommandBuilder GetSelectByCatalogBuilder<T>(Uri catalog)
            where T : class, IMediaItem
        {
            var tableName = GetTableName<T>();

            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Catalog = @Catalog order by Number;", tableName);
            builder.AddParameter("@Catalog", catalog.ToString());
            return builder;
        }

        private ICommandBuilder GetSelectByCreatorBuilder<T>(Uri creator)
            where T : class, IMediaItem
        {
            var tableName = GetTableName<T>();

            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Creator = @Creator order by FromDate;", tableName);
            builder.AddParameter("@Creator", creator.ToString());
            return builder;
        }

        private ICommandBuilder GetSelectByCreatorAndNameBuilder<T>(Uri creator, string name)
            where T : class, IMediaItem
        {
            var tableName = GetTableName<T>();

            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Creator = @Creator and Name = @Name;", tableName);
            builder.AddParameter("@Creator", creator.ToString());
            builder.AddParameter("@Name", name);
            return builder;
        }

        private ICommandBuilder GetSelectByNameBuilder<T>(string name)
            where T : class, IMediaItem
        {
            var tableName = GetTableName<T>();

            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Name like @Name order by FromDate;", tableName);
            builder.AddParameter("@Name", name.ToString());
            return builder;
        }

        private ICommandBuilder GetSelectByTargetBuilder<T>(Uri target)
            where T : class, IMediaItem
        {
            var tableName = GetTableName<T>();

            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Target = @Target order by Number;", tableName);
            builder.AddParameter("@Target", target.ToString());
            return builder;
        }

        private ICommandBuilder GetSelectByTagBuilder<T>(TagDomain domain, string pattern, IAlgorithm algorithm)
            where T : class, IMediaItem
        {
            var tableName = GetTableName<T>();

            var builder = new CommandBuilder();
            builder.AppendFormatLine("select {0}.* from {0} inner join Tag on {0}.Location = Tag.Target where Tag.Algorithm = @Algorithm and Tag.Domain = @Domain and Tag.Value like @Pattern;", tableName);
            builder.AddParameter("@Algorithm", algorithm.Id);
            builder.AddParameter("@Domain", (int)domain);
            builder.AddParameter("@Pattern", pattern);
            return builder;
        }

        private IEnumerable<T> GetItems<T>(ICommandBuilder builder)
            where T : class, IMediaItem
        {
            IDbConnection connection = null;
            var items = new List<T>();

            try
            {
                connection = GetConnection();

                var command = builder.ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = ReadItem<T>(reader);
                        items.Add(item);
                    }
                }

                return items;
            }
            finally
            {
                if (defaultConnection == null && connection != null)
                    connection.Close();
            }
        }

        protected virtual T ReadItem<T>(IDataRecord record)
            where T : class, IMediaItem
        {
            var location = record.GetUri("Location");

            var defaultItem = GetDefaultItem<T>();

            if (defaultItem != null && defaultItem.Location.ToString() == location.ToString())
                return defaultItem;

            var name = record.GetString("Name");
            var summary = record.GetString("Summary");
            var fromDate = record.GetDateTime("FromDate");
            var toDate = record.GetDateTime("ToDate");
            var number = record.GetUInt32("Number");
            var duration = record.GetTimeSpan("Duration");
            var height = record.GetUInt32("Height");
            var width = record.GetUInt32("Width");
            var creator = record.GetUri("Creator");
            var creatorName = record.GetString("CreatorName");
            var catalog = record.GetUri("Catalog");
            var catalogName = record.GetString("CatalogName");
            var target = record.GetUri("Target");
            var targetType = record.GetStringLookup<IContentType>("TargetType", code => contentTypeFactory.GetByCode(code));
            var user = record.GetUri("User");
            var userName = record.GetString("UserName");
            var thumbnail = record.GetUri("Thumbnail");
            var thumbnailData = record.GetBytes("ThumbnailData");

            var identityInfo = new IdentityInfo(location, defaultItem.Type, name, summary, fromDate, toDate, number);
            var sizeInfo = new SizeInfo(duration, height, width);
            var creatorInfo = new CreatorInfo(creator, creatorName);
            var catalogInfo = new CatalogInfo(catalog, catalogName);
            var targetInfo = new TargetInfo(target, targetType);
            var userInfo = new UserInfo(user, userName);
            var thumbnailInfo = new ThumbnailInfo(thumbnail, thumbnailData);

            return BuildItem<T>(identityInfo, sizeInfo, creatorInfo, catalogInfo, targetInfo, userInfo, thumbnailInfo);
        }

        public void Initialize()
        {
            foreach (var type in tables.Keys)
            {
                try
                {
                    var builder = GetInitializeBuilder(type);

                    ExecuteNonQuery(builder);

                    var defaultItem = GetDefaultItemByType(type);
                    if (defaultItem != null)
                    {
                        var saveDefaultBuilder = GetSaveBuilderByType(defaultItem, type);
                        ExecuteNonQuery(saveDefaultBuilder);
                    }

                }
                catch (Exception ex)
                {
                    logger.Error("  SQLiteMediaItemRepository.Initialize failed for type: " + type.Name, ex);
                    throw;
                }
            }
        }

        public void Save<T>(IEnumerable<T> items)
            where T : class, IMediaItem
        {
            if (items == null)
                throw new ArgumentNullException("items");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var item in items)
                    builders.Add(GetSaveBuilder<T>(item));

                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error("  SQLiteMediaItemRepository.Save", ex);
                throw;
            }
        }

        public void Delete<T>(IEnumerable<Uri> items)
            where T : class, IMediaItem
        {
            if (items == null)
                throw new ArgumentNullException("items");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var location in items)
                {
                    var builder = GetDeleteBuilder<T>(location);
                    if (builder != null)
                        builders.Add(builder);
                }
                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error("  SQLiteMediaItemRepository.Delete", ex);
                throw;
            }
        }

        public T GetByLocation<T>(Uri location)
            where T : class, IMediaItem
        {
            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                var builder = GetSelectByLocationBuilder<T>(location);

                return GetItems<T>(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("  SQLiteMediaItemRepository.GetByLocation", ex);
                throw;
            }
        }

        public T GetByCreatorAndName<T>(Uri creator, string name)
            where T : class, IMediaItem
        {
            if (creator == null)
                throw new ArgumentNullException("creator");
            if (name == null)
                throw new ArgumentNullException("name");

            try
            {
                var builder = GetSelectByCreatorAndNameBuilder<T>(creator, name);

                return GetItems<T>(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error("  SQLiteMediaItemRepository.GetByCreatorAndName", ex);
                throw;
            }
        }

        public IEnumerable<T> GetByCatalog<T>(Uri catalog)
            where T : class, IMediaItem
        {
            if (catalog == null)
                throw new ArgumentNullException("catalog");

            try
            {
                var builder = GetSelectByCatalogBuilder<T>(catalog);

                return GetItems<T>(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  SQLiteMediaItemRepository.GetByCatalog", ex);
                throw;
            }
        }

        public IEnumerable<T> GetByCreator<T>(Uri creator)
            where T : class, IMediaItem
        {
            try
            {
                var builder = GetSelectByCreatorBuilder<T>(creator);

                return GetItems<T>(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  SQLiteMediaItemRepository.GetByCreator", ex);
                throw;
            }
        }

        public IEnumerable<T> GetByName<T>(string name)
            where T : class, IMediaItem
        {
            try
            {
                var builder = GetSelectByNameBuilder<T>(name);

                return GetItems<T>(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  SQLiteMediaItemRepository.GetByTarget", ex);
                throw;
            }
        }

        public IEnumerable<T> GetByTarget<T>(Uri target)
            where T : class, IMediaItem
        {
            if (target == null)
                throw new ArgumentNullException("target");

            try
            {
                var builder = GetSelectByTargetBuilder<T>(target);

                return GetItems<T>(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  SQLiteMediaItemRepository.GetByTarget", ex);
                throw;
            }
        }
        
        public IEnumerable<T> GetByTag<T>(TagDomain domain, string pattern)
            where T : class, IMediaItem
        {
            return GetByTag<T>(domain, pattern, Algorithms.Algorithm.Default);
        }

        public IEnumerable<T> GetByTag<T>(TagDomain domain, string pattern, IAlgorithm algorithm)
            where T : class, IMediaItem
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");

            try
            {
                var builder = GetSelectByTagBuilder<T>(domain, pattern, algorithm);

                return GetItems<T>(builder);
            }
            catch (Exception ex)
            {
                logger.Error("  SQLiteMediaItemRepository.GetByTarget", ex);
                throw;
            }
        }
    }
}
