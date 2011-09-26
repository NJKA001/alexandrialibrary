﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.Atom
{
    public class AtomTitle
        : AtomTextConstruct, IAtomTitle
    {
        public AtomTitle(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}