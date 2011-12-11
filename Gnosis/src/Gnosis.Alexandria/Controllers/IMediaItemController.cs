using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public interface IMediaItemController
    {
        void SetArtistThumbnail(Uri artist, Uri thumbnail, byte[] thumbnailData);
        void SetAlbumThumbnail(Uri album, Uri thumbnail, byte[] thumbnailData);
        void SetTrackThumbnail(Uri track, Uri thumbnail, byte[] thumbnailData);

        IEnumerable<ITrack> GetTracks(Uri album);
        IAlbum GetAlbum(Uri album);
    }
}
