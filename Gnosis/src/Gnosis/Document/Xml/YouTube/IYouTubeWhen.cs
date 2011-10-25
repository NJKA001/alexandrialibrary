using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public interface IYouTubeWhen
        : IYouTubeElement
    {
        DateTime Start { get; }
        DateTime End { get; }
    }
}
