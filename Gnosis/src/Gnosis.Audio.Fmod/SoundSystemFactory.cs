using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public static class SoundSystemFactory
	{		
		#region Private static fields
		private static Result currentResult = Result.Ok;
		private static SoundSystem defaultSoundSystem;
		#endregion
		
		#region Public static properties
		public static Result CurrentResult
		{
			get {return currentResult;}
		}
		
		public static SoundSystem DefaultSoundSystem
		{
			get
			{
				if (defaultSoundSystem == null)
					defaultSoundSystem = CreateSoundSystem(true, 64);
					
				return defaultSoundSystem;
			}
		}
		#endregion
		
		#region Public static methods
		/// <summary>
		/// Create an FMOD SoundSystem
		/// </summary>
		/// <param name="initialize">Indicates whether or not the system should be initialized</param>
		/// <param name="numberOfChannels">The number of channels that this system supports</param>
		/// <returns>A SoundSystem</returns>
		public static SoundSystem CreateSoundSystem(bool initialize, int numberOfChannels)
		{
			currentResult = Result.Ok;
			IntPtr systemRaw = new IntPtr();
			SoundSystem system = null;

			currentResult = NativeMethods.FMOD_System_Create(ref systemRaw);
			if (currentResult != Result.Ok)
			{
				return null;
			}

			system = new SoundSystem();
			system.Handle = systemRaw;
			
			// *** I'm not sure why they have the extra step of systemnew ***
			//systemnew = new SoundSystem();
			//systemnew.SetRaw(systemraw);
			//system = systemnew;

			if (initialize)
			{
				system.Initialize(numberOfChannels, InitializationOptions.None, IntPtr.Zero);//(IntPtr)null);
			}

			return system;
		}
		#endregion
	}
}
