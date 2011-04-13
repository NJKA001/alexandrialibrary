using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Repositories
{
    public class SourceRepository : RepositoryBase<ISource>
    {
        public SourceRepository()
            : base("Alexandria.db", "Source", "Number, Date, NameHash")
        {
        }

        private readonly ISourcePropertyFactory propertyFactory = new SourcePropertyFactory();

        private int GetSourceType(ISource record)
        {
            if (record is FolderSource)
                return (int)SourceType.Folder;
            else if (record is MediaSource)
                return (int)SourceType.Media;
            else if (record is PlaylistSource)
                return (int)SourceType.Playlist;
            else if (record is PlaylistItemSource)
                return (int)SourceType.PlaylistItem;
            else if (record is FileSystemSource)
                return (int)SourceType.FileSystem;
            else if (record is DirectorySource)
                return (int)SourceType.Directory;
            else if (record is PodcastSource)
                return (int)SourceType.Podcast;
            else if (record is SpiderSource)
                return (int)SourceType.Spider;
            else
                return (int)SourceType.None;
        }

        private ISource GetSource(SourceType sourceType, Guid id)
        {
            switch (sourceType)
            {
                case SourceType.Folder:
                    return new FolderSource(id);
                case SourceType.Media:
                    return new MediaSource(id);
                case SourceType.Playlist:
                    return new PlaylistSource(id);
                case SourceType.PlaylistItem:
                    return new PlaylistItemSource(id);
                case SourceType.FileSystem:
                    return new FileSystemSource(id);
                case SourceType.Directory:
                    return new DirectorySource(id);
                case SourceType.Podcast:
                    return new PodcastSource(id);
                case SourceType.Spider:
                    return new SpiderSource(id);
                default:
                    return new ProxySource(id);
            }
        }

        protected override string GetInitializeText()
        {
            var sql = new StringBuilder();

            sql.AppendLine("create table if not exists Source (");
            sql.AppendLine("  Id TEXT PRIMARY KEY NOT NULL,");
            sql.AppendLine("  Type INTEGER NOT NULL,");
            sql.AppendLine("  Path TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("  ImagePath TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("  Parent TEXT,");
            sql.AppendLine("  Name TEXT NOT NULL DEFAULT 'Unknown Source',");
            sql.AppendLine("  NameHash TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("  NameMetaphone TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("  Creator TEXT NOT NULL DEFAULT 'Unknown Creator',");
            sql.AppendLine("  Summary TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("  Number INTEGER NOT NULL DEFAULT 0,");
            sql.AppendLine("  Date TEXT NOT NULL DEFAULT '2000-01-01T00:00:00',");
            sql.AppendLine("  ImagePattern TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("  ChildPattern TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("  PagePattern TEXT NOT NULL DEFAULT ''");
            sql.AppendLine(");");
            sql.AppendLine("create index if not exists Source_Type on Source (Type ASC);");
            sql.AppendLine("create index if not exists Source_Path on Source (Path ASC);");
            sql.AppendLine("create index if not exists Source_Parent on Source (Parent ASC);");
            sql.AppendLine("create index if not exists Source_Name on Source (Name ASC);");
            sql.AppendLine("create index if not exists Source_NameHash on Source (NameHash ASC);");
            sql.AppendLine("create index if not exists Source_NameMetaphone on Source (NameMetaphone ASC);");
            sql.AppendLine("create index if not exists Source_Creator on Source (Creator ASC);");
            sql.AppendLine("create index if not exists Source_Summary on Source (Summary ASC);");
            sql.AppendLine("create index if not exists Source_Nuber on Source (Number ASC);");
            sql.AppendLine("create index if not exists Source_Date on Source (Date ASC);");
            sql.AppendLine("create index if not exists Source_DefaultSortOrder on Source (Number ASC, NameHash ASC);");

            sql.AppendLine("create table if not exists SourceProperty (");
            sql.AppendLine("  Id TEXT PRIMARY KEY NOT NULL,");
            sql.AppendLine("  Source TEXT NOT NULL,");
            sql.AppendLine("  Name TEXT NOT NULL,");
            sql.AppendLine("  Value TEXT,");
            sql.AppendLine("  ValueHash TEXT NOT NULL DEFAULT '',");
            sql.AppendLine("  ValueMetaphone TEXT NOT NULL DEFAULT ''");
            sql.AppendLine(");");
            sql.AppendLine("create index if not exists SourceProperty_Source on SourceProperty (Source ASC);");
            sql.AppendLine("create index if not exists SourceProperty_Name on SourceProperty (Name ASC);");
            sql.AppendLine("create index if not exists SourceProperty_Value on SourceProperty (Value ASC);");
            sql.AppendLine("create index if not exists SourceProperty_ValueHash on SourceProperty (ValueHash ASC);");
            sql.AppendLine("create index if not exists SourceProperty_ValueMetaphone on SourceProperty (ValueMetaphone ASC);");

            return sql.ToString();
        }

        protected override ISource GetRecord(IDataReader reader)
        {
            ISource source = null;

            var idIndex = reader.GetOrdinal("Id");
            var typeIndex = reader.GetOrdinal("Type");
            var pathIndex = reader.GetOrdinal("Path");
            var imagePathIndex = reader.GetOrdinal("ImagePath");
            var parentIndex = reader.GetOrdinal("Parent");
            var nameIndex = reader.GetOrdinal("Name");
            var creatorIndex = reader.GetOrdinal("Creator");
            var summaryIndex = reader.GetOrdinal("Summary");
            var numberIndex = reader.GetOrdinal("Number");
            var dateIndex = reader.GetOrdinal("Date");
            var imagePatternIndex = reader.GetOrdinal("ImagePattern");
            var childPatternIndex = reader.GetOrdinal("ChildPattern");
            var pagePatternIndex = reader.GetOrdinal("PagePattern");

            var id = new Guid(reader.GetString(idIndex));
            var type = Convert.ToInt32(reader.GetValue(typeIndex));
            var sourceType = (SourceType)type;

            source = GetSource(sourceType, id);
            if (source != null)
            {
                source.Path = reader.GetString(pathIndex);
                source.ImagePath = reader.GetString(imagePathIndex);
                source.Name = reader.GetString(nameIndex);
                source.Creator = reader.GetString(creatorIndex);
                source.Summary = reader.GetString(summaryIndex);
                source.Number = Convert.ToInt32(reader.GetValue(numberIndex));
                source.Date = DateTime.Parse(reader.GetString(dateIndex));
                source.ImagePattern = reader.GetString(imagePatternIndex);
                source.ChildPattern = reader.GetString(childPatternIndex);
                source.PagePattern = reader.GetString(pagePatternIndex);

                if (!reader.IsDBNull(parentIndex))
                {
                    source.Parent = new ProxySource(new Guid(reader.GetString(parentIndex)));
                }
            }

            return source;
        }

        protected override IDbCommand GetSaveCommand(IDbConnection connection, ISource record)
        {
            if (record == null)
                throw new ArgumentNullException("record");

            var command = connection.CreateCommand();

            var sql = new StringBuilder();
            sql.AppendLine("insert or replace into Source (Id, Type, Path, ImagePath, Parent, Name, NameHash, NameMetaphone, Creator, Summary, Number, Date, ImagePattern, ChildPattern, PagePattern)");
            sql.AppendLine(" values (@Id, @Type, @Path, @ImagePath, @Parent, @Name, @NameHash, @NameMetaphone, @Creator, @Summary, @Number, @Date, @ImagePattern, @ChildPattern, @PagePattern);");
            command.CommandText = sql.ToString();

            AddParameter(command, "@Id", record.Id.ToString());
            AddParameter(command, "@Type", GetSourceType(record));
            AddParameter(command, "@Path", record.Path);
            AddParameter(command, "@ImagePath", record.ImagePath);
            AddParameter(command, "@Parent", record.Parent != null ? record.Parent.Id.ToString() : null);
            AddParameter(command, "@Name", record.Name);
            AddParameter(command, "@NameHash", record.NameHash);
            AddParameter(command, "@NameMetaphone", record.NameMetaphone);
            AddParameter(command, "@Creator", record.Creator);
            AddParameter(command, "@Summary", record.Summary);
            AddParameter(command, "@Number", record.Number);
            AddParameter(command, "@Date", record.Date);
            AddParameter(command, "@ImagePattern", record.ImagePattern);
            AddParameter(command, "@ChildPattern", record.ChildPattern);
            AddParameter(command, "@PagePattern", record.PagePattern);

            return command;
        }
    }
}
