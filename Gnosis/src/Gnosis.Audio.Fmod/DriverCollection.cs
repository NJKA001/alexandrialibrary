using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class DriverCollection : IEnumerable<Driver>, IDisposable
	{						
		#region Constructors
		public DriverCollection(SoundSystem system, bool initialize)
		{
			this.system = system;
			
			if (initialize) GetDrivers();
		}
		#endregion
		
		#region Finalizer
		~DriverCollection()
		{
			Dispose(false);
		}
		#endregion
		
		#region IDisposable Members
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
				
				}
			}
			disposed = true;
		}
		
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		#region Private Fields
		private SoundSystem system;
		private Result currentResult = Result.Ok;
		private List<Driver> drivers = new List<Driver>();
		private int totalCount;
		private Driver currentDriver;
		private bool disposed;
		#endregion

		#region Private Methods

		#region GetDrivers
		private void GetDrivers()
		{
			// Some of these methods cannot be called after the SoundSystem is initialized
			// so it is easier to just skip everything and set currentResult accordingly
			if (!system.IsInitialized)
			{
				NativeMethods.FMOD_System_GetNumDrivers(system.Handle, ref totalCount);

				StringBuilder name = null;
				Capabilities capabilities;
				int minimumFrequency = 0;
				int maximumFrequency = 0;
				SpeakerMode speakerMode;
				Driver driver;

				drivers.Clear();
				drivers.Capacity = totalCount + 1;

				for (int i = 0; i < totalCount; i++)
				{
					capabilities = new Capabilities();
					speakerMode = new SpeakerMode();

					name = new StringBuilder(100);
					currentResult = NativeMethods.FMOD_System_GetDriverName(system.Handle, i, name, name.Capacity);
					currentResult = NativeMethods.FMOD_System_GetDriverCaps(system.Handle, i, ref capabilities, ref minimumFrequency, ref maximumFrequency, ref speakerMode);

					driver = new Driver(system.Handle, i, name.ToString(), capabilities, minimumFrequency, maximumFrequency, speakerMode);
					drivers.Add(driver);
				}
			}
			else currentResult = Result.InitializedError;
		}
		#endregion

		#endregion

		#region Protected Properties

		#region Drivers
		protected IList<Driver> Drivers
		{
			get {return drivers;}
		}
		#endregion

		#endregion
		
		#region Public Properties

		#region SoundSystem
		public SoundSystem SoundSystem
		{
			get { return system; }
		}
		#endregion

		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
			protected set {currentResult = value;}
		}
		#endregion
		
		#region CurrentDriver
		public virtual Driver CurrentDriver
		{
			get
			{
				// Lazy initialization
				if (currentDriver == null)
				{
					int id = -1;
					StringBuilder name = new StringBuilder(100);
					Capabilities capabilities = new Capabilities();
					int minimumFrequency = 0;
					int maximumFrequency = 0;
					SpeakerMode speakerMode = new SpeakerMode();
			
					currentResult = NativeMethods.FMOD_System_GetDriver(system.Handle, ref id);
					currentResult = NativeMethods.FMOD_System_GetDriverName(system.Handle, id, name, name.Capacity);
					currentResult = NativeMethods.FMOD_System_GetDriverCaps(system.Handle, id, ref capabilities, ref minimumFrequency, ref maximumFrequency, ref speakerMode);
				
					currentDriver = new Driver(system.Handle, id, name.ToString(), capabilities, minimumFrequency, maximumFrequency, speakerMode);
				}
				
				return currentDriver;
			}
			set
			{
				// FMOD_System_SetDriver cannot be called after the SoundSystem is initialized
				if (!system.IsInitialized)
				{
					currentDriver = value;
					if (currentDriver != null)
					{
						currentResult = NativeMethods.FMOD_System_SetDriver(system.Handle, currentDriver.Id);
					}
				}
				else currentResult = Result.InitializedError;
			}
		}
		#endregion		
				
		#endregion
		
		#region Public Methods
		
		#region Refresh
		public virtual void Refresh()
		{
			GetDrivers();
		}
		#endregion
		
		#endregion

		#region IEnumerable<Driver> Members
		public IEnumerator<Driver> GetEnumerator()
		{
			System.Collections.ObjectModel.ReadOnlyCollection<Driver> readOnlyDrivers = drivers.AsReadOnly();

			foreach (Driver driver in readOnlyDrivers)
				yield return driver;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			System.Collections.ObjectModel.ReadOnlyCollection<Driver> readOnlyDrivers = drivers.AsReadOnly();

			foreach (Driver driver in readOnlyDrivers)
				yield return driver;
		}
		#endregion
	}
}
