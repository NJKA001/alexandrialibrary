using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class SoundSettings
	{
		#region Constructors
		public SoundSettings(float frequency, float volume, float pan, int priority)
		{
			this.frequency = frequency;
			this.volume = volume;
			this.pan = pan;
			this.priority = priority;
		}
		#endregion
	
		#region Private Fields
		private float frequency;
		private float volume;
		private float pan;
		private int priority;
		#endregion
		
		#region Public Properties
						
		#region Frequency
		public float Frequency
		{
			get {return frequency;}
			set {frequency = value;}
		}
		#endregion
		
		#region Volume
		public float Volume
		{
			get {return volume;}
			set {volume = value;}
		}
		#endregion
		
		#region Pan
		public float Pan
		{
			get {return pan;}
			set {pan = value;}
		}
		#endregion
		
		#region Priority
		public int Priority
		{
			get {return priority;}
			set {priority = value;}
		}
		#endregion
		
		#endregion				
	}
}
