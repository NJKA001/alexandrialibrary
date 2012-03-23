using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Data;
using GTag=Gnosis.Data.Tag;

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
                source = Source.Id3v2;
            }
            else if ((tag.TagTypes & TagTypes.Id3v1) == TagTypes.Id3v1)
            {
                source = Source.Id3v1;
            }
            else if ((tag.TagTypes & TagTypes.RiffInfo) == TagTypes.RiffInfo)
            {
                source = Source.Riff;
            }

            switch (category)
            {
                case Category.Album:
                    return tag.Album != null ? new GTag(tag.Album, category, source) : null;
                case Category.AlbumArtist:
                    return tag.JoinedAlbumArtists != null ? new GTag(tag.JoinedAlbumArtists, category, source) : null;
                case Category.Artist:
                    return tag.JoinedPerformers != null ? new GTag(tag.JoinedPerformers, category, source) : null;
                case Category.BeatsPerMinute:
                    return tag.BeatsPerMinute > 0 ? new GTag(tag.BeatsPerMinute.ToString(), category, source) : null;
                case Category.Comment:
                    return tag.Comment != null ? new GTag(tag.Comment, category, source) : null;
                case Category.Composer:
                    return tag.JoinedComposers != null ? new GTag(tag.JoinedComposers, category, source) : null;
                case Category.Conductor:
                    return tag.Conductor != null ? new GTag(tag.Conductor, category, source) : null;
                case Category.Copyright:
                    return tag.Copyright != null ? new GTag(tag.Copyright, category, source) : null;
                case Category.Disc:
                    return tag.Disc > 0 ? new GTag(tag.Disc.ToString(), category, source) : null;
                case Category.DiscCount:
                    return tag.DiscCount > 0 ? new GTag(tag.DiscCount.ToString(), category, source) : null;
                case Category.Genre:
                    return tag.JoinedGenres != null ? new GTag(tag.JoinedGenres, category, source) : null;
                case Category.Grouping:
                    return tag.Grouping != null ? new GTag(tag.Grouping, category, source) : null;
                case Category.Lyrics:
                    return tag.Lyrics != null ? new GTag(tag.Lyrics, category, source) : null;
                case Category.Title:
                    return tag.Title != null ? new GTag(tag.Title, category, source) : null;
                case Category.Track:
                    return tag.Track > 0 ? new GTag(tag.Track.ToString(), category, source) : null;
                case Category.TrackCount:
                    return tag.TrackCount > 0 ? new GTag(tag.TrackCount.ToString(), category, source) : null;
                case Category.Year:
                    return tag.Year > 0 ? new GTag(tag.Year.ToString(), category, source) : null;
                case Category.AudioBitrate:
                    return file.Properties != null && file.Properties.AudioBitrate > 0 ? new GTag(file.Properties.AudioBitrate.ToString(), category, source) : null;
                case Category.AudioChannels:
                    return file.Properties != null && file.Properties.AudioChannels > 0 ? new GTag(file.Properties.AudioChannels.ToString(), category, source) : null;
                case Category.AudioSampleRate:
                    return file.Properties != null && file.Properties.AudioSampleRate > 0 ? new GTag(file.Properties.AudioSampleRate.ToString(), category, source) : null;
                case Category.MediaCodec:
                    return file.Properties != null && file.Properties.Codecs != null && file.Properties.Codecs.Count() > 0 ? new GTag(string.Join(", ", file.Properties.Codecs.Select(x => x.Description)), category, source) : null;
                case Category.MediaDescription:
                    return file.Properties != null && file.Properties.Description != null ? new GTag(file.Properties.Description, category, source) : null;
                case Category.MediaDuration:
                    return file.Properties != null && file.Properties.Duration != TimeSpan.Zero ? new GTag(file.Properties.Duration.ToString(), category, source) : null;
                case Category.VideoHeight:
                    return file.Properties != null && file.Properties.VideoHeight > 0 ? new GTag(file.Properties.VideoHeight.ToString(), category, source) : null;
                case Category.VideoWidth:
                    return file.Properties != null && file.Properties.VideoWidth > 0 ? new GTag(file.Properties.VideoWidth.ToString(), category, source) : null;
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
