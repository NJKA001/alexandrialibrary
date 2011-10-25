using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Xspf
{
    public class XspfTrack
        : Element, IXspfTrack
    {
        public XspfTrack(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public IXspfLocation Location
        {
            get { return Children.OfType<IXspfLocation>().FirstOrDefault(); }
        }

        public IXspfIdentifier Identifier
        {
            get { return Children.OfType<IXspfIdentifier>().FirstOrDefault(); }
        }

        public IXspfTitle Title
        {
            get { return Children.OfType<IXspfTitle>().FirstOrDefault(); }
        }

        public IXspfCreator Creator
        {
            get { return Children.OfType<IXspfCreator>().FirstOrDefault(); }
        }

        public IXspfAnnotation Annotation
        {
            get { return Children.OfType<IXspfAnnotation>().FirstOrDefault(); }
        }

        public IXspfInfo Info
        {
            get { return Children.OfType<IXspfInfo>().FirstOrDefault(); }
        }

        public IXspfImage Image
        {
            get { return Children.OfType<IXspfImage>().FirstOrDefault(); }
        }

        public IXspfAlbum Album
        {
            get { return Children.OfType<IXspfAlbum>().FirstOrDefault(); }
        }

        public IXspfTrackNum TrackNum
        {
            get { return Children.OfType<IXspfTrackNum>().FirstOrDefault(); }
        }

        public IXspfDuration Duration
        {
            get { return Children.OfType<IXspfDuration>().FirstOrDefault(); }
        }

        public IEnumerable<IXspfLink> Links
        {
            get { return Children.OfType<IXspfLink>(); }
        }

        public IEnumerable<IXspfMeta> Metadata
        {
            get { return Children.OfType<IXspfMeta>(); }
        }

        public IEnumerable<IXspfExtension> Extensions
        {
            get { return Children.OfType<IXspfExtension>(); }
        }
    }
}
