using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Fmod
{
	public class OpticalDrive
	{
		#region Private fields
		private IntPtr systemHandle = IntPtr.Zero;
		private int id = -1;
		private string name = string.Empty;
		private string scsiName = string.Empty;
		private string deviceName = string.Empty;		
		#endregion
		
		#region Constructors
		public OpticalDrive(IntPtr systemHandle, int id, string name, string scsiName, string deviceName)
		{
			this.systemHandle = systemHandle;
			this.id = id;
			this.name = name;
			this.scsiName = scsiName;
			this.deviceName = deviceName;
		}
		#endregion
		
		#region Public Properties
		
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
		
		#region ScsiName
		public string ScsiName
		{
			get {return scsiName;}
		}
		#endregion
		
		#region DeviceName
		public string DeviceName
		{
			get {return deviceName;}
		}
		#endregion
		
		#endregion
	}
}
