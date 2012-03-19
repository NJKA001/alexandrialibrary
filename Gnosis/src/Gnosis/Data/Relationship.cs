using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public enum Relationship : ushort
    {
        None = 0,
        Alternate = 1,
        Appendix = 2,
        Bookmark = 3,
        Chapter = 4,
        Contents = 5,
        Copyright = 6,
        Glossary = 7,
        Help = 8,
        Home = 9,
        Index = 10,
        Next = 11,
        Prev = 12,
        Section = 13,
        Start = 14,
        Stylesheet = 15,
        Subsection = 16,
        Enclosure = 17,
        Thumbnail = 18
    }
}
