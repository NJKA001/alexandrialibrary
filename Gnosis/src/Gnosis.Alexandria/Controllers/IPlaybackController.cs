using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models;

namespace Gnosis.Alexandria.Controllers
{
    public interface IPlaybackController
    {
        ITrack CurrentTrack { get; }
        TimeSpan CurrentDuration { get; }
        TimeSpan CurrentElapsed { get; }
        IPlaybackStatus Status { get; }

        EventHandler<EventArgs> CurrentTrackPlayed { get; set; }
        EventHandler<EventArgs> CurrentTrackPaused { get; set; }
        EventHandler<EventArgs> CurrentTrackStopped { get; set; }
        EventHandler<EventArgs> CurrentTrackEnded { get; set; }

        void BeginSeek();
        void Reset();
        void Play();
        void Seek(int position);
        void Load(ITrack track);
        void Stop();
    }
}
