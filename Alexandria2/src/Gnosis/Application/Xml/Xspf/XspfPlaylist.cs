﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Xspf
{
    public class XspfPlaylist
        : Element, IXspfPlaylist
    {
        public XspfPlaylist(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string Version
        {
            get { return GetAttributeString("version"); }
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

        public IXspfLocation Location
        {
            get { return Children.OfType<IXspfLocation>().FirstOrDefault(); }
        }

        public IXspfIdentifier Identifier
        {
            get { return Children.OfType<IXspfIdentifier>().FirstOrDefault(); }
        }

        public IXspfImage Image
        {
            get { return Children.OfType<IXspfImage>().FirstOrDefault(); }
        }

        public IXspfDate Date
        {
            get { return Children.OfType<IXspfDate>().FirstOrDefault(); }
        }

        public IXspfLicense License
        {
            get { return Children.OfType<IXspfLicense>().FirstOrDefault(); }
        }

        public IXspfAttribution Attribution
        {
            get { return Children.OfType<IXspfAttribution>().FirstOrDefault(); }
        }

        public IXspfTrackList TrackList
        {
            get { return Children.OfType<IXspfTrackList>().FirstOrDefault(); }
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
