﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.Xspf
{
    public interface IXspfTrackList
        : IXspfElement
    {
        IEnumerable<IXspfTrack> Tracks { get; }
    }
}
