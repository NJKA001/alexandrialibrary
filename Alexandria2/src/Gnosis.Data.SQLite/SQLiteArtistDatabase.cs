using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.SQLite
{
    public class SQLiteArtistDatabase
    : SQLiteEntityDatabase<IArtist>
    {
        public SQLiteArtistDatabase()
            : base("alexandria", "Artist")
        {
        }

        protected override void LoadEntity(IDataRecord record, Action<uint, IArtist> entityLoaded)
        {
            var id = (uint)record.GetInt64(0);
            var type = (ArtistType)record.GetInt32(1);
            var name = record.GetString(2);
            var year = record.GetInt16(3);

            entityLoaded(id, new Artist(type, name, year));
        }

        protected override string GetInitEntityCommandText()
        {
            return
@"create table if not exists Artist (Id integer primary key, Type integer not null, Name text not null, Year integer not null);
create unique index if not exists Artist_unique on Artist (Name, Year);";
        }

        protected override IStep GetInsertEntityStep(IArtist entity)
        {
            var step = new Step("insert into Artist (Type, Name, Year) values (@Type, @Name, @Year); select last_insert_rowid();");
            step.AddItem("@Type", (ushort)entity.Type);
            step.AddItem("@Name", entity.Name);
            step.AddItem("@Year", entity.Year);

            return step;
        }

        protected override IStep GetUpdateEntityStep(uint id, IArtist entity)
        {
            var step = new Step("update Artist set Type = @Type, Name = @Name, Year = @Year where Id = @Id;");
            step.AddItem("@Type", (ushort)entity.Type);
            step.AddItem("@Name", entity.Name);
            step.AddItem("@Year", entity.Year);
            step.AddItem("@Id", id);

            return step;
        }
    }
}
