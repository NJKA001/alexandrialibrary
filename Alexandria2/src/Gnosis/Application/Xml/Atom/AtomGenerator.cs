﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Atom
{
    public class AtomGenerator
        : AtomCommon, IAtomGenerator
    {
        public AtomGenerator(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string GeneratorName
        {
            get { return GetContentString(); }
        }

        public Uri Uri
        {
            get { return GetAttributeUri("uri"); }
        }

        public string Version
        {
            get { return GetAttributeString("version"); }
        }
    }
}
