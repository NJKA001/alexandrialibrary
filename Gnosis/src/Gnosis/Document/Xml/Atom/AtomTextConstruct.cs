using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Atom
{
    public abstract class AtomTextConstruct
        : AtomCommon, IAtomTextConstruct
    {
        protected AtomTextConstruct(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string Text
        {
            get
            {
                switch (Type)
                {
                    case AtomTextType.html:
                    case AtomTextType.xhtml:
                        {
                            var child = Children.FirstOrDefault();
                            return child != null ?
                                child.ToString()
                                : null;
                        };
                    case AtomTextType.text:
                    default:
                        return GetContentString();
                }
            }
        }

        public AtomTextType Type
        {
            get { return GetAttributeEnum<AtomTextType>("type", AtomTextType.None); }
        }
    }
}
