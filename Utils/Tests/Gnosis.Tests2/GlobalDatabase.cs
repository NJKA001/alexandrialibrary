using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class GlobalDatabase
    {
        #region Constants
        private const string initializeCommandText = 
@"create table if not exists Artist (Id integer primary key, Type integer not null, Name text not null, Year integer not null);
create unique index if not exists Artist_unique on Artist (Name, Year);
create table if not exists Work (Id integer primary key, Type integer not null, Parent integer not null, Artist integer not null, Name text not null, Year integer not null, Number integer not null);
create unique index if not exists Work_unique on Work (Type, Parent, Artist, Name, Year, Number);
create table if not exists ArtistLink (Id integer primary key, Artist integer not null, Name text not null, Relationship integer not null, Source integer not null, Target text not null);
create unique index if not exists ArtistLink_unique on ArtistLink (Artist, Name, Relationship, Source, Target);
create table if not exists ArtistTag (Id integer primary key, Artist integer not null, Name text not null, Category integer not null, Source integer not null);
create unique index if not exists ArtistTag_unique on ArtistTag (Artist, Name, Category, Source);
create table if not exists WorkLink (Id integer primary key, Work integer not null, Name text not null, Relationship integer not null, Source integer not null, Target text not null);
create unique index if not exists WorkLink_unique on WorkLink (Work, Name, Relationship, Source, Target);
create table if not exists WorkTag (Id integer primary key, Work integer not null, Name text not null, Category integer not null, Source integer not null);
create unique index if not exists WorkTag_unique on WorkTag (Work, Name, Category, Source);";
        #endregion

        private IDbCommand GetCommand(IDbConnection connection, string commandText)
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = commandText;
            return command;
        }

        private void AddParameter(IDbCommand command, string name, object value)
        {
            command.Parameters.Add(new SQLiteParameter(name, value));
        }

        private void LoadArtists(IDbConnection connection, GlobalCache cache)
        {
            var command = GetCommand(connection, "select * from Artist;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = (uint)reader.GetInt64(0);
                    var type = (ArtistType)reader.GetInt32(1);
                    var name = reader.GetString(2);
                    var year = reader.GetInt16(3);
                    cache.Add(id, new Artist() { Type = type, Name = name, Year = year });
                }
            }

            LoadArtistLinks(connection, cache);
            LoadArtistTags(connection, cache);
        }

        private void LoadArtistLinks(IDbConnection connection, GlobalCache cache)
        {
            var command = GetCommand(connection, "select * from ArtistLink;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = (uint)reader.GetInt64(0);
                    var artist = cache.GetArtist((uint)reader.GetInt64(1));
                    var name = reader.GetString(2);
                    var relationship = (Relationship)reader.GetInt32(3);
                    var source = (Source)reader.GetInt32(4);
                    var target = reader.GetString(5);

                    if (artist == null)
                        continue;

                    var link = new Link(name, relationship, source, target);
                    cache.Add(id, artist, link);
                    artist.AddLink(link);
                }
            }
        }

        private void LoadArtistTags(IDbConnection connection, GlobalCache cache)
        {
            var command = GetCommand(connection, "select * from ArtistTag;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = (uint)reader.GetInt64(0);
                    var artist = cache.GetArtist((uint)reader.GetInt64(1));
                    var name = reader.GetString(2);
                    var category = (Category)reader.GetInt32(3);
                    var source = (Source)reader.GetInt32(4);

                    if (artist == null)
                        continue;

                    var tag = new Tag(name, category, source);
                    cache.Add(id, artist, tag);
                    artist.AddTag(tag);
                }
            }
        }

        private void LoadWorks(IDbConnection connection, GlobalCache cache)
        {
            var command = GetCommand(connection, "select * from Work;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = (uint)reader.GetInt64(0);
                    var type = (WorkType)reader.GetInt32(1);
                    var parent = cache.GetWork((uint)reader.GetInt64(2));
                    var artist = cache.GetArtist((uint)reader.GetInt64(3));
                    var name = reader.GetString(4);
                    var year = reader.GetInt16(5);
                    var number = (uint)reader.GetInt64(6);
                    cache.Add(id, new Work() { Type = type, Parent = parent, Artist = artist, Name = name, Year = year, Number = number });
                }
            }

            LoadWorkLinks(connection, cache);
            LoadWorkTags(connection, cache);
        }

        private void LoadWorkLinks(IDbConnection connection, GlobalCache cache)
        {
            var command = GetCommand(connection, "select * from WorkLink;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = (uint)reader.GetInt64(0);
                    var work = cache.GetWork((uint)reader.GetInt64(1));
                    var name = reader.GetString(2);
                    var relationship = (Relationship)reader.GetInt32(3);
                    var source = (Source)reader.GetInt32(4);
                    var target = reader.GetString(5);

                    if (work == null)
                        continue;

                    var link = new Link(name, relationship, source, target);
                    cache.Add(id, work, link);
                    work.AddLink(link);
                }
            }
        }

        private void LoadWorkTags(IDbConnection connection, GlobalCache cache)
        {
            var command = GetCommand(connection, "select * from WorkTag;");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = (uint)reader.GetInt64(0);
                    var work = cache.GetWork((uint)reader.GetInt64(1));
                    var name = reader.GetString(2);
                    var category = (Category)reader.GetInt32(3);
                    var source = (Source)reader.GetInt32(4);

                    if (work == null)
                        continue;

                    var tag = new Tag(name, category, source);
                    cache.Add(id, work, tag);
                    work.AddTag(tag);
                }
            }
        }

        public void Initialize(GlobalCache cache)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = GetCommand(connection, initializeCommandText);
                command.ExecuteNonQuery();

                LoadArtists(connection, cache);
                LoadWorks(connection, cache);
            }
        }

        public void DeleteArtist(IDbConnection connection, uint id)
        {
            var command = connection.CreateCommand();
            command.CommandText = "delete from Artist where Id = @Id; delete from ArtistLink where Artist = @Id; delete from ArtistTag where Artist = @Id;";
            AddParameter(command, "@Id", id);
            command.ExecuteNonQuery();
        }

        public void DeleteArtistLink(IDbConnection connection, uint id)
        {
            var command = GetCommand(connection, "delete from ArtistLink where Id = @Id;");
            AddParameter(command, "@Id", id);
            command.ExecuteNonQuery();
        }

        public void DeleteArtistTag(IDbConnection connection, uint id)
        {
            var command = GetCommand(connection, "delete from ArtistTag where Id = @Id;");
            AddParameter(command, "@Id", id);
            command.ExecuteNonQuery();
        }

        public void DeleteWork(IDbConnection connection, uint id)
        {
            var command = connection.CreateCommand();
            command.CommandText = "delete from Work where Id = @Id; delete from WorkLink where Work = @Id; delete from WorkTag where Work = @Id;";
            AddParameter(command, "@Id", id);
            command.ExecuteNonQuery();
        }

        public void DeleteWorkLink(IDbConnection connection, uint id)
        {
            var command = GetCommand(connection, "delete from WorkLink where Id = @Id;");
            AddParameter(command, "@Id", id);
            command.ExecuteNonQuery();
        }

        public void DeleteWorkTag(IDbConnection connection, uint id)
        {
            var command = GetCommand(connection, "delete from WorkTag where Id = @Id;");
            AddParameter(command, "@Id", id);
            command.ExecuteNonQuery();
        }

        public uint InsertArtist(IDbConnection connection, Artist artist)
        {
            var command = connection.CreateCommand();
            AddParameter(command, "@Type", (ushort)artist.Type);
            AddParameter(command, "@Name", artist.Name);
            AddParameter(command, "@Year", artist.Year);

            command.CommandText = "insert into Artist (Type, Name, Year) values (@Type, @Name, @Year); select last_insert_rowid();";
            var result = command.ExecuteScalar();
            
            uint id = 0;
            return (result != null && uint.TryParse(result.ToString(), out id)) ? id : 0;
        }

        public uint InsertArtistLink(IDbConnection connection, Link link, uint artistId)
        {
            var command = connection.CreateCommand();
            AddParameter(command, "@Artist", artistId);
            AddParameter(command, "@Name", link.Name);
            AddParameter(command, "@Relationship", (ushort)link.Relationship);
            AddParameter(command, "@Source", (ushort)link.Source);
            AddParameter(command, "@Target", link.Target);

            command.CommandText = "insert into ArtistLink (Artist, Name, Relationship, Source, Target) values (@Artist, @Name, @Relationship, @Source, @Target); select last_insert_rowid();";
            var result = command.ExecuteScalar();

            uint id = 0;
            return (result != null && uint.TryParse(result.ToString(), out id)) ? id : 0;
        }

        public uint InsertArtistTag(IDbConnection connection, Tag tag, uint artistId)
        {
            var command = connection.CreateCommand();
            AddParameter(command, "@Artist", artistId);
            AddParameter(command, "@Name", tag.Name);
            AddParameter(command, "@Category", (ushort)tag.Category);
            AddParameter(command, "@Source", (ushort)tag.Source);

            command.CommandText = "insert into ArtistTag (Artist, Name, Category, Source) values (@Artist, @Name, @Category, @Source); select last_insert_rowid();";
            var result = command.ExecuteScalar();

            uint id = 0;
            return (result != null && uint.TryParse(result.ToString(), out id)) ? id : 0;
        }

        public uint InsertWork(IDbConnection connection, Work work, uint parentId, uint artistId)
        {
            var command = connection.CreateCommand();
            AddParameter(command, "@Type", (ushort)work.Type);
            AddParameter(command, "@Parent", parentId);
            AddParameter(command, "@Artist", artistId);
            AddParameter(command, "@Name", work.Name);
            AddParameter(command, "@Year", work.Year);
            AddParameter(command, "@Number", work.Number);
            command.CommandText = "insert into Work (Type, Parent, Artist, Name, Year, Number) values (@Type, @Parent, @Artist, @Name, @Year, @Number); select last_insert_rowid();";
            var result = command.ExecuteScalar();

            uint id = 0;
            return (result != null && uint.TryParse(result.ToString(), out id)) ? id : 0;
        }

        public uint InsertWorkLink(IDbConnection connection, Link link, uint workId)
        {
            var command = connection.CreateCommand();
            AddParameter(command, "@Work", workId);
            AddParameter(command, "@Name", link.Name);
            AddParameter(command, "@Relationship", (ushort)link.Relationship);
            AddParameter(command, "@Source", (ushort)link.Source);
            AddParameter(command, "@Target", link.Target);
            command.CommandText = "insert into WorkLink (Work, Name, Relationship, Source, Target) values (@Work, @Name, @Relationship, @Source, @Target); select last_insert_rowid();";
            var result = command.ExecuteScalar();

            uint id = 0;
            return (result != null && uint.TryParse(result.ToString(), out id)) ? id : 0;
        }

        public uint InsertWorkTag(IDbConnection connection, Tag tag, uint workId)
        {
            var command = connection.CreateCommand();
            AddParameter(command, "@Work", workId);
            AddParameter(command, "@Name", tag.Name);
            AddParameter(command, "@Category", (ushort)tag.Category);
            AddParameter(command, "@Source", (ushort)tag.Source);
            command.CommandText = "insert into WorkTag (Work, Name, Category, Source) values (@Work, @Name, @Category, @Source); select last_insert_rowid();";
            var result = command.ExecuteScalar();

            uint id = 0;
            return (result != null && uint.TryParse(result.ToString(), out id)) ? id : 0;
        }

        public void UpdateArtist(IDbConnection connection, Artist artist, uint id)
        {
            if (!artist.IsChanged)
                return;

            var command = connection.CreateCommand();
            AddParameter(command, "@Type", (ushort)artist.Type);
            AddParameter(command, "@Name", artist.Name);
            AddParameter(command, "@Year", artist.Year);
            AddParameter(command, "@Id", id);

            command.CommandText = "update Artist set Type = @Type, Name = @Name, Year = @Year where Id = @Id;";
            command.ExecuteNonQuery();
        }

        public void UpdateArtistLink(IDbConnection connection, Link link, uint id, uint artistId)
        {
            if (!link.IsChanged)
                return;

            var command = connection.CreateCommand();
            AddParameter(command, "@Artist", artistId);
            AddParameter(command, "@Name", link.Name);
            AddParameter(command, "@Relationship", (ushort)link.Relationship);
            AddParameter(command, "@Source", (ushort)link.Source);
            AddParameter(command, "@Target", link.Target);
            AddParameter(command, "@Id", id);
            command.CommandText = "update ArtistLink set Artist = @Artist, Name = @Name, Relationship = @Relationship, Source = @Source, Target = @Target where Id = @Id;";
            command.ExecuteNonQuery();
        }

        public void UpdateArtistTag(IDbConnection connection, Tag tag, uint id, uint artistId)
        {
            if (!tag.IsChanged)
                return;

            var command = connection.CreateCommand();
            AddParameter(command, "@Artist", artistId);
            AddParameter(command, "@Name", tag.Name);
            AddParameter(command, "@Category", (ushort)tag.Category);
            AddParameter(command, "@Source", (ushort)tag.Source);
            AddParameter(command, "@Id", id);
            command.CommandText = "update ArtistTag set Artist = @Artist, Name = @Name, Category = @Category, Source = @Source where Id = @Id;";
            command.ExecuteNonQuery();
        }

        public void UpdateWork(IDbConnection connection, Work work, uint id, uint parentId, uint artistId)
        {
            if (!work.IsChanged)
                return;

            var command = connection.CreateCommand();
            AddParameter(command, "@Type", (ushort)work.Type);
            AddParameter(command, "@Parent", parentId);
            AddParameter(command, "@Artist", artistId);
            AddParameter(command, "@Name", work.Name);
            AddParameter(command, "@Year", work.Year);
            AddParameter(command, "@Number", work.Number);
            AddParameter(command, "@Id", id);
            command.CommandText = "update Work set Type = @Type, Parent = @Parent, Artist = @Artist, Name = @Name, Year = @Year, Number = @Number where Id = @Id;";
            command.ExecuteNonQuery();
        }

        public void UpdateWorkLink(IDbConnection connection, Link link, uint id, uint workId)
        {
            if (!link.IsChanged)
                return;

            var command = connection.CreateCommand();
            AddParameter(command, "@Work", workId);
            AddParameter(command, "@Name", link.Name);
            AddParameter(command, "@Relationship", (ushort)link.Relationship);
            AddParameter(command, "@Source", (ushort)link.Source);
            AddParameter(command, "@Target", link.Target);
            AddParameter(command, "@Id", id);
            command.CommandText = "update WorkLink set Work = @Work, Name = @Name, Relationship = @Relationship, Source = @Source, Target = @Target where Id = @Id;";
            command.ExecuteNonQuery();
        }

        public void UpdateWorkTag(IDbConnection connection, Tag tag, uint id, uint workId)
        {
            if (!tag.IsChanged)
                return;

            var command = connection.CreateCommand();
            AddParameter(command, "@Work", workId);
            AddParameter(command, "@Name", tag.Name);
            AddParameter(command, "@Category", (ushort)tag.Category);
            AddParameter(command, "@Source", (ushort)tag.Source);
            AddParameter(command, "@Id", id);
            command.CommandText = "update WorkTag set Work = @Work, Name = @Name, Category = @Category, Source = @Source where Id = @Id;";
            command.ExecuteNonQuery();
        }

        public IDbConnection GetConnection()
        {
            return new SQLiteConnection("Data Source=alexandria.db;");
        }
    }
}
