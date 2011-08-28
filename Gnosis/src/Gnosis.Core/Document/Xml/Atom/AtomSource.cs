using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.Atom
{
    public class AtomSource
        : AtomCommon, IAtomSource
    {
        public AtomSource(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public IEnumerable<IAtomAuthor> Authors
        {
            get { return Children.OfType<IAtomAuthor>(); }
        }

        public IAtomId Id
        {
            get { return Children.OfType<IAtomId>().FirstOrDefault(); }
        }

        public IEnumerable<IAtomLink> Links
        {
            get { return Children.OfType<IAtomLink>(); }
        }

        public IAtomTitle Title
        {
            get { return Children.OfType<IAtomTitle>().FirstOrDefault(); }
        }

        public IAtomUpdated Updated
        {
            get { return Children.OfType<IAtomUpdated>().FirstOrDefault(); }
        }

        public IEnumerable<IAtomCategory> Categories
        {
            get { return Children.OfType<IAtomCategory>(); }
        }

        public IEnumerable<IAtomContributor> Contributors
        {
            get { return Children.OfType<IAtomContributor>(); }
        }

        public IAtomGenerator Generator
        {
            get { return Children.OfType<IAtomGenerator>().FirstOrDefault(); }
        }

        public IAtomIcon Icon
        {
            get { return Children.OfType<IAtomIcon>().FirstOrDefault(); }
        }

        public IAtomLogo Logo
        {
            get { return Children.OfType<IAtomLogo>().FirstOrDefault(); }
        }

        public IAtomRights Rights
        {
            get { return Children.OfType<IAtomRights>().FirstOrDefault(); }
        }

        public IAtomSubtitle Subtitle
        {
            get { return Children.OfType<IAtomSubtitle>().FirstOrDefault(); }
        }
    }
}
