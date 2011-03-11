using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Fmod
{
	/*
		Sound Callbacks
	*/
	public delegate Result SoundNonBlockingCallback(IntPtr soundRaw, Result result);
	[CLSCompliant(false)]
	public delegate Result SoundPcmReadCallback(IntPtr soundRaw, IntPtr data, uint dataLength);
	[CLSCompliant(false)]
	public delegate Result SoundPcmSetPositionCallback(IntPtr soundRaw, int subSound, uint position, TimeUnits positionType);

	/* 
		FMOD Callbacks
	*/
	[CLSCompliant(false)]
	public delegate Result ChannelCallback(IntPtr channelRaw, ChannelCallbackType type, int command, uint commandData1, uint commandData2);
	[CLSCompliant(false)]
	public delegate Result FileOpenCallback(string name, int unicode, ref uint fileSize, ref IntPtr handle, ref IntPtr userData);
	public delegate Result FileCloseCallback(IntPtr handle, IntPtr userData);
	[CLSCompliant(false)]
	public delegate Result FileReadCallback(IntPtr handle, IntPtr buffer, uint sizeBytes, ref uint bytesRead, IntPtr userData);
	public delegate Result FileSeekCallback(IntPtr handle, int position, IntPtr userData);

	/* 
		DSP callbacks
	*/
	public delegate Result DspCreateCallback(ref Dsp dsp);
	public delegate Result DspReleaseCallback(ref Dsp dsp);
	public delegate Result DspResetCallback(ref Dsp dsp);
	[CLSCompliant(false)]
	public delegate Result DspReadCallback(ref Dsp dsp, IntPtr inputBuffer, IntPtr outputBuffer, uint length, int inputChannels, int outputChannels);
	[CLSCompliant(false)]
	public delegate Result DspSetPositionCallback(ref Dsp dsp, uint seekLength);
	public delegate Result DspSetParameterCallback(ref Dsp dsp, int index, float parameterValue);
	public delegate Result DspGetParameterCallback(ref Dsp dsp, int index, ref float parameterValue, StringBuilder valueBuilder);
	public delegate Result DspDialogCallback(ref Dsp dsp, IntPtr windowHandle, bool show);
}
