﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Atom
{
    public class AtomSummary
        : AtomTextConstruct, IAtomSummary
    {
        public AtomSummary(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
