using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Links.Html
{
    public class HtmlLinkType
        : LinkType
    {
        private HtmlLinkType(int id, string name)
            : base(id, name)
        {
        }

        static HtmlLinkType()
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

        private static readonly IList<ILinkType> all = new List<ILinkType>();

        public static readonly ILinkType DefaultLink = new LinkType(101, string.Empty);
        public static readonly ILinkType Alternate = new LinkType(102, "Alternate");
        public static readonly ILinkType Stylesheet = new LinkType(103, "Stylesheet");
        public static readonly ILinkType Start = new LinkType(104, "Start");
        public static readonly ILinkType Next = new LinkType(105, "Next");
        public static readonly ILinkType Prev = new LinkType(106, "Prev");
        public static readonly ILinkType Contents = new LinkType(107, "Contents");
        public static readonly ILinkType Index = new LinkType(108, "Index");
        public static readonly ILinkType Glossary = new LinkType(109, "Glossary");
        public static readonly ILinkType Copyright = new LinkType(110, "Copyright");
        public static readonly ILinkType Chapter = new LinkType(111, "Chapter");
        public static readonly ILinkType Section = new LinkType(112, "Section");
        public static readonly ILinkType Subsection = new LinkType(113, "Subsection");
        public static readonly ILinkType Appendix = new LinkType(114, "Appendix");
        public static readonly ILinkType Help = new LinkType(115, "Help");
        public static readonly ILinkType Bookmark = new LinkType(116, "Bookmark");
        public static readonly ILinkType NoFollow = new LinkType(117, "NoFollow");
        public static readonly ILinkType Tag = new LinkType(118, "Tag");

        public static IEnumerable<ILinkType> GetAll()
        {
            return all;
        }
    }
}
