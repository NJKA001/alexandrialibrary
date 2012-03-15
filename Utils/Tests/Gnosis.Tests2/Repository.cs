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
            var id = cache.GetId(artist);
            if (id == 0)
                return;

            var command = connection.CreateCommand();
            command.CommandText = "delete from Artist where Id = @Id";
            AddParameter(command, "@Id", id);
            command.ExecuteNonQuery();

            cache.Remove(artist);
        }

        private void DeleteWork(IDbConnection connection, Work work)
        {
            if (work == null)
                return;

            var id = cache.GetId(work);
            if (id == 0)
                return;

            var command = connection.CreateCommand();
            command.CommandText = "delete from Work where Id = @Id";
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

            var id = cache.GetId(artist);

            var command = connection.CreateCommand();
            AddParameter(command, "@Type", (ushort)artist.Type);
            AddParameter(command, "@Name", artist.Name);
            AddParameter(command, "@Year", artist.Year);

            if (id > 0)
            {
                AddParameter(command, "@Id", id);
                command.CommandText = "update Artist set Type = @Type, Name = @Name, Year = @Year where Id = @Id;";
                command.ExecuteNonQuery();
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

            foreach (var work in artist.Works)
            {
                SaveWork(connection, work);
            }
        }

        private void SaveWork(IDbConnection connection, Work work)
        {
            if (work == null)
                return;

            var id = cache.GetId(work);
            
            var command = connection.CreateCommand();
            AddParameter(command, "@Type", (ushort)work.Type);
            AddParameter(command, "@Parent", cache.GetId(work.Parent));
            AddParameter(command, "@Artist", cache.GetId(work.Artist));
            AddParameter(command, "@Name", work.Name);
            AddParameter(command, "@Year", work.Year);
            AddParameter(command, "@Number", work.Number);

            if (id > 0)
            {
                AddParameter(command, "@Id", id);
                command.CommandText = "update Work set Type = @Type, Parent = @Parent, Artist = @Artist, Name = @Name, Year = @Year, Number = @Number where Id = @Id;";
                command.ExecuteNonQuery();
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

            foreach (var child in work.Children)
            {
                SaveWork(connection, child);
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

                var command = GetCommand(connection, 
@"create table if not exists Artist (Id integer primary key, Type integer not null, Name text not null, Year integer not null);
create unique index if not exists Artist_unique on Artist (Name, Year);
create table if not exists Work (Id integer primary key, Type integer not null, Parent integer not null, Artist integer not null, Name text not null, Year integer not null, Number integer not null);
create unique index if not exists Work_unique on Work (Type, Parent, Artist, Name, Year, Number);");

                command.ExecuteNonQuery();

                var artistCommand = GetCommand(connection, "select * from Artist;");
                using (var artistReader = artistCommand.ExecuteReader())
                {
                    while (artistReader.Read())
                    {
                        var id = (uint)artistReader.GetInt64(0);
                        var type = (ArtistType)artistReader.GetInt32(1);
                        var name = artistReader.GetString(2);
                        var year = artistReader.GetInt16(3);
                        cache.Add(id, new Artist() { Type = type, Name = name, Year = year });
                    }
                }

                var workCommand = GetCommand(connection, "select * from Work;");
                using (var workReader = workCommand.ExecuteReader())
                {
                    while (workReader.Read())
                    {
                        var id = (uint)workReader.GetInt64(0);
                        var type = (WorkType)workReader.GetInt32(1);
                        var parent = cache.GetWork((uint)workReader.GetInt64(2));
                        var artist = cache.GetArtist((uint)workReader.GetInt64(3));
                        var name = workReader.GetString(4);
                        var year = workReader.GetInt16(5);
                        var number = (uint)workReader.GetInt64(6);
                        cache.Add(id, new Work() { Type = type, Parent = parent, Artist = artist, Name = name, Year = year, Number = number });
                    }
                }
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
