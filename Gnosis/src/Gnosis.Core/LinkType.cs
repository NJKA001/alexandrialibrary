using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class LinkType
        : ILinkType
    {
        private LinkType(int id, string name, Uri scheme)
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

        static LinkType()
        {
            InitializeLinkTypes();

            foreach (var linkType in all)
            {
                byId[linkType.Id] = linkType;
                byName[linkType.Name] = linkType;
            }
        }

        private static readonly IList<ILinkType> all = new List<ILinkType>();
        private static readonly IDictionary<int, ILinkType> byId = new Dictionary<int, ILinkType>();
        private static readonly IDictionary<string, ILinkType> byName = new Dictionary<string, ILinkType>();

        private static void InitializeLinkTypes()
        {
            all.Add(Alternate);
            all.Add(Stylesheet);
            all.Add(Start);
            all.Add(Next);
            all.Add(Prev);
            all.Add(Contents);
            all.Add(Index);
            all.Add(Glossary);
            all.Add(Copyright);
            all.Add(Chapter);
            all.Add(Section);
            all.Add(Subsection);
            all.Add(Appendix);
            all.Add(Help);
            all.Add(Bookmark);
            all.Add(NoFollow);
            all.Add(Tag);
        }

        public static readonly Uri Html4LinkTypeScheme = new Uri("http://www.w3.org/TR/html4/types.html#h-6.12");
        public static readonly Uri NoFollowScheme = new Uri("http://microformats.org/wiki/rel-nofollow");
        public static readonly Uri TagScheme = new Uri("http://microformats.org/wiki/rel-tag");

        public static readonly ILinkType Alternate = new LinkType(1, "Alternate", Html4LinkTypeScheme);
        public static readonly ILinkType Stylesheet = new LinkType(2, "Stylesheet", Html4LinkTypeScheme);
        public static readonly ILinkType Start = new LinkType(3, "Start", Html4LinkTypeScheme);
        public static readonly ILinkType Next = new LinkType(4, "Next", Html4LinkTypeScheme);
        public static readonly ILinkType Prev = new LinkType(5, "Prev", Html4LinkTypeScheme);
        public static readonly ILinkType Contents = new LinkType(6, "Contents", Html4LinkTypeScheme);
        public static readonly ILinkType Index = new LinkType(7, "Index", Html4LinkTypeScheme);
        public static readonly ILinkType Glossary = new LinkType(8, "Glossary", Html4LinkTypeScheme);
        public static readonly ILinkType Copyright = new LinkType(9, "Copyright", Html4LinkTypeScheme);
        public static readonly ILinkType Chapter = new LinkType(10, "Chapter", Html4LinkTypeScheme);
        public static readonly ILinkType Section = new LinkType(11, "Section", Html4LinkTypeScheme);
        public static readonly ILinkType Subsection = new LinkType(12, "Subsection", Html4LinkTypeScheme);
        public static readonly ILinkType Appendix = new LinkType(13, "Appendix", Html4LinkTypeScheme);
        public static readonly ILinkType Help = new LinkType(14, "Help", Html4LinkTypeScheme);
        public static readonly ILinkType Bookmark = new LinkType(15, "Bookmark", Html4LinkTypeScheme);
        public static readonly ILinkType NoFollow = new LinkType(16, "NoFollow", NoFollowScheme);
        public static readonly ILinkType Tag = new LinkType(17, "Tag", TagScheme);

        public static IEnumerable<ILinkType> GetAll()
        {
            return all;
        }

        public static ILinkType Parse(int id)
        {
            return byId.ContainsKey(id) ?
                byId[id]
                : null;
        }

        public static ILinkType Parse(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            return byName.ContainsKey(name) ?
                byName[name]
                : null;
        }
    }
}
