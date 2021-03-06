﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Rss
{
    public interface IRssTextInput
        : IRssElement
    {
        string Title { get; }
        string Description { get; }
        string InputName { get; }
        Uri Link { get; }
    }
}
