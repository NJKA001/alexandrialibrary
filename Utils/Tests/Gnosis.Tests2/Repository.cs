using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class Repository
        : IRepository
    {
        public Repository(ICache<Artist> artistCache, IEntityStore<Artist> artistStore, ICache<Work> workCache, IEntityStore<Work> workStore)
        {
            if (artistCache == null)
                throw new ArgumentNullException("artistCache");
            if (artistStore == null)
                throw new ArgumentNullException("artistStore");
            if (workCache == null)
                throw new ArgumentNullException("workCache");
            if (workStore == null)
                throw new ArgumentNullException("workStore");

            this.artistCache = artistCache;
            this.artistStore = artistStore;
            this.workCache = workCache;
            this.workStore = workStore;
        }

        private readonly ICache<Artist> artistCache;
        private readonly IEntityStore<Artist> artistStore;
        private readonly ICache<Work> workCache;
        private readonly IEntityStore<Work> workStore;

        private void InitializeArtists()
        {
            Action<uint, Artist> entityLoaded = (id, entity) => artistCache.Add(id, entity);
            
            Action<uint, Link, uint> linkLoaded = (id, link, entityId) => 
                {
                    var entity = artistCache.GetEntity(entityId);
                    if (entity != null)
                        artistCache.Add(id, entity, link);
                };
            
            Action<uint, Tag, uint> tagLoaded = (id, tag, entityId) =>
                {
                    var entity = artistCache.GetEntity(entityId);
                    if (entity != null)
                        artistCache.Add(id, entity, tag);
                };

            artistStore.Initialize(entityLoaded, linkLoaded, tagLoaded);
        }

        private void InitializeWorks()
        {
            Action<uint, Work> entityLoaded = (id, entity) => workCache.Add(id, entity);

            Action<uint, Link, uint> linkLoaded = (id, link, entityId) =>
            {
                var entity = workCache.GetEntity(entityId);
                if (entity != null)
                    workCache.Add(id, entity, link);
            };

            Action<uint, Tag, uint> tagLoaded = (id, tag, entityId) =>
            {
                var entity = workCache.GetEntity(entityId);
                if (entity != null)
                    workCache.Add(id, entity, tag);
            };

            workStore.Initialize(entityLoaded, linkLoaded, tagLoaded);
        }

        private void DeleteArtists(IEnumerable<Artist> artists)
        {
            var batch = artistStore.CreateBatch(artistCache);
            try
            {
                batch.Start();

                foreach (var artist in artists)
                    batch.Delete(artist);

                batch.Finish();
            }
            catch (Exception)
            {
                batch.Cancel();

                throw;
            }
            finally
            {
                batch.Close();
            }
        }

        private void DeleteWorks(IEnumerable<Work> works)
        {
            var batch = workStore.CreateBatch(workCache);
            try
            {
                batch.Start();

                foreach (var work in works)
                    batch.Delete(work);

                batch.Finish();
            }
            catch (Exception)
            {
                batch.Cancel();

                throw;
            }
            finally
            {
                batch.Close();
            }
        }

        private void SaveArtists(IEnumerable<Artist> artists)
        {
            var batch = artistStore.CreateBatch(artistCache);
            try
            {
                batch.Start();

                foreach (var artist in artists)
                    batch.Save(artist);

                batch.Finish();
            }
            catch (Exception)
            {
                batch.Cancel();

                throw;
            }
            finally
            {
                batch.Close();
            }
        }

        private void SaveWorks(IEnumerable<Work> works)
        {
            var batch = workStore.CreateBatch(workCache);
            try
            {
                batch.Start();

                foreach (var work in works)
                    SaveWork(batch, work);

                batch.Finish();
            }
            catch (Exception)
            {
                batch.Cancel();

                throw;
            }
            finally
            {
                batch.Close();
            }
        }

        private void SaveWork(IBatch<Work> batch, Work work)
        {
            batch.Save(work);

            foreach (var child in work.Children)
            {
                SaveWork(batch, child);
            }
        }

        public IEnumerable<Artist> Artists
        {
            get { return artistCache.Entities; }
        }

        public IEnumerable<Work> Works
        {
            get { return workCache.Entities; }
        }

        public void Initialize()
        {
            InitializeArtists();
            InitializeWorks();
        }

        public void Save(IEnumerable<Entity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            var artists = entities.OfType<Artist>();
            if (artists.Count() > 0)
                SaveArtists(artists);

            var works = entities.OfType<Work>();
            if (works.Count() > 0)
                SaveWorks(works);
        }

        public void Delete(IEnumerable<Entity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            var artists = entities.OfType<Artist>();
            if (artists.Count() > 0)
                DeleteArtists(artists);

            var works = entities.OfType<Work>();
            if (works.Count() > 0)
                DeleteWorks(works);
        }
    }
}
