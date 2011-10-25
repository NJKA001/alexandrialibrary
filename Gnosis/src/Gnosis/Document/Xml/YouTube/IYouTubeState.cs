using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml.YouTube
{
    public interface IYouTubeState
        : IYouTubeSimpleContentElement
    {
        YouTubeStateName StateName { get; }
        YouTubeStateReasonCode ReasonCode { get; }
        Uri HelpUrl { get; }
    }
}
