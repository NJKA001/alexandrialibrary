using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml.Namespaces.YouTube
{
    public interface IYouTubeDerived
        : IYouTubeElement
    {
        YouTubeDerivedSource Content { get; }
    }
}
