using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Timers;

namespace Gnosis.Audio.Fmod
{
	internal class Sound : IDisposable, ILoopable, IHasRange, IHasDefault
	{
		#region Constructors
		/// <summary>
		/// The full Sound constructor
		/// </summary>
		/// <param name="soundSystem">The sound system that this sound is associated with</param>
		/// <param name="mediaFile">The path or url of the file that the sound comes from</param>
		internal Sound(SoundSystem soundSystem, Uri uri)
		{			
			this.soundSystem = soundSystem;
			this.uri = uri;
		}
		
		internal Sound(SoundSystem soundSystem, string path)
		{
			this.soundSystem = soundSystem;
			this.uri = new Uri(string.Format("file://{0}{1}", path, System.IO.Path.DirectorySeparatorChar));
		}
				
		/// <summary>
		/// SubSound constructor
		/// </summary>
		/// <param name="parentSound">The parent Sound of this SubSound</param>
		internal Sound(Sound parentSound)
		{
			if (parentSound != null)
			{
				this.soundSystem = parentSound.SoundSystem;
				this.parentSound = parentSound;
			}
		}
		#endregion
		
		#region Finalizer
		~Sound()
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
				}

				if (channel != null && channel.Handle != IntPtr.Zero)
				{
					channel.Dispose();
					channel = null;
				}

				if (currentSubSound != null && currentSubSound.Handle != IntPtr.Zero)
				{
					currentSubSound.Dispose();
					currentSubSound = null;
				}
				
