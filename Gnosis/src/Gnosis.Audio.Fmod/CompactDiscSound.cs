using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Gnosis.Core;

namespace Gnosis.Audio.Fmod
{
	public class CompactDiscSound : IDisposable, IAudioStream
	{
		#region Constructors
		public CompactDiscSound(string path)
		{
			this.path = path;
			sound = SoundSystemFactory.DefaultSoundSystem.CreateSound(path);
		}
		#endregion
		
		#region Finalizer
		~CompactDiscSound()
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
		private int streamIndex;
		private BufferState bufferState = BufferState.None;
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
			get {
				if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null)
					return sound.CurrentSubSound.Channel.Mute;
				else return sound.Channel.Mute;
			}
			set {
				if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null) {
					if (sound.CurrentSubSound.Channel.Mute != value) {
						sound.CurrentSubSound.Channel.Mute = value;
						if (VolumeChanged != null)
							VolumeChanged(this, new AudioStateChangedEventArgs(sound.CurrentSubSound.Channel.Volume, sound.CurrentSubSound.Channel.Mute));
					}
				}
				else if (sound.Channel.Mute != value) {
					sound.Channel.Mute = value;
					if (VolumeChanged != null)
						VolumeChanged(this, new AudioStateChangedEventArgs(Volume, IsMuted));
				}
			}
		}

		public float Volume
		{
			get {
				if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null)
					return sound.CurrentSubSound.Channel.Volume;
				else return sound.Channel.Volume;
			}
			set {
				if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null) {
					if (sound.CurrentSubSound.Channel.Volume != value) {
						sound.CurrentSubSound.Channel.Volume = value;
						if (VolumeChanged != null)
							VolumeChanged(this, new AudioStateChangedEventArgs(sound.CurrentSubSound.Channel.Volume, sound.CurrentSubSound.Channel.Mute));
					}
				}
				else if (sound.Channel.Volume != value) {
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
			get { return true; }
		}

		public bool CanSetElapsed
		{
			get { return true; }
		}

		public bool CanSetPosition
		{
			get { return true; }
		}

		public bool CanTimeout
		{
			get { return false; }
		}

		public bool CanWrite
		{
			get { return false; }
		}

		public TimeSpan Duration
		{
			get
			{
				if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null)
					return sound.CurrentSubSound.Duration;
				else return sound.Duration;
			}
		}

		public TimeSpan Elapsed
		{
			get {
				if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null)
					return new TimeSpan(0, 0, 0, 0, (int)sound.CurrentSubSound.Channel.Position);
				else return new TimeSpan(0, 0, 0, 0, (int)sound.Channel.Position);
			}
			set {
				lock (sound)
				{
					if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null)
						sound.CurrentSubSound.Channel.Position = (uint)value.TotalMilliseconds;
					else sound.Channel.Position = (uint)value.TotalMilliseconds;
				}
			}
		}

		public void Flush()
		{
			throw new InvalidOperationException("This stream cannot be flushed");
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
			get { return NetworkState.None; }
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
			lock (sound)
			{
				if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null)
					sound.CurrentSubSound.Pause();
				else sound.Pause();
				
				RefreshPlaybackState();
			}
		}

		public float PercentBuffered
		{
			get { return (float)sound.PercentBuffered; }
		}

		public void Play()
		{
			lock (sound)
			{
				if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null)
					sound.CurrentSubSound.Play();
				else sound.Play();
				
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
			set
			{
				lock (sound)
				{
					sound.Channel.PositionInBytes = (uint)value;
				}
			}
		}

		public SeekState SeekState
		{
			get { return seekState; }
		}

		public int StreamIndex
		{
			get { return streamIndex; }
			set { 
				streamIndex = value;
				if (sound != null)
					sound.CurrentSubSoundIndex = streamIndex;
			}
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
					lock (sound)
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

		public void RefreshBufferState()
		{
			BufferState nextBufferState = bufferState;
		
			if (sound != null && sound.CurrentSubSound != null)
			{
				if (sound.CurrentSubSound.BufferIsStarving)
				{
					nextBufferState = BufferState.Starving;
				}
				else
				{
					if (sound.CurrentSubSound.OpenState == OpenState.Loading)
						nextBufferState = BufferState.Loading;
					else if (sound.CurrentSubSound.OpenState == OpenState.Buffering)
					{
						if (sound.CurrentSubSound.PercentBuffered < 100)
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
		}
		
		public void RefreshPlaybackState()
		{
			PlaybackState nextPlaybackState = playbackState;
		
			if (sound != null)
			{
				if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null)
				{
					if (sound.CurrentSubSound.Channel.IsPlaying)
					{
						if (sound.CurrentSubSound.Channel.Paused)
							nextPlaybackState = PlaybackState.Paused;
						else nextPlaybackState = PlaybackState.Playing;
					}
					else nextPlaybackState = PlaybackState.Stopped;
				}
				else
				{
					if (sound.Channel != null)
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
				}
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

		public void Resume()
		{
			lock (sound)
			{
				if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null)
					sound.CurrentSubSound.Resume();
				else sound.Resume();
				
				RefreshPlaybackState();
			}
		}

		public long Seek(long offset, SeekOrigin origin)
		{
			uint trueOffset = (offset <= uint.MaxValue) ? (uint)offset : uint.MaxValue;

			lock (sound)
			{
				switch (origin)
				{
					case SeekOrigin.Begin:
						if (trueOffset < 0) trueOffset = 0;
						else if (trueOffset > Length) trueOffset = (uint)Length;
						sound.Channel.PositionInBytes = trueOffset;
						break;
					case SeekOrigin.Current:
						uint currentPosition = sound.Channel.PositionInBytes;
						if (currentPosition + trueOffset > uint.MaxValue)
						{
							if (Length < uint.MaxValue)
								sound.Channel.PositionInBytes = (uint)Length;
							else sound.Channel.PositionInBytes = uint.MaxValue;
						}
						else
						{
							if (currentPosition + trueOffset > Length)
								sound.Channel.PositionInBytes = (uint)Length;
							else sound.Channel.PositionInBytes = (currentPosition + trueOffset);
						}
						break;
					case SeekOrigin.End:
						if (Length + trueOffset > uint.MaxValue)
						{
							if (Length < uint.MaxValue)
								sound.Channel.PositionInBytes = (uint)Length;
							else sound.Channel.PositionInBytes = uint.MaxValue;
						}
						else
						{
							if (Length + trueOffset > Length)
								sound.Channel.PositionInBytes = (uint)Length;
							else sound.Channel.PositionInBytes = Convert.ToUInt32(Length + trueOffset);
						}
						break;
					default:
						break;
				}

				return Position;
			}
		}

		public void SetLength(long value)
		{
		}

		public void Stop()
		{
			lock (sound)
			{
				if (sound.CurrentSubSoundIndex != -1 && sound.CurrentSubSound != null)
					sound.CurrentSubSound.Stop();
				else sound.Stop();
				
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
