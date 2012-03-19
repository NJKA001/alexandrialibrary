using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class SoundVariation
	{
		#region Private Fields
		private IntPtr soundHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private bool isSet;
		private float frequency = 0f;
		private float volume = 0f;
		private float pan = 0f;
		#endregion
		
		#region Private methods

		#region GetVariations
		private void GetVariations()
		{
			currentResult = NativeMethods.FMOD_Sound_GetVariations(soundHandle, ref frequency, ref volume, ref pan);
		}
		#endregion
				
		#region SetVariations
		private void SetVariations()
		{
			currentResult = NativeMethods.FMOD_Sound_SetVariations(soundHandle, frequency, volume, pan);
		}
		#endregion				
		
		#endregion
		
		#region Constructors
		public SoundVariation(IntPtr soundHandle, bool isSet, float frequency, float volume, float pan)
		{
			this.soundHandle = soundHandle;
			this.frequency = frequency;
			this.volume = volume;
			this.pan = pan;
			
			IsSet = isSet;
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
		
		#region IsSet
		public bool IsSet
		{
			get
			{
				GetVariations();
				return isSet;
			}
			set
			{
				isSet = value;
				if (isSet) SetVariations();
			}
		}
		#endregion
		
		#region Frequency
		public float Frequency
		{
			get
			{
				GetVariations();
				return frequency;
			}
			set
			{
				frequency = value;
				if (isSet) SetVariations();
			}
		}
		#endregion
		
		#region Volume
		public float Volume
		{
			get
			{
				GetVariations();
				return volume;
			}
			set
			{
				volume = value;
				if (isSet) SetVariations();
			}
		}
		#endregion
		
		#region Pan
		public float Pan
		{
			get
			{
				GetVariations();
				return pan;
			}
			set
			{
				pan = value;
				if (isSet) SetVariations();
			}
		}
		#endregion
		
		#endregion
	}
}
