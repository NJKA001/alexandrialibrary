using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public interface IYouTubeCountHint
        : IYouTubeElement
    {
        int Content { get; }
    }
}
