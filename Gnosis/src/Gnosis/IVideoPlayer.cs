using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IVideoPlayer
    {
        PlaybackState PlaybackState { get; }
        TimeSpan Elapsed { get; }
        TimeSpan Duration { get; }

        void Initialize(ILogger logger, Func<IVideoHost> getHost);
        void Load(Uri location);
        
        void Play();
        void Pause();
        void Resume();
        void Stop();
    }
}
