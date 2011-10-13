using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Tags.Id3.Id3v1
{
    public class Id3v1TagType
    {
        static Id3v1TagType()
        {
            all.Add(Title);
            all.Add(Artist);
            all.Add(Album);
            all.Add(Year);
            all.Add(Comment);
            all.Add(Track);
            all.Add(Genre);
        }

        private static readonly IList<ITagType> all = new List<ITagType>();

        public static readonly ITagType Title = new Id3v1TagType<string>(2, "Title", TagDomain.String);
        public static readonly ITagType Artist = new Id3v1TagType<string>(3, "Artist", TagDomain.String);
        public static readonly ITagType Album = new Id3v1TagType<string>(4, "Album", TagDomain.String);
        public static readonly ITagType Year = new Id3v1TagType<uint>(5, "Year", TagDomain.PositiveInteger);
        public static readonly ITagType Comment = new Id3v1TagType<string>(6, "Comment", TagDomain.String);
        public static readonly ITagType Track = new Id3v1TagType<uint>(7, "Track", TagDomain.PositiveInteger);
        public static readonly ITagType Genre = new Id3v1TagType<uint>(8, "Genre", TagDomain.PositiveInteger);

        public static IEnumerable<ITagType> GetAll()
        {
            return all;
        }
    }

    public class Id3v1TagType<T>
        : TagType<T>
    {
        public Id3v1TagType(int id, string name, ITagDomain domain)
            : base(id, name, Core.Algorithm.Default, TagSchema.Id3v1, domain)
        {
        }
    }
}
