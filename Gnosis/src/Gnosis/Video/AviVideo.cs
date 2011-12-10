using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
