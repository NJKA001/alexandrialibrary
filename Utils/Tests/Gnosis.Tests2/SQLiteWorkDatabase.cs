using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class SQLiteWorkDatabase
        : SQLiteDatabase<Work>
    {
        public SQLiteWorkDatabase(ICache<Artist> artistCache, ICache<Work> workCache)
            : base("alexandria", "Work")
        {
            if (artistCache == null)
                throw new ArgumentNullException("artistCache");
            if (workCache == null)
                throw new ArgumentNullException("workCache");

            this.artistCache = artistCache;
            this.workCache = workCache;
        }

        private readonly ICache<Artist> artistCache;
        private readonly ICache<Work> workCache;

        protected override void LoadEntity(IDataRecord record, Action<uint, Work> entityLoaded)
        {
            var id = (uint)record.GetInt64(0);
            var type = (WorkType)record.GetInt32(1);
            var parentId = (uint)record.GetInt64(2);
            var artist = artistCache.GetEntity((uint)record.GetInt64(3));
            var name = record.GetString(4);
            var year = record.GetInt16(5);
            var number = (uint)record.GetInt64(6);

            AddPostLoadAction(() =>
            {
                var work = workCache.GetEntity(id);
                if (work == null)
                    return;

                work.Parent = workCache.GetEntity(parentId);
            });

            entityLoaded(id, new Work(type, null, artist, name, year, number));
        }

        protected override string GetInitEntityCommandText()
        {
            return
@"create table if not exists Work (Id integer primary key, Type integer not null, Parent integer not null, Artist integer not null, Name text not null, Year integer not null, Number integer not null);
create unique index if not exists Work_unique on Work (Type, Parent, Artist, Name, Year, Number);";
        }

        protected override IStep GetInsertEntityStep(Work entity)
        {
            var step = new Step("insert into Work (Type, Parent, Artist, Name, Year, Number) values (@Type, @Parent, @Artist, @Name, @Year, @Number); select last_insert_rowid();");
            step.AddItem("@Type", (ushort)entity.Type);
            step.AddItem("@Parent", workCache.GetId(entity.Parent));
            step.AddItem("@Artist", artistCache.GetId(entity.Artist));
            step.AddItem("@Name", entity.Name);
            step.AddItem("@Year", entity.Year);
            step.AddItem("@Number", entity.Number);

            return step;
        }

        protected override IStep GetUpdateEntityStep(uint id, Work entity)
        {
            var step = new Step("update Work set Type = @Type, Parent = @Parent, Artist = @Artist, Name = @Name, Year = @Year, Number = @Number where Id = @Id;");
            step.AddItem("@Type", (ushort)entity.Type);
            step.AddItem("@Parent", workCache.GetId(entity.Parent));
            step.AddItem("@Artist", artistCache.GetId(entity.Artist));
            step.AddItem("@Name", entity.Name);
            step.AddItem("@Year", entity.Year);
            step.AddItem("@Number", entity.Number);
            step.AddItem("@Id", id);

            return step;
        }
    }
}
