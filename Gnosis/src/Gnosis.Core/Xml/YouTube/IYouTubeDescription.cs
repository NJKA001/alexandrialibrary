using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.YouTube
{
    public interface IYouTubeDescription
        : IYouTubeElement
    {
        string Content { get; }
    }
}
