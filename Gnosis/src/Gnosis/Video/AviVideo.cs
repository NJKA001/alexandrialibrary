using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Metadata;

namespace Gnosis.Video
{
    public class AviVideo
        : VideoBase
    {
        public AviVideo(Uri location, IContentType mediaType)
            : base(location, mediaType)
        {
        }

        private Gnosis.Tags.TagLib.File file;
        private Gnosis.Tags.TagLib.Tag riffTag;

        public override void Load()
        {
            if (Location.IsFile)
            {
                file = Gnosis.Tags.TagLib.File.Create(Location.LocalPath);
                riffTag = file.GetTag(Gnosis.Tags.TagLib.TagTypes.RiffInfo);
            }
        }

        public override IEnumerable<ITag> GetTags()
        {
            var tags = new List<ITag>();

            if (riffTag != null)
            {
            }

            return tags;
        }

        public override IClip GetClip(ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaItemRepository mediaItemRepository, IArtist artist, IAlbum album)
        {
            var clip = mediaItemRepository.GetByTarget<IClip>(Location).FirstOrDefault();
            if (clip != null)
            {
                return clip;
            }

            var name = GetClipName();
            var summary = string.Empty;
            var number = GetClipNumber();
            var date = GetDate();
            var duration = file != null && file.Properties != null ? file.Properties.Duration : TimeSpan.FromMinutes(5);
            var height = file != null && file.Properties != null ? (uint)file.Properties.VideoHeight : 480;
            var width = file != null && file.Properties != null ? (uint)file.Properties.VideoWidth : 640;

            var builder = new MediaItemBuilder<IClip>(securityContext, contentTypeFactory)
                .Identity(name, summary, date, date, number)
                .Size(duration, height, width)
                .Target(Location, Type);

            return builder.ToMediaItem();
        }
    }
}
