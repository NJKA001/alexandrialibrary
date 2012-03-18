using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class GlobalRepository
        : IRepository
    {
        public GlobalRepository(GlobalCache cache, GlobalDatabase database)
        {
            if (cache == null)
                throw new ArgumentNullException("cache");
            if (database == null)
                throw new ArgumentNullException("database");

            this.cache = cache;
            this.database = database;
        }

        private readonly GlobalCache cache;
        private readonly GlobalDatabase database;

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
        }

        public void Save(IEnumerable<Entity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            
            if (entities.Count() == 0)
                return;

            var batch = new GlobalBatch(cache, database);

            try
            {
                batch.Start();

                foreach (var entity in entities)
                {
                    if (entity is Artist)
                        batch.SaveArtist(entity as Artist);
                    else if (entity is Work)
                        batch.SaveWork(entity as Work);
                }
                batch.Stop();
            }
            finally
            {
                batch.Close();
            }
        }

        public void Delete(IEnumerable<Entity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            if (entities.Count() == 0)
                return;

            var batch = new GlobalBatch(cache, database);
            try
            {
                batch.Start();

                foreach (var entity in entities)
                {
                    if (entity is Artist)
                        batch.DeleteArtist(entity as Artist);
                    else if (entity is Work)
                        batch.DeleteWork(entity as Work);
                }

                batch.Stop();
            }
            finally
            {
                batch.Close();
            }
        }
    }
}
