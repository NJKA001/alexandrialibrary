using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis
{
    public interface IVideoPlayer
    {
        bool SeekIsPending { get; }
        PlaybackState PlaybackState { get; }
        TimeSpan Elapsed { get; }
        TimeSpan Duration { get; }
        bool IsMuted { get; }

        void Initialize(ILogger logger, Func<IVideoHost> getHost);
        void Load(Uri location);
        
        void Play();
        void Pause();
        void Resume();
        void Stop();
        void Mute();
        void Unmute();
        void SetVolume(int volume);

        void AddLoadedCallback(Action callback);
        void AddPlayedCallback(Action callback);
        void AddPausedCallback(Action callback);
        void AddResumedCallback(Action callback);
        void AddStoppedCallback(Action callback);
        void AddEndedCallback(Action callback);
        void AddVolumeChangedCallback(Action callback);
        void AddPreviousItemCallback(Action callback);
        void AddNextItemCallback(Action callback);
    }
}
