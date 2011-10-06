using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags.Id3
{
    public static class Id3v1TagTypes
    {
        public static readonly ITagType Id3v1Title = new TagType(2, "Title", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1Artist = new TagType(3, "Artist", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1Album = new TagType(4, "Album", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1Year = new TagType(5, "Year", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1Comment = new TagType(6, "Comment", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1Track = new TagType(7, "Track", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1Genre = new TagType(8, "Genre", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1TitleExtension = new TagType(9, "Title Extension", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1ArtistExtension = new TagType(10, "Artist Extension", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1AlbumExtension = new TagType(11, "Album Extension", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1Speed = new TagType(12, "Speed", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1GenreExtension = new TagType(13, "Genre Extension", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1StartTime = new TagType(14, "Start Time", Id3Schemas.Id3v1Schema);
        public static readonly ITagType Id3v1EndTime = new TagType(15, "End Time", Id3Schemas.Id3v1Schema);
    }
}
