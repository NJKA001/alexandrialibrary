using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public enum YouTubeStateReasonCode
    {
        unspecified = 0,

        /// <summary>
        /// The video is not available in the user's region.
        /// </summary>
        requesterRegion,
        
        /// <summary>
        ///  The video is not and, based on the content owner's current preferences, 
        ///  will not be available to play in non-browser devices, such as mobile phones.
        /// </summary>
        limitedSyndication,
        
        /// <summary>
        /// The video owner has restricted access to the video. This reasonCode signals that a video in a feed, 
        /// such as a playlist or favorite videos feed, has been made a private video by the video's owner and 
        /// is therefore unavailable.
        /// </summary>
        @private,

        /// <summary>
        /// The video commits a copyright infringement.
        /// </summary>
        copyright,

        /// <summary>
        /// The video contains inappropriate content.
        /// </summary>
        inappropriate,

        /// <summary>
        /// The video is a duplicate of another uploaded video.
        /// </summary>
        duplicate,

        /// <summary>
        /// The video commits a terms of use violate.
        /// </summary>
        termsOfUse,

        /// <summary>
        /// The account associated with the video has been suspended.
        /// </summary>
        suspended,

        /// <summary>
        /// The video exceeds the maximum duration of 10 minutes.
        /// </summary>
        tooLong,

        /// <summary>
        /// The video has been blocked by the content owner.
        /// </summary>
        blocked,
        
        /// <summary>
        /// YouTube is unable to convert the video file.
        /// </summary>
        cantProcess,

        /// <summary>
        /// The uploaded video is in an invalid file format.
        /// </summary>
        invalidFormat,

        /// <summary>
        /// The video uses an unsupported codec.
        /// </summary>
        unsupportedCodec,
        
        /// <summary>
        /// The uploaded file is empty.
        /// </summary>
        empty,

        /// <summary>
        /// The uploaded file is too small.
        /// </summary>
        tooSmall
    }
}
