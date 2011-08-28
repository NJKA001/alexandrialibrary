using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public interface IYouTubeState
        : IYouTubeSimpleContentElement
    {
        YouTubeStateName StateName { get; }
        YouTubeStateReasonCode ReasonCode { get; }
        Uri HelpUrl { get; }
    }
}
