using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags.Id3
{
    public static class Id3v1TagTypes
    {
        public static readonly ITagType Id3v1Title = new TagType(2, "Title");
        public static readonly ITagType Id3v1Artist = new TagType(3, "Artist");
        public static readonly ITagType Id3v1Album = new TagType(4, "Album");
        public static readonly ITagType Id3v1Year = new TagType(5, "Year");
        public static readonly ITagType Id3v1Comment = new TagType(6, "Comment");
        public static readonly ITagType Id3v1Track = new TagType(7, "Track");
        public static readonly ITagType Id3v1Genre = new TagType(8, "Genre");
        public static readonly ITagType Id3v1TitleExtension = new TagType(9, "Title Extension");
        public static readonly ITagType Id3v1ArtistExtension = new TagType(10, "Artist Extension");
        public static readonly ITagType Id3v1AlbumExtension = new TagType(11, "Album Extension");
        public static readonly ITagType Id3v1Speed = new TagType(12, "Speed");
        public static readonly ITagType Id3v1GenreExtension = new TagType(13, "Genre Extension");
        public static readonly ITagType Id3v1StartTime = new TagType(14, "Start Time");
        public static readonly ITagType Id3v1EndTime = new TagType(15, "End Time");
    }
}
