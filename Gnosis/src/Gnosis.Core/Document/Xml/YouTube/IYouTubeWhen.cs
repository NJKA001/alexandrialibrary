using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public interface IYouTubeWhen
        : IYouTubeElement
    {
        DateTime Start { get; }
        DateTime End { get; }
    }
}
