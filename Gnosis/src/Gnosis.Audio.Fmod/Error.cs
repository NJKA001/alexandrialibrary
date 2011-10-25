/* ============================================================================================= = */
/* FMOD Ex - Error string header file. Copyright (c), Firelight Technologies Pty, Ltd. 2004-2005.  */
/*                                                                                                 */
/* Use this header if you want to store or display a string Version / english explanation of       */
/* the FMOD error codes.                                                                           */
/*                                                                                                 */
/* =============================================================================================== */

namespace Gnosis.Fmod
{
	public abstract class Error
	{
		public static string GetString(Result result)
		{
			switch (result)
			{
				case Result.Ok: return "No errors.";
				case Result.AlreadyLockedError: return "Tried to call lock a second time before unlock was called.";
				case Result.BadCommandError: return "Tried to call a function on a data type that does not allow this type of functionality (ie calling Sound::lock / FMOD_Sound_Lock on a streaming sound).";
				case Result.CddaDriversError: return "Neither NTSCSI nor ASPI could be initialised.";
				case Result.CddaInitializationError: return "An error occurred while initialising the CDDA subsystem.";
				case Result.CddaInvalidDeviceError: return "Couldn't find the specified device.";
				case Result.CddaNoAudioError: return "No audio tracks on the specified disc.";
				case Result.CddaNoDevicesError: return "No CD/DVD devices were found.";
				case Result.CddaNoDiscError: return "No disc present in the specified drive.";
				case Result.CddaReadError: return "A CDDA read error occurred.";
				case Result.ChannelAllocateError: return "Error trying to allocate a channel.";
				case Result.ChannelStolenError: return "The specified channel has been reused to play another sound.";
				case Result.ComError: return "A Win32 COM related error occured. COM failed to initialize or a QueryInterface failed meaning a Windows codec or driver was not installed properly.";
				case Result.DmaError: return "DMA Failure.  See debug output for more information.";
				case Result.DspConnectionError: return "DSP connection error.  Either the connection caused a cyclic dependancy or a generator unit attempted to have a unit attached to it.";
				case Result.DspFormatError: return "DSP Format error.  A DSP unit may have attempted to connect to this network with the wrong format.  IE a floating point unit on a PocketPC system.";
				case Result.DspNotFoundError: return "DSP connection error.  Couldn't find the DSP unit specified.";
				case Result.DspRunningError: return "DSP error.  Cannot perform this operation while the network is in the middle of running.  This will most likely happen if a connection or disconnection is attempted in a DSP callback.";
				case Result.DspTooManyConnectionsError: return "DSP connection error.  The unit being connected to or disconnected should only have 1 input or output.";
				case Result.FileBadError: return "Error loading file.";
				case Result.FileCouldNotSeekError: return "Couldn't perform seek operation.";
				case Result.FileEndOfFileError: return "End of file unexpectedly reached while trying to read essential data (truncated data?).";
				case Result.FileNotFoundError: return "File not found.";
				case Result.FormatError: return "Unsupported file or audio format.";
				case Result.HttpError: return "A HTTP error occurred. This is a catch-all for HTTP errors not listed elsewhere.";
				case Result.HttpAccessError: return "The specified resource requires authentication or is forbidden.";
				case Result.HttpProxyAuthenticationError: return "Proxy authentication is required to access the specified resource.";
				case Result.HttpServerError: return "A HTTP server error occurred.";
				case Result.HttpTimeoutError: return "The HTTP request timed out.";
				case Result.InitializationError: return "FMOD was not initialized correctly to support this function.";
				case Result.InitializedError: return "Cannot call this command after FMOD_System_Init.";
				case Result.InternalError: return "An error occured that wasnt supposed to.  Contact support.";
				case Result.InvalidHandleError: return "An invalid object handle was used.";
				case Result.InvalidParameterError: return "An invalid parameter was passed to this function.";
				case Result.IrxError: return "PS2 only.  fModex.irx failed to initialize.  This is most likely because you forgot to load it.";
				case Result.MemoryError: return "Not enough memory or resources.";
				case Result.MemoryIopError: return "PS2 only.  Not enough memory or resources on PlayStation 2 IOP ram.";
				case Result.MemorySoundRamError: return "Not enough memory or resources on console sound ram.";
				case Result.NeedSoftwareError: return "Tried to use a feature that requires the software engine but the software engine has been turned off.";
				case Result.NetworkConnectError: return "Couldn't connect to the specified host.";
				case Result.NetworkSocketError: return "A socket error occurred.  This is a catch-all for socket-related errors not listed elsewhere.";
				case Result.NetworkUrlError: return "The specified URL couldn't be resolved.";
				case Result.NotReadyError: return "Operation could not be performed because specified sound is not ready.";
				case Result.OutputAllocatedError: return "Error initializing output device, but more specifically, the output device is already in use and cannot be reused.";
				case Result.OutputCreateBufferError: return "Error creating hardware sound buffer.";
				case Result.OutputDriverCallError: return "A call to a standard soundcard driver failed, which could possibly mean a bug in the driver.";
				case Result.OutputFormatError: return "Soundcard does not support the minimum features needed for this soundsystem (16bit stereo output).";
				case Result.OutputInitError: return "Error initializing output device.";
				case Result.OutputNoHardwareError: return "FMOD_HARDWARE was specified but the sound card does not have the resources nescessary to play it.";
				case Result.OutputNoSoftwareError: return "Attempted to create a software sound but no software channels were specified in System::init.";
				case Result.PanError: return "Panning only works with mono or stereo sound sources.";
				case Result.PluginError: return "An unspecified error has been returned from a 3rd party plugin.";
				case Result.PluginMissingError: return "A requested output, dsp unit type or codec was not available.";
				case Result.PluginResourceError: return "A resource that the plugin requires cannot be found.";
				case Result.RecordError: return "An error occured trying to initialize the recording device.";
				case Result.ReverbInstanceError: return "Specified Instance in FMOD_ReverbProperties couldn't be set. Most likely because another application has locked the EAX4 FX slot.";
				case Result.SubSoundAllocatedError: return "This subsound is already being used by another sound, you cannot have more than one parent to a sound.  Null out the other parent's entry first.";
				case Result.TagNotFoundError: return "The specified Tag could not be found or there are no Tags.";
				case Result.TooManyChannelsError: return "The sound created exceeds the allowable input channel count.  This can be increased with System::setMaxInputChannels";
				case Result.UnimplementedError: return "Something in FMOD hasn't been implemented when it should be! contact support!";
				case Result.UninitializedError: return "This command failed because FMOD_System_Init or FMOD_System_SetDriver was not called.";
				case Result.UnsupportedError: return "A commmand issued was not supported by this object.  Possibly a plugin without certain callbacks specified.";
				case Result.VersionError: return "The Version number of this file format is not supported.";
				default: return "Unknown error.";
			};
		}
	}
}

