using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class SoundLock
	{
		#region Private Constant Fields
		private const string ERROR = "A sound lock cannot be modified when it is in the 'locked' state.";
		#endregion
	
		#region Private Fields
		private IntPtr soundHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private bool isLocked;
		private uint offset;
		private uint length;
		private IntPtr part1 = IntPtr.Zero;
		private IntPtr part2 = IntPtr.Zero;
		private uint length1;
		private uint length2;		
		#endregion
		
		#region Private Methods
				
		#region Lock
		private void Lock()
		{
			currentResult = NativeMethods.FMOD_Sound_Lock(soundHandle, offset, length, ref part1, ref part2, ref length1, ref length2);
		}
		#endregion
		
		#region Unlock
		private void Unlock()
		{
			currentResult = NativeMethods.FMOD_Sound_Unlock(soundHandle, part1, part2, length1, length2);
		}
		#endregion
		
		#endregion
		
		#region Constructors
		public SoundLock(IntPtr soundHandle, bool isLocked, uint offset, uint length)
		{
			this.soundHandle = soundHandle;			
			this.offset = offset;
			this.length = length;

			IsLocked = isLocked;
		}
		#endregion
				
		#region Public properties
		
		#region SoundHandle
		public IntPtr SoundHandle
		{
			get {return soundHandle;}
		}
		#endregion		
		
		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
		
		#region IsLocked
		public bool IsLocked
		{
			get {return isLocked;}
			set
			{
				isLocked = value;
				
				if (isLocked) Lock();
				else Unlock();
			}
		}
		#endregion
		
		#region Offset
		public uint Offset
		{
			get {return offset;}
			set {offset = value;}
		}
		#endregion
		
		#region FmodLength
		public uint Length
		{
			get {return length;}
			set {length = value;}
		}				
		#endregion
		
		#region Part1
		public IntPtr Part1
		{
			get {return part1;}
			set
			{
				if (!isLocked)
				{
					part1 = value;
				}
				else throw new InvalidOperationException();
			}
		}
		#endregion
		
		#region Part2
		public IntPtr Part2
		{
			get {return part2;}
			set
			{
				if (!isLocked)
				{
					part2 = value;
				}
				else throw new InvalidOperationException();
			}
		}
		#endregion
		
		#region Length1
		public uint Length1
		{
			get {return length1;}
			set
			{
				if (!isLocked)
				{
					length1 = value;
				}
				else throw new InvalidOperationException();
			}
		}
		#endregion
		
		#region Length2
		public uint Length2
		{
			get { return length2; }
			set
			{
				if (!isLocked)
				{
					length2 = value;
				}
				else throw new InvalidOperationException();
			}
		}		
		#endregion
		
		#endregion
	}
}
