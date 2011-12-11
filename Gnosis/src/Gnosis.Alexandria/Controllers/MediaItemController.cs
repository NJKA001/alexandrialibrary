using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis.Alexandria.Controllers
{
    public class MediaItemController
        : IMediaItemController
    {
        public MediaItemController(ILogger logger, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<ITrack> trackRepository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (artistRepository == null)
                throw new ArgumentNullException("artistRepository");
            if (albumRepository == null)
                throw new ArgumentNullException("albumRepository");
            if (trackRepository == null)
                throw new ArgumentNullException("trackRepository");

            this.logger = logger;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;
        }

        private readonly ILogger logger;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;

        public void SetArtistThumbnail(Uri artist, Uri thumbnail, byte[] thumbnailData)
        {
            if (artist == null)
                throw new ArgumentNullException("artist");
            if (thumbnail == null)
                throw new ArgumentNullException("thumbnail");
            if (thumbnailData == null)
                throw new ArgumentNullException("thumbnailData");

            try
            {
                var item = artistRepository.GetByLocation(artist);
                if (item == null)
                    return;

                var updated = new GnosisArtist(item.Name, item.FromDate, item.ToDate, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, item.User, item.UserName, thumbnail, thumbnailData, item.Location);
                artistRepository.Save(new List<IArtist> { updated });
            }
            catch (Exception ex)
            {
                logger.Error("  SetArtistThumbnail", ex);
                throw;
            }
        }

        public void SetAlbumThumbnail(Uri album, Uri thumbnail, byte[] thumbnailData)
        {
            if (album == null)
                throw new ArgumentNullException("album");
            if (thumbnail == null)
                throw new ArgumentNullException("thumbnail");
            if (thumbnailData == null)
                throw new ArgumentNullException("thumbnailData");

            try
            {
                var item = albumRepository.GetByLocation(album);
                if (item == null)
                    return;

                var updated = new GnosisAlbum(item.Name, item.FromDate, item.Number, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, item.User, item.UserName, thumbnail, thumbnailData, item.Location);
                albumRepository.Save(new List<IAlbum> { updated });
            }
            catch (Exception ex)
            {
                logger.Error("  SetArtistThumbnail", ex);
                throw;
            }
        }

        public void SetTrackThumbnail(Uri track, Uri thumbnail, byte[] thumbnailData)
        {
            if (track == null)
                throw new ArgumentNullException("track");
            if (thumbnail == null)
                throw new ArgumentNullException("thumbnail");
            if (thumbnailData == null)
                throw new ArgumentNullException("thumbnailData");

            try
            {
                var item = trackRepository.GetByLocation(track);
                if (item == null)
                    return;

                var updated = new GnosisTrack(item.Name, item.FromDate, item.ToDate, item.Number, item.Duration, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, item.User, item.UserName, thumbnail, thumbnailData, item.Location);
                trackRepository.Save(new List<ITrack> { updated });
            }
            catch (Exception ex)
            {
                logger.Error("  SetTrackThumbnail", ex);
                throw;
            }
        }

        public IAlbum GetAlbum(Uri album)
        {
            return albumRepository.GetByLocation(album);
        }

        public IEnumerable<ITrack> GetTracks(Uri album)
        {
            return trackRepository.GetByCatalog(album);
        }
    }
}
