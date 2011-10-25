using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public interface IYouTubeGender
        : IYouTubeElement
    {
        YouTubeGenderContent Content { get; }
    }
}
