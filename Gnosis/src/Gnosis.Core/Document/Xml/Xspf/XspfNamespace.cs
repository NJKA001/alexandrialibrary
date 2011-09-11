using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.Xspf
{
    public class XspfNamespace
        : Namespace
    {
        public XspfNamespace()
            : base("xspf", new Uri("http://xspf.org/ns/0/"))
        {
            AddElementConstructor("playlist", (parent, name) => new XspfPlaylist(parent, name));
            AddElementConstructor("title", (parent, name) => new XspfTitle(parent, name));
            AddElementConstructor("creator", (parent, name) => new XspfCreator(parent, name));
            AddElementConstructor("annotation", (parent, name) => new XspfAnnotation(parent, name));
            AddElementConstructor("info", (parent, name) => new XspfInfo(parent, name));
            AddElementConstructor("location", (parent, name) => new XspfLocation(parent, name));
            AddElementConstructor("identifier", (parent, name) => new XspfIdentifier(parent, name));
            AddElementConstructor("image", (parent, name) => new XspfImage(parent, name));
            AddElementConstructor("date", (parent, name) => new XspfDate(parent, name));
            AddElementConstructor("license", (parent, name) => new XspfLicense(parent, name));
            AddElementConstructor("attribution", (parent, name) => new XspfAttribution(parent, name));
            AddElementConstructor("link", (parent, name) => new XspfLink(parent, name));
            AddElementConstructor("meta", (parent, name) => new XspfMeta(parent, name));
            AddElementConstructor("extension", (parent, name) => new XspfExtension(parent, name));
            AddElementConstructor("trackList", (parent, name) => new XspfTrackList(parent, name));
            AddElementConstructor("track", (parent, name) => new XspfTrack(parent, name));
            AddElementConstructor("album", (parent, name) => new XspfAlbum(parent, name));
            AddElementConstructor("trackNum", (parent, name) => new XspfTrackNum(parent, name));
            AddElementConstructor("duration", (parent, name) => new XspfDuration(parent, name));
        }
    }
}
