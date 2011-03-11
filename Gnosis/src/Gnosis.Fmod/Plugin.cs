using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Fmod
{
	public class Plugin
	{
		#region Private fields
		private IntPtr systemHandle = IntPtr.Zero;		
		private PluginType type = PluginType.Codec;
		private int id = -1;
		private string fileName = string.Empty;
		private uint version = 0;
		#endregion
		
		#region Constructors
		[CLSCompliant(false)]
		public Plugin(IntPtr systemHandle, PluginType type, int id, string fileName, uint version)
		{
			this.systemHandle = systemHandle;
			this.type = type;
			this.id = id;
			this.fileName = fileName;
			this.version = version;
		}
		#endregion
		
		#region Public properties
		
		#region SystemHandle
		public IntPtr SystemHandle
		{
			get {return systemHandle;}
		}
		#endregion
		
		#region Type
		public PluginType Type
		{
			get {return type;}
		}
		#endregion
		
		#region Id
		public int Id
		{
			get {return id;}
		}
		#endregion
		
		#region FileName
		public string FileName
		{
			get {return fileName;}
		}
		#endregion
		
		#region Version
		[CLSCompliant(false)]
		public uint Version
		{
			get {return version;}
		}
		#endregion
		
		#endregion	
	}
}
