using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class Listener
	{
		#region Private Fields
		private IntPtr systemHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private int id = -1;
		private Vector position;
		private Vector velocity;
		private Vector forward;
		private Vector up;
		#endregion
		
		#region Private Methods
		
		#region GetListenerInfo
		private void GetListenerInfo()
		{
			currentResult = NativeMethods.FMOD_System_Get3DListenerAttributes(systemHandle, id, ref position, ref velocity, ref forward, ref up);
		}
		#endregion
		
		#region SetListenerInfo
		private void SetListerInfo()
		{
			currentResult = NativeMethods.FMOD_System_Set3DListenerAttributes(systemHandle, id, ref position, ref velocity, ref forward, ref up);
		}
		#endregion
		
		#endregion
		
		#region Constructors
		public Listener(IntPtr systemHandle, int id, Vector position, Vector velocity, Vector forward, Vector up)
		{
			this.systemHandle = systemHandle;
			this.id = id;
			this.position = position;
			this.velocity = velocity;
			this.forward = forward;
			this.up = up;
		}
		
		public Listener(IntPtr systemHandle, int id)
		{
			this.systemHandle = systemHandle;
			this.id = id;
			
			GetListenerInfo();
		}
		#endregion
		
		#region Public properties
		
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
		
		#region Id
		public int Id
		{
			get {return id;}
		}
		#endregion
		
		#region Position
		public Vector Position
		{
			get
			{
				GetListenerInfo();
				return position;
			}
			set
			{
				position = value;
				SetListerInfo();
			}
		}
		#endregion
		
		#region Velocity
		public Vector Velocity
		{
			get
			{
				GetListenerInfo();
				return velocity;
			}
			set
			{
				velocity = value;
				SetListerInfo();
			}
		}
		#endregion
		
		#region Forward
		public Vector Forward
		{
			get
			{
				GetListenerInfo();
				return forward;
			}
			set
			{
				forward = value;
				SetListerInfo();
			}
		}
		#endregion
		
		#region Up
		public Vector Up
		{
			get
			{
				GetListenerInfo();
				return up;
			}
			set
			{
				up = value;
				SetListerInfo();
			}
		}
		#endregion
		
		#endregion
	}
}
