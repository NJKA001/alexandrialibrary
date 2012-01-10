using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;

using Gnosis.Audio;

namespace Gnosis.Tasks
{
    public class PlaylistTask
        : TaskBase<TaskItem>
    {
        public PlaylistTask(ILogger logger, IAudioPlayer audioPlayer, IVideoPlayer videoPlayer, TaskItem item, TimeSpan delay, Func<TaskItem> getPreviousItem, Func<TaskItem> getNextItem)
            : base(logger, item)
        {
            if (audioPlayer == null)
                throw new ArgumentNullException("audioPlayer");
            if (getPreviousItem == null)
                throw new ArgumentNullException("getPreviousItem");
            if (getNextItem == null)
                throw new ArgumentNullException("getNextItem");

            this.audioPlayer = audioPlayer;
            this.videoPlayer = videoPlayer;
            this.delay = delay;
            this.getPreviousItem = getPreviousItem;
            this.getNextItem = getNextItem;

            videoPlayer.AddEndedCallback(() => CurrentStreamEnded());
            videoPlayer.AddPlayedCallback(() => PlayCurrentStream());
            videoPlayer.AddPausedCallback(() => PauseCurrentStream());
            videoPlayer.AddPausedCallback(() => PauseVideo());
            videoPlayer.AddResumedCallback(() => ResumeCurrentStream());
            videoPlayer.AddResumedCallback(() => ResumeVideo());
            videoPlayer.AddStoppedCallback(() => StopCurrentStream());
            videoPlayer.AddStoppedCallback(() => StopVideo());
            videoPlayer.AddPreviousItemCallback(() => PreviousItem());
            videoPlayer.AddNextItemCallback(() => NextItem());

            AddPausedCallback(() => PauseCurrentStream());
            AddResumedCallback(() => ResumeCurrentStream());
            AddStoppedCallback(() => StopCurrentStream());

            this.timer = new Timer(200);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        private readonly IAudioPlayer audioPlayer;
        private readonly IVideoPlayer videoPlayer;
        private readonly Timer timer;
        private readonly TimeSpan delay;
        private readonly Func<TaskItem> getPreviousItem;
        private readonly Func<TaskItem> getNextItem;

        //private readonly IDictionary<string, IAudioStream> audioStreams = new Dictionary<string, IAudioStream>();

        private bool streamEnded = false;
        private int errorCount;
        private int errorMax = 0;

        private void PauseVideo()
        {
            Pause();
        }

        private void ResumeVideo()
        {
            Resume();
        }

        private void StopVideo()
        {
            Stop();
        }

        private void LoadAudioStream(Uri target)
        {
            var audioStream = audioPlayer.AudioStreamFactory.CreateAudioStream(Item.Target);
            audioPlayer.Load(audioStream);
        }

        private void LoadVideoStream(Uri target)
        {
            videoPlayer.Load(target);
        }

        private void PlayAudioStream()
        {
            audioPlayer.Play();
        }

        private void PlayVideoStream()
        {
            videoPlayer.Play();
        }

        private void PlayCurrentStream()
        {
            if (Item.Target != null && Item.TargetType != null)
            {
                if (Item.TargetType.Type == MediaType.TypeAudio)
                {
                    LoadAudioStream(Item.Target);
                    PlayAudioStream();
                    UpdateAudioProgress();
                }
                else if (Item.TargetType.Type == MediaType.TypeVideo)
                {
                    if (videoPlayer.PlaybackState != PlaybackState.Playing)
                    {
                        LoadVideoStream(Item.Target);
                        PlayVideoStream();
                        UpdateVideoProgress();
                    }
                }
            }
            else
            {
                Stop();
                UpdateProgress(0, 100, "Stopped Playback");
            }
        }

        private void PauseCurrentStream()
        {
            if (Item.Target != null && Item.TargetType != null)
            {
                if (Item.TargetType.Type == MediaType.TypeAudio)
                {
                    if (audioPlayer.CurrentAudioStream != null && audioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Playing)
                    {
                        audioPlayer.CurrentAudioStream.Pause();
                        //audioPlayer.Pause();
                    }
                }
                else if (Item.TargetType.Type == MediaType.TypeVideo)
                {
                    if (videoPlayer.PlaybackState == PlaybackState.Playing)
                    {
                        videoPlayer.Pause();
                    }
                }
            }
        }

        private void ResumeCurrentStream()
        {
            if (Item.Target != null && Item.TargetType != null)
            {
                if (Item.TargetType.Type == MediaType.TypeAudio)
                {
                    if (audioPlayer.CurrentAudioStream != null && audioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Paused)
                    {
                        //audioPlayer.Resume();
                        audioPlayer.CurrentAudioStream.Resume();
                    }
                }
                else if (Item.TargetType.Type == MediaType.TypeVideo)
                {
                    if (videoPlayer.PlaybackState == PlaybackState.Paused)
                    {
                        videoPlayer.Resume();
                    }
                }
            }
        }

        private void StopCurrentStream()
        {
            if (Item.Target != null && Item.TargetType != null)
            {
                if (Item.TargetType.Type == MediaType.TypeAudio)
                {
                    if (audioPlayer.CurrentAudioStream != null && audioPlayer.CurrentAudioStream.PlaybackState != PlaybackState.Stopped)
                    {
                        audioPlayer.CurrentAudioStream.Stop();
                    }
                    //audioPlayer.Stop();
                }
                else if (Item.TargetType.Type == MediaType.TypeVideo)
                {
                    if (videoPlayer.PlaybackState != PlaybackState.Stopped)
                    {
                        videoPlayer.Stop();
                    }
                }
            }
        }

        private void UpdateAudioProgress()
        {
            if (Item.Target != null)
            {
                var count = (int)Math.Ceiling(audioPlayer.Elapsed.TotalMilliseconds);
                var maximum = (int)Math.Ceiling(audioPlayer.Duration.TotalMilliseconds);
                UpdateProgress(count, maximum, "Playing Audio: " + Item.Target);
            }
        }

        private void UpdateVideoProgress()
        {
            if (Item.Target != null)
            {
                var count = (int)Math.Ceiling(videoPlayer.Elapsed.TotalMilliseconds);
                var maximum = (int)Math.Ceiling(videoPlayer.Duration.TotalMilliseconds);
                UpdateProgress(count, maximum, "Playing Video: " + Item.Target);
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Status == TaskStatus.Running)
            {
                if (Item.Target != null && Item.TargetType != null)
                {
                    if (Item.TargetType.Type == MediaType.TypeAudio)
                    {
                        if (audioPlayer.CurrentAudioStream != null && !audioPlayer.SeekIsPending)
                        {
                            if (audioPlayer.CurrentAudioStream.Elapsed == audioPlayer.CurrentAudioStream.Duration)
                            {
                                if (!streamEnded)
                                {
                                    streamEnded = true;
                                    CurrentStreamEnded();
                                }
                            }
                            else
                            {
                                streamEnded = false;
                                UpdateAudioProgress();
                            }
                        }
                    }
                    else if (Item.TargetType.Type == MediaType.TypeVideo)
                    {
                        if (videoPlayer.Elapsed > TimeSpan.Zero && videoPlayer.Elapsed == videoPlayer.Duration)
                        {
                            if (!streamEnded)
                            {
                                streamEnded = true;
                                CurrentStreamEnded();
                            }
                        }
                        else
                        {
                            streamEnded = false;
                            UpdateVideoProgress();
                        }
                    }
                }
            }
        }

        private void CurrentStreamEnded()
        {
            if (Status == TaskStatus.Running)
            {
                var next = getNextItem();
                UpdateItem(next);
                UpdateResults(next);
                PlayCurrentStream();
            }
        }

        protected override void DoWork()
        {
            try
            {
                if (Item.Target != null && Item.TargetType != null)
                {
                    if (Item.TargetType.Type == MediaType.TypeAudio)
                    {
                        LoadAudioStream(Item.Target);
                        PlayAudioStream();
                    }
                    else if (Item.TargetType.Type == MediaType.TypeVideo)
                    {
                        LoadVideoStream(Item.Target);
                        PlayVideoStream();
                    }
                }
            }
            catch (Exception ex)
            {
                errorCount++;
                var description = Item.Target != null ? "Error Loading Stream: " + Item.Target.ToString() : "Error Loading Stream";
                UpdateError(errorCount, errorMax, description, ex);
            }
        }

        protected override void WorkCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
        }

        private bool IsPlaying()
        {
            if (Item.TargetType != null)
            {
                if (Item.TargetType.Type == MediaType.TypeAudio)
                {
                    return audioPlayer.CurrentAudioStream != null && audioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Playing;
                }
                else if (Item.TargetType.Type == MediaType.TypeVideo)
                {
                    return videoPlayer.PlaybackState == PlaybackState.Playing;
                }
            }

            return false;
        }

        public override void PreviousItem()
        {
            var previous = getPreviousItem();

            var isPlaying = IsPlaying();
            StopCurrentStream();
            UpdateItem(previous);

            if (isPlaying)
                PlayCurrentStream();
        }

        public override void NextItem()
        {
            var next = getNextItem();

            var isPlaying = IsPlaying();
            StopCurrentStream();
            UpdateItem(next);

            if (isPlaying)
                PlayCurrentStream();
        }

        public override void BeginProgressUpdate()
        {
            if (Item.TargetType != null)
            {
                if (Item.TargetType.Type == MediaType.TypeAudio)
                {
                    if (audioPlayer.CurrentAudioStream != null)
                    {
                        audioPlayer.BeginSeek();
                    }
                }
                else if (Item.TargetType.Type == MediaType.TypeVideo)
                {
                    //videoPlayer.BeginSeek();
                }
            }
        }

        public override void UpdateProgress(int value)
        {
            if (Item.TargetType != null)
            {
                if (Item.TargetType.Type == MediaType.TypeAudio)
                {
                    if (audioPlayer.CurrentAudioStream != null)
                    {
                        audioPlayer.Seek(value);
                    }
                }
                else if (Item.TargetType.Type == MediaType.TypeVideo)
                {
                    //videoPlayer.Seek(value);
                }
            }
        }
    }
}
