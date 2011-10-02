using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags.Id3
{
    public class Id3v1Schema
        : Schema
    {
        public Id3v1Schema()
            : base(Id3v1.ToUri(), "ID3v1")
        {
            AddChild(new Schema(Id3v1Title.ToUri(), "Title"));
            AddChild(new Schema(Id3v1Artist.ToUri(), "Artist"));
            AddChild(new Schema(Id3v1Album.ToUri(), "Album"));
            AddChild(new Schema(Id3v1Year.ToUri(), "Year"));
            AddChild(new Schema(Id3v1Comment.ToUri(), "Comment"));
            AddChild(new Schema(Id3v1Track.ToUri(), "Track"));
            AddChild(new Schema(Id3v1Genre.ToUri(), "Genre"));
            AddChild(new Schema(Id3v1TitleExtension.ToUri(), "Title Extension"));
            AddChild(new Schema(Id3v1ArtistExtension.ToUri(), "Artist Extension"));
            AddChild(new Schema(Id3v1AlbumExtension.ToUri(), "Album Extension"));
            AddChild(new Schema(Id3v1Speed.ToUri(), "Speed"));
            AddChild(new Schema(Id3v1GenreExtension.ToUri(), "Genre Extension"));
            AddChild(new Schema(Id3v1StartTime.ToUri(), "Start Time"));
            AddChild(new Schema(Id3v1EndTime.ToUri(), "End Time"));
        }

        public const string Id3v1 = "http://gn0s1s.com/ns/1/tag-types/id3/v1/";
        public const string Id3v1Title = "http://gn0s1s.com/ns/1/tag-types/id3/v1/title";
        public const string Id3v1Artist = "http://gn0s1s.com/ns/1/tag-types/id3/v1/artist";
        public const string Id3v1Album = "http://gn0s1s.com/ns/1/tag-types/id3/v1/album";
        public const string Id3v1Year = "http://gn0s1s.com/ns/1/tag-types/id3/v1/year";
        public const string Id3v1Comment = "http://gn0s1s.com/ns/1/tag-types/id3/v1/title";
        public const string Id3v1Track = "http://gn0s1s.com/ns/1/tag-types/id3/v1/track";
        public const string Id3v1Genre = "http://gn0s1s.com/ns/1/tag-types/id3/v1/genre";
        public const string Id3v1TitleExtension = "http://gn0s1s.com/ns/1/tag-types/id3/v1/title-extension";
        public const string Id3v1ArtistExtension = "http://gn0s1s.com/ns/1/tag-types/id3/v1/artist-extension";
        public const string Id3v1AlbumExtension = "http://gn0s1s.com/ns/1/tag-types/id3/v1/album-extension";
        public const string Id3v1Speed = "http://gn0s1s.com/ns/1/tag-types/id3/v1/speed";
        public const string Id3v1GenreExtension = "http://gn0s1s.com/ns/1/tag-types/id3/v1/genre-extension";
        public const string Id3v1StartTime = "http://gn0s1s.com/ns/1/tag-types/id3/v1/start-time";
        public const string Id3v1EndTime = "http://gn0s1s.com/ns/1/tag-types/id3/v1/end-time";
    }
}
