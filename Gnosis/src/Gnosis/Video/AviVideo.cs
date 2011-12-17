using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;

namespace Gnosis.Video
{
    public class AviVideo
        : VideoBase
    {
        public AviVideo(Uri location)
            : base(location, MediaType.VideoAvi)
        {
        }

        private TagLib.File file;
        private TagLib.Tag riffTag;

        public override void Load()
        {
            if (Location.IsFile)
            {
                file = TagLib.File.Create(Location.LocalPath);
                riffTag = file.GetTag(TagLib.TagTypes.RiffInfo);
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

        public override IClip GetClip(ISecurityContext securityContext, IMediaItemRepository<IClip> clipRepository, IArtist artist, IAlbum album)
        {
            var clip = clipRepository.GetByTarget(Location).FirstOrDefault();
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
            var user = securityContext.CurrentUser;

            return new GnosisClip(name, summary, date, number, duration, height, width, artist.Location, artist.Name, album.Location, album.Name, Location, Type, user.Location, user.Name, Guid.Empty.ToUrn(), new byte[0]);
        }
    }
}
