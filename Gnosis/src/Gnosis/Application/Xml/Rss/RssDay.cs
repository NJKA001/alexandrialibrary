using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Rss
{
    public class RssDay
        : Element, IRssDay
    {
        public RssDay(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Day Value
        {
            get
            {
                var child = Children.FirstOrDefault() as ICharacterData;

                return (child != null && child.Content != null && Enum.IsDefined(typeof(Day), child.Content)) ?
                    (Day)Enum.Parse(typeof(Day), child.Content)
                    : Day.Unknown;
            }
        }
    }
}
