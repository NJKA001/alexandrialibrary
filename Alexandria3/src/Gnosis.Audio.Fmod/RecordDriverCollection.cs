using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class RecordDriverCollection : DriverCollection
	{
		#region Private Fields
		private int totalCount;
		private Driver currentDriver;
		#endregion
		
		#region Private Methods
		
		#region GetRecordDrivers
		private void GetRecordDrivers()
		{
			CurrentResult = NativeMethods.FMOD_System_GetRecordNumDrivers(SoundSystem.Handle, ref totalCount);
			
			Driver driver = null;
			StringBuilder nameBuilder = null;
			
			Drivers.Clear();
			//Items.Capacity = totalCount+1;
			
			for(int i = 0; i < totalCount; i++)
			{
				nameBuilder = new StringBuilder(100);				
												
				try
				{
					CurrentResult = NativeMethods.FMOD_System_GetRecordDriverName(SoundSystem.Handle, i, nameBuilder, nameBuilder.Capacity);
				}
				catch (Exception ex)
				{
					//throw new InvalidOperationException("Could not find record driver " + i.ToString(Sy ) + " in this FMOD SoundSystem", ex);
					throw new ApplicationException("Could not find audio recording driver", ex);
				}
				driver = new Driver(SoundSystem.Handle, i, nameBuilder.ToString());
				
				Drivers.Add(driver);
			}
		}
		#endregion
		
		#endregion
		
		#region Constructors
		public RecordDriverCollection(SoundSystem system, bool initialize) : base(system, initialize)
		{
			if (initialize) GetRecordDrivers();
		}
		#endregion
		
		#region Public Properties
		
		#region CurrentDriver
		public override Driver CurrentDriver
		{
			get
			{
				int index = -1;
				CurrentResult = NativeMethods.FMOD_System_GetRecordDriver(SoundSystem.Handle, ref index);
				if (index > -1 && index < Drivers.Count-1)
				{
					currentDriver = Drivers[index];
				}
				else currentDriver = null;
				
				return currentDriver;
			}
			//set
			//{
				//throw new NotImplementedException("The method or operation is not implemented.");
			//}
		}
		#endregion
		
		#endregion
		
		#region Public Methods
		public override void Refresh()
		{
			GetRecordDrivers();
		}
		#endregion
	}
}
