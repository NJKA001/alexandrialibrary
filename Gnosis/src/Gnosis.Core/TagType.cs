using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagType
        : ITagType
    {
        private TagType(long id, string name, Uri scheme)
        {
            this.id = id;
            this.name = name;
            this.scheme = scheme;
        }

        private readonly long id;
        private readonly string name;
        private readonly Uri scheme;

        public long Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public Uri Scheme
        {
            get { return scheme; }
        }

        static TagType()
        {
            foreach (var tagType in tagTypes)
                byId[tagType.Id] = tagType;
        }

        public static readonly Uri DefaultScheme = new Uri("http://gn0s1s.com/alexandria/ns/1/tags/default");
        public static readonly Uri Id3v2Scheme = new Uri("http://gn0s1s.com/alexandria/ns/1/tags/id3v2");

        public static ITagType GeneralTagType = new TagType(1, "General", DefaultScheme);
        public static ITagType Id3v2TitleTagType = new TagType(2, "Title", Id3v2Scheme);
        public static ITagType Id3v2ArtistTagType = new TagType(3, "Artist", Id3v2Scheme);

        private static readonly IList<ITagType> tagTypes = new List<ITagType>()
        {
            GeneralTagType,
            Id3v2TitleTagType,
            Id3v2ArtistTagType
        };
        private static readonly IDictionary<long, ITagType> byId = new Dictionary<long, ITagType>();

        public static IEnumerable<ITagType> TagTypes
        {
            get { return tagTypes; }
        }

        public static ITagType Parse(long id)
        {
            return byId.ContainsKey(id) ?
                byId[id]
                : null;
        }
    }
}
