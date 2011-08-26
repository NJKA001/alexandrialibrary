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
                if (child == null)
                    return Hour.Unknown;

                var value = 0;
                if (!int.TryParse(child.Content, out value))
                    return Hour.Unknown;

                return (Enum.IsDefined(typeof(Hour), value)) ?
                    (Hour)value
                    : Hour.Unknown;
            }
        }
    }
}
