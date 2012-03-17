using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Tests2
{
    public class GlobalBatch
    {
        public GlobalBatch(GlobalCache cache, GlobalDatabase database)
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

        private IDbConnection connection;

        private void SaveArtistLinks(Artist artist, uint id)
        {
            foreach (var existingLink in cache.GetLinksByArtist(id))
            {
                var existingId = cache.GetArtistLinkId(existingLink);
                if (existingId > 0 && !artist.Links.Contains(existingLink))
                {
                    database.DeleteArtistLink(connection, existingId);
                    cache.Remove(artist, existingLink);
                }
            }

            foreach (var link in artist.Links)
            {
                SaveArtistLink(artist, link);
            }
        }

        private void SaveArtistTags(Artist artist, uint id)
        {
            foreach (var existingTag in cache.GetTagsByArtist(id))
            {
                var existingId = cache.GetArtistTagId(existingTag);
                if (existingId > 0 && !artist.Tags.Contains(existingTag))
                {
                    database.DeleteArtistTag(connection, existingId);
                    cache.Remove(artist, existingTag);
                }
            }

            foreach (var tag in artist.Tags)
            {
                SaveArtistTag(artist, tag);
            }
        }

        private void SaveArtistLink(Artist artist, Link link)
        {
            if (artist == null || link == null)
                return;

            var artistId = cache.GetArtistId(artist);
            if (artistId == 0)
                return;

            var id = cache.GetArtistLinkId(link);

            if (id > 0)
            {
                database.UpdateArtistLink(connection, link, id, artistId);
            }
            else
            {
                id = database.InsertArtistLink(connection, link, artistId);
                cache.Add(id, artist, link);
            }
        }

        private void SaveArtistTag(Artist artist, Tag tag)
        {
            if (artist == null || tag == null)
                return;

            var artistId = cache.GetArtistId(artist);
            if (artistId == 0)
                return;

            var id = cache.GetArtistTagId(tag);

            if (id > 0)
            {
                database.UpdateArtistTag(connection, tag, id, artistId);
            }
            else
            {
                id = database.InsertArtistTag(connection, tag, artistId);
                cache.Add(id, artist, tag);
            }
        }

        private void SaveWorkLinks(Work work, uint id)
        {
            foreach (var existingLink in cache.GetLinksByWork(id))
            {
                var existingId = cache.GetWorkLinkId(existingLink);
                if (existingId > 0 && !work.Links.Contains(existingLink))
                {
                    database.DeleteWorkLink(connection, existingId);
                    cache.Remove(work, existingLink);
                }
            }

            foreach (var link in work.Links)
            {
                SaveWorkLink(work, link);
            }
        }

        private void SaveWorkTags(Work work, uint id)
        {
            foreach (var existingTag in cache.GetTagsByWork(id))
            {
                var existingId = cache.GetArtistTagId(existingTag);
                if (existingId > 0 && !work.Tags.Contains(existingTag))
                {
                    database.DeleteWorkTag(connection, existingId);
                    cache.Remove(work, existingTag);
                }
            }

            foreach (var tag in work.Tags)
            {
                SaveWorkTag(work, tag);
            }
        }

        private void SaveWorkLink(Work work, Link link)
        {
            if (work == null || link == null)
                return;

            var workId = cache.GetWorkId(work);
            if (workId == 0)
                return;

            var id = cache.GetWorkLinkId(link);

            if (id > 0)
            {
                database.UpdateWorkLink(connection, link, id, workId);
            }
            else
            {
                id = database.InsertWorkLink(connection, link, workId);
                cache.Add(id, work, link);
            }
        }

        private void SaveWorkTag(Work work, Tag tag)
        {
            if (work == null || tag == null)
                return;

            var workId = cache.GetWorkId(work);
            if (workId == 0)
                return;

            var id = cache.GetWorkTagId(tag);

            if (id > 0)
            {
                database.UpdateWorkTag(connection, tag, id, workId);
            }
            else
            {
                id = database.InsertWorkTag(connection, tag, workId);
                cache.Add(id, work, tag);
            }
        }

        public void Start()
        {
            if (connection != null)
                throw new InvalidOperationException("UnitOfWork has already been started");

            connection = database.GetConnection();
            connection.Open();
        }

        public void DeleteArtist(Artist artist)
        {
            if (artist == null)
                return;

            var id = cache.GetArtistId(artist);
            if (id == 0)
                return;

            database.DeleteArtist(connection, id);

            cache.Remove(artist);
        }

        public void DeleteWork(Work work)
        {
            if (work == null)
                return;

            var id = cache.GetWorkId(work);
            if (id == 0)
                return;

            database.DeleteWork(connection, id);

            cache.Remove(work);
        }

        public void SaveArtist(Artist artist)
        {
            if (artist == null)
                return;

            var id = cache.GetArtistId(artist);

            if (id > 0)
            {
                database.UpdateArtist(connection, artist, id);
            }
            else
            {
                id = database.InsertArtist(connection, artist);
                cache.Add(id, artist);
            }

            SaveArtistLinks(artist, id);

            SaveArtistTags(artist, id);

            foreach (var work in artist.Works)
            {
                SaveWork(work);
            }
        }

        public void SaveWork(Work work)
        {
            if (work == null)
                return;

            var id = cache.GetWorkId(work);
            var parentId = cache.GetWorkId(work.Parent);
            var artistId = cache.GetArtistId(work.Artist);

            if (id > 0)
            {
                database.UpdateWork(connection, work, id, parentId, artistId);
            }
            else
            {
                id = database.InsertWork(connection, work, parentId, artistId);
                cache.Add(id, work);
            }

            SaveWorkLinks(work, id);

            SaveWorkTags(work, id);

            foreach (var child in work.Children)
            {
                SaveWork(child);
            }
        }

        public void Stop()
        {
        }

        public void Close()
        {
            if (connection == null)
                throw new InvalidOperationException("UnitOfWork is not in progress");

            connection.Close();
        }
    }
}
