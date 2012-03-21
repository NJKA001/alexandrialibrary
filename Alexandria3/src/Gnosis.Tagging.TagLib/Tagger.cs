using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Data;

namespace Gnosis.Tagging.TagLib
{
    public class Tagger
        : ITagger
    {
        public ITag GetTag(string path, Category category)
        {
            var file = File.Create(path);
            var tag = file.GetTag(TagTypes.AllTags);
            if (tag == null)
                return null;

            var source = Source.Embedded;
            if ((tag.TagTypes & TagTypes.Id3v2) == TagTypes.Id3v2)
            {
                source = Source.Embedded_Id3v2;
            }
            else if ((tag.TagTypes & TagTypes.Id3v1) == TagTypes.Id3v1)
            {
                source = Source.Embedded_Id3v1;
            }

            switch (category)
            {
                case Category.Album:
                    return tag.Album != null ? new Gnosis.Data.Tag(tag.Album, category, source) : null;
                case Category.AlbumArtist:
                    return tag.JoinedAlbumArtists != null ? new Gnosis.Data.Tag(tag.JoinedAlbumArtists, category, source) : null;
                case Category.Artist:
                    return tag.JoinedPerformers != null ? new Gnosis.Data.Tag(tag.JoinedPerformers, category, source) : null;
                case Category.BeatsPerMinute:
                    return tag.BeatsPerMinute > 0 ? new Gnosis.Data.Tag(tag.BeatsPerMinute.ToString(), category, source) : null;
                case Category.Comment:
                    return tag.Comment != null ? new Gnosis.Data.Tag(tag.Comment, category, source) : null;
                case Category.Composer:
                    return tag.JoinedComposers != null ? new Gnosis.Data.Tag(tag.JoinedComposers, category, source) : null;
                case Category.Conductor:
                    return tag.Conductor != null ? new Gnosis.Data.Tag(tag.Conductor, category, source) : null;
                case Category.Copyright:
                    return tag.Copyright != null ? new Gnosis.Data.Tag(tag.Copyright, category, source) : null;
                case Category.Disc:
                    return tag.Disc > 0 ? new Gnosis.Data.Tag(tag.Disc.ToString(), category, source) : null;
                case Category.DiscCount:
                    return tag.DiscCount > 0 ? new Gnosis.Data.Tag(tag.DiscCount.ToString(), category, source) : null;
                case Category.Genre:
                    return tag.JoinedGenres != null ? new Gnosis.Data.Tag(tag.JoinedGenres, category, source) : null;
                case Category.Grouping:
                    return tag.Grouping != null ? new Gnosis.Data.Tag(tag.Grouping, category, source) : null;
                case Category.Lyrics:
                    return tag.Lyrics != null ? new Gnosis.Data.Tag(tag.Lyrics, category, source) : null;
                case Category.Title:
                    return tag.Title != null ? new Gnosis.Data.Tag(tag.Title, category, source) : null;
                case Category.Track:
                    return tag.Track > 0 ? new Gnosis.Data.Tag(tag.Track.ToString(), category, source) : null;
                case Category.TrackCount:
                    return tag.TrackCount > 0 ? new Gnosis.Data.Tag(tag.TrackCount.ToString(), category, source) : null;
                case Category.Year:
                    return tag.Year > 0 ? new Gnosis.Data.Tag(tag.Year.ToString(), category, source) : null;
                default:
                    return null;
            }
        }

        public IEnumerable<ITag> GetTags(string path)
        {
            throw new NotImplementedException();
        }

        public void SaveTag(string path, ITag tag)
        {
            throw new NotImplementedException();
        }
    }
}
