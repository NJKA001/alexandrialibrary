using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagType
        : ITagType
    {
        private TagType(int id, string name, Uri scheme)
        {
            this.id = id;
            this.name = name;
            this.scheme = scheme;
        }

        private readonly int id;
        private readonly string name;
        private readonly Uri scheme;

        public int Id
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
            InitializeTagTypes();

            foreach (var tagType in all)
            {
                byId[tagType.Id] = tagType;
                byName[tagType.Name] = tagType;
            }
        }

        private static readonly IList<ITagType> all = new List<ITagType>();
        private static readonly IDictionary<int, ITagType> byId = new Dictionary<int, ITagType>();
        private static readonly IDictionary<string, ITagType> byName = new Dictionary<string, ITagType>();

        private static void InitializeTagTypes()
        {
            all.Add(GeneralTagType);
            all.Add(Id3v2TitleTagType);
            all.Add(Id3v2ArtistTagType);
        }

        public static readonly Uri DefaultScheme = new Uri("http://gn0s1s.com/alexandria/ns/1/tags/default/");
        public static readonly Uri Id3v2Scheme = new Uri("http://gn0s1s.com/alexandria/ns/1/tags/id3v2/");

        public static ITagType GeneralTagType = new TagType(1, "General", DefaultScheme);
        public static ITagType Id3v2TitleTagType = new TagType(2, "Title", Id3v2Scheme);
        public static ITagType Id3v2ArtistTagType = new TagType(3, "Artist", Id3v2Scheme);

        public static IEnumerable<ITagType> GetAll()
        {
            return all;
        }

        public static ITagType Parse(int id)
        {
            return byId.ContainsKey(id) ?
                byId[id]
                : null;
        }

        public static ITagType Parse(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            return byName.ContainsKey(name) ?
                byName[name]
                : null;
        }
    }
}
