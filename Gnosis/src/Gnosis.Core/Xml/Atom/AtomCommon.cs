using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Xml.Atom
{
    public abstract class AtomCommon
        : Element, IAtomCommon
    {
        protected AtomCommon(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri BaseId
        {
            get
            {
                var baseAttrib = Attributes.OfType<IBaseAttribute>().FirstOrDefault();

                return baseAttrib != null ?
                    baseAttrib.Value
                    : null;
            }
        }

        public ILanguageTag Lang
        {
            get
            {
                var langAttrib = Attributes.OfType<ILangAttribute>().FirstOrDefault();

                return langAttrib != null ?
                    langAttrib.Value
                    : null;
            }
        }
    }
}
