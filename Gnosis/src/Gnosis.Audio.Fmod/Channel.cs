using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class Channel : ILoopable,IDisposable
	{		
		#region Constructors
		public Channel()
		{
		}
		#endregion
		
		#region Finalizer
		~Channel()
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
					if (currentSound != null)
					{
						currentSound.Dispose();
						currentSound = null;
					}
					
					if (dspHead != null)
					{
						dspHead.Dispose();
						dspHead = null;
					}
					
					if (group != null)
					{
						group.Dispose();
						group = null;
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
		private IntPtr handle;
		private IntPtr userData;
		private Result currentResult = Result.Ok;
		//private SoundSystem soundSystem;
		private SpeakerSystem speakerSystem;
		private bool paused;
		private float volume;
		private float frequency;
		private float pan;
		private SoundDelay delay;
		private bool mute;
		private int priority;
		private uint position;
		private uint positionInBytes;
		private ReverbChannelProperties reverbProperties;
		private ChannelGroup group;
		private PositionAndVelocity channel3DAttributes;
		private Range range;
		private ConeSettings channel3DConeSettings;
		private Vector channelConeOrientation;
		private SoundOcclusion channel3DOcclusion;
		private bool isPlaying;
		private bool isVirtual;
		private float audibility;
		private Sound currentSound;
		private ChannelSpectrum channelSpectrum;
		private ChannelWaveData waveData;
		private Dsp dspHead;
		private Modes channelMode;
		private SoundLoop loop;
		private bool disposed;
		#endregion
		
		#region Public Properties
		
		#region Handle
		public IntPtr Handle
		{
			get {return handle;}
			set {handle = value;}
		}
		#endregion

		#region UserData
		public IntPtr UserData
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetUserData(handle, ref userData);
				return userData;
			}
			set
			{
				userData = value;
				currentResult = NativeMethods.FMOD_Channel_SetUserData(handle, userData);
			}
		}
		#endregion
		
		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
		
		#region SoundSystem
		/*
		public SoundSystem SoundSystem
		{
			get
			{
				if (soundSystem == null)
				{
					currentResult = Result.Ok;
					IntPtr systemHandle = new IntPtr();

					try
					{
						currentResult = NativeMethods.FMOD_Channel_GetSystemObject(handle, ref systemHandle);
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

				return soundSystem;
			}
		}
		*/
		#endregion
		
		#region SpeakerSystem
		public SpeakerSystem SpeakerSystem
		{
			get {return speakerSystem;}
			set {speakerSystem = value;}
		}
		#endregion
		
		#region Paused
		public bool Paused
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetPaused(handle, ref paused);
				return paused;
			}
			set
			{
				paused = value;
				currentResult = NativeMethods.FMOD_Channel_SetPaused(handle, paused);
				Result r = currentResult;
			}
		}
		#endregion
		
		#region Volume
		public float Volume
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetVolume(handle, ref volume);
				return volume;
			}
			set
			{
				//normalize the volume
				if (value < 0) value = 0;
				if (value > 1) value = 1;
				
				volume = value;
				currentResult = NativeMethods.FMOD_Channel_SetVolume(handle, volume); 
			}
		}
		#endregion
		
		#region Frequency
		public float Frequency
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetFrequency(handle, ref frequency);
				return frequency;
			}
			set
			{
				frequency = value;
				currentResult = NativeMethods.FMOD_Channel_SetFrequency(handle, frequency);
			}
		}
		#endregion
		
		#region Pan
		public float Pan
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetPan(handle, ref pan);
				return pan;
			}
			set
			{
				pan = value;
				currentResult = NativeMethods.FMOD_Channel_SetPan(handle, pan);
			}
		}
		#endregion
		
		#region Delay
		public SoundDelay Delay
		{
			get
			{
				uint startdelay = 0, enddelay = 0;
				currentResult = NativeMethods.FMOD_Channel_GetDelay(handle, ref startdelay, ref enddelay);
				delay.Start = startdelay;
				delay.End = enddelay;
				return delay;
			}
			set
			{
				delay = value;
				currentResult = NativeMethods.FMOD_Channel_SetDelay(handle, delay.Start, delay.End);
			}
		}
		#endregion

		#region Mute
		public bool Mute
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetMute(handle, ref mute);
				return mute;
			}
			set
			{
				mute = value;
				currentResult = NativeMethods.FMOD_Channel_SetMute(handle, mute);
			}
		}
		#endregion		
		
		#region Priority
		public int Priority
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetPriority(handle, ref priority);
				return priority;
			}
			set
			{
				priority = value;
				currentResult = NativeMethods.FMOD_Channel_SetPriority(handle, priority);
			}
		}
		#endregion
		
		#region Position
		/// <summary>
		/// Gets and sets the Channel's current position in Milliseconds
		/// </summary>
		public uint Position
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetPosition(handle, ref position, TimeUnits.Millisecond);
				return position;
			}
			set
			{
				position = value;
				currentResult = NativeMethods.FMOD_Channel_SetPosition(handle, position, TimeUnits.Millisecond);
			}
		}
		#endregion
		
		#region PositionInBytes
		public uint PositionInBytes
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetPosition(handle, ref positionInBytes, TimeUnits.RawByte);
				return positionInBytes;
			}
			set
			{
				positionInBytes = value;
				currentResult = NativeMethods.FMOD_Channel_SetPosition(handle, positionInBytes, TimeUnits.RawByte);
			}
		}
		#endregion
		
		#region ReverbProperties
		public ReverbChannelProperties ReverbProperties
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetReverbProperties(handle, ref reverbProperties);
				return reverbProperties;
			}
			set
			{
				reverbProperties = value;
				currentResult = NativeMethods.FMOD_Channel_SetReverbProperties(handle, ref reverbProperties);
			}
		}
		#endregion
		
		#region ChannelGroup
		public ChannelGroup Group
		{
			get
			{
				currentResult = Result.Ok;
				IntPtr channelGroupHandle = new IntPtr();
				group = null;

				try
				{
					currentResult = NativeMethods.FMOD_Channel_GetChannelGroup(handle, ref channelGroupHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}

				if (currentResult == Result.Ok)
				{
					group = new ChannelGroup();
					group.Handle = channelGroupHandle;					
				}

				return group;		
			}
			set
			{
				group = value;
				if (group != null)
				{
					currentResult = NativeMethods.FMOD_Channel_SetChannelGroup(handle, group.Handle);
				}
			}
		}
		#endregion
		
		#region Channel3DAttributes
		public PositionAndVelocity Channel3DAttributes
		{
			get
			{
				Vector position = new Vector();
				Vector velocity = new Vector();
				currentResult = NativeMethods.FMOD_Channel_Get3DAttributes(handle, ref position, ref velocity);
				channel3DAttributes.Position = position;
				channel3DAttributes.Velocity = velocity;
				return channel3DAttributes;
			}
			set
			{
				channel3DAttributes = value;
				currentResult = NativeMethods.FMOD_Channel_Set3DAttributes(handle, ref channel3DAttributes.Position, ref channel3DAttributes.Velocity);
			}
		}
		#endregion		
		
		#region Range
		public Range Range
		{
			get {return range;}
			set
			{
				range = value;
				if (range != null)
					currentResult = NativeMethods.FMOD_Channel_Set3DMinMaxDistance(handle, range.Minimum, range.Maximum);
			}
		}
		#endregion
		
		#region Channel3DConeSettings
		public ConeSettings Channel3DConeSettings
		{
			get
			{
				float insideAngle = 0f, outsideAngle = 0f, outsideVolume = 0f;
				currentResult = NativeMethods.FMOD_Channel_Get3DConeSettings(handle, ref insideAngle, ref outsideAngle, ref outsideVolume);
				channel3DConeSettings.InsideAngle = insideAngle;
				channel3DConeSettings.OutsideAngle = outsideAngle;
				channel3DConeSettings.OutsideVolume = outsideVolume;
				return channel3DConeSettings;
			}
			set
			{
				channel3DConeSettings = value;
				currentResult = NativeMethods.FMOD_Channel_Set3DConeSettings(handle, channel3DConeSettings.InsideAngle, channel3DConeSettings.OutsideAngle, channel3DConeSettings.OutsideVolume);
			}
		}
		#endregion
		
		#region ChannelConeOrientation
		public Vector ChannelConeOrientation
		{
			get
			{
				Vector channelConeOrientation = new Vector();
				currentResult = NativeMethods.FMOD_Channel_Get3DConeOrientation(handle, ref channelConeOrientation);
				return channelConeOrientation;
			}
			set
			{
				channelConeOrientation = value;
				currentResult = NativeMethods.FMOD_Channel_Set3DConeOrientation(handle, ref channelConeOrientation);
			}
		}
		#endregion
		
		#region Channel3DOcclusion
		public SoundOcclusion Channel3DOcclusion
		{
			get
			{
				float directOcclusion = 0f, reverbOcclusion = 0f;
				currentResult = NativeMethods.FMOD_Channel_Get3DOcclusion(handle, ref directOcclusion, ref reverbOcclusion);
				channel3DOcclusion.DirectOcclusion = directOcclusion;
				channel3DOcclusion.ReverbOcclusion = reverbOcclusion;
				return channel3DOcclusion;
			}
			set
			{
				channel3DOcclusion = value;
				currentResult = NativeMethods.FMOD_Channel_Set3DOcclusion(handle, channel3DOcclusion.DirectOcclusion, channel3DOcclusion.ReverbOcclusion);
			}		
		}
		#endregion
		
		#region IsPlaying
		public bool IsPlaying
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_IsPlaying(handle, ref isPlaying);
				return isPlaying;
			}			
		}
		#endregion
		
		#region IsVirtual
		public bool IsVirtual
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_IsVirtual(handle, ref isVirtual);
				return isVirtual;
			}
		}
		#endregion
		
		#region Audibility
		public float Audibility
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetAudibility(handle, ref audibility);
				return audibility;
			}
		}
		#endregion
		
		#region Spectrum
		public ChannelSpectrum Spectrum
		{
			get
			{
				float[] spectrumArray = null;
				int numberOfValues = 0;
				int channelOffset = 0;
				DspFftWindow windowType = new DspFftWindow();
				currentResult = NativeMethods.FMOD_Channel_GetSpectrum(handle, spectrumArray, numberOfValues, channelOffset, windowType);
				channelSpectrum.SpectrumArray = spectrumArray;
				channelSpectrum.NumberOfValues = numberOfValues;
				channelSpectrum.ChannelOffset = channelOffset;
				channelSpectrum.WindowType = windowType;
				return channelSpectrum;
			}
		}
		#endregion
		
		#region WaveData
		public ChannelWaveData WaveData
		{
			get
			{
				float[] waveArray = null;
				int numberOfValues = 0;
				int channelOffset = 0;
				currentResult = NativeMethods.FMOD_Channel_GetWaveData(handle, waveArray, numberOfValues, channelOffset);
				waveData.WaveArray = waveArray;
				waveData.NumberOfValues = numberOfValues;
				waveData.ChannelOffset = channelOffset;
				return waveData;
			}
		}
		#endregion

		#region DspHead
		public Dsp DspHead
		{
			get
			{
				currentResult = Result.Ok;
				IntPtr dspHandle = new IntPtr();
				dspHead = null;

				try
				{
					currentResult = NativeMethods.FMOD_Channel_GetDSPHead(handle, ref dspHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}

				if (currentResult == Result.Ok)
				{
					dspHead = new Dsp();
					dspHead.Handle = dspHandle;
				}

				return dspHead;
			}
		}
		#endregion

		#region ChannelMode
		public Modes ChannelMode
		{
			get
			{
				currentResult = NativeMethods.FMOD_Channel_GetMode(handle, ref channelMode);
				return channelMode;
			}
			set
			{
				channelMode = value;
				currentResult = NativeMethods.FMOD_Channel_SetMode(handle, channelMode);
			}
		}
		#endregion
				
		#region Loop
		public SoundLoop Loop
		{
			get {return loop;}
			set
			{
				loop = value;
				if (loop != null)
				{
					currentResult = NativeMethods.FMOD_Channel_SetLoopPoints(handle, loop.Start, loop.StartUnit, loop.End, loop.EndUnit);
					currentResult = NativeMethods.FMOD_Channel_SetLoopCount(handle, loop.Count);
				}
			}
		}
		#endregion
		
		#endregion

		#region Public Methods

		#region GetCurrentSound
		//FIXME: use of this method as is would be problematic - refactor it
		/*
		public Sound GetCurrentSound(FmodAudioPlayer audioPlayer)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			currentSound = null;

			try
			{
				currentResult = NativeMethods.FMOD_Channel_GetCurrentSound(handle, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}

			if (currentResult == Result.Ok)
			{
				currentSound = new Sound();
				currentSound.Handle = soundHandle;
			}

			return currentSound;
		}
		*/
		#endregion

		#region Stop
		public void Stop()
		{
			currentResult = NativeMethods.FMOD_Channel_Stop(handle);
		}
		#endregion

		#region SetCallback
		public void SetCallback(ChannelCallbackType callbackType, ChannelCallback callback, int command)
		{
			currentResult = NativeMethods.FMOD_Channel_SetCallback(handle, callbackType, callback, command);
		}
		#endregion

		#region AddDsp
		public void AddDsp(Dsp dsp)
		{
			if (dsp != null)
			{
				currentResult = NativeMethods.FMOD_Channel_AddDSP(handle, dsp.Handle);
			}
		}
		#endregion

		#endregion
	}
}
