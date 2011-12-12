using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public interface IMediaItemController
    {
        void UpdateThumbnail<T>(Uri id, Uri thumbnail, byte[] thumbnailData) where T : IMediaItem;
        void UpdateSummary<T>(Uri id, string summary) where T : IMediaItem;

        IEnumerable<ITrack> GetTracks(Uri album);
        IAlbum GetAlbum(Uri album);
    }
}
