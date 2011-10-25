using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	/// <summary>
	/// This is a buffer of bytes read from a given sound
	/// You can access the bytes in this buffer and seek through it but be aware that
	/// any manipulation of this buffer (including seeking) effects the sound that the
	/// buffer was created from.  The best way to use buffers without modifying the sound
	/// is to make a copy of the sound for use with buffers which you can later discard	
	/// *** USER BEWARE ***
	/// </summary>
	public class SoundBuffer
	{
		#region Private fields
		private IntPtr soundHandle = IntPtr.Zero;
		private IntPtr data = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private uint totalBytes = 0;
		private uint currentBytes = 0;
		#endregion
				
		#region Constructors
		public SoundBuffer(IntPtr soundHandle, IntPtr data, uint totalBytes, uint currentBytes)
		{
			this.soundHandle = soundHandle;
			this.data = data;
			this.totalBytes = totalBytes;
			this.currentBytes = currentBytes;
		}
		#endregion
		
		#region Public properties

		#region SoundHandle
		public IntPtr SoundHandle
		{
			get { return soundHandle; }
		}
		#endregion
		
		#region Data
		/// <summary>
		/// Gets a pointer to the data in this buffer
		/// </summary>
		public IntPtr Data
		{
			get {return data;}
		}
		#endregion
		
		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
		
		#region TotalBytes
		/// <summary>
		/// Gets the total numnber of bytes allocated for this buffer
		/// </summary>
		public uint TotalBytes
		{
			get {return totalBytes;}
		}
		#endregion
		
		#region CurrentBytes
		/// <summary>
		/// Gets the current number of bytes read into this buffer
		/// </summary>
		public uint CurrentBytes
		{
			get {return currentBytes;}
		}
		#endregion
		
		#endregion
		
		#region Public methods
		
		#region Seek
		/// <summary>
		/// Seeks forward the indicated number of bytes in this buffer
		/// WARNING! this moves the internal file pointer of the sound that this buffer originated from
		/// Use this for custom handling of sound buffering or other low-level skullduggery
		/// Do NOT use this method for normal seeking within a Sound - use Channel.SetPosition() for that
		/// </summary>		
		/// <param name="length">the length of bytes to seek forward in the buffer</param>
		public void Seek(uint length)
		{
			currentResult = NativeMethods.FMOD_Sound_SeekData(soundHandle, length);
		}
		#endregion
		
		#endregion
	}
}
