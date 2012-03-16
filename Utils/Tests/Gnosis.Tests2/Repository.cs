using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Repository
        : IRepository
    {
        public Repository(Cache cache)
        {
            if (cache == null)
                throw new ArgumentNullException("cache");

            this.cache = cache;
        }

        private readonly Cache cache;

        private IDbConnection GetConnection()
        {
            return new SQLiteConnection("Data Source=alexandria.db;");
        }

        private IDbCommand GetCommand(IDbConnection connection, string commandText)
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = commandText;
            return command;
        }

        private object GetParameter(string name, object value)
        {
            return new SQLiteParameter(name, value);
        }

        private void AddParameter(IDbCommand command, string name, object value)
        {
            command.Parameters.Add(GetParameter(name, value));
        }

        private void DeleteArtist(IDbConnection connection, Artist artist)
        {
            var id = cache.GetArtistId(artist);
            if (id == 0)
                return;

            var command = connection.CreateCommand();
            command.CommandText = "delete from Artist where Id = @Id; delete from ArtistLink where Artist = @Id; delete from ArtistTag where Artist = @Id;";
            AddParameter(command, "@Id", id);
            command.ExecuteNonQuery();

            cache.Remove(artist);
        }

        private void DeleteWork(IDbConnection connection, Work work)
        {
            if (work == null)
                return;

            var id = cache.GetWorkId(work);
            if (id == 0)
                return;

            var command = connection.CreateCommand();
            command.CommandText = "delete from Work where Id = @Id; delete from WorkLink where Work = @Id; delete from WorkTag where Work = @Id;";
            AddParameter(command, "@Id", id);
            command.ExecuteNonQuery();

            cache.Remove(work);

            //TODO: Decide whether or not to cascade delete works
            //foreach (var child in work.Children)
            //{
            //    DeleteWork(connection, child);
            //}
        }

        private void SaveArtist(IDbConnection connection, Artist artist)
        {
            if (artist == null)
                return;

            var id = cache.GetArtistId(artist);

            var command = connection.CreateCommand();
            AddParameter(command, "@Type", (ushort)artist.Type);
            AddParameter(command, "@Name", artist.Name);
            AddParameter(command, "@Year", artist.Year);

            if (id > 0)
            {
                if (artist.IsChanged)
                {
                    AddParameter(command, "@Id", id);
                    command.CommandText = "update Artist set Type = @Type, Name = @Name, Year = @Year where Id = @Id;";
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                command.CommandText = "insert into Artist (Type, Name, Year) values (@Type, @Name, @Year); select last_insert_rowid();";
                var result = command.ExecuteScalar();
                if (result != null && uint.TryParse(result.ToString(), out id))
                {
                    cache.Add(id, artist);
                }
            }

            foreach (var link in artist.Links)
            {
                SaveArtistLink(connection, artist, link);
            }

            foreach (var existingLink in cache.GetLinksByArtist(id))
            {
                var existingId = cache.GetArtistLinkId(existingLink);
                if (existingId > 0 && !artist.Links.Contains(existingLink))
                {
                    using (var deleteLinkCommand = GetCommand(connection, "delete from ArtistLink where Id = @Id;"))
                    {
                        AddParameter(deleteLinkCommand, "@Id", existingId);
                        deleteLinkCommand.ExecuteNonQuery();
                        cache.Remove(artist, existingLink);
                    }
                }
            }

            foreach (var tag in artist.Tags)
            {
                SaveArtistTag(connection, artist, tag);
            }

            foreach (var existingTag in cache.GetTagsByArtist(id))
            {
                var existingId = cache.GetArtistTagId(existingTag);
                if (existingId > 0 && !artist.Tags.Contains(existingTag))
                {
                    using (var deleteTagCommand = GetCommand(connection, "delete from ArtistTag where Id = @Id;"))
                    {
                        AddParameter(deleteTagCommand, "@Id", existingId);
                        deleteTagCommand.ExecuteNonQuery();
                        cache.Remove(artist, existingTag);
                    }
                }
            }

            foreach (var work in artist.Works)
            {
                SaveWork(connection, work);
            }
        }

        private void SaveArtistLink(IDbConnection connection, Artist artist, Link link)
        {
            if (artist == null || link == null)
                return;

            var artistId = cache.GetArtistId(artist);
            if (artistId == 0)
                return;

            var id = cache.GetArtistLinkId(link);

            //create table if not exists ArtistLink (Id integer primary key, Artist integer not null, Name text not null, Relationship integer not null, Source integer not null, Target text not null);
            var command = connection.CreateCommand();
            AddParameter(command, "@Artist", artistId);
            AddParameter(command, "@Name", link.Name);
            AddParameter(command, "@Relationship", (ushort)link.Relationship);
            AddParameter(command, "@Source", (ushort)link.Source);
            AddParameter(command, "@Target", link.Target);

            if (id > 0)
            {
                if (link.IsChanged)
                {
                    AddParameter(command, "@Id", id);
                    command.CommandText = "update ArtistLink set Artist = @Artist, Name = @Name, Relationship = @Relationship, Source = @Source, Target = @Target where Id = @Id;";
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                command.CommandText = "insert into ArtistLink (Artist, Name, Relationship, Source, Target) values (@Artist, @Name, @Relationship, @Source, @Target); select last_insert_rowid();";
                var result = command.ExecuteScalar();
                if (result != null && uint.TryParse(result.ToString(), out id))
                {
                    cache.Add(id, artist, link);
                }
            }
        }

        private void SaveArtistTag(IDbConnection connection, Artist artist, Tag tag)
        {
            if (artist == null || tag == null)
                return;

            var artistId = cache.GetArtistId(artist);
            if (artistId == 0)
                return;

            var id = cache.GetArtistTagId(tag);

            //create table if not exists ArtistTag (Id integer primary key, Artist integer not null, Name text not null, Category integer not null, Source integer not null, Target text not null)
            var command = connection.CreateCommand();
            AddParameter(command, "@Artist", artistId);
            AddParameter(command, "@Name", tag.Name);
            AddParameter(command, "@Category", (ushort)tag.Category);
            AddParameter(command, "@Source", (ushort)tag.Source);
            AddParameter(command, "@Target", tag.Target);

            if (id > 0)
            {
                if (tag.IsChanged)
                {
                    AddParameter(command, "@Id", id);
                    command.CommandText = "update ArtistTag set Artist = @Artist, Name = @Name, Category = @Category, Source = @Source, Target = @Target where Id = @Id;";
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                command.CommandText = "insert into ArtistTag (Artist, Name, Category, Source, Target) values (@Artist, @Name, @Category, @Source, @Target); select last_insert_rowid();";
                var result = command.ExecuteScalar();
                if (result != null && uint.TryParse(result.ToString(), out id))
                {
                    cache.Add(id, artist, tag);
                }
            }
        }

        private void SaveWork(IDbConnection connection, Work work)
        {
            if (work == null)
                return;

            var id = cache.GetWorkId(work);
            
            var command = connection.CreateCommand();
            AddParameter(command, "@Type", (ushort)work.Type);
            AddParameter(command, "@Parent", cache.GetWorkId(work.Parent));
            AddParameter(command, "@Artist", cache.GetArtistId(work.Artist));
            AddParameter(command, "@Name", work.Name);
            AddParameter(command, "@Year", work.Year);
            AddParameter(command, "@Number", work.Number);

            if (id > 0)
            {
                if (work.IsChanged)
                {
                    AddParameter(command, "@Id", id);
                    command.CommandText = "update Work set Type = @Type, Parent = @Parent, Artist = @Artist, Name = @Name, Year = @Year, Number = @Number where Id = @Id;";
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                command.CommandText = "insert into Work (Type, Parent, Artist, Name, Year, Number) values (@Type, @Parent, @Artist, @Name, @Year, @Number); select last_insert_rowid();";
                var result = command.ExecuteScalar();
                if (result != null && uint.TryParse(result.ToString(), out id))
                {
                    cache.Add(id, work);
                }
            }

            foreach (var link in work.Links)
            {
                SaveWorkLink(connection, work, link);
            }

            foreach (var existingLink in cache.GetLinksByWork(id))
            {
                var existingId = cache.GetWorkLinkId(existingLink);
                if (existingId > 0 && !work.Links.Contains(existingLink))
                {
                    using (var deleteLinkCommand = GetCommand(connection, "delete from WorkLink where Id = @Id;"))
                    {
                        AddParameter(deleteLinkCommand, "@Id", existingId);
                        deleteLinkCommand.ExecuteNonQuery();
                        cache.Remove(work, existingLink);
                    }
                }
            }

            foreach (var tag in work.Tags)
            {
                SaveWorkTag(connection, work, tag);
            }

            foreach (var existingTag in cache.GetTagsByWork(id))
            {
                var existingId = cache.GetArtistTagId(existingTag);
                if (existingId > 0 && !work.Tags.Contains(existingTag))
                {
                    using (var deleteTagCommand = GetCommand(connection, "delete from WorkTag where Id = @Id;"))
                    {
                        AddParameter(deleteTagCommand, "@Id", existingId);
                        deleteTagCommand.ExecuteNonQuery();
                        cache.Remove(work, existingTag);
                    }
                }
            }

            foreach (var child in work.Children)
            {
                SaveWork(connection, child);
            }
        }

        private void SaveWorkLink(IDbConnection connection, Work work, Link link)
        {
            if (work == null || link == null)
                return;

            var workId = cache.GetWorkId(work);
            if (workId == 0)
                return;

            var id = cache.GetWorkLinkId(link);
            
            //create table if not exists WorkLink (Id integer primary key, Work integer not null, Name text not null, Relationship integer not null, Source integer not null, Target text not null);
            var command = connection.CreateCommand();
            AddParameter(command, "@Work", workId);
            AddParameter(command, "@Name", link.Name);
            AddParameter(command, "@Relationship", (ushort)link.Relationship);
            AddParameter(command, "@Source", (ushort)link.Source);
            AddParameter(command, "@Target", link.Target);

            if (id > 0)
            {
                if (link.IsChanged)
                {
                    AddParameter(command, "@Id", id);
                    command.CommandText = "update WorkLink set Work = @Work, Name = @Name, Relationship = @Relationship, Source = @Source, Target = @Target where Id = @Id;";
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                command.CommandText = "insert into WorkLink (Work, Name, Relationship, Source, Target) values (@Work, @Name, @Relationship, @Source, @Target); select last_insert_rowid();";
                var result = command.ExecuteScalar();
                if (result != null && uint.TryParse(result.ToString(), out id))
                {
                    cache.Add(id, work, link);
                }
            }
        }

        private void SaveWorkTag(IDbConnection connection, Work work, Tag tag)
        {
            if (work == null || tag == null)
                return;

            var workId = cache.GetWorkId(work);
            if (workId == 0)
                return;

            var id = cache.GetWorkTagId(tag);

            //create table if not exists WorkTag (Id integer primary key, Work integer not null, Name text not null, Category integer not null, Source integer not null, Target text not null)
            var command = connection.CreateCommand();
            AddParameter(command, "@Work", workId);
            AddParameter(command, "@Name", tag.Name);
            AddParameter(command, "@Category", (ushort)tag.Category);
            AddParameter(command, "@Source", (ushort)tag.Source);
            AddParameter(command, "@Target", tag.Target);

            if (id > 0)
            {
                if (tag.IsChanged)
                {
                    AddParameter(command, "@Id", id);
                    command.CommandText = "update WorkTag set Work = @Work, Name = @Name, Category = @Category, Source = @Source, Target = @Target where Id = @Id;";
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                command.CommandText = "insert into WorkTag (Work, Name, Category, Source, Target) values (@Work, @Name, @Category, @Source, @Target); select last_insert_rowid();";
                var result = command.ExecuteScalar();
                if (result != null && uint.TryParse(result.ToString(), out id))
                {
                    cache.Add(id, work, tag);
                }
            }
        }

        private void InitializeDatabase(IDbConnection connection)
        {
            var command = GetCommand(connection,
@"create table if not exists Artist (Id integer primary key, Type integer not null, Name text not null, Year integer not null);
create unique index if not exists Artist_unique on Artist (Name, Year);
create table if not exists Work (Id integer primary key, Type integer not null, Parent integer not null, Artist integer not null, Name text not null, Year integer not null, Number integer not null);
create unique index if not exists Work_unique on Work (Type, Parent, Artist, Name, Year, Number);
create table if not exists ArtistLink (Id integer primary key, Artist integer not null, Name text not null, Relationship integer not null, Source integer not null, Target text not null);
create unique index if not exists ArtistLink_unique on ArtistLink (Artist, Name, Relationship, Source, Target);
create table if not exists ArtistTag (Id integer primary key, Artist integer not null, Name text not null, Category integer not null, Source integer not null, Target text not null);
create unique index if not exists ArtistTag_unique on ArtistTag (Artist, Name, Category, Source, Target);
create table if not exists WorkLink (Id integer primary key, Work integer not null, Name text not null, Relationship integer not null, Source integer not null, Target text not null);
create unique index if not exists WorkLink_unique on WorkLink (Work, Name, Relationship, Source, Target);
create table if not exists WorkTag (Id integer primary key, Work integer not null, Name text not null, Category integer not null, Source integer not null, Target text not null);
create unique index if not exists WorkTag_unique on WorkTag (Work, Name, Category, Source, Target);");

            command.ExecuteNonQuery();
        }

        private void LoadArtists(IDbConnection connection)
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
        }

        private void LoadArtistLinks(IDbConnection connection)
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

        private void LoadArtistTags(IDbConnection connection)
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
                    var target = reader.GetString(5);

                    if (artist == null)
                        continue;

                    var tag = new Tag(name, category, source, target);
                    cache.Add(id, artist, tag);
                    artist.AddTag(tag);
                }
            }
        }

        private void LoadWorks(IDbConnection connection)
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
        }

        private void LoadWorkLinks(IDbConnection connection)
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

        private void LoadWorkTags(IDbConnection connection)
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
                    var target = reader.GetString(5);

                    if (work == null)
                        continue;

                    var tag = new Tag(name, category, source, target);
                    cache.Add(id, work, tag);
                    work.AddTag(tag);
                }
            }
        }

        public IEnumerable<Artist> Artists
        {
            get { return cache.Artists; }
        }

        public IEnumerable<Work> Works
        {
            get { return cache.Works; }
        }

        public void Initialize()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                InitializeDatabase(connection);

                LoadArtists(connection);
                LoadArtistLinks(connection);
                LoadArtistTags(connection);

                LoadWorks(connection);
                LoadWorkLinks(connection);
                LoadWorkTags(connection);
            }
        }

        public void Save(IEnumerable<Entity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            
            using (var connection = GetConnection())
            {
                connection.Open();

                foreach (var entity in entities)
                {
                    if (entity is Artist)
                    {
                        SaveArtist(connection, entity as Artist);
                    }
                    else if (entity is Work)
                    {
                        SaveWork(connection, entity as Work);
                    }
                }
            }
        }

        public void Delete(IEnumerable<Entity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            using (var connection = GetConnection())
            {
                connection.Open();

                foreach (var entity in entities)
                {
                    if (entity is Artist)
                    {
                        DeleteArtist(connection, entity as Artist);
                    }
                    else if (entity is Work)
                    {
                        DeleteWork(connection, entity as Work);
                    }
                }
            }
        }
    }
}
