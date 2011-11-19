﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Tags.Id3.Id3v1
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

        public static readonly ITagType Title = new Id3v1TagType<string>(102, "Title", TagDomain.String);
        public static readonly ITagType Artist = new Id3v1TagType<string>(103, "Artist", TagDomain.String);
        public static readonly ITagType Album = new Id3v1TagType<string>(104, "Album", TagDomain.String);
        public static readonly ITagType Year = new Id3v1TagType<uint>(105, "Year", TagDomain.PositiveInteger);
        public static readonly ITagType Comment = new Id3v1TagType<string>(106, "Comment", TagDomain.String);
        public static readonly ITagType Track = new Id3v1TagType<uint>(107, "Track", TagDomain.PositiveInteger);
        public static readonly ITagType Genre = new Id3v1TagType<uint>(108, "Genre", TagDomain.Id3v1SimpleGenre);

        public static IEnumerable<ITagType> GetAll()
        {
            return all;
        }
    }

    public class Id3v1TagType<T>
        : TagType<T>
    {
        public Id3v1TagType(int id, string name, ITagDomain domain)
            : base(id, name, TagSchema.Id3v1, domain)
        {
        }
    }
}