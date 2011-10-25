using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	internal static class NativeMethods
	{
		#region Channel Methods
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetSystemObject(IntPtr channelHandle, ref IntPtr systemHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_Stop(IntPtr channelHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetPaused(IntPtr channelHandle, bool paused);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetPaused(IntPtr channelHandle, ref bool paused);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetVolume(IntPtr channelHandle, float volume);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetVolume(IntPtr channelHandle, ref float volume);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetFrequency(IntPtr channelHandle, float frequency);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetFrequency(IntPtr channelHandle, ref float frequency);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetPan(IntPtr channelHandle, float pan);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetPan(IntPtr channelHandle, ref float pan);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetDelay(IntPtr channelHandle, uint startDelay, uint endDelay);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetDelay(IntPtr channelHandle, ref uint startDelay, ref uint endDelay);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetSpeakerMix(IntPtr channelHandle, float frontLeft, float frontRight, float center, float lowFrequencyEffect, float backLeft, float backRight, float sideLeft, float sideRight);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetSpeakerMix(IntPtr channelHandle, ref float frontleft, ref float frontright, ref float center, ref float lowFrequencyEffect, ref float backLeft, ref float backRight, ref float sideLeft, ref float sideRight);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetSpeakerLevels(IntPtr channelHandle, SpeakerPosition speakerPosition, float[] levels, int numberOfLevels);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetSpeakerLevels(IntPtr channelHandle, SpeakerPosition speakerPosition, [MarshalAs(UnmanagedType.LPArray)]float[] levels, int numlevels);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetMute(IntPtr channelHandle, bool mute);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetMute(IntPtr channelHandle, ref bool mute);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetPriority(IntPtr channelHandle, int priority);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetPriority(IntPtr channelHandle, ref int priority);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_Set3DAttributes(IntPtr channelHandle, ref Vector position, ref Vector velocity);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_Get3DAttributes(IntPtr channelHandle, ref Vector position, ref Vector velocity);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_Set3DMinMaxDistance(IntPtr channelHandle, float minimumDistance, float maximumDistance);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_Get3DMinMaxDistance(IntPtr channelHandle, ref float minimumDistance, ref float maximumDistance);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_Set3DConeSettings(IntPtr channelHandle, float insideConeAngle, float outsideConeAngle, float outsideVolume);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_Get3DConeSettings(IntPtr channelHandle, ref float insideConeAngle, ref float outsideConeAngle, ref float outsideVolume);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_Set3DConeOrientation(IntPtr channelHandle, ref Vector orientation);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_Get3DConeOrientation(IntPtr channelHandle, ref Vector orientation);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_Set3DOcclusion(IntPtr channelHandle, float directOcclusion, float reverbOcclusion);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_Get3DOcclusion(IntPtr channelHandle, ref float directOcclusion, ref float reverbOcclusion);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetReverbProperties(IntPtr channelHandle, ref ReverbChannelProperties properties);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetReverbProperties(IntPtr channelHandle, ref ReverbChannelProperties properties);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetChannelGroup(IntPtr channelHandle, IntPtr channelGroupHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetChannelGroup(IntPtr channelHandle, ref IntPtr channelGroupHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_IsPlaying(IntPtr channelHandle, ref bool isPlaying);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_IsVirtual(IntPtr channelHandle, ref bool isVirtual);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetAudibility(IntPtr channelHandle, ref float audibility);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetCurrentSound(IntPtr channelHandle, ref IntPtr soundHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetSpectrum(IntPtr channelHandle, [MarshalAs(UnmanagedType.LPArray)] float[] spectrumArray, int numberOfValues, int channelOffset, DspFftWindow windowType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetWaveData(IntPtr channelHandle, [MarshalAs(UnmanagedType.LPArray)] float[] waveArray, int numberOfValues, int channelOffset);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetCallback(IntPtr channelHandle, ChannelCallbackType type, ChannelCallback callback, int command);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetPosition(IntPtr channelHandle, uint position, TimeUnits positionType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetPosition(IntPtr channelHandle, ref uint position, TimeUnits positionType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetDSPHead(IntPtr channelHandle, ref IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_AddDSP(IntPtr channelHandle, IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetMode(IntPtr channelHandle, Modes mode);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetMode(IntPtr channelHandle, ref Modes mode);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetLoopCount(IntPtr channelHandle, int loopCount);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetLoopCount(IntPtr channelHandle, ref int loopCount);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetLoopPoints(IntPtr channelHandle, uint loopStart, TimeUnits loopStartType, uint loopEnd, TimeUnits loopEndType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetLoopPoints(IntPtr channelHandle, ref uint loopStart, TimeUnits loopStartType, ref uint loopEnd, TimeUnits loopEndType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_SetUserData(IntPtr channelHandle, IntPtr userData);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Channel_GetUserData(IntPtr channelHandle, ref IntPtr userData);
		#endregion

		#region ChannelGroup Methods
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_Release(IntPtr channelGroupHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetSystemObject(IntPtr channelGroupHandle, ref IntPtr systemHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_SetVolume(IntPtr channelGroupHandle, float volume);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetVolume(IntPtr channelGroupHandle, ref float volume);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_SetPitch(IntPtr channelGroupHandle, float pitch);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetPitch(IntPtr channelGroupHandle, ref float pitch);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_Stop(IntPtr channelGroupHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_OverridePaused(IntPtr channelGroupHandle, bool paused);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_OverrideVolume(IntPtr channelGroupHandle, float volume);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_OverrideFrequency(IntPtr channelGroupHandle, float frequency);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_OverridePan(IntPtr channelGroupHandle, float pan);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_OverrideMute(IntPtr channelGroupHandle, bool mute);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_OverrideReverbProperties(IntPtr channelGroupHandle, ref ReverbChannelProperties properties);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_Override3DAttributes(IntPtr channelGroupHandle, ref Vector position, ref Vector velocity);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_AddGroup(IntPtr channelGroupHandle, IntPtr groupHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetNumGroups(IntPtr channelGroupHandle, ref int numberOfGroups);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetGroup(IntPtr channelGroupHandle, int index, ref IntPtr groupHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetDSPHead(IntPtr channelGroupHandle, ref IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_AddDSP(IntPtr channelGroupHandle, IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetName(IntPtr channelGroupHandle, StringBuilder name, int nameLength);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetNumChannels(IntPtr channelGroupHandle, ref int numberOfChannels);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetChannel(IntPtr channelGroupHandle, int index, ref IntPtr channelHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetSpectrum(IntPtr channelGroupHandle, [MarshalAs(UnmanagedType.LPArray)] float[] spectrumArray, int numberOfValues, int channelOffset, DspFftWindow windowType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetWaveData(IntPtr channelGroupHandle, [MarshalAs(UnmanagedType.LPArray)] float[] waveArray, int numberOfValues, int channelOffset);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_SetUserData(IntPtr channelGroupHandle, IntPtr userData);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_ChannelGroup_GetUserData(IntPtr channelGroupHandle, ref IntPtr userData);
		#endregion

		#region DSP Methods
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_Release(IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetSystemObject(IntPtr dspHandle, ref IntPtr systemHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_AddInput(IntPtr dspHandle, IntPtr targetHanel);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_DisconnectFrom(IntPtr dspHandle, IntPtr targetHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_Remove(IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetNumInputs(IntPtr dspHandle, ref int numberOfInputs);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetNumOutputs(IntPtr dspHandle, ref int numberOfOutputs);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetInput(IntPtr dspHandle, int index, ref IntPtr input);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetOutput(IntPtr dspHandle, int index, ref IntPtr output);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_SetInputMix(IntPtr dspHandle, int index, float volume);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetInputMix(IntPtr dspHandle, int index, ref float volume);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_SetActive(IntPtr dspHandle, bool active);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetActive(IntPtr dspHandle, ref bool active);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_SetInputLevels(IntPtr dspHandle, int index, Speaker speaker, float[] levels, int numberOfLevels);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetInputLevels(IntPtr dspHandle, int index, Speaker speaker, float[] levels, int numberOfLevels);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_SetBypass(IntPtr dspHandle, bool bypass);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetBypass(IntPtr dspHandle, ref bool bypass);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_Reset(IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_SetParameter(IntPtr dspHandle, int index, float parameterValue);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetParameter(IntPtr dspHandle, int index, ref float parameterValue, StringBuilder valueName, int valueNameLength);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetParameterInfo(IntPtr dspHandle, int index, StringBuilder name, StringBuilder label, StringBuilder description, int descriptionLength, ref float minimum, ref float maximum);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetNumParameters(IntPtr dspHandle, ref int numberOfParameters);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_ShowConfigDialog(IntPtr dspHandle, IntPtr windowHandle, bool show);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetInfo(IntPtr dspHandle, StringBuilder name, ref uint Version, ref int channels, ref int configwidth, ref int configheight);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_SetDefaults(IntPtr dspHandle, float frequency, float volume, float pan, int priority);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetDefaults(IntPtr dspHandle, ref float frequency, ref float volume, ref float pan, ref int priority);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_SetUserData(IntPtr dspHandle, IntPtr userData);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetUserData(IntPtr dspHandle, ref IntPtr userData);		
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_SetInputLevels(IntPtr dspHandle, int index, SpeakerPosition speakerPosition, float[] levels, int numberOflevels);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_DSP_GetInputLevels(IntPtr dspHandle, int index, SpeakerPosition speakerPosition, float[] levels, int numberOfLevels);
		#endregion

		#region Geometry Methods
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_Release(IntPtr geometry);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_AddPolygon(IntPtr geometry, float directOcclusion, float reverbOcclusion, bool doubleSided, int numVertices, [MarshalAs(UnmanagedType.LPArray)]Vector[] vertices, ref int polygonIndex);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_GetNumPolygons(IntPtr geometry, ref int numPolygons);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_GetMaxPolygons(IntPtr geometry, ref int maxPolygons, ref int maxVertices);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_GetPolygonNumVertices(IntPtr geometry, int polygonIndex, ref int numVertices);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_SetPolygonVertex(IntPtr geometry, int polygonIndex, int vertexIndex, ref Vector vertex);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_GetPolygonVertex(IntPtr geometry, int polygonIndex, int vertexIndex, ref Vector vertex);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_SetPolygonAttributes(IntPtr geometry, int polygonIndex, float directOcclusion, float reverbOcclusion, bool doubleSided);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_GetPolygonAttributes(IntPtr geometry, int polygonIndex, ref float directOcclusion, ref float reverbOcclusion, ref bool doubleSided);
		//[DllImport(Constants.DllName)]
		//internal static extern Result FMOD_Geometry_Flush(IntPtr geometry); //An entry point for this does not exist in the DLL
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_SetActive(IntPtr gemoetry, bool active);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_GetActive(IntPtr gemoetry, ref bool active);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_SetRotation(IntPtr geometry, ref Vector forward, ref Vector up);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_GetRotation(IntPtr geometry, ref Vector forward, ref Vector up);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_SetPosition(IntPtr geometry, ref Vector position);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_GetPosition(IntPtr geometry, ref Vector position);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_SetScale(IntPtr geometry, ref Vector scale);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_GetScale(IntPtr geometry, ref Vector scale);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_Save(IntPtr geometry, IntPtr data, ref int dataSize);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_SetUserData(IntPtr geometry, IntPtr userData);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Geometry_GetUserData(IntPtr geometry, ref IntPtr userData);
		#endregion

		#region Sound Methods
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_Release(IntPtr soundHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetSystemObject(IntPtr soundHandle, ref IntPtr system);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_Lock(IntPtr soundHandle, uint offset, uint length, ref IntPtr lockStartHandle, ref IntPtr lockEndHandle, ref uint lengthStart, ref uint lengthEnd);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_Unlock(IntPtr soundHandle, IntPtr lockStartHandle, IntPtr lockEndHandle, uint lengthStart, uint lengthEnd);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_SetDefaults(IntPtr soundHandle, float frequency, float volume, float pan, int priority);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetDefaults(IntPtr soundHandle, ref float frequency, ref float volume, ref float pan, ref int priority);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_SetVariations(IntPtr soundHandle, float frequencyVariation, float volumeVariation, float panVariation);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetVariations(IntPtr soundHandle, ref float frequencyVariation, ref float volumeVariation, ref float panVariation);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_Set3DMinMaxDistance(IntPtr soundHandle, float minimumDistance, float maximumDistance);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_Get3DMinMaxDistance(IntPtr soundHandle, ref float minimumDistance, ref float maximumDistance);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_SetSubSound(IntPtr soundHandle, int index, IntPtr subSoundHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetSubSound(IntPtr soundHandle, int index, ref IntPtr subSoundHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_SetSubSoundSentence(IntPtr soundHandle, int[] subSoundList, int numberOfSubSounds);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetName(IntPtr soundHandle, StringBuilder name, int nameLength);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetLength(IntPtr soundHandle, ref uint length, TimeUnits lengthType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetFormat(IntPtr soundHandle, ref FmodSoundType type, ref FmodSoundFormat format, ref int channels, ref int bits);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetNumSubSounds(IntPtr soundHandle, ref int numberOfSubSounds);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetNumTags(IntPtr soundHandle, ref int numberOfTags, ref int numberOfTagsUpdated);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetTag(IntPtr soundHandle, string name, int index, ref Tag tag);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetOpenState(IntPtr soundHandle, ref OpenState openState, ref uint percentBuffered, ref bool starving);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_ReadData(IntPtr soundHandle, IntPtr bufferHandle, uint lengthBytes, ref uint readBytes);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_SeekData(IntPtr soundHandle, uint pcmBytes);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_SetMode(IntPtr soundHandle, Modes mode);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetMode(IntPtr soundHandle, ref Modes mode);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_SetLoopCount(IntPtr soundHandle, int loopCount);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetLoopCount(IntPtr soundHandle, ref int loopCount);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_SetLoopPoints(IntPtr soundHandle, uint loopStart, TimeUnits loopStartType, uint loopEnd, TimeUnits loopEndType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetLoopPoints(IntPtr soundHandle, ref uint loopStart, TimeUnits loopStartType, ref uint loopEnd, TimeUnits loopEndType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_SetUserData(IntPtr soundHandle, IntPtr userData);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_Sound_GetUserData(IntPtr soundHandle, ref IntPtr userData);
		#endregion

		#region SoundSystem Methods
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_Create(ref IntPtr systemHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_Release(IntPtr systemHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetOutput(IntPtr systemHandle, OutputType outputType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetOutput(IntPtr systemHandle, ref OutputType outputType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetNumDrivers(IntPtr systemHandle, ref int numberOfDrivers);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetDriverName(IntPtr systemHandle, int id, StringBuilder name, int nameLength);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetDriverCaps(IntPtr systemHandle, int id, ref Capabilities capabilities, ref int minimumFrequency, ref int maximumFrequency, ref SpeakerMode controlPanelSpeakerMode);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetDriver(IntPtr systemHandle, int driver);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetDriver(IntPtr systemHandle, ref int driver);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetHardwareChannels(IntPtr systemHandle, int minimum2d, int maximum2d, int minimum3d, int maximum3d);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetHardwareChannels(IntPtr systemHandle, ref int number2d, ref int number3d, ref int total);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetSoftwareChannels(IntPtr systemHandle, int numberOfSoftwareChannels);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetSoftwareChannels(IntPtr systemHandle, ref int numberOfSoftwareChannels);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetSoftwareFormat(IntPtr systemHandle, int sampleRate, FmodSoundFormat format, int numberOfOutputChannels, int maximumInputChannels, DspResampler resampleMethod);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetSoftwareFormat(IntPtr systemHandle, ref int sampleRate, ref FmodSoundFormat format, ref int numberOfOutputChannels, ref int maximumInputChannels, ref DspResampler resampleMethod, ref int bits);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetDSPBufferSize(IntPtr systemHandle, uint bufferLength, int numberOfBuffers);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetDSPBufferSize(IntPtr systemHandle, ref uint bufferLength, ref int numberOfBuffers);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetFileSystem(IntPtr systemHandle, FileOpenCallback userOpen, FileCloseCallback userClose, FileReadCallback userRead, FileSeekCallback userSeek, int bufferSize);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_AttachFileSystem(IntPtr systemHandle, FileOpenCallback userOpen, FileCloseCallback userClose, FileReadCallback userRead, FileSeekCallback userSeek);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetPluginPath(IntPtr systemHandle, string path);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_LoadPlugin(IntPtr systemHandle, string fileName, ref PluginType pluginType, ref int index);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetNumPlugins(IntPtr systemHandle, PluginType pluginType, ref int numberOfPlugins);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetPluginInfo(IntPtr systemHandle, PluginType pluginType, int index, StringBuilder name, int nameLength, ref uint version);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_UnloadPlugin(IntPtr systemHandle, PluginType pluginType, int index);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetOutputByPlugin(IntPtr systemHandle, int index);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetOutputByPlugin(IntPtr systemHandle, ref int index);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_Init(IntPtr systemHandle, int maximumChannels, InitializationOptions initFlag, IntPtr extraData);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_Close(IntPtr systemHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_Update(IntPtr systemHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetSpeakerMode(IntPtr systemHandle, SpeakerMode speakerMode);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetSpeakerMode(IntPtr systemHandle, ref SpeakerMode speakerMode);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetSpeakerPosition(IntPtr systemHandle, Speaker speaker, float x, float y);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetSpeakerPosition(IntPtr systemHandle, Speaker speaker, ref float x, ref float y);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_Set3DSettings(IntPtr systemHandle, float dopplerScale, float distanceFactor, float rollOffScale);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_Get3DSettings(IntPtr systemHandle, ref float dopplerScale, ref float distancefactor, ref float rollOffScale);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_Set3DNumListeners(IntPtr systemHandle, int numberOfListeners);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_Get3DNumListeners(IntPtr systemHandle, ref int numberOfListeners);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_Set3DListenerAttributes(IntPtr systemHandle, int listener, ref Vector position, ref Vector velocity, ref Vector forward, ref Vector up);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_Get3DListenerAttributes(IntPtr systemHandle, int listener, ref Vector position, ref Vector velocity, ref Vector forward, ref Vector up);
		//[DllImport(Constants.DllName)]
		//internal static extern Result FMOD_System_SetFileBufferSize(IntPtr systemHandle, int sizeBytes); //An entry point for this method does not exist in the DLL
		//[DllImport(Constants.DllName)]
		//internal static extern Result FMOD_System_GetFileBufferSize(IntPtr systemHandle, ref int sizeBytes);  //An entry point for this method does not exist in the DLL
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetStreamBufferSize(IntPtr systemHandle, uint fileBufferSize, TimeUnits fileBufferSizeType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetStreamBufferSize(IntPtr systemHandle, ref uint fileBufferSize, ref TimeUnits fileBufferSizeType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetVersion(IntPtr systemHandle, ref uint version);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetOutputHandle(IntPtr systemHandle, ref IntPtr outputHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetChannelsPlaying(IntPtr systemHandle, ref int nunberOfChannelsPlaying);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetCPUUsage(IntPtr systemHandle, ref float dsp, ref float stream, ref float update, ref float total);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetNumCDROMDrives(IntPtr systemHandle, ref int numberOfDrives);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetCDROMDriveName(IntPtr systemHandle, int drive, StringBuilder driveName, int driveNameLength, StringBuilder scsiName, int scsiNameLength, StringBuilder deviceName, int deviceNameLength);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetSpectrum(IntPtr systemHandle, [MarshalAs(UnmanagedType.LPArray)]float[] spectrumArray, int numberOfValues, int channelOffset, DspFftWindow windowType);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetWaveData(IntPtr systemHandle, [MarshalAs(UnmanagedType.LPArray)]float[] waveArray, int numberOfValues, int channelOffset);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateSound(IntPtr systemHandle, string nameOrData, Modes mode, ref CreateSoundExtendedInfo ifo, ref IntPtr sound);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateStream(IntPtr systemHandle, string nameOrData, Modes mode, ref CreateSoundExtendedInfo info, ref IntPtr sound);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateSound(IntPtr systemHandle, string nameOrData, Modes mode, int info, ref IntPtr soundHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateStream(IntPtr systemHandle, string nameOrData, Modes mode, int info, ref IntPtr soundHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateSound(IntPtr systemHandle, byte[] nameOrData, Modes mode, ref CreateSoundExtendedInfo info, ref IntPtr soundHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateStream(IntPtr systemHandle, byte[] nameOrData, Modes mode, ref CreateSoundExtendedInfo info, ref IntPtr soundHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateSound(IntPtr systemHandle, byte[] nameOrData, Modes mode, int info, ref IntPtr soundHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateStream(IntPtr systemHandle, byte[] nameOrData, Modes mode, int info, ref IntPtr soundHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateDSP(IntPtr systemHandle, ref DspDescription dspDescription, ref IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateDSPByType(IntPtr systemHandle, DspType dspType, ref IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateDSPByIndex(IntPtr systemHandle, int index, ref IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateChannelGroup(IntPtr systemHandle, string name, ref IntPtr channelGroupHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_PlaySound(IntPtr systemHandle, ChannelIndex channelId, IntPtr soundHandle, bool paused, ref IntPtr channelHandle);
		[DllImport(Constants.DllName)]
		public static extern Result FMOD_System_PlayDSP(IntPtr systemHandle, ChannelIndex channelId, IntPtr dspHandle, bool paused, ref IntPtr channelHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetChannel(IntPtr systemHandle, int channelId, ref IntPtr channelHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetMasterChannelGroup(IntPtr systemHandle, ref IntPtr channelgroup);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetReverbProperties(IntPtr systemHandle, ReverbProperties properties);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetReverbProperties(IntPtr systemHandle, ref ReverbProperties properties);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetDSPHead(IntPtr systemHandle, ref IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_AddDSP(IntPtr systemHandle, IntPtr dspHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_LockDSP(IntPtr systemHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_UnlockDSP(IntPtr systemHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetRecordDriver(IntPtr systemHandle, int driver);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetRecordDriver(IntPtr systemHandle, ref int driver);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetRecordNumDrivers(IntPtr systemHandle, ref int numberOfDrivers);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetRecordDriverName(IntPtr systemHandle, int id, StringBuilder name, int nameLength);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetRecordPosition(IntPtr systemHandle, ref uint position);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_RecordStart(IntPtr systemHandle, IntPtr sound, bool loop);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_RecordStop(IntPtr systemHandle);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_IsRecording(IntPtr systemHandle, ref bool isRecording);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_CreateGeometry(IntPtr systemHandle, int maximumPolygons, int maximumVertices, ref IntPtr geometry);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetGeometrySettings(IntPtr systemHandle, float maximumWorldSize);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetGeometrySettings(IntPtr systemHandle, ref float maximumWorldSize);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_LoadGeometry(IntPtr systemHandle, IntPtr data, int dataSize, ref IntPtr geometry);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetNetworkProxy(IntPtr systemHandle, string proxy);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetNetworkProxy(IntPtr systemHandle, StringBuilder proxy, int proxyLength);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_SetUserData(IntPtr systemHandle, IntPtr userData);
		[DllImport(Constants.DllName)]
		internal static extern Result FMOD_System_GetUserData(IntPtr systemHandle, ref IntPtr userData);
		#endregion
	}
}
