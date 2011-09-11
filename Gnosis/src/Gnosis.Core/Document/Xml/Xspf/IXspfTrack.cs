using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.Xspf
{
    public interface IXspfTrack
        : IXspfElement
    {
        IXspfLocation Location { get; }
        IXspfIdentifier Identifier { get; }
        IXspfTitle Title { get; }
        IXspfCreator Creator { get; }
        IXspfAnnotation Annotation { get; }
        IXspfInfo Info { get; }
        IXspfImage Image { get; }
        IXspfAlbum Album { get; }
        IXspfTrackNum TrackNum { get; }
        IXspfDuration Duration { get; }
        IEnumerable<IXspfLink> Links { get; }
        IEnumerable<IXspfMeta> Metadata { get; }
        IEnumerable<IXspfExtension> Extensions { get; }
    }
}
