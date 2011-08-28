using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Document.Xml.YouTube
{
    public enum YouTubeStatusValue
    {
        unspecified = 0,

        /// <summary>
        /// The authenticated user and the contact have marked each other as friends.
        /// </summary>
        accepted,

        /// <summary>
        /// The contact has asked to be added to the authenticated user's contact list, but the request has not yet been accepted (or rejected).
        /// </summary>
        requested,

        /// <summary>
        /// The authenticated user has asked to be added to the contact's contact list, but the request has not yet been accepted or rejected.
        /// For live events: the event has not started yet.
        /// </summary>
        pending,

        /// <summary>
        /// The event has already started but has not yet ended.
        /// </summary>
        active,

        /// <summary>
        /// The event has already ended.
        /// </summary>
        completed,

        /// <summary>
        /// The event has not yet started and will start later than its originally scheduled time.
        /// </summary>
        delayed,
        
        /// <summary>
        /// The event, though scheduled, will not happen.
        /// </summary>
        cancelled,

        /// <summary>
        /// YouTube will not stream the event.
        /// </summary>
        rejected
    }
}
