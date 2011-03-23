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
            : base("Alexandria.db", "Source", "Number, NameHash")
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
            else
                return (int)SourceType.None;
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
            sql.AppendLine("  Number INTEGER NOT NULL DEFAULT 0");
            sql.AppendLine(");");
            sql.AppendLine("create index if not exists Source_Type on Source (Type ASC);");
            sql.AppendLine("create index if not exists Source_Path on Source (Path ASC);");
            sql.AppendLine("create index if not exists Source_Parent on Source (Parent ASC);");
            sql.AppendLine("create index if not exists Source_Name on Source (Name ASC);");
            sql.AppendLine("create index if not exists Source_NameHash on Source (NameHash ASC);");
            sql.AppendLine("create index if not exists Source_NameMetaphone on Source (NameMetaphone ASC);");
            sql.AppendLine("create index if not exists Source_Creator on Source (Creator ASC);");
            sql.AppendLine("create index if not exists Source_Nuber on Source (Number ASC);");
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
            var numberIndex = reader.GetOrdinal("Number");

            var id = new Guid(reader.GetString(idIndex));
            var type = Convert.ToInt32(reader.GetValue(typeIndex));
            var sourceType = (SourceType)type;

            switch (sourceType)
            {
                case SourceType.Folder:
                    source = new FolderSource(id);
                    break;
                case SourceType.Media:
                    source = new MediaSource(id);
                    break;
                case SourceType.Playlist:
                    source = new PlaylistSource(id);
                    break;
                case SourceType.PlaylistItem:
                    source = new PlaylistItemSource(id);
                    break;
                case SourceType.FileSystem:
                    source = new FileSystemSource(id);
                    break;
                default:
                    source = new ProxySource(id);
                    break;
            }

            if (source != null)
            {
                source.Path = reader.GetString(pathIndex);
                source.ImagePath = reader.GetString(imagePathIndex);
                source.Name = reader.GetString(nameIndex);
                source.Creator = reader.GetString(creatorIndex);
                source.Number = Convert.ToInt32(reader.GetValue(numberIndex));

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
            sql.AppendLine("insert or replace into Source (Id, Type, Path, ImagePath, Parent, Name, NameHash, NameMetaphone, Creator, Number)");
            sql.AppendLine(" values (@Id, @Type, @Path, @ImagePath, @Parent, @Name, @NameHash, @NameMetaphone, @Creator, @Number);");
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
            AddParameter(command, "@Number", record.Number);

            return command;
        }
    }
}
