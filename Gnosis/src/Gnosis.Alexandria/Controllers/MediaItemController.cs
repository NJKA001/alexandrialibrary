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
        public MediaItemController(ILogger logger, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<ITrack> trackRepository, IMediaItemRepository<IClip> clipRepository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (artistRepository == null)
                throw new ArgumentNullException("artistRepository");
            if (albumRepository == null)
                throw new ArgumentNullException("albumRepository");
            if (trackRepository == null)
                throw new ArgumentNullException("trackRepository");
            if (clipRepository == null)
                throw new ArgumentNullException("clipRepository");

            this.logger = logger;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;
            this.clipRepository = clipRepository;
        }

        private readonly ILogger logger;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;
        private readonly IMediaItemRepository<IClip> clipRepository;

        public void UpdateThumbnail<T>(Uri id, Uri thumbnail, byte[] thumbnailData)
            where T : IMediaItem
        {
            if (id == null)
                throw new ArgumentNullException("id");
            if (thumbnail == null)
                throw new ArgumentNullException("thumbnail");
            if (thumbnailData == null)
                throw new ArgumentNullException("thumbnailData");

            try
            {
                if (typeof(T) == typeof(IArtist) || typeof(T) == typeof(GnosisArtist))
                {
                    var item = artistRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var updated = new GnosisArtist(item.Name, item.Summary, item.FromDate, item.ToDate, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, item.User, item.UserName, thumbnail, thumbnailData, item.Location);
                    artistRepository.Save(new List<IArtist> { updated });
                }
                else if (typeof(T) == typeof(IAlbum) || typeof(T) == typeof(GnosisAlbum))
                {
                    var item = albumRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var updated = new GnosisAlbum(item.Name, item.Summary, item.FromDate, item.Number, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, item.User, item.UserName, thumbnail, thumbnailData, item.Location);
                    albumRepository.Save(new List<IAlbum> { updated });
                }
                else if (typeof(T) == typeof(ITrack) || typeof(T) == typeof(GnosisTrack))
                {
                    var item = trackRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var updated = new GnosisTrack(item.Name, item.Summary, item.FromDate, item.ToDate, item.Number, item.Duration, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, item.User, item.UserName, thumbnail, thumbnailData, item.Location);
                    trackRepository.Save(new List<ITrack> { updated });
                }
                else if (typeof(T) == typeof(IClip) || typeof(T) == typeof(GnosisClip))
                {
                    var item = clipRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var updated = new GnosisClip(item.Name, item.Summary, item.FromDate, item.Number, item.Duration, item.Height, item.Width, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, item.User, item.UserName, thumbnail, thumbnailData, item.Location);
                    clipRepository.Save(new List<IClip> { updated });
                }
            }
            catch (Exception ex)
            {
                logger.Error("  UpdateTrackThumbnail", ex);
                throw;
            }
        }

        public void UpdateSummary<T>(Uri id, string summary)
            where T : IMediaItem
        {
            if (id == null)
                throw new ArgumentNullException("id");
            if (summary == null)
                throw new ArgumentNullException("summary");

            try
            {
                if (typeof(T) == typeof(IArtist) || typeof(T) == typeof(GnosisArtist))
                {
                    var item = artistRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var updated = new GnosisArtist(item.Name, summary, item.FromDate, item.ToDate, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, item.User, item.UserName, item.Thumbnail, item.ThumbnailData, item.Location);
                    artistRepository.Save(new List<IArtist> { updated });
                }
                else if (typeof(T) == typeof(IAlbum) || typeof(T) == typeof(GnosisAlbum))
                {
                    var item = albumRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var updated = new GnosisAlbum(item.Name, summary, item.FromDate, item.Number, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, item.User, item.UserName, item.Thumbnail, item.ThumbnailData, item.Location);
                    albumRepository.Save(new List<IAlbum> { updated });
                }
                else if (typeof(T) == typeof(ITrack) || typeof(T) == typeof(GnosisTrack))
                {
                    var item = trackRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var updated = new GnosisTrack(item.Name, summary, item.FromDate, item.ToDate, item.Number, item.Duration, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, item.User, item.UserName, item.Thumbnail, item.ThumbnailData, item.Location);
                    trackRepository.Save(new List<ITrack> { updated });
                }
                else if (typeof(T) == typeof(IClip) || typeof(T) == typeof(GnosisClip))
                {
                    var item = clipRepository.GetByLocation(id);
                    if (item == null)
                        return;

                    var updated = new GnosisClip(item.Name, summary, item.FromDate, item.Number, item.Duration, item.Height, item.Width, item.Creator, item.CreatorName, item.Catalog, item.CatalogName, item.Target, item.TargetType, item.User, item.UserName, item.Thumbnail, item.ThumbnailData, item.Location);
                    clipRepository.Save(new List<IClip> { updated });
                }
            }
            catch (Exception ex)
            {
                logger.Error("  MediaItemController.UpdateSummary", ex);
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
