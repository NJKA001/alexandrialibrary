#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;

namespace Gnosis.Audio.Fmod
{
	public class AudioPlayer : IAudioPlayer
	{
		#region Constructors
		public AudioPlayer()
		{
		}

        public AudioPlayer(IAudioStreamFactory audioStreamFactory)
        {
            this.audioStreamFactory = audioStreamFactory;
        }
		#endregion

		#region IDisposable Members
		~AudioPlayer()
		{
			Dispose(false);
		}

		protected void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					if (currentAudioStream != null)
					{
						currentAudioStream.Dispose();
						currentAudioStream = null;
					}
				}
			}
			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		#region Private Fields
		private bool disposed;
		private IAudioStreamFactory audioStreamFactory;
		private IAudioStream currentAudioStream;
		private EventHandler<EventArgs> currentAudioStreamChanged;
		private EventHandler<EventArgs> currentAudioStreamEnded;
		private EventHandler<MediaStateChangedEventArgs> bufferStateChanged;
		private EventHandler<MediaStateChangedEventArgs> networkStateChanged;
		private EventHandler<MediaStateChangedEventArgs> playbackStateChanged;
		private EventHandler<MediaStateChangedEventArgs> seekStateChanged;
		private EventHandler<AudioStateChangedEventArgs> volumeChanged;
		private bool muteToggles;
		private bool playToggles;
		private bool seekIsPending;
		private bool isPlaying;
		private bool isMuted;
		private float volume = 1.0f;
		#endregion

		#region Private Event Methods
		private void OnBufferStateChanged(object sender, MediaStateChangedEventArgs e)
		{
			if (BufferStateChanged != null)
				BufferStateChanged(this, e);
		}

		private void OnNetworkStateChanged(object sender, MediaStateChangedEventArgs e)
		{
			if (NetworkStateChanged != null)
				NetworkStateChanged(this, e);
		}

		private void OnPlaybackStateChanged(object sender, MediaStateChangedEventArgs e)
		{
			if (PlaybackStateChanged != null)
				PlaybackStateChanged(this, e);
		}

		private void OnSeekStateChanged(object sender, MediaStateChangedEventArgs e)
		{
			if (SeekStateChanged != null)
				SeekStateChanged(this, e);
		}

		private void OnVolumeChanged(object sender, AudioStateChangedEventArgs e)
		{
			if (VolumeChanged != null)
				VolumeChanged(this, e);
		}
		#endregion

		#region IAudioPlayer Members
		public IAudioStreamFactory AudioStreamFactory
		{
			get { return audioStreamFactory; }
			set { audioStreamFactory = value; }
		}

		public IAudioStream CurrentAudioStream
		{
			get { return currentAudioStream; }
		}

		public TimeSpan Duration
		{
			get { return (currentAudioStream != null) ? currentAudioStream.Duration : TimeSpan.Zero; }
		}

		public TimeSpan Elapsed
		{
			get { return (currentAudioStream != null) ? currentAudioStream.Elapsed : TimeSpan.Zero; }
		}

		public bool IsMuted
		{
			get { return isMuted; }
			//return (currentAudioStream != null) ? currentAudioStream.IsMuted : false; }
		}

		public bool MuteToggles
		{
			get { return muteToggles; }
			set { muteToggles = value; }
		}

		public bool PlayToggles
		{
			get { return playToggles; }
			set { playToggles = value; }
		}

		public bool SeekIsPending
		{
			get { return seekIsPending; }
		}

		public float Volume
		{
			get { return volume; }
			//return (currentAudioStream != null) ? currentAudioStream.Volume : 0f; }
		}

		public EventHandler<MediaStateChangedEventArgs> BufferStateChanged
		{
			get { return bufferStateChanged; }
			set { bufferStateChanged = value; }
		}

		public EventHandler<EventArgs> CurrentAudioStreamChanged
		{
			get { return currentAudioStreamChanged; }
			set { currentAudioStreamChanged = value; }
		}

		public EventHandler<EventArgs> CurrentAudioStreamEnded
		{
			get { return currentAudioStreamEnded; }
			set { currentAudioStreamEnded = value; }
		}

		public EventHandler<MediaStateChangedEventArgs> NetworkStateChanged
		{
			get { return networkStateChanged; }
			set { networkStateChanged = value; }
		}

		public EventHandler<MediaStateChangedEventArgs> PlaybackStateChanged
		{
			get { return playbackStateChanged; }
			set { playbackStateChanged = value; }
		}

		public EventHandler<MediaStateChangedEventArgs> SeekStateChanged
		{
			get { return seekStateChanged; }
			set { seekStateChanged = value; }
		}

		public EventHandler<AudioStateChangedEventArgs> VolumeChanged
		{
			get { return volumeChanged; }
			set { volumeChanged = value; }
		}

		public void BeginSeek()
		{
			seekIsPending = true;
		}

		public void CancelSeek()
		{
			seekIsPending = false;
		}

		public void Load(IAudioStream audioStream)
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Stop();
				currentAudioStream.Dispose();
			}

			currentAudioStream = audioStream;

			if (CurrentAudioStreamChanged != null)
				CurrentAudioStreamChanged(this, EventArgs.Empty);

			if (currentAudioStream != null)
			{
				currentAudioStream.Volume = volume;
				currentAudioStream.IsMuted = isMuted;
				currentAudioStream.BufferStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnBufferStateChanged);
				currentAudioStream.NetworkStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnNetworkStateChanged);
				currentAudioStream.PlaybackStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnPlaybackStateChanged);
				currentAudioStream.SeekStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnSeekStateChanged);
				currentAudioStream.VolumeChanged += new EventHandler<AudioStateChangedEventArgs>(OnVolumeChanged);
				RefreshPlayerStates();
			}
		}

		public void Load(Uri path)
		{
			if (path != null && audioStreamFactory != null)
			{
				IAudioStream audioStream = audioStreamFactory.CreateAudioStream(path);
				Load(audioStream);
			}
		}

		public void Mute()
		{
			if (currentAudioStream != null)
			{
				isMuted = (MuteToggles) ? !isMuted : true;
				currentAudioStream.IsMuted = isMuted;
				RefreshPlayerStates();
			}
		}

		public void Pause()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Pause();
				RefreshPlayerStates();
			}
		}

		public void Play()
		{
			if (currentAudioStream != null)
			{
				if (PlayToggles)
				{
					switch (currentAudioStream.PlaybackState)
					{
						case PlaybackState.Error:
							break;
						case PlaybackState.None:
						case PlaybackState.Stopped:
							currentAudioStream.Play();
							break;
						case PlaybackState.Paused:
							currentAudioStream.Resume();
							break;
						case PlaybackState.Playing:
							currentAudioStream.Pause();
							break;
						default:
							break;
					}
				}
				else
				{
					currentAudioStream.Play();
				}

				RefreshPlayerStates();
			}
		}

		public void RefreshPlayerStates()
		{
			if (currentAudioStream != null)
			{
				if (isPlaying && currentAudioStream.Elapsed == currentAudioStream.Duration)
				{
					if (CurrentAudioStreamEnded != null)
						CurrentAudioStreamEnded(this, EventArgs.Empty);
				}

				currentAudioStream.RefreshBufferState();
				currentAudioStream.RefreshNetworkState();
				currentAudioStream.RefreshPlaybackState();
				currentAudioStream.RefreshSeekState();
				isPlaying = (currentAudioStream.PlaybackState == PlaybackState.Playing);
				//isMuted = (currentAudioStream.IsMuted);
			}
			else
			{
				isPlaying = false;
			}
		}

		public void Resume()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Resume();
				RefreshPlayerStates();
			}
		}

		public void Seek(int position)
		{
			seekIsPending = false;

			if (currentAudioStream != null)
			{
				currentAudioStream.Elapsed = new TimeSpan(0, 0, 0, 0, position);
				RefreshPlayerStates();
			}
		}

		public void SetVolume(float volume)
		{
			if (currentAudioStream != null)
			{
				this.volume = volume;
				currentAudioStream.Volume = volume;
				RefreshPlayerStates();
			}
		}

		public void Stop()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Stop();
				RefreshPlayerStates();
			}
		}

		public void Unmute()
		{
			if (currentAudioStream != null)
			{
				isMuted = false;
				currentAudioStream.IsMuted = isMuted;
				RefreshPlayerStates();
			}
		}
		#endregion
	}
}
