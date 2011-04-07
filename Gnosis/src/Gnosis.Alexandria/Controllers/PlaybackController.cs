using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

using log4net;

using Gnosis.Alexandria.Models;
using Gnosis.Core;
using Gnosis.Fmod;
using System.Net;

namespace Gnosis.Alexandria.Controllers
{
    public class PlaybackController : IPlaybackController
    {
        public PlaybackController(ITrackController trackController)
        {
            this.trackController = trackController;

            playbackTimer.Elapsed += new ElapsedEventHandler(PlaybackTimer_Elapsed);
            playbackTimer.Start();

            player.CurrentAudioStreamEnded += new EventHandler<EventArgs>(CurrentAudioStreamEnded);
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(PlaybackController));
        private readonly ITrackController trackController;
        private readonly IAudioPlayer player = new AudioPlayer(new Fmod.AudioStreamFactory()) { PlayToggles = true };
        private readonly Timer playbackTimer = new Timer(1000);
        private readonly IPlaybackStatus playbackStatus = new PlaybackStatus();
        private ITrack currentTrack;
        private bool isAboutToPlay = false;
        private bool hasSeek = false;
        
        private void DoCurrentTrackEnded()
        {
            if (CurrentTrackEnded != null)
                CurrentTrackEnded(this, EventArgs.Empty);
        }

        private void CurrentAudioStreamEnded(object sender, EventArgs args)
        {
            DoCurrentTrackEnded();
        }

        private Uri GetCurrentUri()
        {
            return (currentTrack != null) ? new Uri(currentTrack.Path, UriKind.Absolute) : null;
        }

        private void UpdatePlaybackStatus()
        {
            if (player != null && player.CurrentAudioStream != null && currentTrack != null)
            {
                player.RefreshPlayerStates();
                var elapsed = player.CurrentAudioStream.Elapsed;
                currentTrack.ElapsedLabel = string.Format("{0}:{1:00}", elapsed.Minutes, elapsed.Seconds);

                if (!player.SeekIsPending)
                {
                    currentTrack.Elapsed = elapsed.TotalSeconds;

                    if (!isAboutToPlay && !hasSeek)
                    {
                        if (!currentTrack.HasClipAt(elapsed))
                        {
                            var nextClip = currentTrack.GetNextClipFrom(elapsed);
                            if (nextClip != null)
                            {
                                player.Mute();
                                player.BeginSeek();
                                player.Seek(Convert.ToInt32(nextClip.Item1.TotalMilliseconds));
                                player.Unmute();
                            }
                            else
                            {
                                Stop();
                                DoCurrentTrackEnded();
                            }
                        }
                    }
                }
            }
        }

        private void PlaybackTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdatePlaybackStatus();
        }

        public ITrack CurrentTrack
        {
            get { return currentTrack; }
        }

        public TimeSpan CurrentDuration
        {
            get { return (player != null && player.CurrentAudioStream != null) ? player.CurrentAudioStream.Duration : TimeSpan.Zero; }
        }

        public TimeSpan CurrentElapsed
        {
            get { return (player != null && player.CurrentAudioStream != null) ? player.CurrentAudioStream.Elapsed : TimeSpan.Zero; }
        }

        public IPlaybackStatus Status
        {
            get { return playbackStatus; }
        }

        public EventHandler<EventArgs> CurrentTrackEnded { get; set; }

        public void BeginSeek()
        {
            if (player != null && player.CurrentAudioStream != null && currentTrack != null)
            {
                player.BeginSeek();
            }
        }

        public void Reset()
        {
            if (currentTrack != null)
            {
                currentTrack.PlaybackStatus = null;
            }

            Stop();
            currentTrack = null;
        }

        public void Play()
        {
            try
            {
                if (currentTrack != null)
                {
                    var currentUri = GetCurrentUri();
                    Uri cachedUri = null;
                    if (!currentUri.IsFile)
                    {
                        cachedUri = trackController.GetCachedUri(currentTrack.Id);
                        if (cachedUri == null)
                        {
                            trackController.CacheTrack(currentTrack);
                            cachedUri = trackController.GetCachedUri(currentTrack.Id);
                        }
                    }

                    if (player.CurrentAudioStream == null)
                    {
                        if (cachedUri != null)
                        {
                            currentTrack.CachePath = cachedUri.LocalPath;
                            player.LoadAudioStream(cachedUri);
                        }
                        else
                        {
                            player.LoadAudioStream(new Uri(currentTrack.Path, UriKind.Absolute));
                        }
                    }
                    else
                    {
                        var streamUri = new Uri(player.CurrentAudioStream.Path, UriKind.Absolute);

                        if (cachedUri != null)
                        {
                            if (cachedUri != streamUri)
                            {
                                currentTrack.CachePath = cachedUri.LocalPath;
                                player.Stop();
                                player.LoadAudioStream(cachedUri);
                            }
                        }
                        else
                        {
                            if (currentUri != streamUri)
                            {
                                player.Stop();
                                player.LoadAudioStream(new Uri(currentTrack.Path, UriKind.Absolute));
                            }
                        }
                    }

                    currentTrack.DurationLabel = string.Format("{0}:{1:00}", player.CurrentAudioStream.Duration.Minutes, player.Duration.Seconds);

                    currentTrack.ElapsedLabel = string.Format("{0}:{1:00}", player.Elapsed.Minutes, player.Elapsed.Seconds);

                    isAboutToPlay = true;

                    bool startAtZero = currentTrack.HasClipAt(TimeSpan.Zero);
                    if (!startAtZero)
                    {
                        //NOTE: We're going to be seeking so we want to mute to avoid any popping
                        player.Mute();
                    }

                    player.Play();

                    if (!startAtZero)
                    {
                        var clip = currentTrack.Clips.FirstOrDefault();
                        if (clip != null)
                        {
                            player.BeginSeek();
                            player.Seek(Convert.ToInt32(clip.Item1.TotalMilliseconds));
                        }
                        player.Unmute();
                    }

                    isAboutToPlay = false;

                    playbackStatus.IsPlaying = (player.CurrentAudioStream.PlaybackState == PlaybackState.Playing);
                }
            }
            catch (Exception ex)
            {
                log.Error("PlaybackController.Play", ex);
            }
        }

        public void Seek(int position)
        {
            if (player != null && player.CurrentAudioStream != null && currentTrack != null && position >= 0)
            {
                hasSeek = true;
                player.Seek(position);
                UpdatePlaybackStatus();
            }
        }

        public void Load(ITrack track)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            if (currentTrack != null)
            {
                currentTrack.PlaybackStatus = null;
            }

            currentTrack = track;
            hasSeek = false;

            currentTrack.PlaybackStatus = "Now Playing";
        }

        public void Stop()
        {
            if (player != null)
            {
                player.Stop();
            }
        }
    }
}
