using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	/*
	[STRUCTURE] 
	[
		[DESCRIPTION]
		Structure defining a reverb environment.
        
		For more indepth descriptions of the reverb properties under win32, please see the EAX2 and EAX3
		documentation at http://developer.creative.com/ under the 'downloads' section.
		If they do not have the EAX3 documentation, then most information can be attained from
		the EAX2 documentation, as EAX3 only adds some more parameters and functionality on top of 
		EAX2.

		[REMARKS]
		Note the default reverb properties are the same as the FMOD_PRESET_GENERIC preset.
		Note that integer values that typically range from -10,000 to 1000 are represented in 
		decibels, and are of a logarithmic scale, not linear, wheras float values are always linear.
		PORTABILITY: Each member has the platform it supports in braces ie (win32/xbox).  
		Some reverb parameters are only supported in win32 and some only on xbox. If all parameters are set then
		the reverb should product a similar effect on either platform.
		Win32/Win64 - This is only supported with FMOD_OutputType_DSOUND and EAX compatible sound cards. 
		Macintosh - Currently unsupported. 
		Linux - Currently unsupported. 
		XBox - Only a subset of parameters are supported.  
		PlayStation 2 - Only the Environment and Flags paramenters are supported. 
		GameCube - Only a subset of parameters are supported. 
        
		The numerical values listed below are the maximum, minimum and default values for each variable respectively.
        
		Members marked with [in] mean the user sets the value before passing it to the function.
		Members marked with [out] mean FMOD sets the value to be used after the function exits.

		[PLATFORMS]
		Win32, Win64, Xbox, Xbox360, PlayStation 2, GameCube, PlayStation Portable

		[SEE_ALSO]
		System::setReverbProperties
		System::getReverbProperties
		REVERB_PRESETS
		REVERB_FLAGS
	]
	*/
	public class ReverbProperties		
	{	
		#region Public Fields
		/*          MIN     MAX    DEFAULT   DESCRIPTION */
		/// <summary>
		/// [in] Min=0, Max=2, Default=0, EAX4 only. Environment Instance. 3 seperate reverbs simultaneously are possible. This specifies which one to set. (win32 only)
		/// </summary>									
		public int Instance;
		
		/// <summary>
		/// [in/out] 0     , 25    , 0      , sets all listener properties (win32/ps2)
		/// </summary>
		public uint Environment;
		
		/// <summary>
		/// [in/out] 1.0   , 100.0 , 7.5    , environment size in meters (win32 only)
		/// </summary>
		public float EnvironmentSize;
		
		/// <summary>
		/// [in/out] 0.0   , 1.0   , 1.0    , environment diffusion (win32/xbox)
		/// </summary>
		public float EnvironmentDiffusion;
		
		/// <summary>
		/// [in/out] -10000, 0     , -1000  , room effect level (at mid frequencies) (win32/xbox)
		/// </summary>
		public int Room;
		
		/// <summary>
		/// [in/out] -10000, 0     , -100   , relative room effect level at high frequencies (win32/xbox)
		/// </summary>
		public int RoomHighFrequency;
		
		/// <summary>
		/// [in/out] -10000, 0     , 0      , relative room effect level at low frequencies (win32 only)
		/// </summary>
		public int RoomLowFrequency;
		
		/// <summary>
		/// [in/out] 0.1   , 20.0  , 1.49   , reverberation decay time at mid frequencies (win32/xbox)
		/// </summary>
		public float DecayTime;
		
		/// <summary>
		/// [in/out] 0.1   , 2.0   , 0.83   , high-frequency to mid-frequency decay time ratio (win32/xbox)
		/// </summary>
		public float DecayHighFrequencyRatio;
		
		/// <summary>
		/// [in/out] 0.1   , 2.0   , 1.0    , low-frequency to mid-frequency decay time ratio (win32 only)
		/// </summary>
		public float DecayLowFrequencyRatio;
		
		/// <summary>
		/// [in/out] -10000, 1000  , -2602  , early reflections level relative to room effect (win32/xbox)
		/// </summary>
		public int Reflections;
		
		/// <summary>
		/// [in/out] 0.0   , 0.3   , 0.007  , initial reflection delay time (win32/xbox)
		/// </summary>
		public float ReflectionsDelay;
		
		/// <summary>
		/// [in/out]       ,       , [0,0,0], early reflections panning Vector (win32 only)
		/// </summary>
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public float[] ReflectionsPan;
		
		/// <summary>
		/// [in/out] -10000, 2000  , 200    , late reverberation level relative to room effect (win32/xbox)
		/// </summary>
		public int Reverb;
		
		/// <summary>
		/// [in/out] 0.0   , 0.1   , 0.011  , late reverberation delay time relative to initial reflection (win32/xbox)
		/// </summary>
		public float ReverbDelay;
		
		/// <summary>
		/// [in/out]       ,       , [0,0,0], late reverberation panning Vector (win32 only)
		/// </summary>
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public float[] ReverbPan;
		
		/// <summary>
		/// [in/out] .075  , 0.25  , 0.25   , echo time (win32 only)
		/// </summary>
		public float EchoTime;
		
		/// <summary>
		/// [in/out] 0.0   , 1.0   , 0.0    , echo depth (win32 only)
		/// </summary>
		public float EchoDepth;
		
		/// <summary>
		/// [in/out] 0.04  , 4.0   , 0.25   , modulation time (win32 only)
		/// </summary>
		public float ModulationTime;
		
		/// <summary>
		/// [in/out] 0.0   , 1.0   , 0.0    , modulation depth (win32 only)
		/// </summary>
		public float ModulationDepth;
		
		/// <summary>
		/// [in/out] -100  , 0.0   , -5.0   , change in level per meter at high frequencies (win32 only)
		/// </summary>
		public float AirAbsorptionHighFrequency;
		
		/// <summary>
		/// [in/out] 1000.0, 20000 , 5000.0 , reference high frequency (hz) (win32/xbox)
		/// </summary>
		public float HighFrequencyReference;
		
		/// <summary>
		/// [in/out] 20.0  , 1000.0, 250.0  , reference low frequency (hz) (win32 only)
		/// </summary>
		public float LowFrequencyReference;
		
		/// <summary>
		/// [in/out] 0.0   , 10.0  , 0.0    , like 3D_Listener_SetRolloffFactor but for room effect (win32/xbox)
		/// </summary>
		public float RoomRollOffFactor;
		
		/// <summary>
		/// [in/out] 0.0   , 100.0 , 100.0  , Value that controls the echo density in the late reverberation decay. (xbox only)
		/// </summary>
		public float Diffusion;
		
		/// <summary>
		/// [in/out] 0.0   , 100.0 , 100.0  , Value that controls the modal density in the late reverberation decay (xbox only)
		/// </summary>
		public float Density;
		
		/// <summary>
		/// [in/out] ReverbFlags - modifies the behavior of above properties (win32/ps2)
		/// </summary>
		public uint ReverbFlags;
		#endregion
		
		#region Constructors
		/// <summary>
		/// ReverbProperties Constructor
		/// </summary>
		/// <param name="instance">EAX4 enviornment instance</param>
		/// <param name="environment">environment (sets all listener properties)</param>
		/// <param name="environmentSize">environment size</param>
		/// <param name="environmentDiffusion">environment diffusion</param>
		/// <param name="room">room effect level</param>
		/// <param name="roomHighFrequency">room high frequency effect level</param>
		/// <param name="roomLowFrequency">room low frequency effect level</param>
		/// <param name="decayTime">decay time</param>
		/// <param name="decayHighFrequencyRatio">decay high frequency ratio</param>
		/// <param name="decayLowFrequencyRatio">decay low frequency ratio</param>
		/// <param name="reflections">reflections</param>
		/// <param name="reflectionsDelay">reflections decay</param>
		/// <param name="reflectionsPanX">reflections pan left and right</param>
		/// <param name="reflectionsPanY">reflections pan front and back</param>
		/// <param name="reflectionsPanZ">reflections pan up and down</param>
		/// <param name="reverb">reverb</param>
		/// <param name="reverbDelay">reverb delay</param>
		/// <param name="reverbPanX">reverb pan left and right</param>
		/// <param name="reverbPanY">reverb pan front and back</param>
		/// <param name="reverbPanZ">reverb pan up and down</param>
		/// <param name="echoTime">echo time</param>
		/// <param name="echoDepth">echo depth</param>
		/// <param name="modulationTime">modulation time</param>
		/// <param name="modulationDepth">modulation depth</param>
		/// <param name="airAbsorptionHighFrequency">air absoption high frequency</param>
		/// <param name="highFrequencyReference">high frequency reference</param>
		/// <param name="lowFrequencyReference">low frequency reference</param>
		/// <param name="roomRollOffFactor">room rolloff factor</param>
		/// <param name="diffusion">diffusion</param>
		/// <param name="density">density</param>
		/// <param name="reverbFlags">reverb flags</param>
		public ReverbProperties(int instance, uint environment, float environmentSize, float environmentDiffusion, int room, int roomHighFrequency, int roomLowFrequency,
			float decayTime, float decayHighFrequencyRatio, float decayLowFrequencyRatio, int reflections, float reflectionsDelay,
			float reflectionsPanX, float reflectionsPanY, float reflectionsPanZ, int reverb, float reverbDelay,
			float reverbPanX, float reverbPanY, float reverbPanZ, float echoTime, float echoDepth, float modulationTime,
			float modulationDepth, float airAbsorptionHighFrequency, float highFrequencyReference, float lowFrequencyReference, float roomRollOffFactor,
			float diffusion, float density, uint reverbFlags)
		{
			Instance = instance;
			Environment = environment;
			EnvironmentSize = environmentSize;
			EnvironmentDiffusion = environmentDiffusion;
			Room = room;
			RoomHighFrequency = roomHighFrequency;
			RoomLowFrequency = roomLowFrequency;
			DecayTime = decayTime;
			DecayHighFrequencyRatio = decayHighFrequencyRatio;
			DecayLowFrequencyRatio = decayLowFrequencyRatio;
			Reflections = reflections;
			ReflectionsDelay = reflectionsDelay;
			ReflectionsPan[0] = reflectionsPanX;
			ReflectionsPan[1] = reflectionsPanY;
			ReflectionsPan[2] = reflectionsPanZ;
			Reverb = reverb;
			ReverbDelay = reverbDelay;
			ReverbPan[0] = reverbPanX;
			ReverbPan[1] = reverbPanY;
			ReverbPan[2] = reverbPanZ;
			EchoTime = echoTime;
			EchoDepth = echoDepth;
			ModulationTime = modulationTime;
			ModulationDepth = modulationDepth;
			AirAbsorptionHighFrequency = airAbsorptionHighFrequency;
			HighFrequencyReference = highFrequencyReference;
			LowFrequencyReference = lowFrequencyReference;
			RoomRollOffFactor = roomRollOffFactor;
			Diffusion = diffusion;
			Density = density;
			ReverbFlags = reverbFlags;
		}
		#endregion
		
		#region Public Static Properties
		public static ReverbProperties Off { get { return new ReverbProperties(0, 0, 7.5f, 1.00f, -10000, -10000, 0, 1.00f, 1.00f, 1.0f, -2602, 0.007f, 0.0f, 0.0f, 0.0f, 200, 0.011f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 0.0f, 0.0f, 0x3f); } }
		public static ReverbProperties Generic { get { return new ReverbProperties(0, 0, 7.5f, 1.00f, -1000, -100, 0, 1.49f, 0.83f, 1.0f, -2602, 0.007f, 0.0f, 0.0f, 0.0f, 200, 0.011f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties PaddedCell { get { return new ReverbProperties(0, 1, 1.4f, 1.00f, -1000, -6000, 0, 0.17f, 0.10f, 1.0f, -1204, 0.001f, 0.0f, 0.0f, 0.0f, 207, 0.002f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties NormalRoom { get { return new ReverbProperties(0, 2, 1.9f, 1.00f, -1000, -454, 0, 0.40f, 0.83f, 1.0f, -1646, 0.002f, 0.0f, 0.0f, 0.0f, 53, 0.003f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties Bathroom { get { return new ReverbProperties(0, 3, 1.4f, 1.00f, -1000, -1200, 0, 1.49f, 0.54f, 1.0f, -370, 0.007f, 0.0f, 0.0f, 0.0f, 1030, 0.011f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 60.0f, 0x3f); } }
		public static ReverbProperties LivingRoom { get { return new ReverbProperties(0, 4, 2.5f, 1.00f, -1000, -6000, 0, 0.50f, 0.10f, 1.0f, -1376, 0.003f, 0.0f, 0.0f, 0.0f, -1104, 0.004f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties StoneRoom { get { return new ReverbProperties(0, 5, 11.6f, 1.00f, -1000, -300, 0, 2.31f, 0.64f, 1.0f, -711, 0.012f, 0.0f, 0.0f, 0.0f, 83, 0.017f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties Auditorium { get { return new ReverbProperties(0, 6, 21.6f, 1.00f, -1000, -476, 0, 4.32f, 0.59f, 1.0f, -789, 0.020f, 0.0f, 0.0f, 0.0f, -289, 0.030f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties ConcertHall { get { return new ReverbProperties(0, 7, 19.6f, 1.00f, -1000, -500, 0, 3.92f, 0.70f, 1.0f, -1230, 0.020f, 0.0f, 0.0f, 0.0f, -2, 0.029f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties Cave { get { return new ReverbProperties(0, 8, 14.6f, 1.00f, -1000, 0, 0, 2.91f, 1.30f, 1.0f, -602, 0.015f, 0.0f, 0.0f, 0.0f, -302, 0.022f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x1f); } }
		public static ReverbProperties Arena { get { return new ReverbProperties(0, 9, 36.2f, 1.00f, -1000, -698, 0, 7.24f, 0.33f, 1.0f, -1166, 0.020f, 0.0f, 0.0f, 0.0f, 16, 0.030f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties Hangar { get { return new ReverbProperties(0, 10, 50.3f, 1.00f, -1000, -1000, 0, 10.05f, 0.23f, 1.0f, -602, 0.020f, 0.0f, 0.0f, 0.0f, 198, 0.030f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties CarpettedHallway { get { return new ReverbProperties(0, 11, 1.9f, 1.00f, -1000, -4000, 0, 0.30f, 0.10f, 1.0f, -1831, 0.002f, 0.0f, 0.0f, 0.0f, -1630, 0.030f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties Hallway { get { return new ReverbProperties(0, 12, 1.8f, 1.00f, -1000, -300, 0, 1.49f, 0.59f, 1.0f, -1219, 0.007f, 0.0f, 0.0f, 0.0f, 441, 0.011f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties StoneCorridor { get { return new ReverbProperties(0, 13, 13.5f, 1.00f, -1000, -237, 0, 2.70f, 0.79f, 1.0f, -1214, 0.013f, 0.0f, 0.0f, 0.0f, 395, 0.020f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties Alley { get { return new ReverbProperties(0, 14, 7.5f, 0.30f, -1000, -270, 0, 1.49f, 0.86f, 1.0f, -1204, 0.007f, 0.0f, 0.0f, 0.0f, -4, 0.011f, 0.0f, 0.0f, 0.0f, 0.125f, 0.95f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties Forest { get { return new ReverbProperties(0, 15, 38.0f, 0.30f, -1000, -3300, 0, 1.49f, 0.54f, 1.0f, -2560, 0.162f, 0.0f, 0.0f, 0.0f, -229, 0.088f, 0.0f, 0.0f, 0.0f, 0.125f, 1.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 79.0f, 100.0f, 0x3f); } }
		public static ReverbProperties City { get { return new ReverbProperties(0, 16, 7.5f, 0.50f, -1000, -800, 0, 1.49f, 0.67f, 1.0f, -2273, 0.007f, 0.0f, 0.0f, 0.0f, -1691, 0.011f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 50.0f, 100.0f, 0x3f); } }
		public static ReverbProperties Mountains { get { return new ReverbProperties(0, 17, 100.0f, 0.27f, -1000, -2500, 0, 1.49f, 0.21f, 1.0f, -2780, 0.300f, 0.0f, 0.0f, 0.0f, -1434, 0.100f, 0.0f, 0.0f, 0.0f, 0.250f, 1.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 27.0f, 100.0f, 0x1f); } }
		public static ReverbProperties Quarry { get { return new ReverbProperties(0, 18, 17.5f, 1.00f, -1000, -1000, 0, 1.49f, 0.83f, 1.0f, -10000, 0.061f, 0.0f, 0.0f, 0.0f, 500, 0.025f, 0.0f, 0.0f, 0.0f, 0.125f, 0.70f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }
		public static ReverbProperties Plain { get { return new ReverbProperties(0, 19, 42.5f, 0.21f, -1000, -2000, 0, 1.49f, 0.50f, 1.0f, -2466, 0.179f, 0.0f, 0.0f, 0.0f, -1926, 0.100f, 0.0f, 0.0f, 0.0f, 0.250f, 1.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 21.0f, 100.0f, 0x3f); } }
		public static ReverbProperties ParkingLot { get { return new ReverbProperties(0, 20, 8.3f, 1.00f, -1000, 0, 0, 1.65f, 1.50f, 1.0f, -1363, 0.008f, 0.0f, 0.0f, 0.0f, -1153, 0.012f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x1f); } }
		public static ReverbProperties SewerPipe { get { return new ReverbProperties(0, 21, 1.7f, 0.80f, -1000, -1000, 0, 2.81f, 0.14f, 1.0f, 429, 0.014f, 0.0f, 0.0f, 0.0f, 1023, 0.021f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 0.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 80.0f, 60.0f, 0x3f); } }
		public static ReverbProperties Underwater { get { return new ReverbProperties(0, 22, 1.8f, 1.00f, -1000, -4000, 0, 1.49f, 0.10f, 1.0f, -449, 0.007f, 0.0f, 0.0f, 0.0f, 1700, 0.011f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 1.18f, 0.348f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x3f); } }

		/* Non I3DL2 presets */

		public static ReverbProperties Drugged { get { return new ReverbProperties(0, 23, 1.9f, 0.50f, -1000, 0, 0, 8.39f, 1.39f, 1.0f, -115, 0.002f, 0.0f, 0.0f, 0.0f, 985, 0.030f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 0.25f, 1.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x1f); } }
		public static ReverbProperties Dizzy { get { return new ReverbProperties(0, 24, 1.8f, 0.60f, -1000, -400, 0, 17.23f, 0.56f, 1.0f, -1713, 0.020f, 0.0f, 0.0f, 0.0f, -613, 0.030f, 0.0f, 0.0f, 0.0f, 0.250f, 1.00f, 0.81f, 0.310f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x1f); } }
		public static ReverbProperties Psychotic { get { return new ReverbProperties(0, 25, 1.0f, 0.50f, -1000, -151, 0, 7.56f, 0.91f, 1.0f, -626, 0.020f, 0.0f, 0.0f, 0.0f, 774, 0.030f, 0.0f, 0.0f, 0.0f, 0.250f, 0.00f, 4.00f, 1.000f, -5.0f, 5000.0f, 250.0f, 0.0f, 100.0f, 100.0f, 0x1f); } }

		/* PS2 reverb presets */

		public static ReverbProperties PS2Room { get { return new ReverbProperties(0, 1, 0, 0, 0, 0, 0, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0.000f, 0.00f, 0.00f, 0.000f, 0.0f, 0000.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0x31f); } }
		public static ReverbProperties PS2StudioA { get { return new ReverbProperties(0, 2, 0, 0, 0, 0, 0, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0.000f, 0.00f, 0.00f, 0.000f, 0.0f, 0000.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0x31f); } }
		public static ReverbProperties PS2StudioB { get { return new ReverbProperties(0, 3, 0, 0, 0, 0, 0, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0.000f, 0.00f, 0.00f, 0.000f, 0.0f, 0000.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0x31f); } }
		public static ReverbProperties PS2StudioC { get { return new ReverbProperties(0, 4, 0, 0, 0, 0, 0, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0.000f, 0.00f, 0.00f, 0.000f, 0.0f, 0000.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0x31f); } }
		public static ReverbProperties PS2Hall { get { return new ReverbProperties(0, 5, 0, 0, 0, 0, 0, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0.000f, 0.00f, 0.00f, 0.000f, 0.0f, 0000.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0x31f); } }
		public static ReverbProperties PS2Space { get { return new ReverbProperties(0, 6, 0, 0, 0, 0, 0, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0.000f, 0.00f, 0.00f, 0.000f, 0.0f, 0000.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0x31f); } }
		public static ReverbProperties PS2Echo { get { return new ReverbProperties(0, 7, 0, 0, 0, 0, 0, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0.000f, 0.00f, 0.00f, 0.000f, 0.0f, 0000.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0x31f); } }
		public static ReverbProperties PS2Delay { get { return new ReverbProperties(0, 8, 0, 0, 0, 0, 0, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0.000f, 0.00f, 0.00f, 0.000f, 0.0f, 0000.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0x31f); } }
		public static ReverbProperties PS2Pipe { get { return new ReverbProperties(0, 9, 0, 0, 0, 0, 0, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0, 0.000f, 0.0f, 0.0f, 0.0f, 0.000f, 0.00f, 0.00f, 0.000f, 0.0f, 0000.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0x31f); } }		
		#endregion
	}
}
