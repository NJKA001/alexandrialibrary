﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Atom
{
    public class AtomContributor
        : AtomPerson, IAtomContributor
    {
        public AtomContributor(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }
    }
}
