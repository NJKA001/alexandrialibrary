using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public enum TrackSynchronizedTextType
    {
        Other = 0x00,
        Lyrics = 0x01,
        TextTranscription = 0x02,
        Movement = 0x03,
        Events = 0x04,
        Chord = 0x05,
        Trivia = 0x06,
        WebpageUrls = 0x07,
        ImageUrls = 0x08
    }
}
