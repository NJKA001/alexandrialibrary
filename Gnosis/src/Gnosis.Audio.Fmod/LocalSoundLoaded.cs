using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Fmod
{
	public sealed class LocalSoundLoaded : SoundStatus
	{
		#region Constructors
		private LocalSoundLoaded() : base(false, false, true, true, true, true)
		{
		}
		#endregion
		
		#region Static Members
		private static LocalSoundLoaded example;
		
		public static LocalSoundLoaded Example
		{
			get
			{
				if (example == null) example = new LocalSoundLoaded();
				
				return example;
			}
		}
		#endregion
	}
}
