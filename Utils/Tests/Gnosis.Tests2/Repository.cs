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
        private readonly ObservableCollection<Artist> artists = new ObservableCollection<Artist>();
        private readonly ObservableCollection<Work> works = new ObservableCollection<Work>();
        private readonly IDictionary<uint, Artist> artistsById = new Dictionary<uint, Artist>();
        private readonly IDictionary<uint, Work> worksById = new Dictionary<uint, Work>();

        private IDbConnection GetConnection()
        {
            return new SQLiteConnection("Data Source=alexandria.db;");
        }

        private uint GetId(Artist artist)
        {
            if (artist == null)
                return 0;

            return artistsById.Where(x => x.Value == artist).FirstOrDefault().Key;
        }

        private uint GetId(Work work)
        {
            if (work == null)
                return 0;

            return worksById.Where(x => x.Value == work).FirstOrDefault().Key;
        }

        private IDbCommand GetDeleteCommand(IDbConnection connection, Artist artist)
        {
            return null;
        }

        private IDbCommand GetDeleteCommand(IDbConnection connection, Work work)
        {
            return null;
        }
        
        private IDbCommand GetSaveCommand(IDbConnection connection, Artist artist)
        {
            return null;
        }

        private IDbCommand GetSaveCommand(IDbConnection connection, Work work)
        {
            return null;
        }

        private void SaveArtist(IDbConnection connection, Artist artist)
        {
        }

        private void SaveWork(IDbConnection connection, Work work)
        {
            var id = GetId(work);
            
            var command = connection.CreateCommand();
            command.Parameters.Add(new SQLiteParameter("@Type", (ushort)work.Type));
            command.Parameters.Add(new SQLiteParameter("@Parent", GetId(work.Parent)));

            if (id > 0)
            {
                command.CommandText = "update Work set Type = @Type, Parent = @Parent, Artist = @Artist, Name = @Name, Year = @Year, Number = @Number where Id = @Id;";
                command.Parameters.Add(new SQLiteParameter("@Id", id));
            }
            else
            {
                command.CommandText = "insert into Work (Type, Parent, Artist, Name, Year, Number) values (@Type, @Parent, @Artist, @Name, @Year, @Number); select last_insert_rowid();";
            }

            foreach (var child in work.Children)
            {
                SaveWork(connection, work);
            }
        }

        public IEnumerable<Artist> Artists
        {
            get { return artists; }
        }

        public IEnumerable<Work> Works
        {
            get { return works; }
        }

        public void Initialize()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = 
@"create table if not exists Artist (Id integer primary key, Type integer not null, Name text not null, Year integer not null);
create unique index if not exists Artist_unique on Artist (Name, Year);
create table if not exists Work (Id integer primary key, Type integer not null, Parent integer not null, Artist integer not null, Name text not null, Year integer not null, Number integer not null);
create unique index if not exists Work_unique on Work (Type, Parent, Artist, Name, Year, Number);";

                command.ExecuteNonQuery();
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
            throw new NotImplementedException();
        }
    }
}
