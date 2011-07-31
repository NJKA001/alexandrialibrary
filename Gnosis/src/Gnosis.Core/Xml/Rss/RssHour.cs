using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssHour
        : Element, IRssHour
    {
        public RssHour(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Hour Value
        {
            get
            {
                var child = Children.FirstOrDefault() as ICharacterData;

                return (child != null && child.Content != null && Enum.IsDefined(typeof(Hour), child.Content)) ?
                    (Hour)Enum.Parse(typeof(Hour), child.Content)
                    : Hour.Unknown;
            }
        }
    }
}
