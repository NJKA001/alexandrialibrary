﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.Atom
{
    public interface IAtomId
        : IAtomCommon
    {
        Uri Content { get; }
    }
}