using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Tags.Id3;
using Gnosis.Core.Tags.Id3.Id3v1;
using Gnosis.Core.Tags.Id3.Id3v2;

using TagLib;

namespace Gnosis.Core.Audio
{
    public class MpegAudio
        : AudioBase, IMpegAudio
    {
        private TagLib.File file;
        private TagLib.Id3v1.Tag id3v1Tag;
        private TagLib.Id3v2.Tag id3v2Tag;

        public MpegAudio(Uri location)
            : base(location, MediaType.AudioMpeg)
        {
        }

        public void Load()
        {
            if (Location.IsFile)
            {
                file = TagLib.File.Create(Location.LocalPath);
                id3v1Tag = file.GetTag(TagTypes.Id3v1) as TagLib.Id3v1.Tag;
                id3v2Tag = file.GetTag(TagTypes.Id3v2) as TagLib.Id3v2.Tag;

                if (id3v1Tag == null || id3v1Tag.IsEmpty)
                    System.Diagnostics.Debug.WriteLine("ID3v1 tag is null or empty");
                else
                {
                    if (id3v1Tag.Album != null || id3v1Tag.AlbumArtists != null
                        || id3v1Tag.Comment != null || id3v1Tag.Composers != null || id3v1Tag.Conductor != null
                    || id3v1Tag.Copyright != null || id3v1Tag.Genres != null || id3v1Tag.Grouping != null)
                        System.Diagnostics.Debug.WriteLine("ID3v1 has non null tags");
                }

                if (id3v2Tag == null || id3v1Tag.IsEmpty)
                    System.Diagnostics.Debug.WriteLine("ID3v2 tag is null or empty");
                else
                {
                }
            }
        }

        public IEnumerable<IId3Tag> GetTags()
        {
            throw new NotImplementedException();
        }

        public void SetTag(IId3Tag tag)
        {
            throw new NotImplementedException();
        }

        public void RemoveTag(IId3Tag tag)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
