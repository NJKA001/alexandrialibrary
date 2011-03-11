using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace Gnosis.Fmod
{
	#region CreateSoundExtendedInfo
	/// WAS CREATE_SOUND_EX_INFO
	/*
	[STRUCTURE] 
	[
    [DESCRIPTION]
    Use this structure with System::createSound when more control is needed over loading.
    The possible reasons to use this with System::createSound are:
    <li>Loading a file from memory.
    <li>Loading a file from within another larger (possibly wad/pak) file, by giving the loader an offset and length.
    <li>To create a user created / non file based sound.
    <li>To specify a starting subsound to seek to within a multi-sample sounds (ie FSB/DLS/SF2) when created as a stream.
    <li>To specify which subsounds to load for multi-sample sounds (ie FSB/DLS/SF2) so that memory is saved and only a subset is actually loaded/read from disk.
    <li>To specify 'piggyback' read and seek callbacks for capture of sound data as fmod reads and decodes it.  Useful for ripping decoded PCM data from sounds as they are loaded / played.
    <li>To specify a MIDI DLS/SF2 sample set file to load when opening a MIDI file.
    See below on what members to fill for each of the above types of sound you want to create.

    [REMARKS]
    This structure is optional!  Specify 0 or NULL in System::createSound if you don't need it!
    
    Members marked with [in] mean the user sets the value before passing it to the function.
    Members marked with [out] mean FMOD sets the value to be used after the function exits.
    
    <u>Loading a file from memory.</u>
    <li>Create the sound using the FMOD_OPENMEMORY flag.
    <li>Mandantory.  Specify 'length' for the size of the memory block in bytes.
    <li>Other flags are optional.
    
    
    <u>Loading a file from within another larger (possibly wad/pak) file, by giving the loader an offset and length.</u>
    <li>Mandantory.  Specify 'fileoffset' and 'length'.
    <li>Other flags are optional.
    
    
    <u>To create a user created / non file based sound.</u>
    <li>Create the sound using the FMOD_OPENUSER flag.
    <li>Mandantory.  Specify 'defaultfrequency, 'numchannels' and 'format'.
    <li>Other flags are optional.
    
    
    <u>To specify a starting subsound to seek to and flush with, within a multi-sample stream (ie FSB/DLS/SF2).</u>
    
    <li>Mandantory.  Specify 'initialsubsound'.
    
    
    <u>To specify which subsounds to load for multi-sample sounds (ie FSB/DLS/SF2) so that memory is saved and only a subset is actually loaded/read from disk.</u>
    
    <li>Mandantory.  Specify 'inclusionlist' and 'inclusionlistnum'.
    
    
    <u>To specify 'piggyback' read and seek callbacks for capture of sound data as fmod reads and decodes it.  Useful for ripping decoded PCM data from sounds as they are loaded / played.</u>
    
    <li>Mandantory.  Specify 'pcmreadcallback' and 'pcmseekcallback'.
    
    
    <u>To specify a MIDI DLS/SF2 sample set file to load when opening a MIDI file.</u>
    
    <li>Mandantory.  Specify 'dlsname'.
    
    
    Setting the 'decodebuffersize' is for cpu intensive codecs that may be causing stuttering, not file intensive codecs (ie those from CD or netstreams) which are normally altered with System::setStreamBufferSize.  As an example of cpu intensive codecs, an mp3 file will take more cpu to decode than a PCM wav file.
    If you have a stuttering effect, then it is using more cpu than the decode buffer playback rate can keep up with.  Increasing the decode buffersize will most likely solve this problem.

    [PLATFORMS]
    Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

    [SEE_ALSO]
    System::createSound
    System::setStreamBufferSize
    FMOD_Mode
	]
	*/		
	public struct CreateSoundExtendedInfo
	{
		#region Public Fields
		/// <summary>
		/// [in] (was cbSize) Size of this structure.  This is used so the structure can be expanded in the future and still work on older Versions of FMOD Ex.
		/// </summary>
		public int StructureSize;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. Size in bytes of file to load, or sound to create (in this case only if FMOD_OPENUSER is used).  Required if loading from memory.  If 0 is specified, then it will use the size of the file (unless loading from memory then an error will be returned).
		/// </summary>
		[CLSCompliant(false)]
		public uint Length;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. Offset from start of the file to start loading from.  This is useful for loading files from inside big data files.
		/// </summary>
		[CLSCompliant(false)]
		public uint FileOffset;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. Number of channels in a sound specified only if OPENUSER is used.
		/// </summary>
		public int NumberOfChannels;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. Default frequency of sound in a sound specified only if OPENUSER is used.  Other formats use the frequency determined by the file format.
		/// </summary>
		public int DefaultFrequency;
		
		/// <summary>
		/// [in] Optional. Specify 0 or SoundFormat_NONE to ignore. Format of the sound specified only if OPENUSER is used.  Other formats use the format determined by the file format.
		/// </summary>
		public FmodSoundFormat Format;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. For streams.  This determines the size of the double buffer (in PCM samples) that a stream uses.  Use this for user created streams if you want to determine the size of the callback buffer passed to you.  Specify 0 to use FMOD's default size which is currently equivalent to 400ms of the sound format created/loaded.
		/// </summary>
		[CLSCompliant(false)]
		public uint DecodeBufferSize;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. In a multi-sample file format such as .FSB/.DLS/.SF2, specify the initial subsound to seek to, only if CREATESTREAM is used.
		/// </summary>
		public int InitialSubSound;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore or have no subsounds.  In a user created multi-sample sound, specify the number of subsounds within the sound that are accessable with Sound::getSubSound / SoundGetSubSound.
		/// </summary>
		public int NumberOfSubSounds;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. In a multi-sample format such as .FSB/.DLS/.SF2 it may be desirable to specify only a subset of sounds to be loaded out of the whole file.  This is an array of subsound indicies to load into memory when created.
		/// </summary>
		public IntPtr InclusionList;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. This is the number of integers contained within the inclusion list
		/// </summary>
		public int InclusionListNumber;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. Callback to 'piggyback' on FMOD's read functions and accept or even write PCM data while FMOD is opening the sound.  Used for user sounds created with OPENUSER or for capturing decoded data as FMOD reads it.
		/// </summary>
		[CLSCompliant(false)]
		public SoundPcmReadCallback PcmReadCallback;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. Callback for when the user calls a seeking function such as Channel::setPosition within a multi-sample sound, and for when it is opened.
		/// </summary>
		[CLSCompliant(false)]
		public SoundPcmSetPositionCallback PcmSetPositionCallback;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. Callback for successful completion, or error while loading a sound that used the  FMODNonBlocking flag.
		/// </summary>
		public SoundNonBlockingCallback NonBlockCallback;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. Filename for a DLS or SF2 sample set when loading a MIDI file.   If not specified, on windows it will attempt to open /windows/system32/drivers/gm.dls, otherwise the MIDI will fail to open.
		/// </summary>
		public string DlsName;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. Key for encrypted FSB file.  Without this key an encrypted FSB file will not load.
		/// </summary>
		public string EncryptionKey;
		
		/// <summary>
		/// [in] Optional. Specify 0 to ingore. For sequenced formats with dynamic channel allocation such as .MID and .IT, this specifies the maximum voice count allowed while playing.  .IT defaults to 64.  .MID defaults to 32.
		/// </summary>
		public int MaximumPolyphony;           
		
		/// <summary>
		/// [in] Optional. Specify 0 to ignore. This is user data to be attached to the sound during creation.  Access via Sound::getUserData.
		/// </summary>
		public IntPtr UserData;
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is CreateSoundExtendedInfo)
			{
				CreateSoundExtendedInfo otherInfo = (CreateSoundExtendedInfo)obj;
				return
				(
					StructureSize == otherInfo.StructureSize &&
					Length == otherInfo.Length &&
					FileOffset == otherInfo.FileOffset &&
					NumberOfChannels == otherInfo.NumberOfChannels &&
					DefaultFrequency == otherInfo.DefaultFrequency &&
					Format == otherInfo.Format &&
					DecodeBufferSize == otherInfo.DecodeBufferSize &&
					InitialSubSound == otherInfo.InitialSubSound &&
					NumberOfSubSounds == otherInfo.NumberOfSubSounds &&
					InclusionList == otherInfo.InclusionList &&
					InclusionListNumber == otherInfo.InclusionListNumber &&
					PcmReadCallback == otherInfo.PcmReadCallback &&
					PcmSetPositionCallback == otherInfo.PcmSetPositionCallback &&
					NonBlockCallback == otherInfo.NonBlockCallback &&
					DlsName == otherInfo.DlsName &&
					EncryptionKey == otherInfo.EncryptionKey &&
					MaximumPolyphony == otherInfo.MaximumPolyphony &&
					UserData == otherInfo.UserData
				);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(CreateSoundExtendedInfo c1, CreateSoundExtendedInfo c2)
		{
			return c1.Equals(c2);
		}
		
		public static bool operator !=(CreateSoundExtendedInfo c1, CreateSoundExtendedInfo c2)
		{
			return !c1.Equals(c2);
		}
		#endregion
	}
	#endregion
	
	#region Vector
	/*
	[STRUCTURE] 
	[
	[DESCRIPTION]   
	Structure describing a point in 3D space.

	[REMARKS]
	FMOD uses a left handed co-ordinate system by default.
	To use a right handed co-ordinate system specify FMOD_INIT_3D_RIGHTHANDED from FMOD_InitFlagS in System::init.

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	System::set3DListenerAttributes
	System::get3DListenerAttributes
	Channel::set3DAttributes
	Channel::get3DAttributes
	Geometry::addPolygon
	Geometry::setPolygonVertex
	Geometry::getPolygonVertex
	Geometry::setRotation
	Geometry::getRotation
	Geometry::setPosition
	Geometry::getPosition
	Geometry::setScale
	Geometry::getScale
	FMOD_InitFlagS
	]
	*/
	public struct Vector
	{
		#region Public Fields
		/// <summary>
		/// X coordinate in 3D space
		/// </summary>
		public float X;
		
		/// <summary>
		/// Y coordinate in 3D space
		/// </summary>
		public float Y;
		
		/// <summary>
		/// Z coordinate in 3D space
		/// </summary>
		public float Z;
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is Vector)
			{
				Vector otherVector = (Vector)obj;
				return (this.X == otherVector.X && this.Y == otherVector.Y && this.Z == otherVector.Z);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return ((Convert.ToInt32(X) * 100) + (Convert.ToInt32(Y) * 10) + (Convert.ToInt32(Z)));
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", X, Y, Z);
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(Vector v1, Vector v2)
		{
			return v1.Equals(v2);
		}
		
		public static bool operator !=(Vector v1, Vector v2)
		{
			return !v1.Equals(v2);
		}
		#endregion
	}
	#endregion

	#region Tag
	/*
	[STRUCTURE] 
	[
	[DESCRIPTION]   
	Structure describing a piece of Tag data.

	[REMARKS]
	Members marked with [in] mean the user sets the value before passing it to the function.
	Members marked with [out] mean FMOD sets the value to be used after the function exits.

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

	[SEE_ALSO]      
	Sound::getTag
	TagTYPE
	TagDATATYPE
	]
	*/
	public struct Tag
	{
		#region Public Fields
		public TagType Type;         /* [out] The type of this Tag. */
		public TagDataType DataType;     /* [out] The type of data that this Tag contains */
		public string Name;         /* [out] The name of this Tag i.e. "TITLE", "ARTIST" etc. */

		/// <summary>
		/// Pointer to the Tag data - its format is determined by the datatype member
		/// </summary>
		public IntPtr Data;
		
		/// <summary>
		/// [out] FmodLength of the data contained in this Tag
		/// </summary>
		[CLSCompliant(false)]
		public uint DataLength;      
		public bool IsUpdated;      /* [out] True if this Tag has been updated since last being accessed with Sound::getTag */
		#endregion
		
		#region Public Properties
		public string Value
		{
			[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
			get
			{
				if (DataType == TagDataType.String)
				{
					return Marshal.PtrToStringAnsi(Data);					
				}
				else
				{
					return null; //"[ " + DataLength.ToString() + " byte(s) of binary data]";					
				}
			}
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is Tag)
			{
				Tag otherTag = (Tag)obj;
				return (this.Name == otherTag.Name && this.Type == otherTag.Type && this.DataType == otherTag.DataType);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return (this.Name.GetHashCode() ^ this.Type.GetHashCode() ^ this.DataType.GetHashCode());
		}

		public override string ToString()
		{
			return this.Name; // + "=" + this.Value;
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator == (Tag t1, Tag t2)
		{
			return t1.Equals(t2);
		}
		
		public static bool operator !=(Tag t1, Tag t2)
		{
			return !t1.Equals(t2);
		}
		#endregion
	}
	#endregion

	#region CDTocCore
	/*
	[STRUCTURE] 
	[
		[DESCRIPTION]   
		Structure describing a CD/DVD table of contents

		[REMARKS]
		Members marked with [in] mean the user sets the value before passing it to the function.
		Members marked with [out] mean FMOD sets the value to be used after the function exits.

		[PLATFORMS]
		Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

		[SEE_ALSO]      
		Sound::getTag
	]
	*/
	public struct CDTocCore
	{
		#region Public Fields
		// was numtracks
		public int NumberOfTracks;                  /* [out] The number of tracks on the CD */
		
		// was min
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
		public int[] Minutes;                   /* [out] The start offset of each track in minutes */
		
		// was sec
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
		public int[] Seconds;                   /* [out] The start offset of each track in seconds */
		
		// was frame
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
		public int[] Frames;                 /* [out] The start offset of each track in frames */
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is CDTocCore)
			{
				CDTocCore otherCore = (CDTocCore)obj;
				return (this.Minutes == otherCore.Minutes && this.Seconds == otherCore.Seconds && this.Frames == otherCore.Frames);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return this.NumberOfTracks;
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(CDTocCore c1, CDTocCore c2)
		{
			return c1.Equals(c2);
		}
		
		public static bool operator !=(CDTocCore c1, CDTocCore c2)
		{
			return !c1.Equals(c2);
		}
		#endregion
	}
	#endregion
		
	#region ReverbChannelProperties
	/*
	[STRUCTURE] 
	[
		[DESCRIPTION]
		Structure defining the properties for a reverb source, related to a FMOD channel.

		For more indepth descriptions of the reverb properties under win32, please see the EAX3
		documentation at http://developer.creative.com/ under the 'downloads' section.
		If they do not have the EAX3 documentation, then most information can be attained from
		the EAX2 documentation, as EAX3 only adds some more parameters and functionality on top of 
		EAX2.

		Note the default reverb properties are the same as the PRESET_GENERIC preset.
		Note that integer values that typically range from -10,000 to 1000 are represented in 
		decibels, and are of a logarithmic scale, not linear, wheras FLOAT values are typically linear.
		PORTABILITY: Each member has the platform it supports in braces ie (win32/xbox).  
		Some reverb parameters are only supported in win32 and some only on xbox. If all parameters are set then
		the reverb should product a similar effect on either platform.
		Linux and FMODCE do not support the reverb api.

		The numerical values listed below are the maximum, minimum and default values for each variable respectively.

		[REMARKS]
		For EAX4 support with multiple reverb environments, set FMOD_REVERB_CHANNELFLAGS_ENVIRONMENT0,
		FMOD_REVERB_CHANNELFLAGS_ENVIRONMENT1 or/and FMOD_REVERB_CHANNELFLAGS_ENVIRONMENT2 in the flags member 
		of FMOD_ReverbChannelProperties to specify which environment instance(s) to target. 
		Only up to 2 environments to target can be specified at once. Specifying three will result in an error.
		If the sound card does not support EAX4, the environment flag is ignored.

		[PLATFORMS]
		Win32, Win64, Linux, Macintosh, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

		[SEE_ALSO]
		Channel::setReverbProperties
		Channel::getReverbProperties
		REVERB_CHANNELFLAGS
	]
	*/
	public struct ReverbChannelProperties
	{
		#region Public Fields
										/*          MIN     MAX    DEFAULT  DESCRIPTION */
		public int Direct;               /* [in/out] -10000, 1000,  0,       direct path level (at low and mid frequencies) (win32/xbox) */
		public int DirectHF;             /* [in/out] -10000, 0,     0,       relative direct path level at high frequencies (win32/xbox) */
		public int Room;                 /* [in/out] -10000, 1000,  0,       room effect level (at low and mid frequencies) (win32/xbox) */
		public int RoomHF;               /* [in/out] -10000, 0,     0,       relative room effect level at high frequencies (win32/xbox) */
		public int Obstruction;          /* [in/out] -10000, 0,     0,       main obstruction control (attenuation at high frequencies)  (win32/xbox) */
		public float ObstructionLFRatio;   /* [in/out] 0.0,    1.0,   0.0,     obstruction low-frequency level re. main control (win32/xbox) */
		public int Occlusion;            /* [in/out] -10000, 0,     0,       main occlusion control (attenuation at high frequencies) (win32/xbox) */
		public float OcclusionLFRatio;     /* [in/out] 0.0,    1.0,   0.25,    occlusion low-frequency level re. main control (win32/xbox) */
		public float OcclusionRoomRatio;   /* [in/out] 0.0,    10.0,  1.5,     relative occlusion control for room effect (win32) */
		public float OcclusionDirectRatio; /* [in/out] 0.0,    10.0,  1.0,     relative occlusion control for direct path (win32) */
		public int Exclusion;            /* [in/out] -10000, 0,     0,       main exlusion control (attenuation at high frequencies) (win32) */
		public float ExclusionLFRatio;     /* [in/out] 0.0,    1.0,   1.0,     exclusion low-frequency level re. main control (win32) */
		public int OutsideVolumeHF;      /* [in/out] -10000, 0,     0,       outside sound cone level at high frequencies (win32) */
		public float DopplerFactor;        /* [in/out] 0.0,    10.0,  0.0,     like DS3D flDopplerFactor but per source (win32) */
		public float RollOffFactor;        /* [in/out] 0.0,    10.0,  0.0,     like DS3D flRolloffFactor but per source (win32) */
		public float RoomRollOffFactor;    /* [in/out] 0.0,    10.0,  0.0,     like DS3D flRolloffFactor but for room effect (win32/xbox) */
		public float AirAbsorptionFactor;  /* [in/out] 0.0,    10.0,  1.0,     multiplies AirAbsorptionHF member of ReverbProperties (win32) */
		
		/// <summary>
		/// [in/out] REVERB_CHANNELFLAGS - modifies the behavior of properties (win32)
		/// </summary>
		[CLSCompliant(false)]
		public uint Flags;
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is ReverbChannelProperties)
			{
				ReverbChannelProperties otherReverb = (ReverbChannelProperties)obj;
				return
				(
					Direct == otherReverb.Direct &&               /* [in/out] -10000, 1000,  0,       direct path level (at low and mid frequencies) (win32/xbox) */
					DirectHF == otherReverb.DirectHF &&             /* [in/out] -10000, 0,     0,       relative direct path level at high frequencies (win32/xbox) */
					Room == otherReverb.Room &&                 /* [in/out] -10000, 1000,  0,       room effect level (at low and mid frequencies) (win32/xbox) */
					RoomHF == otherReverb.RoomHF &&               /* [in/out] -10000, 0,     0,       relative room effect level at high frequencies (win32/xbox) */
					Obstruction == otherReverb.Obstruction &&          /* [in/out] -10000, 0,     0,       main obstruction control (attenuation at high frequencies)  (win32/xbox) */
					ObstructionLFRatio == otherReverb.ObstructionLFRatio &&   /* [in/out] 0.0,    1.0,   0.0,     obstruction low-frequency level re. main control (win32/xbox) */
					Occlusion == otherReverb.Occlusion &&            /* [in/out] -10000, 0,     0,       main occlusion control (attenuation at high frequencies) (win32/xbox) */
					OcclusionLFRatio == otherReverb.OcclusionLFRatio &&     /* [in/out] 0.0,    1.0,   0.25,    occlusion low-frequency level re. main control (win32/xbox) */
					OcclusionRoomRatio == otherReverb.OcclusionRoomRatio &&   /* [in/out] 0.0,    10.0,  1.5,     relative occlusion control for room effect (win32) */
					OcclusionDirectRatio == otherReverb.OcclusionDirectRatio && /* [in/out] 0.0,    10.0,  1.0,     relative occlusion control for direct path (win32) */
					Exclusion == otherReverb.Exclusion &&            /* [in/out] -10000, 0,     0,       main exlusion control (attenuation at high frequencies) (win32) */
					ExclusionLFRatio == otherReverb.ExclusionLFRatio &&     /* [in/out] 0.0,    1.0,   1.0,     exclusion low-frequency level re. main control (win32) */
					OutsideVolumeHF == otherReverb.OutsideVolumeHF &&      /* [in/out] -10000, 0,     0,       outside sound cone level at high frequencies (win32) */
					DopplerFactor == otherReverb.DopplerFactor &&        /* [in/out] 0.0,    10.0,  0.0,     like DS3D flDopplerFactor but per source (win32) */
					RollOffFactor == otherReverb.RollOffFactor &&        /* [in/out] 0.0,    10.0,  0.0,     like DS3D flRolloffFactor but per source (win32) */
					RoomRollOffFactor == otherReverb.RoomRollOffFactor &&    /* [in/out] 0.0,    10.0,  0.0,     like DS3D flRolloffFactor but for room effect (win32/xbox) */
					AirAbsorptionFactor == otherReverb.AirAbsorptionFactor &&  /* [in/out] 0.0,    10.0,  1.0,     multiplies AirAbsorptionHF member of ReverbProperties (win32) */
					Flags == otherReverb.Flags
				);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}				
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(ReverbChannelProperties r1, ReverbChannelProperties r2)
		{
			return r1.Equals(r2);
		}
		
		public static bool operator !=(ReverbChannelProperties r1, ReverbChannelProperties r2)
		{
			return !r1.Equals(r2);
		}
		#endregion
	}
	#endregion

	#region DSP Structures

	#region DspParameterDescription
	/*
	[STRUCTURE]
	[
	[DESCRIPTION]   

	[REMARKS]
	Members marked with [in] mean the user sets the value before passing it to the function.<br>
	Members marked with [out] mean FMOD sets the value to be used after the function exits.<br>
	<br>
	The step parameter tells the gui or application that the parameter has a certain granularity.<br>
	For example in the example of cutoff frequency with a range from 100.0 to 22050.0 you might only want the selection to be in 10hz increments.  For this you would simply use 10.0 as the step value.<br>
	For a boolean, you can use min = 0.0, max = 1.0, step = 1.0.  This way the only possible values are 0.0 and 1.0.<br>
	Some applications may detect min = 0.0, max = 1.0, step = 1.0 and replace a graphical slider bar with a checkbox instead.<br>
	A step value of 1.0 would simulate integer values only.<br>
	A step value of 0.0 would mean the full floating point range is accessable.<br>

	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]    
	System::createDSP
	System::getDSP
	]
	*/
	public struct DspParameterDescription
	{
		#region Public Fields
		public float Min;             /* [in] Minimum value of the parameter (ie 100.0). */
		public float Max;             /* [in] Maximum value of the parameter (ie 22050.0). */
		public float DefaultValue;      /* [in] Default value of parameter. */
		public string Name;            /* [in] Name of the parameter to be displayed (ie "Cutoff frequency"). */
		public string Label;           /* [in] Short string to be put next to value to denote the unit type (ie "hz"). */
		public string Description;     /* [in] Description of the parameter to be displayed as a help item / tooltip for this parameter. */
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is DspParameterDescription)
			{
				DspParameterDescription otherParameter = (DspParameterDescription)obj;
				return (this.Name == otherParameter.Name && this.Label == otherParameter.Label && this.Min == otherParameter.Min && this.Max == otherParameter.Max);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return (this.Name.GetHashCode() ^ this.Label.GetHashCode() ^ this.Description.GetHashCode());
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(DspParameterDescription d1, DspParameterDescription d2)
		{
			return d1.Equals(d2);
		}
		
		public static bool operator !=(DspParameterDescription d1, DspParameterDescription d2)
		{
			return !d1.Equals(d2);
		}
		#endregion	
	}
	#endregion

	#region DspDescription
	/*
	[STRUCTURE] 
	[
	[DESCRIPTION]
	Strcture to define the parameters for a DSP unit.

	[REMARKS]
	Members marked with [in] mean the user sets the value before passing it to the function.<br>
	Members marked with [out] mean FMOD sets the value to be used after the function exits.<br>
	<br>
	There are 2 different ways to change a parameter in this architecture.<br>
	One is to use DSP::setParameter / DSP::getParameter.  This is platform independant and is dynamic, so new unknown plugins can have their parameters enumerated and used.<br>
	The other is to use DSP::showConfigDialog.  This is platform specific and requires a GUI, and will display a dialog box to configure the plugin.<br>
        
	[PLATFORMS]
	Win32, Win64, Linux, Macintosh, XBox, PlayStation 2, GameCube

	[SEE_ALSO]
	System::createDSP
	System::getDSP
	]
	*/
	public struct DspDescription
	{
		#region Public Fields
		public string Name;               /* [in] Name of the unit to be displayed in the network. */
		
		/// <summary>
		/// [in] Plugin writer's Version number.
		/// </summary>
		[CLSCompliant(false)]
		public uint Version;
		
		public int Channels;           /* [in] Number of channels.  Use 0 to process whatever number of channels is currently in the network.  >0 would be mostly used if the unit is a unit that only generates sound. */
		DspCreateCallback create;             /* [in] Create callback.  This is called when DSP unit is created.  Can be null. */
		DspReleaseCallback release;            /* [in] Release callback.  This is called just before the unit is freed so the user can do any cleanup needed for the unit.  Can be null. */
		DspResetCallback reset;              /* [in] Reset callback.  This is called by the user to reset any history buffers that may need resetting for a filter, when it is to be used or re-used for the first time to its initial clean state.  Use to avoid clicks or artifacts. */
		DspReadCallback read;               /* [in] Read callback.  Processing is done here.  Can be null. */
		DspSetPositionCallback setPosition;        /* [in] Setposition callback.  This is called if the unit wants to update its position info but not process data.  Can be null. */

		public int NumberOfParameters;      /* [in] Number of parameters used in this filter.  The user finds this with DSP::getNumParameters */
		DspParameterDescription[] parameterDescriptions;          /* [in] Variable number of parameter structures. */
		DspSetParameterCallback setParameter;       /* [in] This is called when the user calls DSP::setParameter.  Can be null. */
		DspGetParameterCallback getParameter;       /* [in] This is called when the user calls DSP::getParameter.  Can be null. */
		DspDialogCallback configuration;             /* [in] This is called when the user calls DSP::showConfigDialog.  Can be used to display a dialog to configure the filter.  Can be null. */
		public int ConfigurationWidth;        /* [in] Width of config dialog graphic if there is one.  0 otherwise.*/
		public int ConfigurationHeight;       /* [in] Height of config dialog graphic if there is one.  0 otherwise.*/
		public IntPtr UserData;           /* [in] Optional. Specify 0 to ignore. This is user data to be attached to the DSP unit during creation.  Access via DSP::getUserData. */
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is DspDescription)
			{
				DspDescription otherDescription = (DspDescription)obj;
				return (this.Name == otherDescription.Name && this.NumberOfParameters == otherDescription.NumberOfParameters);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(DspDescription d1, DspDescription d2)
		{
			return d1.Equals(d2);
		}
		
		public static bool operator !=(DspDescription d1, DspDescription d2)
		{
			return !d1.Equals(d2);
		}
		#endregion
	}
	#endregion	
	
	#endregion
	
	#region SoundDelay
	public struct SoundDelay
	{
		#region Public Fields
		[CLSCompliant(false)]
		public uint Start;

		[CLSCompliant(false)]
		public uint End;
		#endregion

		#region Constructor
		[CLSCompliant(false)]
		public SoundDelay(uint start, uint end)
		{
			Start = start;
			End = end;
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is SoundDelay)
			{
				SoundDelay otherDelay = (SoundDelay)obj;
				return (this.Start == otherDelay.Start && this.End == otherDelay.End);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return (Convert.ToInt32(this.Start) * 10) + (Convert.ToInt32(this.End));
		}
		#endregion
		
		#region Public Static Fields
		public static bool operator ==(SoundDelay s1, SoundDelay s2)
		{
			return s1.Equals(s2);
		}
		
		public static bool operator !=(SoundDelay s1, SoundDelay s2)
		{
			return !s1.Equals(s2);
		}
		#endregion
	}
	#endregion
	
	#region PositionAndVelocity
	public struct PositionAndVelocity
	{
		#region Public Fields
		public Vector Position;
		public Vector Velocity;
		#endregion
		
		#region Constructor
		public PositionAndVelocity(Vector position, Vector velocity)
		{
			Position = position;
			Velocity = velocity;
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is PositionAndVelocity)
			{
				PositionAndVelocity otherPositionAndVelocity = (PositionAndVelocity)obj;
				return (this.Position == otherPositionAndVelocity.Position && this.Velocity == otherPositionAndVelocity.Velocity);
			}
			else return false;
		}
		
		public override int GetHashCode()
		{
			return (Position.GetHashCode() + Velocity.GetHashCode());
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(PositionAndVelocity p1, PositionAndVelocity p2)
		{
			return p1.Equals(p2);
		}
		
		public static bool operator !=(PositionAndVelocity p1, PositionAndVelocity p2)
		{
			return !p1.Equals(p2);
		}
		#endregion
	}
	#endregion
	
	#region ConeSettings
	public struct ConeSettings
	{
		#region Public Fields
		public float InsideAngle;
		public float OutsideAngle;
		public float OutsideVolume;
		#endregion
		
		#region Constructor
		public ConeSettings(float insideAngle, float outsideAngle, float outsideVolume)
		{
			InsideAngle = insideAngle;
			OutsideAngle = outsideAngle;
			OutsideVolume = outsideVolume;
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is ConeSettings)
			{
				ConeSettings otherSettings = (ConeSettings)obj;
				return (this.InsideAngle == otherSettings.InsideAngle && this.OutsideAngle == otherSettings.OutsideAngle && this.OutsideVolume == otherSettings.OutsideVolume);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return ((Convert.ToInt32(this.InsideAngle) * 100) + (Convert.ToInt32(this.OutsideAngle) * 10) + (Convert.ToInt32(this.OutsideVolume)));
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(ConeSettings c1, ConeSettings c2)
		{
			return c1.Equals(c2);
		}
		
		public static bool operator !=(ConeSettings c1, ConeSettings c2)
		{
			return !c1.Equals(c2);
		}
		#endregion
	}
	#endregion

	#region SoundOcclusion
	public struct SoundOcclusion
	{
		#region Public Fields
		public float DirectOcclusion;
		public float ReverbOcclusion;
		#endregion

		#region Constructor
		public SoundOcclusion(float directOcclusion, float reverbOcclusion)
		{
			DirectOcclusion = directOcclusion;
			ReverbOcclusion = reverbOcclusion;
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is SoundOcclusion)
			{
				SoundOcclusion otherOcclusion = (SoundOcclusion)obj;
				return (this.DirectOcclusion == otherOcclusion.DirectOcclusion && this.ReverbOcclusion == otherOcclusion.ReverbOcclusion);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return ((Convert.ToInt32(this.DirectOcclusion) * 10) + Convert.ToInt32(this.ReverbOcclusion));
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(SoundOcclusion s1, SoundOcclusion s2)
		{
			return s1.Equals(s2);
		}
		
		public static bool operator !=(SoundOcclusion s1, SoundOcclusion s2)
		{
			return !s1.Equals(s2);
		}
		#endregion
	}
	#endregion
	
	#region ChannelSpectrum
	public struct ChannelSpectrum
	{
		#region Public Fields
		public float[] SpectrumArray;
		public int NumberOfValues;
		public int ChannelOffset;
		public DspFftWindow WindowType;
		#endregion
		
		#region Constructor
		public ChannelSpectrum(float[] spectrumArray, int numberOfValues, int channelOffset, DspFftWindow windowType)
		{
			SpectrumArray = spectrumArray;
			NumberOfValues = numberOfValues;
			ChannelOffset = channelOffset;
			WindowType = windowType;
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is ChannelSpectrum)
			{
				ChannelSpectrum otherSpectrum = (ChannelSpectrum)obj;
				return (this.SpectrumArray == otherSpectrum.SpectrumArray && this.ChannelOffset == otherSpectrum.ChannelOffset && this.WindowType == otherSpectrum.WindowType);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(ChannelSpectrum c1, ChannelSpectrum c2)
		{
			return c1.Equals(c2);
		}
		
		public static bool operator !=(ChannelSpectrum c1, ChannelSpectrum c2)
		{
			return !c1.Equals(c2);
		}
		#endregion
	}
	#endregion
	
	#region ChannelWaveData
	public struct ChannelWaveData
	{
		#region Public Fields
		public float[] WaveArray;
		public int NumberOfValues;
		public int ChannelOffset;
		#endregion
		
		#region Constructor
		public ChannelWaveData(float[] waveArray, int numberOfValues, int channelOffset)
		{
			WaveArray = waveArray;
			NumberOfValues = numberOfValues;
			ChannelOffset = channelOffset;
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is ChannelWaveData)
			{
				ChannelWaveData otherWaveData = (ChannelWaveData)obj;
				return (this.WaveArray == otherWaveData.WaveArray && this.ChannelOffset == otherWaveData.ChannelOffset);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return ((this.NumberOfValues * 10) + this.ChannelOffset);
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(ChannelWaveData c1, ChannelWaveData c2)
		{
			return c1.Equals(c2);
		}
		
		public static bool operator !=(ChannelWaveData c1, ChannelWaveData c2)
		{
			return !c1.Equals(c2);
		}
		#endregion
	}
	#endregion	
		
	#region SpeakerVolumeMix
	public struct SpeakerVolumeMix
	{
		#region Public Fields
		public float FrontLeft;
		public float FrontRight;
		public float FrontCenter;
		public float Subwoofer;
		public float BackLeft;
		public float BackRight;
		public float SideLeft;
		public float SideRight;
		#endregion
		
		#region Constructor
		public SpeakerVolumeMix(float frontLeft, float frontRight, float frontCenter, float subwoofer, float backLeft, float backRight, float sideLeft, float sideRight)
		{
			FrontLeft = frontLeft;
			FrontRight = frontRight;
			FrontCenter = frontCenter;
			Subwoofer = subwoofer;
			BackLeft = backLeft;
			BackRight = backRight;
			SideLeft = sideLeft;
			SideRight = sideRight;
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is SpeakerVolumeMix)
			{
				SpeakerVolumeMix otherVolumeMix = (SpeakerVolumeMix)obj;
				return
				(
					FrontLeft == otherVolumeMix.FrontLeft &&
					FrontRight == otherVolumeMix.FrontRight &&
					FrontCenter == otherVolumeMix.FrontCenter &&
					Subwoofer == otherVolumeMix.Subwoofer &&
					BackLeft == otherVolumeMix.BackLeft &&
					BackRight == otherVolumeMix.BackRight &&
					SideLeft == otherVolumeMix.SideLeft &&
					SideRight == otherVolumeMix.SideRight
				);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(SpeakerVolumeMix s1, SpeakerVolumeMix s2)
		{
			return s1.Equals(s2);
		}

		public static bool operator !=(SpeakerVolumeMix s1, SpeakerVolumeMix s2)
		{
			return !s1.Equals(s2);
		}
		#endregion
	}	
	#endregion
	
	#region Rotation
	public struct Rotation
	{
		#region Constructor
		public Rotation(Vector forward, Vector up)
		{
			this.Forward = forward;
			this.Up = up;
		}
		#endregion

		#region Public Fields
		public Vector Forward;
		public Vector Up;
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is Rotation)
			{
				Rotation otherRotation = (Rotation)obj;
				return (this.Forward == otherRotation.Forward && this.Up == otherRotation.Up);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return ((Forward.GetHashCode() * 10) + Up.GetHashCode());
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Forward: {0}, {1}, {2} Up: {3}, {4}, {5}", Forward.X, Forward.Y, Forward.Z, Up.X, Up.Y, Up.Z);
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(Rotation r1, Rotation r2)
		{
			return r1.Equals(r2);
		}
		
		public static bool operator !=(Rotation r1, Rotation r2)
		{
			return !r1.Equals(r2);
		}
		#endregion
	}
	#endregion	
	
	#region WAV Structures
	
	#region RiffChunk
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct RiffChunk
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public char[] Id;
		public int Size;
	}
	#endregion

	#region FmtChunk
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct FmtChunk
	{
		public RiffChunk Chunk;
		public ushort FormatTag;    /* format type  */
		public ushort NumberOfChannels;    /* number of channels (i.e. mono, stereo...)  */
		public uint SamplesPerSecond;    /* sample rate  */
		public uint AverageBytesPerSecond;    /* for buffer estimation  */
		public ushort BlockSize;    /* block size of data  */
		public ushort NumberOfBitsPerSample;    /* number of bits per sample of mono data */
	}
	#endregion

	#region DataChunk
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct DataChunk
	{
		public RiffChunk Chunk;
	}
	#endregion

	#region WavHeader
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct WavHeader
	{
		public RiffChunk Chunk;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public char[] RiffType;
	}
	#endregion
	
	#endregion
}
