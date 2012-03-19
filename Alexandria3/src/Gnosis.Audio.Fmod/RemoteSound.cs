using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Gnosis.Audio.Fmod
{
	public class RemoteSound : IAudioStream
	{
		#region Constructors
		public RemoteSound(string path)
		{
			this.path = path;
			sound = SoundSystemFactory.DefaultSoundSystem.CreateStream(path, (Modes.Software|Modes.Fmod2D|Modes.CreateStream|Modes.IgnoreTags));
		}
		#endregion

		#region Finalizer
		~RemoteSound()
		{
			Dispose(false);
		}
		#endregion

		#region IDisposable Members
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					this.sound.Dispose();
					this.sound = null;
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
		private string path;
		private Sound sound;
		private bool disposed;
		private BufferState bufferState = BufferState.None;
		private NetworkState networkState = NetworkState.None;
		private PlaybackState playbackState = PlaybackState.None;
		private SeekState seekState = SeekState.None;
		private EventHandler<MediaStateChangedEventArgs> bufferStateChanged;
		private EventHandler<MediaStateChangedEventArgs> networkStateChanged;
		private EventHandler<MediaStateChangedEventArgs> playbackStateChanged;
		private EventHandler<MediaStateChangedEventArgs> seekStateChanged;
		private EventHandler<AudioStateChangedEventArgs> volumeChanged;
		#endregion
		
		#region IAudioStream Members
		public bool IsMuted
		{
			get { return sound.Channel.Mute; }
			set {
				if (sound.Channel.Mute != value) {
					sound.Channel.Mute = value;
					if (VolumeChanged != null)
						VolumeChanged(this, new AudioStateChangedEventArgs(Volume, IsMuted));
				}
			}
		}

		public float Volume
		{
			get { return sound.Channel.Volume; }
			set {
				if (sound.Channel.Volume != value) {
					sound.Channel.Volume = value;
					if (VolumeChanged != null)
						VolumeChanged(this, new AudioStateChangedEventArgs(Volume, IsMuted));
				}
			}
		}

		public EventHandler<AudioStateChangedEventArgs> VolumeChanged
		{
			get { return volumeChanged; }
			set { volumeChanged = value; }
		}
		#endregion

		#region IMediaStream Members
		public BufferState BufferState
		{
			get { return bufferState; }
		}

		public bool CanPlay
		{
			get { return true; }
		}

		public bool CanRead
		{
			get { return true; }
		}

		public bool CanSeek
		{
			get { return false; }
		}

		public bool CanSetElapsed
		{
			get { return false; }
		}

		public bool CanSetPosition
		{
			get { return false; }
		}

		public bool CanTimeout
		{
			get { return true; }
		}

		public bool CanWrite
		{
			get { return false; }
		}

		public TimeSpan Duration
		{
			get { return sound.Duration; }
		}

		public TimeSpan Elapsed
		{
			get { return new TimeSpan(0, 0, 0, 0, (int)sound.Channel.Position); }
			set { throw new InvalidOperationException("This stream does not support setting the elapsed time"); }
		}

		public void Flush()
		{
		}

		public long Length
		{
			get
			{
				sound.LengthUnit = TimeUnits.RawByte;
				return (long)sound.FmodLength;
			}
		}

		public NetworkState NetworkState
		{
			get { return networkState; }
		}

		public EventHandler<MediaStateChangedEventArgs> BufferStateChanged
		{
			get { return bufferStateChanged; }
			set { bufferStateChanged = value; }
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

		public string Path
		{
			get { return path; }
		}

		public void Pause()
		{
			lock(sound)
			{
				sound.Pause();
				RefreshPlaybackState();
			}
		}

		public float PercentBuffered
		{
			get { return (float)sound.PercentBuffered; }
		}

		public void Play()
		{
			lock(sound)
			{
				sound.Play();
				RefreshPlaybackState();
			}
		}

		public PlaybackState PlaybackState
		{
			get { return playbackState; }
		}

		public long Position
		{
			get { return (long)sound.Channel.PositionInBytes; }
			set { throw new InvalidOperationException("This stream cannot be positioned"); }
		}

		public void RefreshBufferState()
		{
			BufferState nextBufferState = bufferState;
		
			if (sound != null)
			{
				if (sound.BufferIsStarving)
				{
					nextBufferState = BufferState.Starving;
				}
				else
				{
					if (sound.OpenState == OpenState.Loading)
						nextBufferState = BufferState.Loading;
					else if (sound.OpenState == OpenState.Buffering)
					{
						if (sound.PercentBuffered < 100)
							nextBufferState = BufferState.Buffering;
						else nextBufferState = BufferState.Full;
					}
					else nextBufferState = BufferState.None;
				}
			}
			else nextBufferState = BufferState.None;

			if (bufferState != nextBufferState)
			{
				bufferState = nextBufferState;
				if (BufferStateChanged != null)
					BufferStateChanged(this, new MediaStateChangedEventArgs(BufferState, NetworkState, PlaybackState, SeekState));
			}
		}
		
		public void RefreshNetworkState()
		{
			NetworkState nextNetworkState = networkState;
		
			if (sound != null && sound.Channel != null)
			{
				if (sound.OpenState == OpenState.Connecting)
					nextNetworkState = NetworkState.Connecting;
				else if (sound.OpenState == OpenState.Ready)
					nextNetworkState = NetworkState.Streaming;
				else if (sound.OpenState == OpenState.Error)
					nextNetworkState = NetworkState.Error;
				else nextNetworkState = NetworkState.None;
			}
			else nextNetworkState = NetworkState.None;

			if (networkState != nextNetworkState)
			{
				networkState = nextNetworkState;
				if (NetworkStateChanged != null)
					NetworkStateChanged(this, new MediaStateChangedEventArgs(BufferState, NetworkState, PlaybackState, SeekState));
			}
		}

		public void RefreshPlaybackState()
		{
			PlaybackState nextPlaybackState = playbackState;
			
			if (sound != null && sound.Channel != null)
			{
				if (sound.Channel.IsPlaying)
				{
					if (sound.Channel.Paused)
						nextPlaybackState = PlaybackState.Paused;
					else nextPlaybackState = PlaybackState.Playing;
				}
				else nextPlaybackState = PlaybackState.Stopped;
			}
			else nextPlaybackState = PlaybackState.None;

			if (playbackState != nextPlaybackState)
			{
				playbackState = nextPlaybackState;
				if (PlaybackStateChanged != null)
					PlaybackStateChanged(this, new MediaStateChangedEventArgs(BufferState, NetworkState, PlaybackState, SeekState));
			}
		}
		
		public void RefreshSeekState()
		{
		}

		public SeekState SeekState
		{
			get { return seekState; }
		}

		public int StreamIndex
		{
			get { return 0; }
			set { }
		}

		/// <summary>
		/// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read
		/// </summary>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>		
		/// <exception cref="System.ArgumentException"/>
		/// <exception cref="System.ArgumentNullException"/>
		/// <exception cref="System.OutOfMemoryException"/>
		public int Read(byte[] buffer, int offset, int count)
		{
			if (buffer != null)
			{
				if (offset + count <= buffer.Length)
				{
					lock(sound)
					{
						Stop();

						IntPtr bufferHandle = IntPtr.Zero;
						bufferHandle = Marshal.AllocHGlobal(count);				

						int bytesRead = Convert.ToInt32(sound.Read(bufferHandle, (uint)count));
									
						if (bufferHandle != IntPtr.Zero && bytesRead > 0)
						{
							byte[] data = new byte[bytesRead];
							Marshal.PtrToStructure(bufferHandle, data);
							Array.Copy(data, 0, buffer, offset, bytesRead);
							Marshal.FreeHGlobal(bufferHandle);
							return bytesRead;
						}
						else return 0;
					}
				}
				else throw new ArgumentException("The sum of offset and count is larger than the buffer length.");
			}
			else throw new ArgumentNullException("buffer");
		}

		public void Resume()
		{
			lock(sound)
			{
				sound.Resume();
				RefreshPlaybackState();
			}
		}

		public long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException("This stream is not seekable");
		}

		public void SetLength(long value)
		{
			throw new InvalidOperationException("This stream does not support setting the length");
		}

		public void Stop()
		{
			lock(sound)
			{
				sound.Stop();
				RefreshPlaybackState();
			}
		}

		public void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("This stream is read-only");
		}
		#endregion
	}
}
