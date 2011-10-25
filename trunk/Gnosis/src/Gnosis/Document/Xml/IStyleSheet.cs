﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml
{
    public interface IStyleSheet
        : IProcessingInstruction
    {
        IMediaType Type { get; }
        IStyleMedia Media { get; }
        Uri Href { get; }
    }
}