				if (handle != IntPtr.Zero)
				{
                    try
                    {
                        NativeMethods.FMOD_Sound_Release(handle);
                    }
                    catch (AccessViolationException)
                    {
                        //TODO: The sound handle has already been relased - all that we can do is log it
                    }
                    finally
                    {
                        handle = IntPtr.Zero;
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
		private Guid guid = Guid.NewGuid();
		private Channel channel = new Channel();
		private string name;
		private uint length;
		private Sound parentSound;
		private IntPtr handle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private SoundSystem soundSystem;
		private SoundLock soundLock;
		private SoundSettings defaultSettings;
		private SoundVariation soundVariation;
		private Range range;
		private TimeUnits lengthUnit = TimeUnits.Millisecond;
		private FmodSoundType fmodSoundType = FmodSoundType.Unknown;
		private FmodSoundFormat fmodSoundFormat = FmodSoundFormat.None;
		private int numberOfChannels;
		private int numberOfBitsPerSample;
		private OpenState openState;
		private uint percentBuffered;
		private bool bufferIsStarving;
		private Modes mode;
		private SoundLoop loop;		
		//private SoundCollection subSounds;
		private TagCollection tags;
		private IntPtr userData = IntPtr.Zero;
		private Uri uri;
		private bool disposed;
		//SoundStatus status;
		private TimeSpan duration = TimeSpan.Zero;

		private int numberOfSubSounds = -1;
		private int currentSubSoundIndex;
		private Sound currentSubSound;
		#endregion
		
		#region Private Methods
		private void SelectSubSound(int index)
		{
			currentSubSoundIndex = index;
			IntPtr subSoundHandle = new IntPtr();
			Result result = NativeMethods.FMOD_Sound_GetSubSound(handle, index, ref subSoundHandle);
			if (result == Result.Ok)
			{
				currentSubSound = new Sound(this);
				currentSubSound.Handle = subSoundHandle;
			}
			else throw new ApplicationException(string.Format("Could not get sub-sound {0}", index));
		}
		#endregion
		
		#region Internal Properties
		
		#region Duration
		internal TimeSpan Duration
		{
			get
			{
				if (duration == TimeSpan.Zero)
				{
					LengthUnit = TimeUnits.Millisecond;
					duration = new TimeSpan(0, 0, 0, 0, (int)Milliseconds);
				}
				
				return duration;
			}
		}
		#endregion

		#region Channel
		internal Channel Channel
		{
			get { return channel; }
		}
		#endregion

		#region ParentSound
		internal Sound ParentSound
		{
			get { return parentSound; }
		}
		#endregion

		#region Handle
		/// <summary>
		/// A pointer handle to the unmanaged sound resources
		/// </summary>
		internal IntPtr Handle
		{
			get { return handle; }
			set { handle = value; }
		}
		#endregion

		#region CurrentResult
		/// <summary>
		/// Gets the current result state of this sound
		/// </summary>
		internal Result CurrentResult
		{
			get { return currentResult; }
		}
		#endregion

		#region SoundSystem
		internal SoundSystem SoundSystem
		{
			get
			{
				#region Legacy Code
				/*
				if (soundSystem == null)
				{
					currentResult = Result.Ok;
					IntPtr systemHandle = new IntPtr();

					try
					{
						currentResult = NativeMethods.FMOD_Sound_GetSystemObject(handle, ref systemHandle);
					}
					catch (System.Runtime.InteropServices.ExternalException)
					{
						currentResult = Result.InvalidParameterError;
					}
					
					if (currentResult == Result.Ok)
					{
						soundSystem = new SoundSystem();
						soundSystem.Handle = systemHandle;
					}
				}
				*/
				#endregion

				return soundSystem;
			}
			set { soundSystem = value; }
		}
		#endregion

		#region Lock
		/// <summary>
		/// Gets and sets the lock currently conntected to this sound
		/// </summary>
		internal SoundLock Lock
		{
			get { return soundLock; }
			set { soundLock = value; }
		}
		#endregion

		#region DefaultSettings
		public SoundSettings DefaultSettings
		{
			get { return defaultSettings; }
			set
			{
				defaultSettings = value;
				if (defaultSettings != null)
				{
					currentResult = NativeMethods.FMOD_Sound_SetDefaults(handle, defaultSettings.Frequency, defaultSettings.Volume, defaultSettings.Pan, defaultSettings.Priority);
				}
			}
		}
		#endregion

		#region Variation
		internal SoundVariation Variation
		{
			get { return soundVariation; }
			set { soundVariation = value; }
		}
		#endregion

		#region Range
		public Range Range
		{
			get { return range; }
			set
			{
				range = value;
				if (range != null)
					currentResult = NativeMethods.FMOD_Sound_Set3DMinMaxDistance(handle, range.Minimum, range.Maximum);
			}
		}
		#endregion

		#region SubSounds
		//internal SoundCollection SubSounds
		//{
			//get
			//{
				// Lazy initialization
				//if (subSounds == null && this.Handle != IntPtr.Zero)
				//{
					//subSounds = new SoundCollection(this, true);
				//}

				//return subSounds;
			//}
		//}
		#endregion
		
		#region NumberOfSubSounds
		public int NumberOfSubSounds
		{
			get {
				if (numberOfSubSounds == -1) {
					NativeMethods.FMOD_Sound_GetNumSubSounds(handle, ref numberOfSubSounds);
				}
				
				return numberOfSubSounds;
			}
		}
		#endregion
		
		#region CurrentSubSoundIndex
		public int CurrentSubSoundIndex
		{
			get { return currentSubSoundIndex; }
			set {
				if (value != currentSubSoundIndex)
					SelectSubSound(value);
			}
		}
		#endregion
		
		#region CurrentSubSound
		public Sound CurrentSubSound
		{
			get { return currentSubSound; }
		}
		#endregion

		#region Name
		internal string Name
		{
			get
			{
				StringBuilder nameBuilder = new StringBuilder(100);
				currentResult = NativeMethods.FMOD_Sound_GetName(handle, nameBuilder, nameBuilder.Capacity);
				name = nameBuilder.ToString();
				return name;
			}
		}
		#endregion

		#region LengthUnit
		internal TimeUnits LengthUnit
		{
			get { return lengthUnit; }
			set { lengthUnit = value; }
		}
		#endregion

		#region FmodLength
		internal uint FmodLength
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetLength(handle, ref length, lengthUnit);
				return length;
			}
		}
		#endregion

