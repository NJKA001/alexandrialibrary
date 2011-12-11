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

            audioPlayer.CurrentAudioStreamEnded += audioPlayer_CurrentAudioStreamEnded;

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

        private int errorCount;
        private int errorMax = 0;

        private void LoadAudioStream(Uri target)
        {
            //TODO: Implement a cache of audio streams
            //var key = Item.Target.ToString();
            //if (!audioStreams.ContainsKey(key))
            //{
                var audioStream = audioPlayer.AudioStreamFactory.CreateAudioStream(Item.Target);
                //audioStreams.Add(key, audioStream);
                audioPlayer.Load(audioStream);
            //}
        }

        private void PlayAudioStream()
        {
            audioPlayer.Play();
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
            }
            else
            {
                Stop();
                UpdateProgress(0, 100, "Stopped Playback");
            }
        }

        private void PauseCurrentStream()
        {
            if (audioPlayer.CurrentAudioStream != null && audioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Playing)
            {
                audioPlayer.CurrentAudioStream.Pause();
            }
        }

        private void ResumeCurrentStream()
        {
            if (audioPlayer.CurrentAudioStream != null && audioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Paused)
            {
                audioPlayer.CurrentAudioStream.Resume();
            }
        }

        private void StopCurrentStream()
        {
            if (audioPlayer.CurrentAudioStream != null)
            {
                audioPlayer.CurrentAudioStream.Stop();
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
                            UpdateAudioProgress();
                        }
                    }
                }
            }
        }

        private void audioPlayer_CurrentAudioStreamEnded(object sender, EventArgs e)
        {
            if (Status == TaskStatus.Running)
            {
                var next = getNextItem();
                UpdateItem(next);
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
                        videoPlayer.Load(Item.Target);
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

        public override void PreviousItem()
        {
            var previous = getPreviousItem();

            var isPlaying = audioPlayer.CurrentAudioStream != null && audioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Playing;
            StopCurrentStream();
            UpdateItem(previous);

            if (isPlaying)
                PlayCurrentStream();
        }

        public override void NextItem()
        {
            var next = getNextItem();

            var isPlaying = audioPlayer.CurrentAudioStream != null && audioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Playing;
            StopCurrentStream();
            UpdateItem(next);

            if (isPlaying)
                PlayCurrentStream();
        }

        public override void BeginProgressUpdate()
        {
            if (audioPlayer.CurrentAudioStream != null)
            {
                audioPlayer.BeginSeek();
            }
        }

        public override void UpdateProgress(int value)
        {
            if (audioPlayer.CurrentAudioStream != null)
            {
                audioPlayer.Seek(value);
            }
        }
    }
}
