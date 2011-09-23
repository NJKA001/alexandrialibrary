﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IRepresentation
    {
        Uri Location { get; }
        IContentType ContentType { get; }
    }
}