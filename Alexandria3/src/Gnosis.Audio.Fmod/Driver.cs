using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class Driver
	{
		#region Private fields
		private IntPtr systemHandle = IntPtr.Zero;
		private int id = -1;
		private string name = string.Empty;
		private Capabilities capabilities;
		private int minimumFrequency;
		private int maximumFrequency;
		private SpeakerMode speakerMode;
		#endregion
		
		#region Constructors
		public Driver(IntPtr systemHandle, int id, string name, Capabilities capabilities, int minimumFrequency, int maximumFrequency, SpeakerMode speakerMode)
		{
			this.systemHandle = systemHandle;
			this.id = id;
			this.name = name;
			this.capabilities = capabilities;
			this.minimumFrequency = minimumFrequency;
			this.maximumFrequency = maximumFrequency;
			this.speakerMode = speakerMode;
		}
		
		/// <summary>
		/// Constructor for recording drivers
		/// </summary>
		/// <param name="systemHandle"></param>
		/// <param name="id"></param>
		/// <param name="name"></param>
		public Driver(IntPtr systemHandle, int id, string name)
		{
			this.systemHandle = systemHandle;
			this.id = id;
			this.name = name;
			this.capabilities = Capabilities.None;
			this.speakerMode = SpeakerMode.Stereo;
		}
		#endregion
		
		#region Public properties
		
		#region SystemHandle
		public IntPtr SystemHandle
		{
			get {return systemHandle;}
		}
		#endregion
		
		#region Id
		public int Id
		{
			get {return id;}
		}
		#endregion
		
		#region Name
		public string Name
		{
			get {return name;}
		}
		#endregion
		
		#region Capabilities
		public Capabilities Capabilities
		{
			get {return capabilities;}
		}
		#endregion
		
		#region MinimumFrequency
		public int MinimumFrequency
		{
			get {return minimumFrequency;}
		}
		#endregion
		
		#region MaximumFrequency
		public int MaximumFrequency
		{
			get {return maximumFrequency;}
		}
		#endregion
		
		#region SpeakerMode
		public SpeakerMode SpeakerMode
		{
			get {return speakerMode;}
		}
		#endregion
		
		#endregion
	}
}
