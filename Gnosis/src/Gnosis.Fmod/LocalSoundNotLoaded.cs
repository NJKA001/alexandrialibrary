using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Fmod
{
	public sealed class LocalSoundNotLoaded : SoundStatus
	{
		#region Constructors
		private LocalSoundNotLoaded() : base(true, false, false, false, false, false)
		{
		}
		#endregion
		
		#region Static Members
		private static LocalSoundNotLoaded example;
		
		public static LocalSoundNotLoaded Example
		{
			get
			{
				if (example == null) example = new LocalSoundNotLoaded();
				
				return example;
			}
		}
		#endregion
	}
}
