using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Controllers
{
    public interface IMetadataController
    {
        void UpdateThumbnail<T>(Uri id, Uri thumbnail, byte[] thumbnailData) where T : class, IMetadata;
        void UpdateSummary<T>(Uri id, string summary) where T : class, IMetadata;

        IEnumerable<ILink> GetLinksBySource(Uri source);
        IEnumerable<ILink> GetLinksByTarget(Uri target);
        IEnumerable<ITag> GetTags(Uri target);
        IEnumerable<ITrack> GetTracks(Uri album);
        IAlbum GetAlbum(Uri album);

        void SaveLinks(IEnumerable<ILink> links);
        void SaveTags(IEnumerable<ITag> tags);
    }
}
