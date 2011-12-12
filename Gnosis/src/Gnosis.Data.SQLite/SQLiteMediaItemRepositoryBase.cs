using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.SQLite
{
    public abstract class SQLiteMediaItemRepositoryBase<T>
        : SQLiteRepositoryBase, IMediaItemRepository<T>
        where T : class, IMediaItem
    {
        protected SQLiteMediaItemRepositoryBase(ILogger logger, string tableName)
            : this(logger, tableName, null)
        {
        }

        protected SQLiteMediaItemRepositoryBase(ILogger logger, string tableName, IDbConnection defaultConnection)
            : base(logger, defaultConnection)
        {
            if (tableName == null)
                throw new ArgumentNullException("tableName");

            this.tableName = tableName;
            this.defaultItem = GetDefaultItem();
        }

        private readonly string tableName;
        private readonly T defaultItem;

        protected abstract T GetItem(Uri location, string name, string summary, DateTime fromDate, DateTime toDate, uint number, TimeSpan duration, uint height, uint width, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, byte[] thumbnailData);

        protected virtual ICommandBuilder GetInitializeBuilder()
        {
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

        protected virtual T GetDefaultItem()
        {
            return null;
        }

        protected virtual ICommandBuilder GetDeleteBuilder(Uri location)
        {
            if (defaultItem != null && defaultItem.Location.ToString() == location.ToString())
                return null;

            var builder = new CommandBuilder();
            builder.AppendFormatLine("delete from {0} where Location = @Location;", tableName);
            builder.AddParameter("@Location", location.ToString());
            return builder;
        }

        protected virtual ICommandBuilder GetSaveBuilder(T item)
        {
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

        protected virtual ICommandBuilder GetSelectByLocationBuilder(Uri location)
        {
            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Location = @Location;", tableName);
            builder.AddParameter("@Location", location.ToString());
            return builder;
        }

        protected virtual ICommandBuilder GetSelectByCatalogBuilder(Uri catalog)
        {
            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Catalog = @Catalog order by Number;", tableName);
            builder.AddParameter("@Catalog", catalog.ToString());
            return builder;
        }

        protected virtual ICommandBuilder GetSelectByCreatorBuilder(Uri creator)
        {
            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Creator = @Creator order by FromDate;", tableName);
            builder.AddParameter("@Creator", creator.ToString());
            return builder;
        }

        protected virtual ICommandBuilder GetSelectByCreatorAndNameBuilder(Uri creator, string name)
        {
            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Creator = @Creator and Name = @Name;", tableName);
            builder.AddParameter("@Creator", creator.ToString());
            builder.AddParameter("@Name", name);
            return builder;
        }

        protected virtual ICommandBuilder GetSelectByNameBuilder(string name)
        {
            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Name like @Name order by FromDate;", tableName);
            builder.AddParameter("@Name", name.ToString());
            return builder;
        }

        protected virtual ICommandBuilder GetSelectByTargetBuilder(Uri target)
        {
            var builder = new CommandBuilder();
            builder.AppendFormatLine("select * from {0} where Target = @Target order by Number;", tableName);
            builder.AddParameter("@Target", target.ToString());
            return builder;
        }

        protected virtual ICommandBuilder GetSelectByTagBuilder(TagDomain domain, string pattern, IAlgorithm algorithm)
        {
            var builder = new CommandBuilder();
            builder.AppendFormatLine("select {0}.* from {0} inner join Tag on {0}.Location = Tag.Target where Tag.Algorithm = @Algorithm and Tag.Domain = @Domain and Tag.Value like @Pattern;", tableName);
            builder.AddParameter("@Algorithm", algorithm.Id);
            builder.AddParameter("@Domain", (int)domain);
            builder.AddParameter("@Pattern", pattern);
            return builder;
        }

        protected virtual IEnumerable<T> GetItems(ICommandBuilder builder)
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
                        var item = ReadItem(reader);
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

        protected virtual T ReadItem(IDataRecord record)
        {
            var location = record.GetUri("Location");

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
            var targetType = record.GetStringLookup<IMediaType>("TargetType", x => MediaType.Parse(x));
            var user = record.GetUri("User");
            var userName = record.GetString("UserName");
            var thumbnail = record.GetUri("Thumbnail");
            var thumbnailData = record.GetBytes("ThumbnailData");

            return GetItem(location, name, summary, fromDate, toDate, number, duration, height, width, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, thumbnailData);
        }

        public void Initialize()
        {
            try
            {
                var builder = GetInitializeBuilder();

                ExecuteNonQuery(builder);

                if (defaultItem != null)
                {
                    var saveDefaultBuilder = GetSaveBuilder(defaultItem);
                    ExecuteNonQuery(saveDefaultBuilder);
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("  {0}.Initialize", this.GetType().Name), ex);
                throw;
            }
        }

        public void Save(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var item in items)
                    builders.Add(GetSaveBuilder(item));

                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("  {0}.Save", this.GetType().Name), ex);
                throw;
            }
        }

        public void Delete(IEnumerable<Uri> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            try
            {
                var builders = new List<ICommandBuilder>();

                foreach (var location in items)
                {
                    var builder = GetDeleteBuilder(location);
                    if (builder != null)
                        builders.Add(builder);
                }
                if (builders.Count == 0)
                    return;

                ExecuteTransaction(builders);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("  {0}.Delete", this.GetType().Name), ex);
                throw;
            }
        }

        public T GetByLocation(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            try
            {
                var builder = GetSelectByLocationBuilder(location);

                return GetItems(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("  {0}.GetByLocation", this.GetType().Name), ex);
                throw;
            }
        }

        public T GetByCreatorAndName(Uri creator, string name)
        {
            if (creator == null)
                throw new ArgumentNullException("creator");
            if (name == null)
                throw new ArgumentNullException("name");

            try
            {
                var builder = GetSelectByCreatorAndNameBuilder(creator, name);

                return GetItems(builder).FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("  {0}.GetByCreatorAndName", this.GetType().Name), ex);
                throw;
            }
        }

        public IEnumerable<T> GetByCatalog(Uri catalog)
        {
            if (catalog == null)
                throw new ArgumentNullException("catalog");

            try
            {
                var builder = GetSelectByCatalogBuilder(catalog);

                return GetItems(builder);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("  {0}.GetByCatalog", this.GetType().Name), ex);
                throw;
            }
        }

        public IEnumerable<T> GetByCreator(Uri creator)
        {
            try
            {
                var builder = GetSelectByCreatorBuilder(creator);

                return GetItems(builder);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("  {0}.GetByCreator", this.GetType().Name), ex);
                throw;
            }
        }

        public IEnumerable<T> GetByName(string name)
        {
            try
            {
                var builder = GetSelectByNameBuilder(name);

                return GetItems(builder);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("  {0}.GetByTarget", this.GetType().Name), ex);
                throw;
            }
        }

        public IEnumerable<T> GetByTarget(Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            try
            {
                var builder = GetSelectByTargetBuilder(target);

                return GetItems(builder);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("  {0}.GetByTarget", this.GetType().Name), ex);
                throw;
            }
        }
        
        public IEnumerable<T> GetByTag(TagDomain domain, string pattern)
        {
            return GetByTag(domain, pattern, Algorithms.Algorithm.Default);
        }

        public IEnumerable<T> GetByTag(TagDomain domain, string pattern, IAlgorithm algorithm)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");
            if (algorithm == null)
                throw new ArgumentNullException("algorithm");

            try
            {
                var builder = GetSelectByTagBuilder(domain, pattern, algorithm);

                return GetItems(builder);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("  {0}.GetByTarget", this.GetType().Name), ex);
                throw;
            }
        }
    }
}
