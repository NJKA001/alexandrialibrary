using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Data
{
    public enum Category : ushort
    {
        None = 0,
        
        Content = 100,
        Album = 101,
        AlbumArtist = 102,
        Artist = 103,
        BeatsPerMinute = 104,
        Comment = 105,
        Composer = 106,
        Conductor = 107,
        Copyright = 108,
        Disc = 109,
        DiscCount = 110,
        Genre = 111,
        Grouping = 112,
        Lyrics = 113,
        Title = 114,
        Track = 115,
        TrackCount = 116,
        Year = 117,

        Digital = 1000,
        AudioBitrate = 1001,
        AudioChannels = 1002,
        AudioSampleRate = 1003,
        MediaCodec = 1004,
        MediaDescription = 1005,
        MediaDuration = 1006,
        VideoHeight = 1007,
        VideoWidth = 1008,
        ImageHeight = 1009,
        ImageWidth = 1010,

        Physical = 2000,
        PhysicalHeight = 2001,
        PhysicalWidth = 2002,
    }
}
