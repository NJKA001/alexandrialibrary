using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Links.Html
{
    public class HtmlLinkType
        : LinkType
    {
        private HtmlLinkType(long id, string name)
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

        public static readonly ILinkType Alternate = new LinkType(2, "Alternate");
        public static readonly ILinkType Stylesheet = new LinkType(3, "Stylesheet");
        public static readonly ILinkType Start = new LinkType(4, "Start");
        public static readonly ILinkType Next = new LinkType(5, "Next");
        public static readonly ILinkType Prev = new LinkType(6, "Prev");
        public static readonly ILinkType Contents = new LinkType(7, "Contents");
        public static readonly ILinkType Index = new LinkType(8, "Index");
        public static readonly ILinkType Glossary = new LinkType(9, "Glossary");
        public static readonly ILinkType Copyright = new LinkType(10, "Copyright");
        public static readonly ILinkType Chapter = new LinkType(11, "Chapter");
        public static readonly ILinkType Section = new LinkType(12, "Section");
        public static readonly ILinkType Subsection = new LinkType(13, "Subsection");
        public static readonly ILinkType Appendix = new LinkType(14, "Appendix");
        public static readonly ILinkType Help = new LinkType(15, "Help");
        public static readonly ILinkType Bookmark = new LinkType(16, "Bookmark");
        public static readonly ILinkType NoFollow = new LinkType(17, "NoFollow");
        public static readonly ILinkType Tag = new LinkType(18, "Tag");

        public static IEnumerable<ILinkType> GetAll()
        {
            return all;
        }
    }
}
