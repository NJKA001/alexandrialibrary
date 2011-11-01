using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public class XspfTrackList
        : Element, IXspfTrackList
    {
        public XspfTrackList(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public IEnumerable<IXspfTrack> Tracks
        {
            get { return Children.OfType<IXspfTrack>(); }
        }
    }
}
