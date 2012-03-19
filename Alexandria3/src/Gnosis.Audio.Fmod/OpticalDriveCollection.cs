using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class OpticalDriveCollection : IEnumerable<OpticalDrive>
	{
		#region Private Fields
		private IntPtr systemHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private List<OpticalDrive> items = new List<OpticalDrive>();
		private int totalCount;
		#endregion
				
		#region Constructors
		public OpticalDriveCollection(IntPtr systemHandle, bool initialize)
		{
			this.systemHandle = systemHandle;
			
			if (initialize) Refresh();						
		}
		#endregion
		
		#region Public Properties
		
		#region SystemHandle
		public IntPtr SystemHandle
		{
			get {return systemHandle;}
		}
		#endregion
		
		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
		
		#endregion		
		
		#region Public Methods
		
		#region Refresh
		public void Refresh()
		{
			currentResult = NativeMethods.FMOD_System_GetNumCDROMDrives(systemHandle, ref totalCount);

			OpticalDrive drive = null;
			StringBuilder name = null;
			StringBuilder scsiName = null;
			StringBuilder deviceName = null;
			
			items.Clear();
			items.Capacity = totalCount+1;			
			
			for(int i = 0; i < totalCount; i++)
			{
				name = new StringBuilder(250);
				scsiName = new StringBuilder(250);
				deviceName = new StringBuilder(250);
				
				currentResult = NativeMethods.FMOD_System_GetCDROMDriveName(systemHandle, i, name, name.Capacity, scsiName, scsiName.Capacity, deviceName, deviceName.Capacity);
				
				drive = new OpticalDrive(systemHandle, i, name.ToString(), scsiName.ToString(), deviceName.ToString());
				items.Add(drive);
			}
		}
		#endregion
		
		#endregion

		#region IEnumerable<OpticalDrive> Members
		public IEnumerator<OpticalDrive> GetEnumerator()
		{
			foreach(OpticalDrive drive in items)
			{
				yield return drive;
			}
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach(OpticalDrive drive in items)
			{
				yield return drive;
			}
		}
		#endregion
	}
}