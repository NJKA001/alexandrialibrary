using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public interface IXspfPlaylist
        : IXspfElement
    {
        string Version { get; }
        IXspfTitle Title { get; }
        IXspfCreator Creator { get; }
        IXspfAnnotation Annotation { get; }
        IXspfInfo Info { get; }
        IXspfLocation Location { get; }
        IXspfIdentifier Identifier { get; }
        IXspfImage Image { get; }
        IXspfDate Date { get; }
        IXspfLicense License { get; }
        IXspfAttribution Attribution { get; }
        IXspfTrackList TrackList { get; }
        IEnumerable<IXspfLink> Links { get; }
        IEnumerable<IXspfMeta> Metadata { get; }
        IEnumerable<IXspfExtension> Extensions { get; }
    }
}
