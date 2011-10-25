﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml
{
    public interface INamespace
    {
        string Alias { get; }
        Uri Identifier { get; }

        IElement GetElement(INode parent, IQualifiedName name);
    }
}
