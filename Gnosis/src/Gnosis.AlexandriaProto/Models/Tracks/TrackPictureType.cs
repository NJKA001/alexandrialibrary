using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Tracks
{
    public enum TrackPictureType
    {
        /// <summary>
        ///    The picture is of a type other than those specified.
        /// </summary>
        Other = 0x00,

        /// <summary>
        ///    The picture is a 32x32 PNG image that should be used when
        ///    displaying the file in a browser.
        /// </summary>
        FileIcon = 0x01,

        /// <summary>
        ///    The picture is of an icon different from <see
        ///    cref="FileIcon" />.
        /// </summary>
        OtherFileIcon = 0x02,

        /// <summary>
        ///    The picture is of the front cover of the album.
        /// </summary>
        FrontCover = 0x03,

        /// <summary>
        ///    The picture is of the back cover of the album.
        /// </summary>
        BackCover = 0x04,

        /// <summary>
        ///    The picture is of a leaflet page including with the
        ///    album.
        /// </summary>
        LeafletPage = 0x05,

        /// <summary>
        ///    The picture is of the album or disc itself.
        /// </summary>
        Media = 0x06,
        // Image from the album itself

        /// <summary>
        ///    The picture is of the lead artist or soloist.
        /// </summary>
        LeadArtist = 0x07,

        /// <summary>
        ///    The picture is of the artist or performer.
        /// </summary>
        Artist = 0x08,

        /// <summary>
        ///    The picture is of the conductor.
        /// </summary>
        Conductor = 0x09,

        /// <summary>
        ///    The picture is of the band or orchestra.
        /// </summary>
        Band = 0x0A,

        /// <summary>
        ///    The picture is of the composer.
        /// </summary>
        Composer = 0x0B,

        /// <summary>
        ///    The picture is of the lyricist or text writer.
        /// </summary>
        Lyricist = 0x0C,

        /// <summary>
        ///    The picture is of the recording location or studio.
        /// </summary>
        RecordingLocation = 0x0D,

        /// <summary>
        ///    The picture is one taken during the track's recording.
        /// </summary>
        DuringRecording = 0x0E,

        /// <summary>
        ///    The picture is one taken during the track's performance.
        /// </summary>
        DuringPerformance = 0x0F,

        /// <summary>
        ///    The picture is a capture from a movie screen.
        /// </summary>
        MovieScreenCapture = 0x10,

        /// <summary>
        ///    The picture is of a large, colored fish.
        /// </summary>
        ColoredFish = 0x11,

        /// <summary>
        ///    The picture is an illustration related to the track.
        /// </summary>
        Illustration = 0x12,

        /// <summary>
        ///    The picture contains the logo of the band or performer.
        /// </summary>
        BandLogo = 0x13,

        /// <summary>
        ///    The picture is the logo of the publisher or record
        ///    company.
        /// </summary>
        PublisherLogo = 0x14
    }
}
