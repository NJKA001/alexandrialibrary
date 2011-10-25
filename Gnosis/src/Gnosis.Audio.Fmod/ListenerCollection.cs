using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class ListenerCollection : IEnumerable<Listener>
	{
		#region Constructors
		public ListenerCollection(IntPtr systemHandle, bool initialize)
		{
			this.systemHandle = systemHandle;

			if (initialize) Refresh();
		}
		#endregion
	
		#region Private Fields
		private IntPtr systemHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private List<Listener> listeners = new List<Listener>();
		private int totalCount;
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
		
		#region TotalCount
		public int TotalCount
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_Get3DNumListeners(systemHandle, ref totalCount);
				return totalCount;
			}
			set
			{
				totalCount = value;
				currentResult = NativeMethods.FMOD_System_Set3DNumListeners(systemHandle, totalCount);
				Refresh();
			}
		}
		#endregion
		
		#endregion
		
		#region Public Methods
		
		#region Refresh
		public void Refresh()
		{
			Listener listener = null;
		
			listeners.Clear();
			listeners.Capacity = TotalCount + 1;
			
			for(int i = 0; i < totalCount; i++)
			{
				listener = new Listener(systemHandle, i);

				listeners.Add(listener);
			}
		}
		#endregion
		
		#endregion				
	
		#region IEnumerable<Listener> Members
		public IEnumerator<Listener> GetEnumerator()
		{
			foreach(Listener listener in listeners)
				yield return listener;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (Listener listener in listeners)
				yield return listener;
		}
		#endregion
	}
}
