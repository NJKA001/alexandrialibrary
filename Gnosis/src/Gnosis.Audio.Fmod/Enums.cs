using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Fmod
{		
	#region Result
	/// <summary>
	/// Error codes.  Every method in the FMOD library returns a Result
	/// </summary>
	/// <remarks>Platforms: Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable</remarks>
	public enum Result
	{
		/// <summary>
		/// No errors
		/// </summary>
		Ok,
		
		/// <summary>
		/// Tried to call lock a second time before unlock was called.
		/// </summary>
		AlreadyLockedError,
		
		/// <summary>
		/// Tried to call a function on a data type that does not allow this type of functionality (ie calling Sound::lock / Sound_Lock on a streaming sound).
		/// </summary>
		BadCommandError,
		
		/// <summary>
		/// Neither NTSCSI nor ASPI could be initialised.
		/// </summary>
		CddaDriversError,
		
		/// <summary>
		/// An error occurred while initialising the CDDA subsystem.
		/// </summary>
		CddaInitializationError,
		
		/// <summary>
		/// Couldn't find the specified device.
		/// </summary>
		CddaInvalidDeviceError,
		
		/// <summary>
		/// No audio tracks on the specified disc.
		/// </summary>
		CddaNoAudioError,
		
		/// <summary>
		/// No CD/DVD devices were found.
		/// </summary>
		CddaNoDevicesError,
		
		/// <summary>
		/// No disc present in the specified drive.
		/// </summary>
		CddaNoDiscError,
		
		/// <summary>
		/// A CDDA read error occurred.
		/// </summary>
		CddaReadError,
		
		/// <summary>
		/// Error trying to allocate a channel.
		/// </summary>
		ChannelAllocateError,
		
		ChannelStolenError,             /* The specified channel has been reused to play another sound. */
		
		ComError,                       /* A Win32 COM related error occured. COM failed to initialize or a QueryInterface failed meaning a Windows codec or driver was not installed properly. */
		
		DmaError,                       /* DMA Failure.  See debug output for more information. */
		
		DspConnectionError,             /* DSP connection error.  Either the connection caused a cyclic dependancy or the unit issues variable sized reads and tried to connect to a node that was already connected to. */
		DspFormatError,                 /* DSP Format error.  A DSP unit may have attempted to connect to this network with the wrong format.  IE a floating point unit on a PocketPC system. */
		DspNotFoundError,               /* DSP connection error.  Couldn't find the DSP unit specified. */
		DspRunningError,                /* DSP error.  Cannot perform this operation while the network is in the middle of running.  This will most likely happen if a connection or disconnection is attempted in a DSP callback. */
		DspTooManyConnectionsError,     /* DSP connection error.  The unit being connected to or disconnected should only have 1 input or output. */
		FileBadError,                   /* Error loading file. */
		FileCouldNotSeekError,          /* Couldn't perform seek operation.  This is a limitation of the medium (ie netstreams) or the file format. */
		FileEndOfFileError,             /* End of file unexpectedly reached while trying to read essential data (truncated data?). */
		FileNotFoundError,              /* File not found. */
		FormatError,                    /* Unsupported file or audio format. */
		HttpError,                      /* A HTTP error occurred. This is a catch-all for HTTP errors not listed elsewhere. */
		HttpAccessError,                /* The specified resource requires authentication or is forbidden. */
		HttpProxyAuthenticationError,   /* Proxy authentication is required to access the specified resource. */
		HttpServerError,                /* A HTTP server error occurred. */
		HttpTimeoutError,               /* The HTTP request timed out. */
		InitializationError,            /* FMOD was not initialized correctly to support this function. */
		InitializedError,               /* Cannot call this command after System_Init. */
		InternalError,                  /* An error occured that wasnt supposed to.  Contact support. */
		InvalidHandleError,             /* An invalid object handle was used. */
		InvalidParameterError,          /* An invalid parameter was passed to this function. */
		IrxError,                       /* PS2 only.  fModex.irx failed to initialize.  This is most likely because you forgot to load it. */
		MemoryError,                    /* Not enough memory or resources. */
		MemoryIopError,                 /* PS2 only.  Not enough memory or resources on PlayStation 2 IOP ram. */
		MemorySoundRamError,            /* Not enough memory or resources on console sound ram. */
		NeedSoftwareError,              /* Tried to use a feature that requires the software engine but the software engine has been turned off. */
		NetworkConnectError,            /* Couldn't connect to the specified host. */
		NetworkSocketError,             /* A socket error occurred.  This is a catch-all for socket-related errors not listed elsewhere. */
		NetworkUrlError,                /* The specified URL couldn't be resolved. */
		NotReadyError,                  /* Operation could not be performed because specified sound is not ready. */
		OutputAllocatedError,           /* Error initializing output device, but more specifically, the output device is already in use and cannot be reused. */
		OutputCreateBufferError,        /* Error creating hardware sound buffer. */
		OutputDriverCallError,          /* A call to a standard soundcard driver failed, which could possibly mean a bug in the driver or resources were missing or exhausted. */
		OutputFormatError,              /* Soundcard does not support the minimum features needed for this soundsystem (16bit stereo output). */
		OutputInitError,                /* Error initializing output device. */
		OutputNoHardwareError,          /* HARDWARE was specified but the sound card does not have the resources nescessary to play it. */
		OutputNoSoftwareError,          /* Attempted to create a software sound but no software channels were specified in System::init. */
		PanError,                       /* Panning only works with mono or stereo sound sources. */
		PluginError,                    /* An unspecified error has been returned from a 3rd party plugin. */
		PluginMissingError,             /* A requested output, dsp unit type or codec was not available. */
		PluginResourceError,            /* A resource that the plugin requires cannot be found. */
		RecordError,                    /* An error occured trying to initialize the recording device. */
		ReverbInstanceError,            /* Specified Instance in FMOD_ReverbProperties couldn't be set. Most likely because another application has locked the EAX4 FX slot.*/
		SubSoundAllocatedError,         /* This subsound is already being used by another sound, you cannot have more than one parent to a sound.  Null out the other parent's entry first. */
		TagNotFoundError,               /* The specified Tag could not be found or there are no Tags. */
		TooManyChannelsError,           /* The sound created exceeds the allowable input channel count.  This can be increased with System::setMaxInputChannels */
		UnimplementedError,             /* Something in FMOD hasn't been implemented when it should be! contact support! */
		UninitializedError,             /* This command failed because System_Init or System_SetDriver was not called. */
		UnsupportedError,               /* A commmand issued was not supported by this object.  Possibly a plugin without certain callbacks specified. */
		UpdateError,                    /* An error caused by System::update occured. */
		VersionError,                   /* The Version number of this file format is not supported. */
       
		//NOTE: New values for version 0.4.12.0
		EventFailedError,               /* An Event failed to be retrieved, most likely due to 'just fail' being specified as the max playbacks behavior. */
        EventInternalError,             /* An error occured that wasn't supposed to.  See debug log for reason. */
        EventInfoOnlyError,             /* Can't execute this command on an EVENT_INFOONLY event. */
        EventMaxStreamsError,           /* Event failed because 'Max streams' was hit when FMOD_INIT_FAIL_ON_MAXSTREAMS was specified. */
        EventMismatchError,             /* FSB mis-matches the FEV it was compiled with. */
        EventNameConflictError,         /* A category with the same name already exists. */
        EventNotFoundError,             /* The requested event, event group, event category or event property could not be found. */		
	}
	#endregion

	#region OutputType
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	These output types are used with System::setOutput/System::getOutput/FMOD_System_SetOutput/FMOD_System_GetOutput, to choose which output method to use.

	[REMARKS]
	To drive the output synchronously, and to disable FMOD's timing thread, use the FMOD_INIT_NONREALTIME flag.
    
	To pass information to the driver when initializing fmod use the extradriverdata parameter for the following reasons.
	<li>FMOD_OutputType_WAVWRITER - extradriverdata is a pointer to a char * filename that the wav writer will output to.
	<li>FMOD_OutputType_WAVWRITER_NRT - extradriverdata is a pointer to a char * filename that the wav writer will output to.
	<li>FMOD_OutputType_DSOUND - extradriverdata is a pointer to a HWND so that FMOD can set the focus on the audio for a particular window.
	<li>FMOD_OutputType_GC - extradriverdata is a pointer to a FMOD_ARAMBLOCK_INFO struct. This can be found in fmodgc.h.
	Currently these are the only FMOD drivers that take extra information.  Other unknown plugins may have different requirements.

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	System::setOutput
	System::getOutput
	System::setSoftwareFormat
	System::getSoftwareFormat
	System::init
	]
	*/
	public enum OutputType
	{
		/// <summary>
		/// Picks the best output Mode for the platform.  This is the default.
		/// </summary>
		AutoDetect,
		
		/// <summary>
		/// [All Platforms] 3rd party plugin, unknown.  This is for use with System::getOutput only.
		/// </summary>
		Unknown,
		
		/// <summary>
		/// [All Platforms] Calls in this Mode succeed but make no sound.
		/// </summary>
		NoSound,
		
		/// <summary>
		/// [All Platforms] Writes output to fmodout.wav by default.  Use System::setSoftwareFormat to set the filename.
		/// </summary>
		WavWriter,
		
		/// <summary>
		/// [All Platforms] Non-realtime Version of FMOD_OutputType_NOSOUND.  User can drive mixer with System::update at whatever rate they want.
		/// </summary>
		NoSoundNonRealTime,
		
		/// <summary>
		/// [All Platforms] Non-realtime Version of FMOD_OutputType_WAVWRITER.  User can drive mixer with System::update at whatever rate they want.
		/// </summary>
		WavWriterNonRealTime,
		
		/// <summary>
		/// [Win32/Win64] DirectSound output.  Use this to get EAX Reverb support.
		/// </summary>
		DirectSound,
		
		/// <summary>
		/// [Win32/Win64] Windows Multimedia output.
		/// </summary>
		WindowsMultimedia,
		
		/// <summary>
		/// [Win32] Low latency ASIO driver.
		/// </summary>
		Asio,
		
		/// <summary>
		/// [Linux] Open Sound System output.
		/// </summary>
		Oss,
		
		/// <summary>
		/// [Linux] Advanced Linux Sound Architecture output.
		/// </summary>
		Alsa,
		
		/// <summary>
		/// [Linux] Enlightment Sound Daemon output.
		/// </summary>
		Esd,
		
		/// <summary>
		/// [Mac] Macintosh SoundManager output.
		/// </summary>
		SoundManager,
		
		/// <summary>
		/// [Mac] Macintosh CoreAudio output.
		/// </summary>
		CoreAudio,
		
		/// <summary>
		/// [Xbox] Native hardware output.
		/// </summary>
		XBox,
		
		/// <summary>
		/// [PS2] Native hardware output.
		/// </summary>
		PS2,
		
		/// <summary>
		/// [GameCube] Native hardware output.
		/// </summary>
		GameCube,
		
		/// <summary>
		/// [Xbox 360] Native hardware output.
		/// </summary>
		XBox360,
		
		/// <summary>
		/// [PSP] Native hardware output.
		/// </summary>
		Psp
		
		// NOTE: this value was defined in the reference API but I have
		// chosen to exclude it because it is deprecated by C# best practices
		//Maximum
	}
	#endregion

	#region Capabilities
	/*
	[ENUM] 
	[
	[DESCRIPTION]   

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]
	]
	*/
	[Flags]
	public enum Capabilities
	{
		None = 0x00000000,    /* Device has no special capabilities. */
		Hardware = 0x00000001,    /* Device supports hardware mixing. */
		HardwareEmulated = 0x00000002,    /* Device supports FMOD_HARDWARE but it will be mixed on the CPU by the kernel (not FMOD's software mixer). */
		OutputMultipleChannel = 0x00000004,    /* Device can do multichannel output, ie greater than 2 channels. */
		OutputFormatPcm8 = 0x00000008,    /* Device can output to 8bit integer PCM. */
		OutputFormatPcm16 = 0x00000010,    /* Device can output to 16bit integer PCM. */
		OutputFormatPcm24 = 0x00000020,    /* Device can output to 24bit integer PCM. */
		OutputFormatPcm32 = 0x00000040,    /* Device can output to 32bit integer PCM. */
		OutputFormatPcmFloat = 0x00000080,    /* Device can output to 32bit floating point PCM. */
		ReverbEax2 = 0x00000100,    /* Device supports EAX2 reverb. */
		ReverbEax3 = 0x00000200,    /* Device supports EAX3 reverb. */
		ReverbEax4 = 0x00000400,    /* Device supports EAX4 reverb  */
		ReverbI3dl2 = 0x00000800,    /* Device supports I3DL2 reverb. */
		ReverbLimited = 0x00001000     /* Device supports some form of limited hardware reverb, maybe parameterless and only selectable by environment. */
	}
	#endregion

	#region SpeakerMode
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	These are Speaker types defined for use with the System::setSpeakerMode / System_SetSpeakerMode or 
	System::getSpeakerMode / System_GetSpeakerMode command.

	[REMARKS]
	These are important notes on Speaker Modes in regards to sounds created with SOFTWARE.
	Note below the phrase 'sound channels' is used.  These are the subchannels inside a sound, they are not related and 
	have nothing to do with the FMOD class "Channel / CHANNEL".
	For example a mono sound has 1 sound channel, a stereo sound has 2 sound channels, and an AC3 or 6 channel wav file have 6 "sound channels".

	FMOD_SpeakerMode_RAW
	---------------------
	This Mode is for output devices that are not specifically mono/streo/5.1 or 7.1, but are multichannel.
	Sound channels map to Speakers sequentially, so a mono sound maps to output Speaker 0, stereo sound maps to output Speaker 0 & 1.
	Multichannel sounds map input channels to output channels 1:1. 
	Channel::setPan / Channel_SetPan and Channel::setSpeakerMix / Channel_SetSpeakerMix do not work.
	Speaker levels must be manually set with Channel::setSpeakerLevels / Channel_SetSpeakerLevels.

	SpeakerMode_MONO
	---------------------
	This Mode is for a 1 Speaker arrangement.
	Panning does not work in this Speaker Mode.
	Mono, stereo and multichannel sounds have each sound channel played on the one Speaker unity.
	Mix behaviour for multichannel sounds can be set with Channel::setSpeakerLevels / Channel_SetSpeakerLevels.
	Channel::setSpeakerMix / Channel_SetSpeakerMix does not work.

	SpeakerMode_STEREO
	-----------------------
	This Mode is for 2 Speaker arrangements that have a left and right Speaker.
	Mono sounds default to an even distribution between left and right.  They can be panned with Channel::setPan / Channel_SetPan.
	Stereo sounds default to the middle, or full left in the left Speaker and full right in the right Speaker.  
	They can be cross faded with Channel::setPan / Channel_SetPan.
	Multichannel sounds have each sound channel played on each Speaker at unity.
	Mix behaviour for multichannel sounds can be set with Channel::setSpeakerLevels / Channel_SetSpeakerLevels.
	Channel::setSpeakerMix / Channel_SetSpeakerMix works but only left and right parameters are used, the rest are ignored.

	FMOD_SpeakerMode_4POINT1
	------------------------
	This Mode is for 4.1 Speaker arrangements that have a left/right/center/rear and a subwoofer Speaker.
	Mono sounds default to the center Speaker.  They can be panned with Channel::setPan.
	Stereo sounds default to the left sound channel played on the front left, and the right sound channel played on the front right.  
	They can be cross faded with Channel::setPan.
	Multichannel sounds default to all of their sound channels being played on each Speaker in order of input.
	Mix behaviour for multichannel sounds can be set with Channel::setSpeakerLevels.
	Channel::setSpeakerMix works but side left / side right are ignored, and rear left / rear right are averaged.

	SpeakerMode_5POINT1
	------------------------
	This Mode is for 5.1 Speaker arrangements that have a left/right/center/rear left/rear right and a subwoofer Speaker.
	Mono sounds default to the center Speaker.  They can be panned with Channel::setPan / Channel_SetPan.
	Stereo sounds default to the left sound channel played on the front left, and the right sound channel played on the front right.  
	They can be cross faded with Channel::setPan / Channel_SetPan.
	Multichannel sounds default to all of their sound channels being played on each Speaker in order of input.  
	Mix behaviour for multichannel sounds can be set with Channel::setSpeakerLevels / Channel_SetSpeakerLevels.
	Channel::setSpeakerMix / Channel_SetSpeakerMix works but side left / side right are ignored.

	SpeakerMode_7POINT1
	------------------------
	This Mode is for 7.1 Speaker arrangements that have a left/right/center/rear left/rear right/side left/side right 
	and a subwoofer Speaker.
	Mono sounds default to the center Speaker.  They can be panned with Channel::setPan / Channel_SetPan.
	Stereo sounds default to the left sound channel played on the front left, and the right sound channel played on the front right.  
	They can be cross faded with Channel::setPan / Channel_SetPan.
	Multichannel sounds default to all of their sound channels being played on each Speaker in order of input.  
	Mix behaviour for multichannel sounds can be set with Channel::setSpeakerLevels / Channel_SetSpeakerLevels.
	Channel::setSpeakerMix / Channel_SetSpeakerMix works and every parameter is used to set the balance of a sound in any Speaker.

	SpeakerMode_PROLOGIC
	------------------------------------------------------
	This Mode is for mono, stereo, 5.1 and 7.1 Speaker arrangements, as it is backwards and forwards compatible with stereo, 
	but to get a surround effect a Dolby Prologic or Prologic 2 hardware decoder / amplifier is needed.
	Pan behaviour is the same as SpeakerMode_5POINT1.

	If this function is called the numoutputchannels setting in System::setSoftwareFormat is overwritten.

	For 3D sounds, panning is determined at runtime by the 3D subsystem based on the Speaker Mode to determine which Speaker the 
	sound should be placed in.

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]
	System::setSpeakerMode
	System::getSpeakerMode
	Channel::setSpeakerLevels
	]
	*/
	public enum SpeakerMode
	{
		Raw,				/* There is no specific SpeakerMode.  Sound channels are mapped in order of input to output.  See remarks for more information. */
		Mono,				/* The Speakers are monaural. */
		Stereo,				/* The Speakers are stereo (default value). */
		Surround4Point1,	/* 4.1 Speaker setup.  This includes front, center, left, rear and a subwoofer. Also known as a "quad" Speaker configuration. */
		Surround5Point1,	/* 5.1 Speaker setup.  This includes front, center, left, rear left, rear right and a subwoofer. */
		Surround7Point1,	/* 7.1 Speaker setup.  This includes front, center, left, rear left, rear right, side left, side right and a subwoofer. */
		ProLogic			/* Stereo output, but data is encoded in a way that is picked up by a Prologic/Prologic2 decoder and split into a 5.1 Speaker setup. */
	}
	#endregion

	#region SpeakerPosition
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	These are Speaker types defined for use with the Channel::setSpeakerLevels / Channel_SetSpeakerLevels command.

	[REMARKS]
	If you are using SpeakerMode_NONE and Speaker assignments are meaningless, just cast a raw integer value to this type.
	For example (FMOD_Speaker)7 would use the 7th Speaker (also the same as Speaker_SIDE_RIGHT).  
	Values higher than this can be used if an output system has more than 8 Speaker types / output channels.  15 is the current maximum.

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]
	Channel::setSpeakerLevels
	Channel::getSpeakerLevels
	]
	*/
	public enum SpeakerPosition
	{
		FrontLeft,
		FrontRight,
		FrontCenter,
		Subwoofer,
		BackLeft,
		BackRight,
		SideLeft,
		SideRight,
		Max
	}
	#endregion

	#region PluginType
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	These are plugin types defined for use with the System::getNumPlugins / System_GetNumPlugins, 
	System::getPluginInfo / System_GetPluginInfo and System::unloadPlugin / System_UnloadPlugin functions.

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]
	System::getNumPlugins
	System::getPluginInfo
	System::unloadPlugin
	]
	*/
	public enum PluginType
	{
		Output,     /* The plugin type is an output module.  FMOD mixed audio will play through one of these devices */
		Codec,      /* The plugin type is a file format codec.  FMOD will use these codecs to load file formats for playback. */
		Dsp         /* The plugin type is a DSP unit.  FMOD will use these plugins as part of its DSP network to apply effects to output or generate sound in realtime. */
	}
	#endregion

	#region InitializationOptions
	/*
	[ENUM] 
	[
	[DESCRIPTION]   
	Initialization flags.  Use them with System::init in the flags parameter to change various behaviour.  

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]
	System::init
	]
	*/
	[Flags]
	public enum InitializationOptions
	{
		/// <summary>
		/// [All platforms] - Initialize normally
		/// </summary>
		None = 0,   
		StreamFromUpdate = 0x00000001,   /* All platforms - No stream thread is created internally.  Streams are driven from System::update.  Mainly used with non-realtime outputs. */
		Fmod3DRightHanded = 0x00000002,   /* All platforms - FMOD will treat +X as left, +Y as up and +Z as forwards. */
		DisableSoftware = 0x00000004,   /* All platforms - Disable software mixer to save memory.  Anything created with FMOD_SOFTWARE will fail and DSP will not work. */
		DirectSoundDeferred = 0x00000100,   /* Win32 only - for DirectSound output - 3D commands are batched together and executed at System::update. */
		DirectSoundHrtfNone = 0x00000200,   /* Win32 only - for DirectSound output - FMOD_HARDWARE | FMOD_3D buffers use simple stereo panning/doppler/attenuation when 3D hardware acceleration is not present. */
		DirectSoundHrtfLight = 0x00000400,   /* Win32 only - for DirectSound output - FMOD_HARDWARE | FMOD_3D buffers use a slightly higher quality algorithm when 3D hardware acceleration is not present. */
		DirectSoundHrtfFull = 0x00000800,   /* Win32 only - for DirectSound output - FMOD_HARDWARE | FMOD_3D buffers use full quality 3D playback when 3d hardware acceleration is not present. */
		PS2DisableCore0Reverb = 0x00010000,   /* PS2 only - Disable reverb on CORE 0 to regain SRAM. */
		PS2DisableCore1Reverb = 0x00020000,   /* PS2 only - Disable reverb on CORE 1 to regain SRAM. */
		XBoxRemoveHeadroom = 0x00100000,   /* XBox only - By default DirectSound attenuates all sound by 6db to avoid clipping/distortion.  CAUTION.  If you use this flag you are responsible for the final mix to make sure clipping / distortion doesn't happen. */
	}
	#endregion

	#region FmodSoundType
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	These definitions describe the type of song being played.

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	Sound::getFormat
	]
	*/
	public enum FmodSoundType
	{
		Unknown,         /* 3rd party / unknown plugin format. */
		Aac,             /* AAC.  Currently unsupported. */
		Aiff,            /* AIFF. */
		Asf,             /* Microsoft Advanced Systems Format (ie WMA/ASF/WMV). */
		At3,             /* Sony ATRAC 3 format */
		Cdda,            /* Digital CD audio. */
		Dls,             /* Sound font / downloadable sound bank. */
		Flac,            /* FLAC lossless codec. */
		Fsb,             /* FMOD Sample Bank. */
		GameCubeAdpcm,   /* GameCube ADPCM */
		IT,              /* Impulse Tracker. */
		Midi,            /* MIDI. */
		Mod,             /* Protracker / Fasttracker MOD. */
		Mpeg,            /* MP2/MP3 MPEG. */
		OggVorbis,       /* Ogg vorbis. */
		Playlist,        /* Information only from ASX/PLS/M3U/WAX playlists */
		Raw,             /* Raw PCM data. */
		S3m,             /* ScreamTracker 3. */
		Sf2,             /* Sound font 2 format. */
		User,            /* User created sound. */
		Wav,             /* Microsoft WAV. */
		XM,              /* FastTracker 2 XM. */
		Xma,             /* Xbox360 XMA */
		Vag	
		/*
		UNKNOWN,    // 3rd party / unknown plugin format.
        AIFF,       // AIFF
        ASF,        // Microsoft Advanced Systems Format (ie WMA/ASF/WMV)
        AAC,        // AAC
        CDDA,       // Digital CD audio
        DLS,        // Sound font / downloadable sound bank.
        FLAC,       // FLAC lossless codec.
        FSB,        // FMOD Sample Bank
        GCADPCM,    // GameCube ADPCM
        IT,         // Impulse Tracker.
        MIDI,       // MIDI
        MOD,        // Protracker / Fasttracker MOD.
        MPEG,       // MP2/MP3 MPEG.
        OGGVORBIS,  // Ogg vorbis.
        PLAYLIST,   // Information only from ASX/PLS/M3U/WAX playlists
        S3M,        // ScreamTracker 3.
        SF2,        // Sound font 2 format.
        RAW,        // Raw PCM data.
        USER,       // User created sound
        WAV,        // Microsoft WAV.
        XM          // FastTracker 2 XM.
		*/
		
		/*
		Unknown,    //3rd party / unknown plugin format.
		Aiff,       // AIFF
		Asf,        // Microsoft Advanced Systems Format (ie WMA/ASF/WMV)
		Aac,        // AAC
		Cdda,       // Digital CD audio
		Dls,        // Sound font / downloadable sound bank.
		Flac,       // FLAC lossless codec.
		Fsb,        // FMOD Sample Bank
		GameCubeAdpcm,    // GameCube ADPCM
		IT,         // Impulse Tracker.
		Midi,       // MIDI
		Mod,        // Protracker / Fasttracker MOD.
		Mpeg,       // MP2/MP3 MPEG.
		OggVorbis,  // Ogg vorbis.
		Playlist,   // Information only from ASX/PLS/M3U/WAX playlists
		S3m,        // ScreamTracker 3.
		Sf2,        // Sound font 2 format.
		Raw,        // Raw PCM data.
		User,       // User created sound
		Wav,        // Microsoft WAV.
		XM          // FastTracker 2 XM.
		*/
	}
	#endregion

	#region FmodSoundFormat
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	These definitions describe the native format of the hardware or software buffer that will be used.

	[REMARKS]
	This is the format the native hardware or software buffer will be or is created in.

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]
	System::createSoundEx
	Sound::getFormat
	]
	*/
	public enum FmodSoundFormat
	{
		None,     /* Unitialized / unknown */
		Pcm8,     /* 8bit integer PCM data */
		Pcm16,    /* 16bit integer PCM data  */
		Pcm24,    /* 24bit integer PCM data  */
		Pcm32,    /* 32bit integer PCM data  */
		PcmFloat, /* 32bit floating point PCM data  */
		GameCubeAdpcm,  /* Compressed GameCube DSP data */
		XBoxAdpcm,   /* Compressed XBox ADPCM data */
		Vag       /* Compressed PlayStation 2 ADPCM data */
	}
	#endregion

	#region Modes
	/*
	[DEFINE]
	[
	[NAME] 
	FMOD_Mode

	[DESCRIPTION]   
	Sound description bitfields, bitwise OR them together for loading and describing sounds.

	[REMARKS]
	By default a sound will open as a static sound that is decompressed fully into memory.<br>
	To have a sound stream instead, use FMOD_CREATESTREAM.<br>
	Some opening Modes (ie FMOD_OPENUSER, FMOD_OPENMEMORY, FMOD_OPENRAW) will need extra information.<br>
	This can be provided using the FMOD_CreateSoundExInfo structure.

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]
	System::createSound
	System::createStream
	Sound::setMode
	Sound::getMode
	Channel::setMode
	Channel::getMode
	]
	*/
	[Flags]
	public enum Modes
	{
		/// <summary>
		/// Default sound type.  Equivalent to all the defaults listed below.  FMOD_LOOP_OFF, FMOD_2D, FMOD_HARDWARE.
		/// </summary>
		None = 0,
		LoopOff = 0x00000001,  /* For non looping sounds. (default).  Overrides FMOD_LOOP_NORMAL / FMOD_LOOP_BIDI. */
		LoopNormal = 0x00000002,  /* For forward looping sounds. */
		LoopBidirectional = 0x00000004,  /* For bidirectional looping sounds. (only works on software mixed static sounds). */
		Fmod2D = 0x00000008,  /* Ignores any 3d processing. (default). */
		Fmod3D = 0x00000010,  /* Makes the sound positionable in 3D.  Overrides FMOD_2D. */
		Hardware = 0x00000020,  /* Attempts to make sounds use hardware acceleration. (default). */
		Software = 0x00000040,  /* Makes sound reside in software.  Overrides FMOD_HARDWARE.  Use this for FFT, DSP, 2D multi Speaker support and other software related features. */
		CreateStream = 0x00000080,  /* Decompress at runtime, streaming from the source provided (standard stream).  Overrides FMOD_CREATESAMPLE. */
		CreateSample = 0x00000100,  /* Decompress at loadtime, decompressing or decoding whole file into memory as the target sample format. (standard sample). */
		OpenUser = 0x00000400,  /* Opens a user created static sample or stream. */
		OpenMemory = 0x00000800,  /* "name_or_data" will be interpreted as a pointer to memory instead of filename for creating sounds. */
		OpenRaw = 0x00001000,  /* Will ignore file format and treat as raw pcm.  User may need to declare if data is FMOD_SIGNED or FMOD_UNSIGNED */
		OpenOnly = 0x00002000,  /* Just open the file, dont prebuffer or read.  Good for fast opens for info, or when sound::readData is to be used. */
		AccurateTime = 0x00004000,  /* For FMOD_CreateSound - for accurate Sound::getLengthMs/Channel::setTime on VBR MP3, AAC and MOD/S3M/XM/IT/MIDI files.  Scans file first, so takes longer to open. FMOD_OPENONLY does not affect this. */
		MpegSearch = 0x00008000,  /* For corrupted / bad MP3 files.  This will search all the way through the file until it hits a valid MPEG header.  Normally only searches for 4k. */
		NonBlocking = 0x00010000,  /* For opening sounds asyncronously, return value from open function must be polled for when it is ready. */
		Unique = 0x00020000,  /* Unique sound, can only be played one at a time */
		Fmod3DHeadRelative = 0x00040000,  /* Make the sound's position, velocity and orientation relative to the listener's position, velocity and orientation. */
		Fmod3DWorldRelative = 0x00080000,  /* Make the sound's position, velocity and orientation absolute. (default) */
		Fmod3DLogarithmicRollOff = 0x00100000,  /* This sound will follow the standard logarithmic rolloff Model where mindistance = full volume, maxdistance = where sound stops attenuating, and rolloff is fixed according to the global rolloff factor.  (default) */
		Fmod3DLinearRollOff = 0x00200000,  /* This sound will follow a linear rolloff Model where mindistance = full volume, maxdistance = silence.  */
		CddaForceAspi = 0x00400000,  /* For CDDA sounds only - use ASPI instead of NTSCSI to access the specified CD/DVD device. */
		CddaJitterCorrect = 0x00800000,  /* For CDDA sounds only - perform jitter correction. Jitter correction helps produce a more accurate CDDA stream at the cost of more CPU time. */
		Unicode = 0x01000000,  /* Filename is double-byte unicode. */
		IgnoreTags = 0x02000000,  /* Skips id3v2/asf/etc Tag checks when opening a sound, to reduce seek/read overhead when opening files (helps with CD performance). */
	}
	#endregion

	#region OpenState
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	These values describe what state a sound is in after NONBLOCKING has been used to open it.

	[REMARKS]    

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]
	Sound::getOpenState
	Mode
	]
	*/
	public enum OpenState
	{
		Ready = 0,       /* Opened and ready to play */
		Loading,         /* Initial load in progress */
		Error,           /* Failed to open - file not found, out of memory etc.  See return value of Sound::getOpenState for what happened. */
		Connecting,      /* Connecting to remote host (internet sounds only) */
		Buffering        /* Buffering data */
	}
	#endregion

	#region ChannelCallbackType
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	These callback types are used with Channel::setCallback.

	[REMARKS]
	Each callback has commanddata parameters passed int unique to the type of callback.
	See reference to FMOD_ChannelCallback to determine what they might mean for each type of callback.

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	Channel::setCallback
	FMOD_ChannelCallback
	]
	*/
	public enum ChannelCallbackType
	{
		End,                  /* Called when a sound ends. */
		VirtualVoice,         /* Called when a voice is swapped out or swapped in. */
		SyncPoint,            /* Called when a syncpoint is encountered.  Can be from wav file markers. */
		ModNotImplemented,    /* Not implemented Zxx */
		ModRow,               /* Not implemented */
		ModOrder,             /* Not implemented */
		ModInst,              /* Not implemented */
		Max
	}
	#endregion

	#region DspFftWindow
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	List of windowing methods used in spectrum analysis to reduce leakage / transient signals intefering with the analysis.
	This is a problem with analysis of continuous signals that only have a small portion of the signal sample (the fft window size).
	Windowing the signal with a curve or triangle tapers the sides of the fft window to help alleviate this problem.

	[REMARKS]
	Cyclic signals such as a sine wave that repeat their cycle in a multiple of the window size do not need windowing.
	I.e. If the sine wave repeats every 1024, 512, 256 etc samples and the FMOD fft window is 1024, then the signal would not need windowing.
	Not windowing is the same as FMOD_DspFFTWindow_RECT, which is the default.
	If the cycle of the signal (ie the sine wave) is not a multiple of the window size, it will cause frequency abnormalities, so a different windowing method is needed.
	<exclude>

	FMOD_DspFFTWindow_RECT.
	<img src = "rectangle.gif"></img>

	FMOD_DspFFTWindow_TRIANGLE.
	<img src = "triangle.gif"></img>

	FMOD_DspFFTWindow_HAMMING.
	<img src = "hamming.gif"></img>

	FMOD_DspFFTWindow_HANNING.
	<img src = "hanning.gif"></img>

	FMOD_DspFFTWindow_BLACKMAN.
	<img src = "blackman.gif"></img>

	FMOD_DspFFTWindow_BLACKMANHARRIS.
	<img src = "blackmanharris.gif"></img>
	</exclude>

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	System::getSpectrum
	Channel::getSpectrum
	]
	*/
	public enum DspFftWindow
	{
		Rectangle,      /* w[n] = 1.0                                                                                            */
		Triangle,       /* w[n] = TRI(2n/N)                                                                                      */
		Hamming,        /* w[n] = 0.54 - (0.46 * COS(n/N) )                                                                      */
		Hanning,        /* w[n] = 0.5 *  (1.0  - COS(n/N) )                                                                      */
		Blackman,       /* w[n] = 0.42 - (0.5  * COS(n/N) ) + (0.08 * COS(2.0 * n/N) )                                           */
		BlackmanHarris, /* w[n] = 0.35875 - (0.48829 * COS(1.0 * n/N)) + (0.14128 * COS(2.0 * n/N)) - (0.01168 * COS(3.0 * n/N)) */
		Max
	}
	#endregion

	#region DspResampler
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	List of interpolation types that the FMOD Ex software mixer supports.  

	[REMARKS]
	The default resampler type is FMOD_DspResampler_LINEAR.<br>
	Use System::setSoftwareFormat to tell FMOD the resampling quality you require for FMOD_SOFTWARE based sounds.

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	System::setSoftwareFormat
	System::getSoftwareFormat
	]
	*/		
	public enum DspResampler
	{
		NoInterpolation,        /* No interpolation.  High frequency aliasing hiss will be audible depending on the sample rate of the sound. */
		Linear,          /* Linear interpolation (default method).  Fast and good quality, causes very slight lowpass effect on low frequency sounds. */
		Cubic,           /* Cubic interoplation.  Slower than linear interpolation but better quality. */
		Spline,          /* 5 point spline interoplation.  Slowest resampling method but best quality. */
		Max,             /* Maximum number of resample methods supported. */
	}
	#endregion

	#region TagType
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	List of Tag types that could be stored within a sound.  These include id3 Tags, metadata from netstreams and vorbis/asf data.

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	Sound::getTag
	]
	*/
	public enum TagType
	{
		Unknown = 0,
		ID3v1,
		ID3v2,
		VorbisComment,
		ShoutCast,
		IceCast,
		Asf,
		Midi,
		Playlist,
		Fmod,
		User
	}
	#endregion
		
	#region TagDataType
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	List of data types that can be returned by Sound::getTag

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	Sound::getTag
	]
	*/
	public enum TagDataType
	{
		Binary = 0,
		Int,
		Float,
		String,
		StringUtf16,
		StringUtf16BE,
		StringUtf8,
		CDToc
	}
	#endregion
		
	#region TimeUnits
	/*
	[ENUM]
	[
	[DESCRIPTION]   
	List of time types that can be returned by Sound::getLength and used with Channel::setPosition or Channel::getPosition.

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	Sound::getLength
	Channel::setPosition
	Channel::getPosition
	]
	*/
	[Flags]
	public enum TimeUnits
	{
		Millisecond = 0x00000001,  /* Milliseconds. */
		PcmSample = 0x00000002,  /* PCM Samples, related to milliseconds * samplerate / 1000. */
		PcmByte = 0x00000004,  /* Bytes, related to PCM samples * channels * datawidth (ie 16bit = 2 bytes). */
		RawByte = 0x00000008,  /* Raw file bytes of (compressed) sound data (does not include headers).  Only used by Sound::getLength and Channel::getPosition. */
		ModOrder = 0x00000100,  /* MOD/S3M/XM/IT.  Order in a sequenced module format.  Use Sound::getFormat to determine the format. */
		ModRow = 0x00000200,  /* MOD/S3M/XM/IT.  Current row in a sequenced module format.  Sound::getLength will return the number if rows in the currently playing or seeked to pattern. */
		ModPattern = 0x00000400,  /* MOD/S3M/XM/IT.  Current pattern in a sequenced module format.  Sound::getLength will return the number of patterns in the song and Channel::getPosition will return the currently playing pattern. */
		SentenceMillisecond = 0x00010000,  /* Currently playing subsound in a sentence time in milliseconds. */
		SentencePcmSample = 0x00020000,  /* Currently playing subsound in a sentence time in PCM Samples, related to milliseconds * samplerate / 1000. */
		SentencePcmByte = 0x00040000,  /* Currently playing subsound in a sentence time in bytes, related to PCM samples * channels * datawidth (ie 16bit = 2 bytes). */
		Sentence = 0x00080000,  /* Currently playing sentence index according to the channel. */
		SentenceSubSound = 0x00100000,  /* Currently playing subsound index in a sentence. */
		Buffered = 0x10000000,  /* Time value as seen by buffered stream.  This is always ahead of audible time, and is only used for processing. */
	}
	#endregion

	#region ChannelIndex
	/*
	[ENUM] 
	[
	[NAME] 
	FMOD_MISC_VALUES

	[DESCRIPTION]
	Miscellaneous values for FMOD functions.

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]
	System::playSound
	System::playDSP
	System::getChannel
	]
	*/
	public enum ChannelIndex
	{
		None = 0,
		Free = -1,     /* For a channel index, FMOD chooses a free voice using the priority system. */
		Reuse = -2      /* For a channel index, re-use the channel handle that was passed in. */
	}
	#endregion
	
	#region SourceType
	public enum SourceType
	{
		Sound = 0,
		Channel = 1
	}
	#endregion
		
	#region DSP Enums
		
	#region DspType
	/*
	[ENUM]
	[
    [DESCRIPTION]   
    These definitions can be used for creating FMOD defined special effects or DSP units.

    [REMARKS]
    To get them to be active, first create the unit, then add it somewhere into the DSP network, either at the front of the network near the soundcard unit to affect the global output (by using System::getDSPHead), or on a single channel (using Channel::getDSPHead).

    [PLATFORMS]
    Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

    [SEE_ALSO]
    System::createDSPByType
	]
	*/
	public enum DspType
	{
		Unknown,            /* This unit was created via a non FMOD plugin so has an unknown purpose */
		Mixer,              /* This unit does nothing but take inputs and mix them together then feed the result to the soundcard unit. */
		Oscillator,         /* This unit generates sine/square/saw/triangle or noise tones. */
		LowPass,            /* This unit filters data using a resonant lowpass filter algorithm. */
		ItLowPass,          /* This unit filters sound using a resonant lowpass filter algorithm that is used in Impulse Tracker. */
		HighPass,           /* This unit filters sound using a resonant highpass filter algorithm. */
		Echo,               /* This unit produces an echo on the sound and fades out at the desired rate. */
		Flange,             /* This unit produces a flange effect on the sound. */
		Distortion,         /* This unit distorts the sound. */
		Normalize,          /* This unit normalizes or amplifies the sound to a certain level. */
		ParameterEqualizer, /* This unit attenuates or amplifies a selected frequency range. */
		PitchShift,         /* This unit bends the pitch of a sound without changing the speed of playback. */
		Chorus,             /* This unit produces a chorus effect on the sound. */
		Reverb,             /* This unit produces a reverb effect on the sound. */
		VstPlugin,          /* This unit allows the use of Steinberg VST plugins */
		WinAmpPlugin        /* This unit allows the use of Nullsoft Winamp plugins */
	}
	#endregion

	#region DSP Effect Parameters
	/*
    ==============================================================================================================

    FMOD built in effect parameters.  
    Use DSP::setParameter with these enums for the 'index' parameter.

    ==============================================================================================================
	*/

	#region DspOscillator
	/*
	[ENUM]
	[  
    [DESCRIPTION]   
    Parameter types for the FMOD_DspType_OSCILLATOR filter.

    [REMARKS]

    [PLATFORMS]
    Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

    [SEE_ALSO]
    DSP::setParameter
    DSP::getParameter
    FMOD_DspType   
	]
	*/
	public enum DspOscillator
	{
		Type,   /* Waveform type.  0 = sine.  1 = square. 2 = sawup. 3 = sawdown. 4 = triangle. 5 = noise.  */
		Rate    /* Frequency of the sinewave in hz.  1.0 to 22000.0.  Default = 220.0. */
	}
	#endregion

	#region DspLowPass
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_LOWPASS filter.

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	]
	*/
	public enum DspLowPass
	{
		Cutoff,    /* Lowpass cutoff frequency in hz.   1.0 to 22000.0.  Default = 5000.0. */
		Resonance  /* Lowpass resonance Q value. 1.0 to 10.0.  Default = 1.0. */
	}
	#endregion

	#region DspItLowPass
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_ITLOWPASS filter.

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	]
	*/
	public enum DspItLowPass
	{
		Cutoff,    /* Lowpass cutoff frequency in hz.  1.0 to 22000.0.  Default = 5000.0/ */
		Resonance  /* Lowpass resonance Q value.  0.0 to 127.0.  Default = 1.0. */
	}
	#endregion

	#region DspHighPass
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_HIGHPASS filter.

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	]
	*/
	public enum DspHighPass
	{
		Cutoff,    /* Highpass cutoff frequency in hz.  10.0 to output 22000.0.  Default = 5000.0. */
		Resonance  /* Highpass resonance Q value.  1.0 to 10.0.  Default = 1.0. */
	}
	#endregion

	#region DspEcho
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_ECHO filter.

	[REMARKS]
	Note.  Every time the delay is changed, the plugin re-allocates the echo buffer.  This means the echo will dissapear at that time while it refills its new buffer.<br>
	Larger echo delays result in larger amounts of memory allocated.<br>
	<br>
	'<i>maxchannels</i>' also dictates the amount of memory allocated.  By default, the maxchannels value is 0.  If FMOD is set to stereo, the echo unit will allocate enough memory for 2 channels.  If it is 5.1, it will allocate enough memory for a 6 channel echo, etc.<br>
	If the echo effect is only ever applied to the global mix (ie it was added with System::addDSP), then 0 is the value to set as it will be enough to handle all Speaker Modes.<br>
	When the echo is added to a channel (ie Channel::addDSP) then the channel count that comes in could be anything from 1 to 8 possibly.  It is only in this case where you might want to increase the channel count above the output's channel count.<br>
	If a channel echo is set to a lower number than the sound's channel count that is coming in, it will not echo the sound.<br>

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	]
	*/
	public enum DspEcho
	{
		Delay,       /* Echo delay in ms.  10  to 5000.  Default = 500. */
		DecayRatio,  /* Echo decay per delay.  0 to 1.  1.0 = No decay, 0.0 = total decay.  Default = 0.5. */
		MaxChannels, /* Maximum channels supported.  0 to 16.  0 = same as fmod's default output polyphony, 1 = mono, 2 = stereo etc.  See remarks for more.  Default = 0.  It is suggested to leave at 0! */
		DryMix,      /* Volume of original signal to pass to output.  0.0 to 1.0. Default = 1.0. */
		WetMix       /* Volume of echo signal to pass to output.  0.0 to 1.0. Default = 1.0. */
	}
	#endregion

	#region DspFlange
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_FLANGE filter.

	[REMARKS]
	Flange is an effect where the signal is played twice at the same time, and one copy slides back and forth creating a whooshing or flanging effect.<br>
	As there are 2 copies of the same signal, by default each signal is given 50% mix, so that the total is not louder than the original unaffected signal.<br>
	<br>
	Flange depth is a percenTage of a 10ms shift from the original signal.  Anything above 10ms is not considered flange because to the ear it begins to 'echo' so 10ms is the highest value possible.<br>

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	]
	*/
	public enum DspFlange
	{
		DryMix,      /* Volume of original signal to pass to output.  0.0 to 1.0. Default = 0.45. */
		WetMix,      /* Volume of flange signal to pass to output.  0.0 to 1.0. Default = 0.55. */
		Depth,       /* Flange depth.  0.01 to 1.0.  Default = 1.0. */
		Rate         /* Flange speed in hz.  0.0 to 20.0.  Default = 0.1. */
	}
	#endregion

	#region DspDistortion
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_DISTORTION filter.

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	]
	*/
	public enum DspDistortion
	{
		Level    /* Distortion value.  0.0 to 1.0.  Default = 0.5. */
	}
	#endregion

	#region DspNormalize
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_NORMALIZE filter.

	[REMARKS]
	Normalize amplifies the sound based on the maximum peaks within the signal.<br>
	For example if the maximum peaks in the signal were 50% of the bandwidth, it would scale the whole sound by 2.<br>
	The lower threshold value makes the normalizer ignores peaks below a certain point, to avoid over-amplification if a loud signal suddenly came in, and also to avoid amplifying to maximum things like background hiss.<br>
	<br>
	Because FMOD is a realtime audio processor, it doesn't have the luxury of knowing the peak for the whole sound (ie it can't see into the future), so it has to process data as it comes in.<br>
	To avoid very sudden changes in volume level based on small samples of new data, fmod fades towards the desired amplification which makes for smooth gain control.  The fadetime parameter can control this.<br>

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	]
	*/
	public enum DspNormalize
	{
		FadeTime,    /* Time to ramp the silence to full in ms.  0.0 to 20000.0. Default = 5000.0. */
		Threshold,  /* Lower volume range threshold to ignore.  0.0 to 1.0.  Default = 0.1.  Raise higher to stop amplification of very quiet signals. */
		MaxAmplification /* Maximum amplification allowed.  1.0 to 100000.0.  Default = 20.0.  1.0 = no amplifaction, higher values allow more boost. */
	}
	#endregion

	#region DspParameterEqualizer
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_PARAMEQ filter.

	[REMARKS]
	Parametric EQ is a bandpass filter that attenuates or amplifies a selected frequency and its neighbouring frequencies.<br>
	<br>
	To create a multi-band EQ create multiple FMOD_DspType_PARAMEQ units and set each unit to different frequencies, for example 1000hz, 2000hz, 4000hz, 8000hz, 16000hz with a range of 1 octave each.<br>
	<br>
	When a frequency has its gain set to 1.0, the sound will be unaffected and represents the original signal exactly.<br>

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	]
	*/
	public enum DspParameterEqualizer
	{
		Center,     /* Frequency center.  20.0 to 22000.0.  Default = 8000.0. */
		Bandwidth,  /* Octave range around the center frequency to filter.  0.2 to 5.0.  Default = 1.0. */
		Gain        /* Frequency Gain.  0.05 to 3.0.  Default = 1.0.  */
	}
	#endregion

	#region DspPitchShift
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_PITCHSHIFT filter.

	[REMARKS]
	This pitch shifting unit can be used to change the pitch of a sound without speeding it up or slowing it down.<br>
	It can also be used for time stretching or scaling, for example if the pitch was doubled, and the frequency of the sound was halved, the pitch of the sound would sound correct but it would be twice as slow.<br>
	<br>
	<b>Warning!</b> This filter is very computationally expensive!  Similar to a vocoder, it requires several overlapping FFT and IFFT's to produce smooth output, and can require around 440mhz for 1 stereo 48khz signal using the default settings.<br>
	Reducing the signal to mono will half the cpu usage, as will the overlap count.<br>
	Reducing this will lower audio quality, but what settings to use are largely dependant on the sound being played.  A noisy polyphonic signal will need higher overlap and fft size compared to a speaking voice for example.<br>
	<br>
	This pitch shifter is based on the pitch shifter code at http://www.dspdimension.com, written by Stephan M. Bernsee.<br>
	The original code is COPYRIGHT 1999-2003 Stephan M. Bernsee <smb@dspdimension.com>.<br>
	<br>
	'<i>maxchannels</i>' dictates the amount of memory allocated.  By default, the maxchannels value is 0.  If FMOD is set to stereo, the pitch shift unit will allocate enough memory for 2 channels.  If it is 5.1, it will allocate enough memory for a 6 channel pitch shift, etc.<br>
	If the pitch shift effect is only ever applied to the global mix (ie it was added with System::addDSP), then 0 is the value to set as it will be enough to handle all Speaker Modes.<br>
	When the pitch shift is added to a channel (ie Channel::addDSP) then the channel count that comes in could be anything from 1 to 8 possibly.  It is only in this case where you might want to increase the channel count above the output's channel count.<br>
	If a channel pitch shift is set to a lower number than the sound's channel count that is coming in, it will not pitch shift the sound.<br>

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	]
	*/
	public enum DspPitchShift
	{
		Pitch,       /* Pitch value.  0.5 to 2.0.  Default = 1.0. 0.5 = one octave down, 2.0 = one octave up.  1.0 does not change the pitch. */
		FftSize,     /* FFT window size.  256, 512, 1024, 2048, 4096.  Default = 1024.  Increase this to reduce 'smearing'.  This effect is a warbling sound similar to when an mp3 is encoded at very low bitrates. */
		Overlap,     /* Window overlap.  1 to 32.  Default = 4.  Increase this to reduce 'tremolo' effect.  Increasing it by a factor of 2 doubles the CPU usage. */
		MaxChannels  /* Maximum channels supported.  0 to 16.  0 = same as fmod's default output polyphony, 1 = mono, 2 = stereo etc.  See remarks for more.  Default = 0.  It is suggested to leave at 0! */
	}
	#endregion

	#region DspChorus
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_CHORUS filter.

	[REMARKS]
	Chrous is an effect where the sound is more 'spacious' due to 1 to 3 Versions of the sound being played along side the original signal but with the pitch of each copy modulating on a sine wave.<br>
	This is a highly configurable chorus unit.  It supports 3 taps, small and large delay times and also feedback.<br>
	This unit also could be used to do a simple echo, or a flange effect. 

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	]
	*/
	public enum DspChorus
	{
		DryMix,   /* Volume of original signal to pass to output.  0.0 to 1.0. Default = 0.5. */
		WetMix1,  /* Volume of 1st chorus tap.  0.0 to 1.0.  Default = 0.5. */
		WetMix2,  /* Volume of 2nd chorus tap. This tap is 90 degrees out of phase of the first tap.  0.0 to 1.0.  Default = 0.5. */
		WetMix3,  /* Volume of 3rd chorus tap. This tap is 90 degrees out of phase of the second tap.  0.0 to 1.0.  Default = 0.5. */
		Delay,    /* Chorus delay in ms.  0.1 to 100.0.  Default = 40.0 ms. */
		Rate,     /* Chorus modulation rate in hz.  0.0 to 20.0.  Default = 0.8 hz. */
		Depth,    /* Chorus modulation depth.  0.0 to 1.0.  Default = 0.03. */
		Feedback  /* Chorus feedback.  Controls how much of the wet signal gets fed back into the chorus buffer.  0.0 to 1.0.  Default = 0.0. */
	}
	#endregion

	#region DspReverb
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_REVERB filter.

	[REMARKS]

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	]
	*/
	public enum DspReverb
	{
		RoomSize, /* Roomsize. 0.0 to 1.0.  Default = 0.5 */
		Damp,     /* Damp.     0.0 to 1.0.  Default = 0.5 */
		WetMix,   /* Wet mix.  0.0 to 1.0.  Default = 0.33 */
		DryMix,   /* Dry mix.  0.0 to 1.0.  Default = 0.0 */
		Width,    /* Width.    0.0 to 1.0.  Default = 1.0 */
		Mode      /* Mode.     0 (normal), 1 (freeze).  Default = 0 */
	}
	#endregion
	
	#region DspItEcho
	/*
	[ENUM]
	[  
	[DESCRIPTION]   
	Parameter types for the FMOD_DspType_ITECHO filter.<br>
	This is effectively a software based echo filter that emulates the DirectX DMO echo effect.  Impulse tracker files can support this, and FMOD will produce the effect on ANY platform, not just those that support DirectX effects!<br>

	[REMARKS]
	Note.  Every time the delay is changed, the plugin re-allocates the echo buffer.  This means the echo will dissapear at that time while it refills its new buffer.<br>
	Larger echo delays result in larger amounts of memory allocated.<br>
	<br>
	For stereo signals only!  This will not work on mono or multichannel signals.  This is fine for .IT format purposes, and also if you use System::addDSP with a standard stereo output.<br>

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	DSP::setParameter
	DSP::getParameter
	FMOD_DspType
	System::addDSP
	]
	*/
	public enum DspItEcho
	{
		WetDryMix,      /* Ratio of wet (processed) signal to dry (unprocessed) signal. Must be in the range from 0.0 through 100.0 (all wet). The default value is 50. */
		Feedback,       /* PercenTage of output fed back into input, in the range from 0.0 through 100.0. The default value is 50. */
		LeftDelay,      /* Delay for left channel, in milliseconds, in the range from 1.0 through 2000.0. The default value is 500 ms. */
		RightDelay,     /* Delay for right channel, in milliseconds, in the range from 1.0 through 2000.0. The default value is 500 ms. */
		PanDelay        /* Value that specifies whether to swap left and right delays with each successive echo. The default value is zero, meaning no swap. Possible values are defined as 0.0 (equivalent to FALSE) and 1.0 (equivalent to TRUE). */
	}
	#endregion
	
	#endregion
		
	#endregion		
	
	#region ReverbChannels
	/// <summary>
	///[DEFINE] 
	///[
	///	[NAME] 
	///	REVERB_CHANNELFLAGS
	///
	///	[DESCRIPTION]
	///	Values for the Flags member of the ReverbChannelProperties structure.
	///
	///	[PLATFORMS]
	///	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable
	///
	///	[SEE_ALSO]
	///	ReverbChannelProperties
	///]	
	/// </summary>
	[Flags]
	public enum ReverbChannels
	{
		None = 0,
		DirectHighFrequencyAuto = 0x00000001,
		RoomAuto = 0x00000002,
		RoomHighFrequencyAuto = 0x00000004,
		Environment0 = 0x00000008,
		Environment1 = 0x00000010,
		Environment2 = 0x00000020,
		Default = (DirectHighFrequencyAuto | RoomAuto | RoomHighFrequencyAuto | Environment0)
	}
	#endregion
	
	#region ReverbOptions
	/// <summary>
	/// [DEFINE] 
	///[
	///	[NAME] 
	///	REVERB_FLAGS
	///
	///	[DESCRIPTION]
	///	Values for the Flags member of the ReverbProperties structure.
	///
	///	[PLATFORMS]
	///	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable
	///
	///	[SEE_ALSO]
	///	ReverbProperties
	///]
	/// </summary>
	[Flags]
	public enum ReverbOptions
	{
		None = 0,
		DecayTimescale = 0x00000001,
		ReflectionsScale = 0x00000002,
		ReflectionsDelayScale = 0x00000004,
		ReverbScale = 0x00000008,
		ReverbDelayScale = 0x00000010,
		DecayHighFrequencyLimit = 0x00000020,
		EchoTimescale = 0x00000040,
		ModulationTimescale = 0x00000080,
		Default = (DecayTimescale | ReflectionsScale | ReflectionsDelayScale | ReverbScale | ReverbDelayScale | DecayHighFrequencyLimit)
	}
	#endregion
}