		#region Type
		internal FmodSoundType FmodSoundType
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetFormat(handle, ref fmodSoundType, ref fmodSoundFormat, ref numberOfChannels, ref numberOfBitsPerSample);
				return fmodSoundType;
			}
		}
		#endregion

		#region Format
		internal FmodSoundFormat FmodSoundFormat
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetFormat(handle, ref fmodSoundType, ref fmodSoundFormat, ref numberOfChannels, ref numberOfBitsPerSample);
				return fmodSoundFormat;
			}
		}
		#endregion

		#region NumberOfChannels
		internal int NumberOfChannels
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetFormat(handle, ref fmodSoundType, ref fmodSoundFormat, ref numberOfChannels, ref numberOfBitsPerSample);
				return numberOfChannels;
			}
		}
		#endregion

		#region NumberOfBitsPerSample
		internal int NumberOfBitsPerSample
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetFormat(handle, ref fmodSoundType, ref fmodSoundFormat, ref numberOfChannels, ref numberOfBitsPerSample);
				return numberOfBitsPerSample;
			}
		}
		#endregion

		#region Tags
		internal TagCollection Tags
		{
			get
			{
				// Lazy initialization
				if (tags == null && handle != IntPtr.Zero)
				{
					tags = new TagCollection(handle, true);
				}

				return tags;
			}
		}
		#endregion

		#region OpenState
		internal OpenState OpenState
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetOpenState(handle, ref openState, ref percentBuffered, ref bufferIsStarving);
				return openState;
			}
		}
		#endregion

		#region OpenStateName
		internal string OpenStateName
		{
			get { return OpenState.ToString(); }
		}
		#endregion

		#region PercentBuffered
		internal uint PercentBuffered
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetOpenState(handle, ref openState, ref percentBuffered, ref bufferIsStarving);
				return percentBuffered;
			}
		}
		#endregion

		#region BufferIsStarving
		internal bool BufferIsStarving
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetOpenState(handle, ref openState, ref percentBuffered, ref bufferIsStarving);
				return bufferIsStarving;
			}
		}
		#endregion

		#region Mode
		internal Modes Mode
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetMode(handle, ref mode);
				return mode;
			}
			set
			{
				mode = value;
				currentResult = NativeMethods.FMOD_Sound_SetMode(handle, mode);
			}
		}
		#endregion

		#region Loop
		public SoundLoop Loop
		{
			get { return loop; }
			set
			{
				loop = value;
				if (loop != null)
				{
					currentResult = NativeMethods.FMOD_Sound_SetLoopPoints(handle, loop.Start, loop.StartUnit, loop.End, loop.EndUnit);
					currentResult = NativeMethods.FMOD_Sound_SetLoopCount(handle, loop.Count);
				}
			}
		}
		#endregion

		#region UserData
		internal IntPtr UserData
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetUserData(handle, ref userData);
				return userData;
			}
			set
			{
				userData = value;
				currentResult = NativeMethods.FMOD_Sound_SetUserData(handle, userData);
			}
		}
		#endregion

		#region Milliseconds
		/// <summary>
		/// Get the length of the sound in milliseconds
		/// </summary>		
		internal uint Milliseconds
		{
			get
			{
				this.LengthUnit = TimeUnits.Millisecond;
				return this.FmodLength;
			}
		}
		#endregion

		#region FmodPosition
		/// <summary>
		/// Get the current position of the sound in milliseconds
		/// </summary>
		//[CLSCompliant(false)]
		//internal uint FmodPosition
		//{
		//get { return this.channel.Position; }
		//set { this.channel.Position = value; }
		//}
		#endregion

		#region NumberOfTags
		internal int NumberOfTags
		{
			get
			{
				int numberOfTags = 0;
				int numberOfUpdatedTags = 0;
				currentResult = NativeMethods.FMOD_Sound_GetNumTags(this.handle, ref numberOfTags, ref numberOfUpdatedTags);
				return numberOfTags;
			}
		}
		#endregion

		#region NumberOfUpdatedTags
		internal int NumberOfUpdatedTags
		{
			get
			{
				int numberOfTags = 0;
				int numberOfUpdatedTags = 0;
				NativeMethods.FMOD_Sound_GetNumTags(this.handle, ref numberOfTags, ref numberOfUpdatedTags);
				return numberOfUpdatedTags;
			}
		}
		#endregion		
		
		#endregion
		
		#region Internal Methods

		#region Load
		/*
		internal void Load()
		{
			if (this.Location.IsLocal)
			{
				this.SoundSystem.CreateStream(this, this.Location.Path, Modes.None);
			}
			else
			{
				//soundSystem.StreamBufferUnit = streamBufferUnit;
				//soundSystem.StreamBufferSize = streamBufferSize;
				//soundSystem.StreamBufferUnit = TimeUnits.RawByte;
				//soundSystem.StreamBufferSize = (64 * 1024);
				Load((64 * 1024));
			}
		}

		[CLSCompliant(false)]
		internal void Load(uint streamBufferSize)
		{
			if (this.Location.IsLocal)
			{
				Load();
			}
			else
			{
				this.SoundSystem.StreamBufferUnit = TimeUnits.RawByte; //streamBufferUnit;
				this.SoundSystem.StreamBufferSize = streamBufferSize;
				Modes mode = (Modes.Software | Modes.Fmod2D | Modes.CreateStream | Modes.NonBlocking);
				this.SoundSystem.CreateSound(this, this.Location.Path, mode);
			}
		}
		*/
		#endregion

		#region Play
		internal void Play()
		{
			this.soundSystem.PlaySound(ChannelIndex.Free, this, false, ref channel);
		}
		#endregion

		#region Pause
		internal void Pause()
		{
			this.channel.Paused = true;
		}
		#endregion

		#region Resume
		internal void Resume()
		{
			this.channel.Paused = false;
		}
		#endregion

		#region Stop
		internal void Stop()
		{
			this.channel.Stop();
		}
		#endregion

		#region Read
		/// <summary>
		/// Reads the given number of bytes from the sound and returns them as a SoundBuffer
		/// </summary>
		/// <param name="buffer">A pointer to a buffer that the data will be read into</param>
		/// <param name="length">The length in bytes to read</param>
		/// <returns>The actual number of bytes that were read (this may be less than length, for example when reaching the end of the sound)</returns>
		internal uint Read(IntPtr buffer, uint length)
		{
			uint bytesRead = 0;
			currentResult = NativeMethods.FMOD_Sound_ReadData(handle, buffer, length, ref bytesRead);
			return bytesRead;
		}
		#endregion

		#region SaveAsWaveFile
		/// <summary>
		/// Save this sound to a PCM Wave file with the given path
		/// </summary>
		/// <param name="filePath">The path of the file to save this sound to</param>
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		internal unsafe void SaveAsWaveFile(string filePath)
		{
			const int chunkSize = 4096;
			FileStream file = null;
			IntPtr data = IntPtr.Zero;

			try
			{
				// Create the file
				file = new FileStream(filePath, FileMode.Create, FileAccess.Write);

				// Alocate memory for the PCM data buffer
				data = Marshal.AllocHGlobal(chunkSize);

				// Create a holding buffer that the PCM data will be copied into
				byte[] buffer = new byte[chunkSize];

				uint totalSize = 0;
				uint bytesRead = 0;

				// Get the length of the sound in PCM bytes
				this.LengthUnit = TimeUnits.PcmByte;
				uint pcmLength = this.FmodLength;

				// Create the WAV structures
				FmtChunk fmtChunk = new FmtChunk();
				DataChunk dataChunk = new DataChunk();
				WavHeader wavHeader = new WavHeader();
				RiffChunk riffChunk = new RiffChunk();

				fmtChunk.Chunk = new RiffChunk();
				fmtChunk.Chunk.Id = new char[4] { 'f', 'm', 't', ' ' };
				fmtChunk.Chunk.Size = Marshal.SizeOf(fmtChunk) - Marshal.SizeOf(riffChunk);
				fmtChunk.FormatTag = 1;
				fmtChunk.NumberOfChannels = (ushort)this.NumberOfChannels;
				fmtChunk.SamplesPerSecond = (uint)this.DefaultSettings.Frequency;
				fmtChunk.AverageBytesPerSecond = (uint)(this.DefaultSettings.Frequency * this.NumberOfChannels * this.NumberOfBitsPerSample / 8);
				fmtChunk.BlockSize = (ushort)(1 * this.NumberOfChannels * this.NumberOfBitsPerSample / 8);
				fmtChunk.NumberOfBitsPerSample = (ushort)this.NumberOfBitsPerSample;

				dataChunk.Chunk = new RiffChunk();
				dataChunk.Chunk.Id = new char[4] { 'd', 'a', 't', 'a' };
				dataChunk.Chunk.Size = (int)pcmLength;

				wavHeader.Chunk = new RiffChunk();
				wavHeader.Chunk.Id = new char[4] { 'R', 'I', 'F', 'F' };
				wavHeader.Chunk.Size = (int)(Marshal.SizeOf(fmtChunk) + Marshal.SizeOf(riffChunk) + pcmLength);
				wavHeader.RiffType = new char[4] { 'W', 'A', 'V', 'E' };

				// Write out the WAV header.			
				IntPtr wavHeaderPtr = Marshal.AllocHGlobal(Marshal.SizeOf(wavHeader));
				IntPtr fmtChunkPtr = Marshal.AllocHGlobal(Marshal.SizeOf(fmtChunk));
				IntPtr dataChunkPtr = Marshal.AllocHGlobal(Marshal.SizeOf(dataChunk));
				byte[] wavHeaderBytes = new byte[Marshal.SizeOf(wavHeader)];
				byte[] fmtChunkBytes = new byte[Marshal.SizeOf(fmtChunk)];
				byte[] dataChunkBytes = new byte[Marshal.SizeOf(dataChunk)];

				Marshal.StructureToPtr(wavHeader, wavHeaderPtr, false);
				Marshal.Copy(wavHeaderPtr, wavHeaderBytes, 0, Marshal.SizeOf(wavHeader));

				Marshal.StructureToPtr(fmtChunk, fmtChunkPtr, false);
				Marshal.Copy(fmtChunkPtr, fmtChunkBytes, 0, Marshal.SizeOf(fmtChunk));

				Marshal.StructureToPtr(dataChunk, dataChunkPtr, false);
				Marshal.Copy(dataChunkPtr, dataChunkBytes, 0, Marshal.SizeOf(dataChunk));

				file.Write(wavHeaderBytes, 0, Marshal.SizeOf(wavHeader));
				file.Write(fmtChunkBytes, 0, Marshal.SizeOf(fmtChunk));
				file.Write(dataChunkBytes, 0, Marshal.SizeOf(dataChunk));

				do
				{
					// Read PCM data from the sound
					bytesRead = Read(data, chunkSize);

					// Copy the PCM data into the buffer
					Marshal.Copy(data, buffer, 0, chunkSize);

					// Write the PCM data to the FileStream
					file.Write(buffer, 0, (int)bytesRead);

					// Increment totalRead
					totalSize += bytesRead;
				}
				while (currentResult == Result.Ok && bytesRead == chunkSize);

				//file.Close();
			}
			catch (Exception ex)
			{
				throw new ApplicationException("There was an error saving this sound to " + filePath + " : " + currentResult.ToString() + ":" + ex.Message, ex);
			}
			finally
			{
				// Always free the data pointer
				if (data != IntPtr.Zero) Marshal.FreeHGlobal(data);

				// Always close the file
				if (file != null) file.Close();
			}
		}
		#endregion

		#endregion
	}
}
