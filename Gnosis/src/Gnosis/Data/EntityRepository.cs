using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public class EntityRepository
        : IEntityRepository
    {
        public EntityRepository(ILogger logger, IEntityCache<IArtist> artistCache, IEntityStore<IArtist> artistStore, IEntityCache<IWork> workCache, IEntityStore<IWork> workStore)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (artistCache == null)
                throw new ArgumentNullException("artistCache");
            if (artistStore == null)
                throw new ArgumentNullException("artistStore");
            if (workCache == null)
                throw new ArgumentNullException("workCache");
            if (workStore == null)
                throw new ArgumentNullException("workStore");

            this.logger = logger;
            this.artistCache = artistCache;
            this.artistStore = artistStore;
            this.workCache = workCache;
            this.workStore = workStore;
        }

        private readonly ILogger logger;
        private readonly IEntityCache<IArtist> artistCache;
        private readonly IEntityStore<IArtist> artistStore;
        private readonly IEntityCache<IWork> workCache;
        private readonly IEntityStore<IWork> workStore;

        private void InitializeArtists()
        {
            Action<uint, IArtist> entityLoaded = (id, entity) => artistCache.Add(id, entity);

            Action<uint, ILink, uint> linkLoaded = (id, link, entityId) =>
            {
                var entity = artistCache.GetEntity(entityId);
                if (entity != null)
                {
                    entity.AddLink(link);
                    artistCache.Add(id, entity, link);
                }
            };

            Action<uint, ITag, uint> tagLoaded = (id, tag, entityId) =>
            {
                var entity = artistCache.GetEntity(entityId);
                if (entity != null)
                {
                    entity.AddTag(tag);
                    artistCache.Add(id, entity, tag);
                }
            };

            artistStore.Initialize(entityLoaded, linkLoaded, tagLoaded);
        }

        private void InitializeWorks()
        {
            Action<uint, IWork> entityLoaded = (id, entity) => workCache.Add(id, entity);

            Action<uint, ILink, uint> linkLoaded = (id, link, entityId) =>
            {
                var entity = workCache.GetEntity(entityId);
                if (entity != null)
                {
                    entity.AddLink(link);
                    workCache.Add(id, entity, link);
                }
            };

            Action<uint, ITag, uint> tagLoaded = (id, tag, entityId) =>
            {
                var entity = workCache.GetEntity(entityId);
                if (entity != null)
                {
                    entity.AddTag(tag);
                    workCache.Add(id, entity, tag);
                }
            };

            workStore.Initialize(entityLoaded, linkLoaded, tagLoaded);
        }

        private void DeleteArtists(IEnumerable<IArtist> artists)
        {
            var batch = artistStore.CreateBatch(artistCache);
            try
            {
                batch.Start();

                foreach (var artist in artists)
                    batch.Delete(artist);

                batch.Finish();
            }
            catch (Exception ex)
            {
                batch.Cancel();

                logger.Error("  EntityRepository.DeleteArtists", ex);

                throw;
            }
            finally
            {
                batch.Close();
            }
        }

        private void DeleteWorks(IEnumerable<IWork> works)
        {
            var batch = workStore.CreateBatch(workCache);
            try
            {
                batch.Start();

                foreach (var work in works)
                    batch.Delete(work);

                batch.Finish();
            }
            catch (Exception ex)
            {
                batch.Cancel();

                logger.Error("  EntityRepository.DeleteWorks", ex);

                throw;
            }
            finally
            {
                batch.Close();
            }
        }

        private void SaveArtists(IEnumerable<IArtist> artists)
        {
            var batch = artistStore.CreateBatch(artistCache);
            try
            {
                batch.Start();

                foreach (var artist in artists)
                    batch.Save(artist);

                batch.Finish();
            }
            catch (Exception ex)
            {
                batch.Cancel();

                logger.Error("  EntityRepository.SaveArtists", ex);

                throw;
            }
            finally
            {
                batch.Close();
            }
        }

        private void SaveWorks(IEnumerable<IWork> works)
        {
            var batch = workStore.CreateBatch(workCache);
            try
            {
                batch.Start();

                foreach (var work in works)
                    SaveWork(batch, work);

                batch.Finish();
            }
            catch (Exception ex)
            {
                batch.Cancel();

                logger.Error("  EntityRepository.SaveWorks", ex);

                throw;
            }
            finally
            {
                batch.Close();
            }
        }

        private void SaveWork(IBatch<IWork> batch, IWork work)
        {
            batch.Save(work);

            foreach (var child in work.Children)
            {
                SaveWork(batch, child);
            }
        }

        public IEnumerable<IArtist> Artists
        {
            get { return artistCache.Entities; }
        }

        public IEnumerable<IWork> Works
        {
            get { return workCache.Entities; }
        }

        public void Initialize()
        {
            InitializeArtists();
            InitializeWorks();
        }

        public void Save(IEnumerable<IEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            var artists = entities.OfType<IArtist>();
            if (artists.Count() > 0)
                SaveArtists(artists);

            var works = entities.OfType<IWork>();
            if (works.Count() > 0)
                SaveWorks(works);
        }

        public void Delete(IEnumerable<IEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            var artists = entities.OfType<IArtist>();
            if (artists.Count() > 0)
                DeleteArtists(artists);

            var works = entities.OfType<IWork>();
            if (works.Count() > 0)
                DeleteWorks(works);
        }
    }
}
