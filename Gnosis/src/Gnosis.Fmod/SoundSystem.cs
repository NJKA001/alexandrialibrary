using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class SoundSystem : IDisposable
	{
		#region Private Fields
		private IntPtr handle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private ChannelGroup masterChannelGroup;
		private Dsp dspHead;
		private bool isInitialized;
		private OutputType outputType = OutputType.Unknown;
		private DriverCollection drivers;
		private int numberOf2dHardwareChannels;
		private int minimum2dHardwareChannels;
		private int maximum2dHardwareChannels;
		private int numberOf3dHardwareChannels;
		private int minimum3dHardwareChannels;
		private int maximum3dHardwareChannels;
		private int totalHardwareChannels;
		private int numberOfSoftwareChannels;
		private int sampleRate;
		private FmodSoundFormat soundFormat;
		private int numberOfOutputChannels;
		private int maximumInputChannels;
		private DspResampler resampleMethod;
		private int bitsPerSample;
		private uint dspBufferLength;
		private int numberOfDspBuffers;
		private FileOpenCallback userFileOpenCallback;
		private FileCloseCallback userFileCloseCallback;
		private FileReadCallback userFileReadCallback;
		private FileSeekCallback userFileSeekCallback;
		private int userFileCallbackReadBufferSize = 2048;
		private SpeakerMode speakerMode = SpeakerMode.Mono;
		private string pluginPath = string.Empty;
		private PluginCollection codecPlugins;
		private PluginCollection dspPlugins;
		private PluginCollection outputPlugins;
		//private Plugin currentOutputPlugin = null;
		private float dopplerScale;
		private float distanceFactor;
		private float rollOffScale;
		private ListenerCollection listeners;
		private Speaker currentSpeaker;
		private float currentSpeakerPositionX;
		private float currentSpeakerPositionY;
		private uint streamBufferSize;
		private TimeUnits streamBufferUnit = TimeUnits.Millisecond;
		private uint version;
		private IntPtr outputHandle = IntPtr.Zero;
		private int numberOfChannelsPlaying;
		private float cpuPercentDsp;
		private float cpuPercentStream;
		private float cpuPercentUpdate;
		private float cpuPercentTotal;
		private OpticalDriveCollection opticalDrives;
		
		// spectrum and wave fields - no longer used
		//private float[] spectrumArray;
		//private int spectrumResolution = 2048; // min = 64, max = 8192, use powers of 2
		//private int spectrumChannelOffset; // 0 = left, 1 = right
		//private DspFftWindow spectrumDspWindowType = DspFftWindow.Rectangle;
		//private float[] waveArray;
		//private int waveResolution = 100;
		//private int waveChannelOffset; // 0 = left, 1 = right
		
		//NOTE: there seems to be a bug in FMODex 4.04 with reverbProperties
		//private ReverbProperties reverbProperties = null;
		private RecordDriverCollection recordDrivers;
		private Driver currentRecordDriver;
		private bool isRecording;
		private uint recordPosition;
		private float maximumWorldSize;
		//private string networkProxy = string.Empty;
		private IntPtr userData = IntPtr.Zero;
		private bool disposed;
		#endregion
		
		#region Private Methods
		
		#region GetHardwareChannels
		private void GetHardwareChannels()
		{
			currentResult = NativeMethods.FMOD_System_GetHardwareChannels(handle, ref numberOf2dHardwareChannels, ref numberOf3dHardwareChannels, ref totalHardwareChannels);
		}		
		#endregion
		
		#region SetHardwareChannels
		private void SetHardwareChannels()
		{
			currentResult = NativeMethods.FMOD_System_SetHardwareChannels(handle, minimum2dHardwareChannels, maximum2dHardwareChannels, minimum3dHardwareChannels, maximum3dHardwareChannels);
		}
		#endregion
		
		#region GetSoftwareFormat
		private void GetSoftwareFormat()
		{
			currentResult = NativeMethods.FMOD_System_GetSoftwareFormat(handle, ref sampleRate, ref soundFormat, ref numberOfOutputChannels, ref maximumInputChannels, ref resampleMethod, ref bitsPerSample);
		}
		#endregion
		
		#region SetSoftwareFormat
		private void SetSoftwareFormat()
		{
			currentResult = NativeMethods.FMOD_System_SetSoftwareFormat(handle, sampleRate, soundFormat, numberOfOutputChannels, maximumInputChannels, resampleMethod); 
		}
		#endregion
		
		#region GetDspBufferInfo
		private void GetDspBufferInfo()
		{
			currentResult = NativeMethods.FMOD_System_GetDSPBufferSize(handle, ref dspBufferLength, ref numberOfDspBuffers);
		}
		#endregion
		
		#region SetDspBufferInfo
		private void SetDspBufferInfo()
		{
			currentResult = NativeMethods.FMOD_System_SetDSPBufferSize(handle, dspBufferLength, numberOfDspBuffers);
		}
		#endregion
		
		#region SetUserFileCallbacks
		private void SetUserFileCallbacks()
		{
			//if (attachUserFileCallbacks)
			//{
				currentResult = NativeMethods.FMOD_System_AttachFileSystem(handle, userFileOpenCallback, userFileCloseCallback, userFileReadCallback, userFileSeekCallback);
				//currentResult = NativeMethods.FMOD_System_SetFileSystem(handle, null, null, null, null, userFileCallbackReadBufferSize);
				//}
				//else
				//{
				//currentResult = NativeMethods.FMOD_System_AttachFileSystem(handle, null, null, null, null);
				//currentResult = NativeMethods.FMOD_System_SetFileSystem(handle, userFileOpenCallback, userFileCloseCallback, userFileReadCallback, userFileSeekCallback, userFileCallbackReadBufferSize);
			//}
		}
		#endregion

		#region Get3dSettings
		private void Get3dSettings()
		{
			currentResult = NativeMethods.FMOD_System_Get3DSettings(handle, ref dopplerScale, ref distanceFactor, ref rollOffScale);
		}
		#endregion

		#region Set3dSettings
		private void Set3dSettings()
		{
			currentResult = NativeMethods.FMOD_System_Set3DSettings(handle, dopplerScale, distanceFactor, rollOffScale);
		}
		#endregion		
		
		#region GetSpeakerPosition
		private void GetSpeakerPosition()
		{
			currentResult = NativeMethods.FMOD_System_GetSpeakerPosition(handle, currentSpeaker, ref currentSpeakerPositionX, ref currentSpeakerPositionY);
		}
		#endregion
		
		#region SetSpeakerPosition
		private void SetSpeakerPosition()
		{
			currentResult = NativeMethods.FMOD_System_SetSpeakerPosition(handle, currentSpeaker, currentSpeakerPositionX, currentSpeakerPositionY);
		}
		#endregion
		
		#region SetStreamBufferInfo
		public void SetStreamBufferInfo()
		{
			currentResult = NativeMethods.FMOD_System_SetStreamBufferSize(handle, streamBufferSize, streamBufferUnit);
		}
		#endregion
		
		#region GetStreamBufferInfo
		public void GetStreamBufferInfo()
		{
			currentResult = NativeMethods.FMOD_System_GetStreamBufferSize(handle, ref streamBufferSize, ref streamBufferUnit);
		}
		#endregion

		#region GetCpuUsageInfo
		private void GetCpuUsageInfo()
		{
			currentResult = NativeMethods.FMOD_System_GetCPUUsage(handle, ref cpuPercentDsp, ref cpuPercentStream, ref cpuPercentUpdate, ref cpuPercentTotal);
		}
		#endregion
				
		#endregion
		
		#region Constructors
		public SoundSystem()
		{
		}
		#endregion
		
		#region Finalizer
		~SoundSystem()
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
					if (drivers != null)
					{
						drivers.Dispose();
						drivers = null;
					}
					
					if (recordDrivers != null)
					{
						recordDrivers.Dispose();
						recordDrivers = null;
					}
					
					if (masterChannelGroup != null)
					{
						masterChannelGroup.Dispose();
						masterChannelGroup = null;
					}
					
					if (dspHead != null)
					{
						dspHead.Dispose();
						dspHead = null;
					}
				}
				
				if (handle != IntPtr.Zero)
				{
					NativeMethods.FMOD_System_Release(handle);
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

		#region MasterChannelGroup
		public ChannelGroup MasterChannelGroup
		{
			get
			{
				if (masterChannelGroup == null)
				{
					currentResult = Result.Ok;
					IntPtr channelGroupHandle = new IntPtr();

					try
					{
						currentResult = NativeMethods.FMOD_System_GetMasterChannelGroup(handle, ref channelGroupHandle);
					}
					catch (System.Runtime.InteropServices.ExternalException)
					{
						currentResult = Result.InvalidParameterError;
					}

					if (currentResult == Result.Ok)
					{
						masterChannelGroup = new ChannelGroup();
						masterChannelGroup.Handle = channelGroupHandle;
					}
				}

				return masterChannelGroup;
			}
		}
		#endregion

		#region DspHead
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
						currentResult = NativeMethods.FMOD_System_GetDSPHead(handle, ref dspHandle);
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
		#endregion

		#region IsInitialized
		public bool IsInitialized
		{
			get {return isInitialized;}
		}
		#endregion

		#region OutputType
		public OutputType OutputType
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetOutput(handle, ref outputType);
				return outputType;
			}
			set
			{
				outputType = value;
				currentResult = NativeMethods.FMOD_System_SetOutput(handle, outputType);
			}
		}
		#endregion
		
		#region Drivers
		public DriverCollection Drivers
		{
			get
			{
				// Lazy initialization
				if (drivers == null)
				{
					drivers = new DriverCollection(this, true);
				}
				
				return drivers;
			}
		}
		#endregion
		
		#region NumberOf2dHardwareChannels
		public int NumberOf2dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return numberOf2dHardwareChannels;
			}			
		}
		#endregion
		
		#region Minimum2dHardwareChannels
		public int Minimum2dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return minimum2dHardwareChannels;
			}
			set
			{
				minimum2dHardwareChannels = value;
				SetHardwareChannels();
			}
		}
		#endregion
		
		#region Maximum2dHardwareChannels
		public int Maximum2dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return maximum2dHardwareChannels;
			}
			set
			{
				maximum2dHardwareChannels = value;
				SetHardwareChannels();
			}
		}
		#endregion
		
		#region NumberOf3dHardwareChannels
		public int NumberOf3dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return numberOf3dHardwareChannels;
			}
		}
		#endregion
		
		#region Minimum3dHardwareChannels
		public int Minimum3dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return minimum3dHardwareChannels;
			}
			set
			{
				minimum3dHardwareChannels = value;
				SetHardwareChannels();
			}
		}
		#endregion
		
		#region Maximum3dHardwareChannels
		public int Maximum3dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return maximum3dHardwareChannels;
			}
			set
			{
				maximum3dHardwareChannels = value;
				SetHardwareChannels();
			}
		}
		#endregion
		
		#region NumberOfSoftwareChannels
		public int NumberOfSoftwareChannels
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetSoftwareChannels(handle, ref numberOfSoftwareChannels);
				return numberOfSoftwareChannels;
			}
			set
			{
				numberOfSoftwareChannels = value;
				currentResult = NativeMethods.FMOD_System_SetSoftwareChannels(handle, numberOfSoftwareChannels);
			}
		}
		#endregion
		
		#region SampleRate
		public int SampleRate
		{
			get
			{
				GetSoftwareFormat();
				return sampleRate;
			}
			set
			{
				sampleRate = value;
				SetSoftwareFormat();
			}
		}
		#endregion
		
		#region FmodSoundFormat
		public FmodSoundFormat SoundFormat
		{
			get
			{
				GetSoftwareFormat();
				return soundFormat;
			}
			set
			{
				soundFormat = value;
				SetSoftwareFormat();
			}
		}
		#endregion
		
		#region NumberOfOutputChannels
		public int NumberOfOutputChannels
		{
			get
			{
				GetSoftwareFormat();
				return numberOfOutputChannels;
			}
			set
			{
				numberOfOutputChannels = value;
				SetSoftwareFormat();
			}
		}
		#endregion
		
		#region MaximumInputChannels
		public int MaximumInputChannels
		{
			get
			{
				GetSoftwareFormat();
				return maximumInputChannels;
			}
			set
			{
				maximumInputChannels = value;
				SetSoftwareFormat();
			}
		}
		#endregion
		
		#region ResampleMethod
		public DspResampler ResampleMethod
		{
			get
			{
				GetSoftwareFormat();
				return resampleMethod;
			}
			set
			{
				resampleMethod = value;
				SetSoftwareFormat();
			}
		}
		#endregion
		
		#region BitsPerSample
		public int BitsPerSample
		{
			get
			{
				GetSoftwareFormat();
				return bitsPerSample;
			}
			set
			{
				bitsPerSample = value;
				SetSoftwareFormat();
			}
		}
		#endregion		
		
		#region DspBufferLength
		[CLSCompliant(false)]
		public uint DspBufferLength
		{
			get
			{
				GetDspBufferInfo();
				return dspBufferLength;
			}
			set
			{
				dspBufferLength = value;
				SetDspBufferInfo();
			}
		}
		#endregion
		
		#region NumberOfDspBuffers
		public int NumberOfDspBuffers
		{
			get
			{
				GetDspBufferInfo();
				return numberOfDspBuffers;
			}
			set
			{
				numberOfDspBuffers = value;
				SetDspBufferInfo();
			}
		}
		#endregion
				
		#region UserFileOpenCallback
		[CLSCompliant(false)]
		public FileOpenCallback UserFileOpenCallback
		{
			get {return userFileOpenCallback;}
			set	{userFileOpenCallback = value;}			
		}
		#endregion
		
		#region UserFileCloseCallback
		public FileCloseCallback UserFileCloseCallback
		{
			get {return userFileCloseCallback;}
			set {userFileCloseCallback = value;}
		}
		#endregion
		
		#region UserFileReadCallback
		[CLSCompliant(false)]
		public FileReadCallback UserFileReadCallback
		{
			get {return userFileReadCallback;}
			set {userFileReadCallback = value;}
		}
		#endregion
		
		#region UserFileSeekCallback
		public FileSeekCallback UserFileSeekCallback
		{
			get {return userFileSeekCallback;}
			set {userFileSeekCallback = value;}
		}
		#endregion
		
		#region UserFileCallbackReadBufferSize
		public int UserFileCallbackReadBufferSize
		{
			get {return userFileCallbackReadBufferSize;}
			set {userFileCallbackReadBufferSize = value;}
		}
		#endregion
		
		#region SpeakerMode
		public SpeakerMode SpeakerMode
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetSpeakerMode(handle, ref speakerMode);
				return speakerMode;				
			}
			set
			{
				speakerMode = value;
				currentResult = NativeMethods.FMOD_System_SetSpeakerMode(handle, speakerMode);
			}						
		}
		#endregion

		#region PluginPath
		public string PluginPath
		{
			get
			{
				return pluginPath;
			}
			set
			{	
				pluginPath = value;
				currentResult = NativeMethods.FMOD_System_SetPluginPath(handle, pluginPath);
			}
		}		
		#endregion

		#region CodecPlugins
		public PluginCollection CodecPlugins
		{
			get
			{
				// Lazy initialization
				if (codecPlugins == null)
				{
					codecPlugins = new PluginCollection(handle, PluginType.Codec, true);
				}
				
				return codecPlugins;
			}			
		}
		#endregion

		#region DspPlugins
		public PluginCollection DspPlugins
		{
			get				
			{
				// Lazy initialization
				if (dspPlugins == null)
				{
					dspPlugins = new PluginCollection(handle, PluginType.Dsp, true);
				}
				
				return dspPlugins;
			}
		}
		#endregion
		
		#region OutputPlugins
		public PluginCollection OutputPlugins
		{
			get
			{
				// Lazy initialization
				if (outputPlugins == null)
				{
					outputPlugins = new PluginCollection(handle, PluginType.Output, true);
				}
				
				return outputPlugins;
			}
		}
		#endregion
		
		#region CurrentOutputPlugin
		/*
		public Plugin CurrentOutputPlugin
		{
			get
			{
				int numberOfOutputPlugins = 0;
				currentResult = FMOD_System_GetNumPlugins(handle, PluginType.Output, ref numberOfOutputPlugins);
				if (numberOfOutputPlugins > 0)
				{
					outputPlugins.Refresh();
				
				
					int id = -1;
					currentResult = FMOD_System_GetOutputByPlugin(handle, ref id);
					if (id > -1 && id <= outputPlugins.Items.Count-1)
					{
						currentOutputPlugin = outputPlugins.Items[id];
					}
					else currentOutputPlugin = null;
				
					return currentOutputPlugin;
				}				
				return null;
				//throw new NotImplementedException("OutputPlugin is not implemented");				 
			}
			set
			{
				// Output plugins cannot be set after Init() is invoked				
				if (!isInitialized)
				{
					currentOutputPlugin = value;
					if (currentOutputPlugin != null)
					{
						currentResult = FMOD_System_SetOutputByPlugin(handle, currentOutputPlugin.Id);
					}
				}				
				//throw new NotImplementedException("OutputPlugin is not implemented");
			}
		}
		*/
		#endregion
		
		#region DopplerScale
		public float DopplerScale
		{
			get
			{
				Get3dSettings();
				return dopplerScale;
			}
			set
			{
				dopplerScale = value;
				Set3dSettings();
			}
		}
		#endregion
		
		#region DistanceFactor
		public float DistanceFactor
		{
			get
			{
				Get3dSettings();
				return distanceFactor;
			}
			set
			{
				distanceFactor = value;
				Set3dSettings();
			}
		}
		#endregion
		
		#region RollOffScale
		public float RollOffScale
		{
			get
			{
				Get3dSettings();
				return rollOffScale;
			}
			set
			{
				rollOffScale = value;
				Set3dSettings();
			}
		}
		#endregion
		
		#region Listeners
		public ListenerCollection Listeners
		{
			get
			{
				// Lazy initialization
				if (listeners == null)
				{
					listeners = new ListenerCollection(handle, true);
				}
				
				return listeners;
			}			
		}
		#endregion
		
		#region CurrentSpeaker
		public Speaker CurrentSpeaker
		{
			get {return currentSpeaker;}
			set {currentSpeaker = value;}
		}
		#endregion

		#region CurrentSpeakerPositionX
		public float CurrentSpeakerPositionX
		{
			get
			{
				GetSpeakerPosition();
				return currentSpeakerPositionX;
			}
			set
			{
				currentSpeakerPositionX = value;
				SetSpeakerPosition();
			}
		}
		#endregion
		
		#region CurrentSpeakerPositionY
		public float CurrentSpeakerPositionY
		{
			get
			{
				GetSpeakerPosition();
				return currentSpeakerPositionY;
			}
			set
			{
				currentSpeakerPositionY = value;
				SetSpeakerPosition();
			}
		}
		#endregion

		#region StreamBufferSize
		[CLSCompliant(false)]
		public uint StreamBufferSize
		{
			get
			{
				GetStreamBufferInfo();
				return streamBufferSize;
			}
			set
			{
				streamBufferSize = value;
				SetStreamBufferInfo();
			}
		}
		#endregion
		
		#region StreamBufferUnit
		public TimeUnits StreamBufferUnit
		{
			get
			{
				GetStreamBufferInfo();
				return streamBufferUnit;
			}
			set
			{
				streamBufferUnit = value;
				//SetStreamBufferInfo();
			}
		}
		#endregion
		
		#region Version
		private uint Version
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetVersion(handle, ref version);
				return version;
			}
		}
		#endregion
		
		#region OutputHandle
		public IntPtr OutputHandle
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetOutputHandle(handle, ref outputHandle);
				return outputHandle;
			}
		}
		#endregion
		
		#region NumberOfChannelsPlaying
		public int NumberOfChannelsPlaying
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetChannelsPlaying(handle, ref numberOfChannelsPlaying);
				return numberOfChannelsPlaying;
			}
		}
		#endregion

		#region CpuPercentageDsp
		public float CpuPercentDsp
		{
			get
			{
				GetCpuUsageInfo();
				return cpuPercentDsp;
			}
		}
		#endregion
		
		#region CpuPercentStream
		public float CpuPercentStream
		{
			get
			{
				GetCpuUsageInfo();
				return cpuPercentStream;
			}
		}
		#endregion
		
		#region CpuPercentUpdate
		public float CpuPercentUpdate
		{
			get
			{
				GetCpuUsageInfo();
				return cpuPercentUpdate;
			}
		}
		#endregion
		
		#region CpuPercentTotal
		public float CpuPercentTotal
		{
			get
			{
				GetCpuUsageInfo();
				return cpuPercentTotal;
			}
		}
		#endregion
		
		#region OpticalDrives
		public OpticalDriveCollection OpticalDrives
		{
			get
			{
				// Lazy initialization
				if (opticalDrives == null)
				{
					opticalDrives = new OpticalDriveCollection(handle, true);
				}
				
				return opticalDrives;
			}
		}
		#endregion

		#region SpectrumArray
		/*
		public float[] SpectrumArray
		{
			get
			{
				GetSpectrumData();
				return spectrumArray;
			}
		}
		*/
		#endregion
		
		#region SpectrumResolution
		/*
		/// <summary>
		/// Gets or sets the resolution to use for spectrum analysis
		/// </summary>
		/// <remarks>minimum = 64, maximum = 8192, only use powers of 2</remarks>
		public int SpectrumResolution
		{
			get {return spectrumResolution;}
			set {spectrumResolution = value;}
		}
		*/
		#endregion

		#region SpectrumChannelOffset
		/*
		/// <summary>
		/// Gets or sets which current channel for spectrum analysis
		/// </summary>
		/// <remarks>0 = left, 1 = right</remarks>
		public int SpectrumChannelOffset
		{
			get {return spectrumChannelOffset;}
			set {spectrumChannelOffset = value;}
		}
		*/
		#endregion
		
		#region SpectrumDspWindowType
		/*
		public DspFftWindow SpectrumDspWindowType
		{
			get {return spectrumDspWindowType;}
			set {spectrumDspWindowType = value;}
		}
		*/
		#endregion
		
		#region WaveArray
		/*
		public float[] WaveArray
		{
			get
			{
				GetWaveData();
				return waveArray;
			}
		}
		*/
		#endregion
		
		#region WaveResolution
		/*
		public int WaveResolution
		{
			get	{return waveResolution;}
		}
		*/
		#endregion
		
		#region WaveChannelOffset
		/*
		public int WaveChannelOffset
		{
			get {return waveChannelOffset;}
		}
		*/
		#endregion
		
		#region	ReverbProperties
		/*
		public ReverbProperties ReverbProperties
		{
			get
			{
				//currentResult = FMOD_System_GetReverbProperties(handle, ref reverbProperties);
				//return reverbProperties;
				throw new NotImplementedException("ReverbProperties is not currently implemented");				
			}
			set
			{
				//reverbProperties = value;
				//currentResult = FMOD_System_SetReverbProperties(handle, reverbProperties);
				throw new NotImplementedException("ReverbProperties is not currently implemented");
			}
		}
		*/
		#endregion
				
		#region RecordDrivers
		public RecordDriverCollection RecordDrivers
		{		
			get
			{
				// Lazy initialization
				if (recordDrivers == null)
				{
					// Output has to be set before this list can be accessed
					recordDrivers = new RecordDriverCollection(this, true);
				}
				
				return recordDrivers;
			}
		}
		#endregion

		#region CurrentRecordDriver
		public Driver CurrentRecordDriver
		{
			get
			{
				int id = -1;
				StringBuilder nameBuilder = new StringBuilder(100);
				currentResult = NativeMethods.FMOD_System_GetRecordDriver(handle, ref id);
				currentResult = NativeMethods.FMOD_System_GetRecordDriverName(handle, id, nameBuilder, nameBuilder.Capacity);
				currentRecordDriver = new Driver(handle, id, nameBuilder.ToString());
				return currentRecordDriver;
			}
			set
			{
				currentRecordDriver = value;
				if (currentRecordDriver != null)
				{
					currentResult = NativeMethods.FMOD_System_SetRecordDriver(handle, currentRecordDriver.Id);
				}
			}
		}
		#endregion

		#region IsRecording
		public bool IsRecording
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_IsRecording(handle, ref isRecording);
				return isRecording;		
			}
		}
		#endregion
		
		#region RecordPosition
		[CLSCompliant(false)]
		public uint RecordPosition
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetRecordPosition(handle, ref recordPosition);
				return recordPosition;
			}
		}
		#endregion
		
		#region MaximumWorldSize
		public float MaximumWorldSize
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetGeometrySettings(handle, ref maximumWorldSize);
				return maximumWorldSize;
			}
			set
			{
				maximumWorldSize = value;
				currentResult = NativeMethods.FMOD_System_SetGeometrySettings(handle, maximumWorldSize);
			}
		}
		#endregion				
		
		#region NetworkProxy
		/*
		public string NetworkProxy
		{
			get
			{
				StringBuilder networkProxyBuilder = new StringBuilder(100);
				currentResult = FMOD_System_GetProxy(handle, networkProxyBuilder, networkProxyBuilder.Capacity);
				return networkProxy.ToString();
				//throw new NotImplementedException("NetworkProxy is not currently implemented");
			}
			set
			{
				networkProxy = value;
				currentResult = FMOD_System_SetProxy(handle, networkProxy);
				//throw new NotImplementedException("NetworkProxy is not currently implemented");
			}
		}
		*/
		#endregion
		
		#region UserData
		public IntPtr UserData
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetUserData(handle, ref userData);
				return userData;
			}
			set
			{				
				userData = value;
				currentResult = NativeMethods.FMOD_System_SetUserData(handle, userData);
			}
		}
		#endregion
		
		#endregion
		
		#region Public Methods

		#region Initialize
		public void Initialize(int maximumChannels, InitializationOptions flags, IntPtr extraData)
		{
			isInitialized = true;
			currentResult = NativeMethods.FMOD_System_Init(handle, maximumChannels, flags, extraData);
		}
		#endregion

		#region Close
		public void Close()
		{
			isInitialized = false;
			currentResult = NativeMethods.FMOD_System_Close(handle);
		}
		#endregion
		
		#region Update
		public void Update()
		{
			currentResult = NativeMethods.FMOD_System_Update(handle);
		}
		#endregion

		#region LoadPlugin
		public void LoadPlugin(Plugin plugin)
		{
			if (plugin != null)
			{
				PluginType type = plugin.Type;
				int id = plugin.Id;
				currentResult = NativeMethods.FMOD_System_LoadPlugin(handle, plugin.FileName, ref type, ref id);
			}
			else throw new ArgumentNullException("plugin");
		}
		#endregion
		
		#region UnloadPlugin
		public void UnloadPlugin(Plugin plugin)
		{
			if (plugin != null)
			{
				currentResult = NativeMethods.FMOD_System_UnloadPlugin(handle, plugin.Type, plugin.Id);
			}
			else throw new ArgumentNullException("plugin");
		}
		#endregion

		#region CreateSound
		/*
		public Sound CreateSound(string filePath, Modes mode, ref CreateSoundExtendedInfo info)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			Sound sound = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateSound(handle, filePath, mode, ref info, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				sound = new Sound();
				sound.Handle = soundHandle;				
			}

			return sound;
		}

		public void CreateSound(string filePath, Modes mode, ref CreateSoundExtendedInfo info, ref Sound sound)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			if (sound == null) sound = new Sound();

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateSound(handle, filePath, mode, ref info, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}

			if (currentResult == Result.Ok)
			{
				sound.Handle = soundHandle;
			}
		}
		*/
		
		internal void CreateSound(string filePath, Modes mode, Sound sound)
		{
			if (sound != null)
			{
				currentResult = Result.Ok;
				IntPtr soundHandle = new IntPtr();				

				try
				{
					currentResult = NativeMethods.FMOD_System_CreateSound(handle, filePath, mode, 0, ref soundHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}

				if (currentResult == Result.Ok)
				{
					sound.Handle = soundHandle;
					sound.SoundSystem = this;
				}
				else throw new ApplicationException("could not create sound: " + currentResult.ToString());
			}
			else throw new ArgumentNullException("sound");
		}

		/* 
		public Sound CreateSound(byte[] data, Modes mode, ref CreateSoundExtendedInfo info)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			Sound sound = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateSound(handle, data, mode, ref info, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				sound = new Sound();
				sound.Handle = soundHandle;				
			}

			return sound;
		}

		public Sound CreateSound(string filePath, Modes mode)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			Sound sound = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateSound(handle, filePath, mode, 0, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				sound = new Sound();
				sound.Handle = soundHandle;				
			}

			return sound;
		}
		*/

		internal void CreateSound(Sound sound, string filePath, Modes mode)
		{
			if (sound != null)
			{
				currentResult = Result.Ok;
				IntPtr soundHandle = new IntPtr();			

				try
				{
					currentResult = NativeMethods.FMOD_System_CreateSound(handle, filePath, mode, 0, ref soundHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}

				if (currentResult == Result.Ok)
				{					
					sound.Handle = soundHandle;
					sound.SoundSystem = this;
				}
				else throw new ApplicationException("could not create sound: " + currentResult.ToString());
			}
			else throw new ArgumentNullException("sound");
		}
		
		internal Sound CreateSound(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				currentResult = Result.Ok;
				IntPtr soundHandle = new IntPtr();
				Sound sound = null;

				try
				{
					currentResult = NativeMethods.FMOD_System_CreateSound(handle, path, (Modes.Hardware | Modes.Fmod2D | Modes.CreateStream | Modes.OpenOnly | Modes.IgnoreTags), 0, ref soundHandle);
					//currentResult = FMOD_System_CreateSound(handle, driveName, (Mode.Hardware | Mode.Fmod2D | Mode.IgnoreTags), 0, ref soundHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}

				if (currentResult == Result.Ok)
				{
					sound = new Sound(this, path);
					sound.Handle = soundHandle;
				}
				else throw new ApplicationException("could not create compact disc sound: " + currentResult.ToString());

				return sound;
			}
			else throw new ArgumentNullException("path");
		}
		
		//public CompactDiscSound CreateCompactDiscSound(char driveLetter)
		//{
			//string driveName = driveLetter + ":";
			//return CreateCompactDiscSound(driveName);
		//}
		#endregion

		#region CreateStream
		internal Sound CreateStream(string filePath, Modes mode, ref CreateSoundExtendedInfo info)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			Sound sound = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateStream(handle, filePath, mode, ref info, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				sound = new Sound(this, new Uri(filePath));
				sound.Handle = soundHandle;
				sound.SoundSystem = this;
			}
			else throw new ApplicationException("could not create stream: " + currentResult.ToString());

			return sound;
		}

		internal Sound CreateStream(Sound sound, byte[] data, Modes mode, ref CreateSoundExtendedInfo info)
		{
			if (sound != null)
			{
				currentResult = Result.Ok;
				IntPtr soundHandle = new IntPtr();				

				try
				{
					currentResult = NativeMethods.FMOD_System_CreateStream(handle, data, mode, ref info, ref soundHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}
				
				if (currentResult == Result.Ok)
				{					
					sound.Handle = soundHandle;				
					sound.SoundSystem = this;
				}
				else throw new ApplicationException("could not create stream: " + currentResult.ToString());

				return sound;
			}
			else throw new ArgumentNullException("sound");
		}

		internal Sound CreateStream(string filePath, Modes mode)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			Sound sound = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateStream(handle, filePath, mode, 0, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				sound = new Sound(this, new Uri(filePath));
				sound.Handle = soundHandle;
				sound.SoundSystem = this;
			}
			else throw new ApplicationException("could not create stream: " + currentResult.ToString());

			return sound;
		}
		
		internal void CreateStream(Sound sound, string filePath, Modes mode)
		{
			if (sound != null)
			{
				currentResult = Result.Ok;
				IntPtr soundHandle = new IntPtr();

				try
				{
					currentResult = NativeMethods.FMOD_System_CreateStream(handle, filePath, mode, 0, ref soundHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}

				if (currentResult == Result.Ok)
				{					
					sound.Handle = soundHandle;
					sound.SoundSystem = this;
				}
				else throw new ApplicationException("could not create stream: " + currentResult.ToString());
			}
			else throw new ArgumentNullException("sound");
		}
		#endregion

		#region CreateDsp
		public Dsp CreateDsp(ref DspDescription description)
		{
			currentResult = Result.Ok;
			IntPtr dspHandle = new IntPtr();
			Dsp dsp = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateDSP(handle, ref description, ref dspHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				dsp = new Dsp();
				dsp.Handle = dspHandle;				
			}

			return dsp;
		}

		/// <summary>
		/// Create a Dsp by DspType
		/// </summary>
		/// <param name="type">The type of Dsp to create</param>
		/// <returns>A new Dsp</returns>
		public Dsp CreateDsp(DspType type)
		{
			currentResult = Result.Ok;
			IntPtr dspHandle = new IntPtr();
			Dsp dsp = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateDSPByType(handle, type, ref dspHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				dsp = new Dsp();
				dsp.Handle = dspHandle;				
			}

			return dsp;
		}

		/// <summary>
		/// Create Dsp by index
		/// </summary>
		/// <param name="index">The index of the Dsp to create</param>		
		/// <returns>A new Dsp</returns>
		public Dsp CreateDsp(int index)
		{
			currentResult = Result.Ok;
			IntPtr dspHandle = new IntPtr();
			Dsp dsp = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateDSPByIndex(handle, index, ref dspHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				dsp = new Dsp();
				dsp.Handle = dspHandle;				
			}

			return dsp;
		}
		#endregion

		#region CreateChannelGroup
		public ChannelGroup CreateChannelGroup(string name)
		{
			currentResult = Result.Ok;
			IntPtr channelGroupHandle = new IntPtr();
			ChannelGroup channelGroup = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateChannelGroup(handle, name, ref channelGroupHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{			
				channelGroup = new ChannelGroup();
				channelGroup.Handle = channelGroupHandle;				
			}

			return channelGroup;
		}
		#endregion

		#region PlaySound
		internal void PlaySound(ChannelIndex channelId, Sound sound, bool paused, ref Channel channel)
		{
			if (sound != null)
			{
				currentResult = Result.Ok;
				IntPtr channelHandle;			

				bool wasMuted = false;

				if (channel != null)
				{
					channelHandle = channel.Handle;
					wasMuted = channel.Mute;
				}
				else
				{
					channel = new Channel();
					channelHandle = new IntPtr();
				}

				try
				{
					currentResult = NativeMethods.FMOD_System_PlaySound(handle, channelId, sound.Handle, paused, ref channelHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}
				
				if (currentResult == Result.Ok)
				{
					channel.Handle = channelHandle;
					
					if (wasMuted)
					{
						channel.Mute = true;
					}
				}
				else
				{
					channel = null;
				}
			}
		}
		#endregion

		#region PlayDsp
		public void PlayDsp(ChannelIndex channelId, Dsp dsp, bool paused, ref Channel channel)
		{
			if (dsp != null)
			{
				currentResult = Result.Ok;
				IntPtr channelHandle;

				if (channel != null)
				{
					channelHandle = channel.Handle;
				}
				else
				{
					channel = new Channel();
					channelHandle = new IntPtr();
				}

				try
				{
					currentResult = NativeMethods.FMOD_System_PlayDSP(handle, channelId, dsp.Handle, paused, ref channelHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}
				
				if (currentResult != Result.Ok)
				{
					channel = new Channel();
					channel.Handle = channelHandle;				
				}
				else
				{
					channel = null;
				}
			}
			else throw new ArgumentNullException("dsp");
		}
		#endregion

		#region AttachUserFileCallbacks
		/// <summary>
		/// Attach the user file callbacks to this system
		/// </summary>
		public void AttachUserFileCallbacks()
		{
			SetUserFileCallbacks();
		}
		#endregion

		#region GetChannel
		public Channel GetChannel(int channelId)
		{
			currentResult = Result.Ok;
			IntPtr channelHandle = new IntPtr();
			Channel channel = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_GetChannel(handle, channelId, ref channelHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				channel = new Channel();
				channel.Handle = channelHandle;				
			}

			return channel;
		}
		#endregion

		#region AddDsp
		public void AddDsp(Dsp dsp)
		{
			if (dsp != null)
			{
				currentResult = NativeMethods.FMOD_System_AddDSP(handle, dsp.Handle);
			}
			else throw new ArgumentNullException("dsp");
		}
		#endregion

		#region LockDsp
		public void LockDsp()
		{
			currentResult = NativeMethods.FMOD_System_LockDSP(handle);
		}
		#endregion

		#region UnlockDsp
		public void UnlockDsp()
		{
			currentResult = NativeMethods.FMOD_System_UnlockDSP(handle);
		}
		#endregion

		#region RecordStart
		/// <summary>
		/// Records from audio input to the given sound
		/// </summary>
		/// <param name="sound">The sound to store input audio data to</param>
		/// <param name="loop">Indicates whether or not to loop recording to the beginning of the sound once the end of the sound is reached</param>
		internal void RecordStart(Sound sound, bool loop)
		{
			if (sound != null)
			{
				currentResult = NativeMethods.FMOD_System_RecordStart(handle, sound.Handle, loop);
			}
		}
		#endregion

		#region RecordStop
		public void RecordStop()
		{
			currentResult = NativeMethods.FMOD_System_RecordStop(handle);
		}		
		#endregion

		#region CreateGeometry
		/// <summary>
		/// Creates a geometry object with the given polygon and vertext constraints
		/// </summary>
		/// <param name="maximumPolygons"></param>
		/// <param name="maximumVertices"></param>
		/// <returns></returns>
		public Geometry CreateGeometry(int maximumPolygons, int maximumVertices)
		{
			currentResult = Result.Ok;
			IntPtr geometryHandle = new IntPtr();
			Geometry geometry = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateGeometry(handle, maximumPolygons, maximumVertices, ref geometryHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}

			if (currentResult == Result.Ok)
			{
				geometry = new Geometry();
				geometry.Handle = geometryHandle;
			}

			return geometry;
		}	

		/// <summary>
		/// Creates a geometry object from memory
		/// </summary>
		/// <param name="data"></param>
		/// <param name="dataSize"></param>
		/// <returns></returns>
		public Geometry CreateGeometry(IntPtr data, int dataSize)
		{
			currentResult = Result.Ok;
			IntPtr geometryHandle = new IntPtr();
			Geometry geometry = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_LoadGeometry(handle, data, dataSize, ref geometryHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult != Result.Ok)
			{
				geometry = new Geometry();
				geometry.Handle = geometryHandle;				
			}
			
			return geometry;
		}		
		#endregion

		#region GetSpectrumData
		public float[] GetSpectrumData(int spectrumResolution, int spectrumChannelOffset, DspFftWindow spectrumDspWindowType)
		{
			float[] spectrumArray = null;
			currentResult = NativeMethods.FMOD_System_GetSpectrum(handle, spectrumArray, spectrumResolution, spectrumChannelOffset, spectrumDspWindowType);
			return spectrumArray;

		}
		#endregion

		#region GetWaveData
		public float[] GetWaveData(int waveResolution, int waveChannelOffset)
		{
			float[] waveArray = null;
			currentResult = NativeMethods.FMOD_System_GetWaveData(handle, waveArray, waveResolution, waveChannelOffset);
			return waveArray;
		}
		#endregion
		
		#endregion

		#region OLD CODE
		/*
	public class FmodAudioPlayer : IDisposable
	{		
		#region Private Fields
		private Channel channel;
		private SoundSystem soundSystem;
		private Sound currentAudio;
		//private bool currentSoundIsCreated;
		//private bool currentSoundIsStreaming;
		private System.Timers.Timer timer;
		private DateTime soundLoadStart = DateTime.MinValue;
		//private PlaybackStatus status;
		//private string streamingStatus;				
		private static Sound ripSound;
		private static Channel ripChannel;
		private static FileStream ripFileStream;
		private static string ripSourceFileName;
		private static string ripDestinationFileName;
		private static FmodSoundType ripSoundType = FmodSoundType.Unknown;
		private static bool ripUpdateFileName;
		private static string ripTrackArtist = string.Empty;
		private static string ripTrackTitle = string.Empty;
		private static System.Timers.Timer ripTimer = new Timer(1000);
		private bool disposed;
		#endregion
		
		#region Constructors
		public FmodAudioPlayer()
		{
			soundSystem = SoundSystemFactory.CreateSoundSystem(true, 64);
		
			InitializeComponent();								
		}
		#endregion
		
		#region IDisposable Members
		protected void Dispose(bool disposing)
		{
			try
			{
				if (!disposed)
				{
					if (disposing)
					{
						if (currentAudio != null)
						{
							currentAudio.Dispose();
							currentAudio = null;
						}

						if (channel != null)
						{
							channel.Dispose();
							channel = null;
						}
					
						if (timer != null)
						{
							timer.Dispose();
							timer = null;
						}
					
						if (ripSound != null)
						{
							ripSound.Dispose();
							ripSound = null;
						}
					
						if (ripChannel != null)
						{
							ripChannel.Dispose();
							ripChannel = null;
						}
					
						if (ripTimer != null)
						{
							ripTimer.Dispose();
							ripTimer = null;
						}
					
						if (soundSystem != null)
						{
							soundSystem.Close();
							soundSystem.Dispose();
							soundSystem = null;
						}
					}
				}
				disposed = true;
			}
			finally
			{
				//base.Dispose(disposing);
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
		
		#region Private Methods
		private void InitializeComponent()
		{
			this.timer = new Timer(1000);
			this.timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
			this.timer.Enabled = true;
		}
		#endregion
		
		#region Private Events
		private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{	
			if (this.Status.Name == PlaybackStatus.Playing.Name)
			{			
				if (this.channel != null && this.currentAudio != null)
				{
					if (this.channel.Position == currentAudio.Milliseconds)
					{
						PlaybackEventArgs args = new PlaybackEventArgs();
					
						if (OnSoundEnd != null)
						{
							OnSoundEnd(this, args);
						}
					}
				}
			}
		
			if (currentSoundIsStreaming) // && this.Status.Name == PlaybackStatus.Playing.Name)
			{
				bool checkForTimeout = false;
			
				if (currentSoundIsCreated)
				{
					if (currentAudio.OpenState == OpenState.Ready && channel == null && !currentAudio.BufferIsStarving)
					{
						if (streamingStatus != currentAudio.OpenState.ToString())
						{
							streamingStatus = currentAudio.OpenState.ToString();
							if (OnStreamingStatusChange != null)
								OnStreamingStatusChange(this, new PlaybackEventArgs(true, null, streamingStatus, null));
						}
							
						soundSystem.PlaySound(ChannelIndex.Free, currentAudio, false, ref channel);
					}
					else if (currentAudio.OpenState == OpenState.Loading)
					{
						if (streamingStatus != currentAudio.OpenState.ToString())
						{
							streamingStatus = currentAudio.OpenState.ToString();
							if (OnStreamingStatusChange != null)
								OnStreamingStatusChange(this, new PlaybackEventArgs(true, null, streamingStatus, null));
						}
					
						checkForTimeout = true;
					}
					else if (currentAudio.OpenState == OpenState.Connecting)
					{
						if (streamingStatus != currentAudio.OpenState.ToString())
						{
							streamingStatus = currentAudio.OpenState.ToString();
							if (OnStreamingStatusChange != null)
								OnStreamingStatusChange(this, new PlaybackEventArgs(true, null, streamingStatus, null));
						}

						checkForTimeout = true;
					}
					
					if (currentAudio.CurrentResult != Result.Ok)
						throw new AlexandriaException("Could not play streaming sound: " + currentAudio.CurrentResult.ToString());
					
					if (soundSystem.CurrentResult != Result.Ok)
						throw new AlexandriaException("Could not play streaming sound: " + soundSystem.CurrentResult.ToString());
				}
				else checkForTimeout = true;
				
				if (checkForTimeout)
				{
					if (soundLoadStart == DateTime.MinValue)
					{
						soundLoadStart = DateTime.Now;
					}
					else
					{
						TimeSpan span = DateTime.Now - soundLoadStart;
						if (span.TotalMilliseconds >= this.SoundLoadTimeout)
						{
							OnSoundLoadTimeout(this, EventArgs.Empty);
							
							if (streamingStatus != "Streaming Timeout")
							{
								streamingStatus = "Streaming Timeout";
								if (OnStreamingStatusChange != null)
									OnStreamingStatusChange(this, new PlaybackEventArgs(false, null, streamingStatus, null));
							}
						}
					}
				}

				if (this.OnPlaybackTimerTick != null)
					OnPlaybackTimerTick(this, e);

				if (channel != null)
				{
					//uint ms = 0;
					//bool playing = false;
					//bool paused = false;
					
					//for (; ; )
					//{						
						//Tag tag = new Tag();						
						//if (currentSound.GetTag(null, -1, ref tag) != Result.Ok)
						//{
						//	break;
						//}
						//if (tag.DataType == TagDataType.String)
						//{
							//textBox.Text = tag.name + " = " + Marshal.PtrToStringAnsi(tag.data) + " (" + tag.datalen + " bytes)";
						//}
						//else
						//{
						//	break;
						//}
					//}
					
					if (soundSystem.CurrentResult != Result.Ok)
						throw new AlexandriaException("Error streaming current sound: " + soundSystem.CurrentResult.ToString());

					//result = channel.getPaused(ref paused);
					//ERRCHECK(result);
					//result = channel.isPlaying(ref playing);
					//ERRCHECK(result);
					//result = channel.getPosition(ref ms, FMOD.TIMEUNIT.MS);
					//ERRCHECK(result);

					//statusBar.Text = "Time " + (ms / 1000 / 60) + ":" + (ms / 1000 % 60) + ":" + (ms / 10 % 100) + (openstate == FMOD.OPENSTATE.BUFFERING ? " Buffering..." : (openstate == FMOD.OPENSTATE.CONNECTING ? " Connecting..." : (paused ? " Paused       " : (playing ? " Playing      " : " Stopped      ")))) + "(" + percentbuffered + "%)" + (starving ? " STARVING" : "        ");
				}			

				if (soundSystem != null)
				{
					soundSystem.Update();					
				}
			}
		}
		#endregion
		
		#region Internal Properties
		internal SoundSystem SoundSystem
		{
			get {return soundSystem;}
		}
		#endregion
		
		#region Public Properties
		public bool IsMuted
		{
			get
			{
				if (channel != null)
					return this.channel.Mute;				
				else return false;
			}
			set
			{
				if (channel != null)
					this.channel.Mute = value;
			}
		}

		[CLSCompliant(false)]
		public uint Position
		{
			get
			{
				if (channel != null)
					return this.channel.Position;				
				else return 0;
			}
		}
		
		public Channel Channel
		{
			get
			{
				if (channel == null) channel = new Channel();
				
				return channel;
			}
		}
		
		public float Volume
		{
			get
			{
				if (channel != null)
					return this.channel.Volume;
				else return 0f;
			}
			set
			{
				if (channel != null)
					this.channel.Volume = value;
			}
		}
		
		//public override GetAudioInfo GetAudioInfoHandler
		//{
			//get {return new GetAudioInfo(this.GetAudioInfo);}
		//}
		
		public string CurrentResult
		{
			get {return this.soundSystem.CurrentResult.ToString();}
		}
		
		//public System.ComponentModel.ISynchronizeInvoke TimerSynch
		//{
			//get {return timer.SynchronizingObject;}
			//set {timer.SynchronizingObject = value;}
		//}
		
		public override double PlaybackInterval
		{
			get {return this.timer.Interval;}
			set {this.timer.Interval = value;}
		}
		
		/// <summary>
		/// Get or set the size of the stream buffer in raw bytes
		/// </summary>
		[CLSCompliant(false)]
		public uint StreamBufferSize
		{
			get
			{
				soundSystem.StreamBufferUnit = TimeUnits.RawByte;
				return soundSystem.StreamBufferSize;
			}
			set
			{
				soundSystem.StreamBufferUnit = TimeUnits.RawByte;
				soundSystem.StreamBufferSize = value;
			}
		}
		#endregion
		
		#region Public Methods
		public override void SetCurrentMediaFile(MediaFile mediaFile)
		{
			currentAudio = null;
			currentSoundIsCreated = false;
			currentSoundIsStreaming = false;
			
			streamingStatus = "Not Streaming";
			
			if (OnStreamingStatusChange != null)
				OnStreamingStatusChange(this, new PlaybackEventArgs(true, null, streamingStatus, null));
			
			base.SetCurrentMediaFile(mediaFile);
			if (mediaFile.IsLocal)
			{
				currentSoundIsStreaming = false;
				//currentSound = soundSystem.CreateStream(mediaFile.Path, Modes.None);
				soundSystem.CreateStream(currentAudio, mediaFile.Path, Modes.None);
				//FmodSoundType t = currentSound.Type;
				//Result x = soundSystem.CurrentResult;				
			}
			else
			{
				try
				{
					if (!currentSoundIsCreated)
					{
						// Increase stream buffer size to account for lag
						soundSystem.StreamBufferUnit = TimeUnits.RawByte;
						soundSystem.StreamBufferSize = (64 * 1024);
						//currentSound = soundSystem.CreateSound(mediaFile.Path, (Mode.Hardware | Mode.Fmod2D | Mode.CreateStream | Mode.NonBlocking));
						//currentSound = soundSystem.CreateSound(mediaFile.Path, (Modes.Software | Modes.Fmod2D | Modes.CreateStream | Modes.NonBlocking));
						soundSystem.CreateSound(currentAudio, mediaFile.Path, (Modes.Software | Modes.Fmod2D | Modes.CreateStream | Modes.NonBlocking));

						currentSoundIsCreated = true;
						currentSoundIsStreaming = true;
						
						if (soundSystem.CurrentResult != Result.Ok)
						{
							streamingStatus = "Error: " + soundSystem.CurrentResult.ToString();
							if (OnStreamingStatusChange != null)
								OnStreamingStatusChange(this, new PlaybackEventArgs(false, null, streamingStatus, null));
							throw new AlexandriaException(soundSystem.CurrentResult.ToString());
						}
						else
						{
							streamingStatus = "Started Streaming";
							if (OnStreamingStatusChange != null)
								OnStreamingStatusChange(this, new PlaybackEventArgs(true, null, streamingStatus, null));
						}
					}
				}
				catch (Exception ex)
				{
					throw new AlexandriaException("There was an error loading sound from file: " + mediaFile.Path + "\n" + ex.Message, ex);
				}
			}
			
			if (currentAudio != null)
			{
				currentAudio.MediaFile = mediaFile;
			}
		}

		[CLSCompliant(false)]
		public void SetPosition(uint position)
		{
			if (channel != null)
				this.channel.Position = position;
			else throw new AlexandriaException("Could not set the position in the current sound because the output channel was not initialized");
		}

		public AudioInfo GetAudioInfo(MediaFile file)
		{
			AudioInfo info = null;
			if (file != null)
			{
				uint length = 0;
				if (file.IsLocal)
				{
					Sound sound = new Sound(this.SoundSystem, file);
					soundSystem.CreateSound(sound, file.Path, (Modes.Software|Modes.Fmod2D|Modes.OpenOnly|Modes.IgnoreTags));
					sound.LengthUnit = TimeUnits.Millisecond;
					length = sound.FmodLength;
					sound.Dispose();
				}
				info = new AudioInfo(null, length);
			}
			return info;
		}
		#endregion
		
		#region IAudioPlayer Members
		public string CurrentStatus
		{
			get { return null; }
		}
		
		public double PlaybackInterval
		{
			get { return 0L; }
			set { }
		}
		
		public double SoundLoadTimeout
		{
			get { return 0L; }
			set { }
		}
		
		public void Play()
		{
		}
		
		public void Pause()
		{
		}
		
		public void Stop()
		{
		}
		
		[CLSCompliant(false)]
		public void Seek(bool forward, uint length)
		{
		}
		
		//public void SetStatus(PlaybackStatus status)
		//{
			//this.status = status;
		//}

		public void PlayCurrentSound()
		{					
			if (currentAudio != null)
			{
				PlaybackEventArgs e = new PlaybackEventArgs();

				if (OnPlay != null)
				{
					OnPlay(this, e);
				}

				if (e.IsValid)
				{
					if (!currentSoundIsStreaming)
					{
						if (channel == null) channel = new Channel();										
				
						if (!channel.Paused)
						{
							try
							{
								Debug.WriteLine("soundSystem.PlaySound");
								// The channel was not already paused so start playing a new stream
								soundSystem.PlaySound(ChannelIndex.Free, currentAudio, false, ref channel);
							}
							catch (Exception ex)
							{
								throw new AlexandriaException(ex);
							}
						}
						else
						{
							// The channel was already paused so we can just resume playback
							channel.Paused = false;
						}
					}
					else
					{
						if (channel != null)
						{
							if (channel.IsPlaying && !channel.Paused)
							{
								channel.Paused = true;
							}
							else
							{
								channel.Paused = false;
							}
						}
					}
				}
			}
			else throw new InvalidOperationException("No sound was defined for the audio player to play");
		}

		public void PauseCurrentSound()
		{
			if (currentAudio != null)
			{
				PlaybackEventArgs e = new PlaybackEventArgs();
				if (this.OnPause != null)
				{
					OnPause(this, e);
				}
				
				if (e.IsValid)
				{
					if (channel != null)
					{
						Debug.WriteLine("channel.Pause");
						this.channel.Paused = true;
					}
					else throw new InvalidOperationException("Could not pause the audio player: the output channel is not defined");
				}
			}
			else throw new InvalidOperationException("Could not pause the audio player: the current sound is not defined");
		}

		public void StopCurrentSound()
		{
			if (currentAudio != null)
			{
				PlaybackEventArgs e = new PlaybackEventArgs();
				if (OnStop != null)
				{
					OnStop(this, e);
				}
				
				if (e.IsValid)
				{
					if (channel != null)
					{
						Debug.WriteLine("channel.Stop");
						this.channel.Stop();
					}
					else
					{
						//throw new InvalidOperationException("Could not stop the audio player: the output channel is not defined");
					}
				}
			}
			else throw new InvalidOperationException("Could not stop the audio player: the current sound is not defined");
		}
		#endregion

		#region Stream Save Methods
		
		#region SaveStreamToLocalFile
		public void SaveStreamToLocalFile(string sourceFilePath, string destinationFilePath)
		{
			System.Diagnostics.Debug.WriteLine("SaveStreamToLocalFile");
		
			try
			{
				ripSourceFileName = sourceFilePath;
				ripDestinationFileName = destinationFilePath;
			
				// Increase the size of the buffer
				soundSystem.StreamBufferUnit = TimeUnits.RawByte;
				soundSystem.StreamBufferSize = (128 * 1024);

				// Attach the file system callbacks
				soundSystem.UserFileOpenCallback = new FileOpenCallback(OpenRippingStream);
				soundSystem.UserFileCloseCallback = new FileCloseCallback(CloseRippingStream);
				soundSystem.UserFileReadCallback = new FileReadCallback(ReadRippingStream);
				soundSystem.AttachUserFileCallbacks();

				// Create the ripSound
				ripSound = null; //new Sound(this, MediaFile.Load(ripSourceFileName));
				soundSystem.CreateSound(ripSourceFileName, (Modes.Hardware | Modes.Fmod2D | Modes.CreateStream | Modes.NonBlocking), ripSound);
				if (soundSystem.CurrentResult != Result.Ok)
					throw new AlexandriaException("The sound system could not create a new sound: " + soundSystem.CurrentResult.ToString());
				else if (ripSound == null)
					throw new AlexandriaException("The sound was not created successfully: " + soundSystem.CurrentResult.ToString());
				else if (ripSound.CurrentResult != Result.Ok)
					throw new AlexandriaException("The sound was not created successfully: " + ripSound.CurrentResult.ToString());

				// Initialize the ripTimer and start it
				ripTimer.AutoReset = true;				
				ripTimer.Elapsed += new ElapsedEventHandler(ripTimer_Elapsed);
				ripTimer.Enabled = true;
			}
			catch (Exception ex)
			{
				throw new AlexandriaException("There was an error saving this stream to a local file: " + ripSound.CurrentResult.ToString(), ex);
			}
		}
		#endregion

		#region SaveStreamToLocalFile Events
		private void ripTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{						
			if (ripSound != null)
			{
				System.Diagnostics.Debug.WriteLine("ripTimer_Elapsed OpenState=" + ripSound.OpenState.ToString());	
				if (ripSound.OpenState == OpenState.Ready && ripChannel == null)
				{
					System.Diagnostics.Debug.WriteLine("Playing streaming sound");
				
					soundSystem.PlaySound(ChannelIndex.Free, ripSound, false, ref ripChannel);

					if (ripSound.CurrentResult != Result.Ok)
						throw new AlexandriaException("Unable to read from streaming sound for ripping: " + ripSound.CurrentResult.ToString());
					if (soundSystem.CurrentResult != Result.Ok)
						throw new AlexandriaException("Unable to read from streaming sound for ripping: " + soundSystem.CurrentResult.ToString());
				}
			}

			if (ripChannel != null)
			{
				if (ripSound.NumberOfUpdatedTags != 0)
				{
					for (; ; )
					{
						//Tag tag = ripSound.GetTag(null, -1);

						Tag tag = new Tag();
						if (NativeMethods.FMOD_Sound_GetTag(ripSound.Handle, null, -1, ref tag) != Result.Ok)
						{
							// If no tag was found then stop
							break;
						}

						if (tag.DataType == TagDataType.String)
						{
							FmodSoundFormat testFormat = ripSound.FmodSoundFormat;

							if (tag.Name == "ARTIST")
							{
								if (Marshal.PtrToStringAnsi(tag.Data) != ripTrackArtist)
								{
									ripTrackArtist = Marshal.PtrToStringAnsi(tag.Data);
									ripUpdateFileName = true;
								}
							}
							if (tag.Name == "TITLE")
							{
								if (Marshal.PtrToStringAnsi(tag.Data) != ripTrackTitle)
								{
									ripTrackTitle = Marshal.PtrToStringAnsi(tag.Data);
									ripUpdateFileName = true;
								}
							}
							break;
						}
						else
						{
							break;
						}
					}
				}

				if (ripSound.CurrentResult != Result.Ok || !ripChannel.IsPlaying)
				{
					//System.Diagnostics.Debug.WriteLine("Disposing of ripSound due to invalid CurrentResult or the channel stopped");
					
					ripSound.Dispose();
					ripSound = null;
					ripChannel = null;
				}
				else
				{
					//currentResult = ripChannel.Paused;
					//ERRCHECK(result);
					//result = channel.getPosition(ref ms, FMOD.TIMEUNIT.MS);
					//ERRCHECK(result);
					//statusBar.Text = "Time " + (ms / 1000 / 60) + ":" + (ms / 1000 % 60) + ":" + (ms / 10 % 100) + (paused ? " Paused " : playing ? " Playing" : " Stopped");
				}
			}

			if (ripSound != null)
			{
				if (ripSound.OpenState == OpenState.Error)
				{
					//System.Diagnostics.Debug.WriteLine("Disposing of ripSound due to invalid OpenState");
				
					ripSound.Dispose();
					ripSound = null;
					ripChannel = null;
				}
			}

			if (ripSound == null || ripSound.OpenState == OpenState.Error)
			{
				if (ripSound == null)
					ripSound = new Sound(this.SoundSystem, new Uri(ripSourceFileName));
			
				soundSystem.CreateSound(ripSourceFileName, (Modes.Hardware | Modes.Fmod2D | Modes.CreateStream | Modes.NonBlocking), ripSound);				

				if (soundSystem.CurrentResult != Result.Ok)
					throw new AlexandriaException("An error occurred while ripping the stream, the stream could not be restarted: " + soundSystem.CurrentResult.ToString());
				if (ripSound == null)
					throw new AlexandriaException("Could not restart the sound for ripping: " + soundSystem.CurrentResult.ToString());
			}

			if (soundSystem != null)
			{
				soundSystem.Update();
			}
		}
		#endregion

		#region SaveStreamToLocalFile Callback Methods
		private static Result OpenRippingStream(string name, int unicode, ref uint filelength, ref IntPtr handle, ref IntPtr userData)
		{
			//System.Diagnostics.Debug.WriteLine("OpenRippingStream ripFileName=" + ripDestinationFileName);
		
			try
			{
				ripFileStream = new FileStream(ripDestinationFileName, FileMode.Create, FileAccess.Write);
			}
			catch (Exception ex)
			{
				throw new AlexandriaException("There was an error opening the ripping stream: " + ex.Message, ex);
			}
			return Result.Ok;
		}

		private static Result CloseRippingStream(IntPtr handle, IntPtr userData)
		{
			//System.Diagnostics.Debug.WriteLine("CloseRippingStream ripDestinationFileName=" + ripDestinationFileName == null ? string.Empty : ripDestinationFileName);
		
			try
			{
				// Close the file
				ripFileStream.Close();
			
				// Rename the file if the destination file name has changed
				//if (ripDestinationFileName != ripFileStream.Name)
				//{
					//File.Move(ripFileStream.Name, ripDestinationFileName);
				//}

				// Stop the channel and the timer
				ripChannel.Stop();
				ripTimer.Stop();
			}
			catch (Exception ex)
			{
				throw new AlexandriaException("There was an error closing the ripping stream: " + ex.Message, ex);
			}

			return Result.Ok;
		}

		private static Result ReadRippingStream(IntPtr handle, IntPtr buffer, uint sizeInBytes, ref uint bytesRead, IntPtr userData)
		{
			//System.Diagnostics.Debug.WriteLine("ReadRippingStream sizeInBytes=" + sizeInBytes.ToString() + " timestamp=" + DateTime.Now.ToString());
		
			if (sizeInBytes > 0)
			{
				// Write to the file stream
				byte[] fileBuffer = new byte[(int)sizeInBytes];
				Marshal.Copy(buffer, fileBuffer, 0, (int)sizeInBytes);
				ripFileStream.Write(fileBuffer, 0, (int)sizeInBytes);

				// If the main thread detected a change in the file name
				// update the ripDestinationFileName accordingly
				if (ripUpdateFileName)
				{
					string ext;

					ripUpdateFileName = false;

					switch (ripSoundType)
					{
						case FmodSoundType.Mpeg:       // MP2/MP3
							{
								ext = ".mp3";
								break;
							}
						case FmodSoundType.OggVorbis:  // Ogg vorbis
							{
								ext = ".ogg";
								break;
							}
						default:
							{
								ext = ".unknown";
								break;
							}
					}

					ripDestinationFileName = @"C:\" + ripTrackArtist + " - " + ripTrackTitle + ext;
				}
			}
			else
			{				
				CloseRippingStream(IntPtr.Zero, IntPtr.Zero);
			}

			return Result.Ok;
		}
		#endregion
		
		#endregion
	}
	*/
		#endregion
	}
}
