using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class ChannelGroup : IDisposable
	{
		#region Private Fields
		private IntPtr handle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		//private SoundSystem soundSystem;
		//private Dsp dspHead;
		private float volume;
		private float pitch;
		private bool overridePaused;
		private float overrideVolume;
		private float overrideFrequency;
		private float overridePan;
		private bool overrideMute;
		private ReverbChannelProperties overrideReverbProperties;
		private Vector overridePosition;
		private Vector overrideVelocity;
		private ChannelGroupCollection subChannelGroups;
		private ChannelCollection subChannels;
		private IntPtr userData = IntPtr.Zero;
		private bool disposed;
				
		// This is the same as SoundSystem so it *could* be refactored at some point
		//private float[] spectrumArray;
		//private int spectrumResolution = 2048; // min = 64, max = 8192, use powers of 2
		//private int spectrumChannelOffset; // 0 = left, 1 = right
		//private DspFftWindow spectrumDspWindowType = DspFftWindow.Rectangle;
		//private float[] waveArray;
		//private int waveResolution = 100;
		//private int waveChannelOffset; // 0 = left, 1 = right
		#endregion
		
		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public ChannelGroup()
		{
		}
		#endregion
		
		#region Finalizer
		~ChannelGroup()
		{
			Dispose(false);
		}
		#endregion
		
		#region IDispose Methods
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{				
				if (disposing)
				{
					//dspHead.Dispose();
					//dspHead = null;
					
					//subChannels.Dispose();
					//subChannelGroups.Dispose();
				}				

				if (handle != IntPtr.Zero)
				{
					NativeMethods.FMOD_ChannelGroup_Release(handle);
					handle = IntPtr.Zero;
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
		
		#region Public Properties
		
		#region Handle
		public IntPtr Handle
		{
			get {return handle;}
			set {handle = value;}
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
						currentResult = NativeMethods.FMOD_ChannelGroup_GetSystemObject(handle, ref systemHandle);
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
		
		#region DspHead
		/*
		public Dsp DspHead
		{
			get
			{
				if (dspHead == null)
				{
					currentResult = Result.Ok;
					IntPtr dspHandle = new IntPtr();

					try
					{
						currentResult = NativeMethods.FMOD_ChannelGroup_GetDSPHead(handle, ref dspHandle);
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
				}
				
				return dspHead;
			}
		}
		*/
		#endregion
		
		#region Volume
		// Channelgroup scale values.  (scales the current volume or pitch of all channels and channel groups, DOESN'T overwrite)
		public float Volume
		{
			get
			{
				currentResult = NativeMethods.FMOD_ChannelGroup_GetVolume(handle, ref volume);
				return volume;
			}
			set
			{
				volume = value;
				currentResult = NativeMethods.FMOD_ChannelGroup_SetVolume(handle, volume);
			}
		}
		#endregion
		
		#region Pitch
		// Channelgroup scale values.  (scales the current volume or pitch of all channels and channel groups, DOESN'T overwrite)
		public float Pitch
		{
			get
			{
				currentResult = NativeMethods.FMOD_ChannelGroup_GetPitch(handle, ref pitch);
				return pitch;
			}
			set
			{
				pitch = value;
				currentResult = NativeMethods.FMOD_ChannelGroup_SetPitch(handle, pitch);
			}		
		}
		#endregion
		
		#region OverridePaused
		// Channelgroup override values.  (recursively overwrites whatever settings the channels had)
		public bool OverridePaused
		{
			get
			{
				return overridePaused;
			}
			set
			{
				overridePaused = value;
				currentResult = NativeMethods.FMOD_ChannelGroup_OverridePaused(handle, overridePaused);
			}			
		}
		#endregion
		
		#region OverrideVolume
		// Channelgroup override values.  (recursively overwrites whatever settings the channels had)
		public float OverrideVolume
		{
			get
			{
				return overrideVolume;
			}
			set
			{
				overrideVolume = value;
				currentResult = NativeMethods.FMOD_ChannelGroup_OverrideVolume(handle, overrideVolume);
			}
		}
		#endregion
		
		#region OverrideFrequency
		// Channelgroup override values.  (recursively overwrites whatever settings the channels had)
		public float OverrideFrequency
		{
			get
			{
				return overrideFrequency;
			}
			set
			{
				overrideFrequency = value;
				currentResult = NativeMethods.FMOD_ChannelGroup_OverrideFrequency(handle, overrideFrequency);
			}
		}
		#endregion
		
		#region OverridePan
		// Channelgroup override values.  (recursively overwrites whatever settings the channels had)
		public float OverridePan
		{
			get
			{
				return overridePan;
			}
			set
			{
				overridePan = value;
				currentResult = NativeMethods.FMOD_ChannelGroup_OverridePan(handle, overridePan);
			}
		}
		#endregion
		
		#region OverrideMute
		// Channelgroup override values.  (recursively overwrites whatever settings the channels had)
		public bool OverrideMute
		{
			get
			{
				return overrideMute;
			}
			set
			{
				overrideMute = value;
				currentResult = NativeMethods.FMOD_ChannelGroup_OverrideMute(handle, overrideMute);
			}
		}
		#endregion
		
		#region OverrideReverbProperties
		// Channelgroup override values.  (recursively overwrites whatever settings the channels had)
		public ReverbChannelProperties OverrideReverbProperties
		{
			get
			{
				return overrideReverbProperties;
			}
			set
			{
				overrideReverbProperties = value;
				currentResult = NativeMethods.FMOD_ChannelGroup_OverrideReverbProperties(handle, ref overrideReverbProperties);
			}
		}
		#endregion
		
		#region OverridePosition
		// Channelgroup override values.  (recursively overwrites whatever settings the channels had)
		public Vector OverridePosition
		{
			get
			{
				return overridePosition;
			}
			set
			{
				overridePosition = value;
				currentResult = NativeMethods.FMOD_ChannelGroup_Override3DAttributes(handle, ref overridePosition, ref overrideVelocity);
			}
		}
		#endregion
		
		#region OverrideVelocity
		// Channelgroup override values.  (recursively overwrites whatever settings the channels had)
		public Vector OverrideVelocity
		{
			get
			{
				return overrideVelocity;
			}
			set
			{
				overrideVelocity = value;
				currentResult = NativeMethods.FMOD_ChannelGroup_Override3DAttributes(handle, ref overridePosition, ref overrideVelocity);
			}
		}
		#endregion
		
		#region Name
		public string Name
		{
			get
			{
				StringBuilder nameBuilder = new StringBuilder(100);
				currentResult = NativeMethods.FMOD_ChannelGroup_GetName(handle, nameBuilder, nameBuilder.Capacity);
				return nameBuilder.ToString();
			}
		}
		#endregion
		
		#region SubChannelGroups
		public ChannelGroupCollection SubChannelGroups
		{
			get
			{
				// Lazy initialization
				if (subChannelGroups == null)
				{
					subChannelGroups = new ChannelGroupCollection(handle, true);
				}
				
				return subChannelGroups;
			}
		}
		#endregion
		
		#region SubChannels
		public ChannelCollection SubChannels
		{
			get
			{
				// Lazy Initialization
				if (subChannels == null)
				{
					subChannels = new ChannelCollection(handle, true);
				}
				
				return subChannels;
			}
		}
		#endregion
		
		#region UserData
		public IntPtr UserData
		{
			get
			{
				currentResult = NativeMethods.FMOD_ChannelGroup_GetUserData(handle, ref userData);
				return userData;
			}
			set
			{
				userData = value;
				currentResult = NativeMethods.FMOD_ChannelGroup_SetUserData(handle, userData);
				
			}
		}
		#endregion
		
		#endregion
		
		#region Public Methods
				
		#region Stop
		// Channelgroup override values.  (recursively overwrites whatever settings the channels had)
		public void Stop()
		{
			currentResult = NativeMethods.FMOD_ChannelGroup_Stop(handle);
		}		
		#endregion
		
		#region AddDsp
		public void AddDsp(Dsp dsp)
		{
			if (dsp != null)
			{
				currentResult = NativeMethods.FMOD_ChannelGroup_AddDSP(handle, dsp.Handle);
			}
			else throw new ArgumentNullException("dsp");
		}
		#endregion

		#region GetSpectrumData
		/// <summary>
		/// Get a representation of the sound spectrum data
		/// </summary>
		/// <param name="spectrumResolution">The resolution of the spectrum data</param>
		/// <param name="spectrumChannelOffset">Which channel of the sound to get spectrum data from (0 is left, 1 is right)</param>
		/// <param name="spectrumDspWindowType">The type of fast fourier transform to use for mapping the spectrum data</param>
		/// <returns>An array of floats</returns>
		/// <remarks>A common value for spectrumResolution is 2048,
		/// the minimum value is 64, the maximum value is 8192, all values should be powers of 2</remarks>
		public float[] GetSpectrumData(int spectrumResolution, int spectrumChannelOffset, DspFftWindow spectrumDspWindowType)
		{
			float[] spectrumArray = null;
			currentResult = NativeMethods.FMOD_ChannelGroup_GetSpectrum(handle, spectrumArray, spectrumResolution, spectrumChannelOffset, spectrumDspWindowType);
			return spectrumArray;
		}
		#endregion

		#region GetWaveData
		/// <summary>
		/// Get a representation of the sound wave data
		/// </summary>
		/// <param name="waveResolution">The resolution of the wave data</param>
		/// <param name="waveChannelOffset">Which channel of the sound to get wave data from (0 is left, 1 is right)</param>
		/// <returns>An array of floats</returns>
		/// <remarks>A common value for waveResolution is 100</remarks>
		public float[] GetWaveData(int waveResolution, int waveChannelOffset)
		{
			float[] waveArray = null;
			currentResult = NativeMethods.FMOD_ChannelGroup_GetWaveData(handle, waveArray, waveResolution, waveChannelOffset);
			return waveArray;
		}
		#endregion
		
		#endregion
	}
}
