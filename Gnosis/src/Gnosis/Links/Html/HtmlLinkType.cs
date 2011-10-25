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

        public static readonly ILinkType Alternate = new LinkType(101, "Alternate");
        public static readonly ILinkType Stylesheet = new LinkType(102, "Stylesheet");
        public static readonly ILinkType Start = new LinkType(103, "Start");
        public static readonly ILinkType Next = new LinkType(104, "Next");
        public static readonly ILinkType Prev = new LinkType(105, "Prev");
        public static readonly ILinkType Contents = new LinkType(106, "Contents");
        public static readonly ILinkType Index = new LinkType(107, "Index");
        public static readonly ILinkType Glossary = new LinkType(108, "Glossary");
        public static readonly ILinkType Copyright = new LinkType(109, "Copyright");
        public static readonly ILinkType Chapter = new LinkType(110, "Chapter");
        public static readonly ILinkType Section = new LinkType(111, "Section");
        public static readonly ILinkType Subsection = new LinkType(112, "Subsection");
        public static readonly ILinkType Appendix = new LinkType(113, "Appendix");
        public static readonly ILinkType Help = new LinkType(114, "Help");
        public static readonly ILinkType Bookmark = new LinkType(115, "Bookmark");
        public static readonly ILinkType NoFollow = new LinkType(116, "NoFollow");
        public static readonly ILinkType Tag = new LinkType(117, "Tag");

        public static IEnumerable<ILinkType> GetAll()
        {
            return all;
        }
    }
}
