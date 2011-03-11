using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class PluginCollection : IEnumerable<Plugin>
	{
		#region Constructors
		public PluginCollection(IntPtr systemHandle, PluginType type, bool initialize)
		{
			this.systemHandle = systemHandle;
			this.type = type;

			if (initialize) Refresh();
		}
		#endregion
	
		#region Private Fields
		private IntPtr systemHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private List<Plugin> plugins = new List<Plugin>();
		private PluginType type;
		private int totalCount;
		#endregion
		
		#region Private Properties
		private System.Collections.ObjectModel.ReadOnlyCollection<Plugin> Plugins
		{
			get {return plugins.AsReadOnly();}
		}
		#endregion
		
		#region Private Methods
		
		#region DllImport wrapper methods
		//[DllImport(Constants.DllName)]
		//private static extern Result FMOD_System_GetNumPlugins(IntPtr system, PluginType PluginType, ref int numplugins);
		//[DllImport(Constants.DllName)]
		//private static extern Result FMOD_System_GetPluginInfo(IntPtr system, PluginType PluginType, int index, StringBuilder name, int namelen, ref uint Version);		
		#endregion
		
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
		
		#region Type
		public PluginType Type
		{
			get {return type;}
		}
		#endregion
		
		#region Count
		public int Count
		{
			get {return plugins.Count;}
		}
		#endregion
		
		#endregion
		
		#region Public Methods
		
		#region Refresh
		public void Refresh()
		{
			currentResult = NativeMethods.FMOD_System_GetNumPlugins(systemHandle, type, ref totalCount);
			
			Plugin plugin = null;
			StringBuilder fileName = null;
			uint version = 0;

			plugins.Clear();
			plugins.Capacity = totalCount + 1;

			for(int i = 0; i < totalCount; i++)
			{
				fileName = new StringBuilder(100);
				currentResult = NativeMethods.FMOD_System_GetPluginInfo(systemHandle, type, i, fileName, fileName.Capacity, ref version);
				plugin = new Plugin(systemHandle, type, i, fileName.ToString(), version); 
				
				plugins.Add(plugin);
			}
		}
		#endregion
		
		#endregion

		#region IEnumerable<Plugin> Members
		public IEnumerator<Plugin> GetEnumerator()
		{
			foreach (Plugin plugin in Plugins)
				yield return plugin;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (Plugin plugin in Plugins)
				yield return plugin;
		}
		#endregion
	}
}
