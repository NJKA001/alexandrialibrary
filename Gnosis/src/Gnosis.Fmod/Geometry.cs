using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class Geometry : IDisposable
	{
		#region Private Fields
		private IntPtr handle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private int maximumPolygons;
		private int maximumVertices;
		private PolygonCollection polygons;
		private bool active;
		private Rotation rotation;
		private Vector position;
		private Vector scale;		
		private IntPtr userData = IntPtr.Zero;
		private bool disposed;
		#endregion
		
		#region Private Methods
		
		#region GetGeometryLimits
		private void GetGeometryLimits()
		{
			currentResult = NativeMethods.FMOD_Geometry_GetMaxPolygons(handle, ref maximumPolygons, ref maximumVertices);
		}
		#endregion
		
		#endregion
		
		#region Constructors
		#endregion
		
		#region Finalizer
		~Geometry()
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

				if (handle != IntPtr.Zero)
				{
					NativeMethods.FMOD_Geometry_Release(handle);
					handle = IntPtr.Zero;
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
		
		#region Public Properties
		
		#region Handle
		public IntPtr Handle
		{
			get {return handle;}
			set {handle = value;}
		}
		#endregion
				
		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
		
		#region MaximumPolygons
		public int MaximumPolygons
		{
			get
			{
				GetGeometryLimits();
				return maximumPolygons;
			}	
		}
		#endregion
		
		#region MaximumVertices
		public int MaximumVertices
		{
			get
			{
				GetGeometryLimits();
				return maximumVertices;
			}
		}
		#endregion
		
		#region Polygons
		public PolygonCollection Polygons
		{
			get
			{
				// Lazy initialization
				if (polygons == null)
				{
					polygons = new PolygonCollection(handle, true);
				}
				
				return polygons;
			}
		}
		#endregion
		
		#region Active
		public bool Active
		{
			get 
			{
				currentResult = NativeMethods.FMOD_Geometry_GetActive(handle, ref active);
				return active;
			}
			set
			{
				active = value;
				currentResult = NativeMethods.FMOD_Geometry_SetActive(handle, active);
			}
		}
		#endregion
		
		#region Rotation
		public Rotation Rotation
		{
			get
			{
				currentResult = NativeMethods.FMOD_Geometry_GetRotation(handle, ref rotation.Forward, ref rotation.Up);
				return rotation;
			}
			set
			{
				rotation = value;
				currentResult = NativeMethods.FMOD_Geometry_SetRotation(handle, ref rotation.Forward, ref rotation.Up);
			}
		}
		#endregion
		
		#region Position
		public Vector Position
		{
			get
			{
				currentResult = NativeMethods.FMOD_Geometry_GetPosition(handle, ref position);
				return position;
			}
			set
			{
				position = value;
				currentResult = NativeMethods.FMOD_Geometry_SetPosition(handle, ref position);
			}
		}
		#endregion
		
		#region Scale
		public Vector Scale
		{
			get
			{
				currentResult = NativeMethods.FMOD_Geometry_GetScale(handle, ref scale);
				return scale;
			}
			set
			{
				scale = value;
				currentResult = NativeMethods.FMOD_Geometry_SetScale(handle, ref scale);
			}
		}
		#endregion
		
		#region Size
		/// <summary>
		/// Gets the size of this geometry object when saved as a serialized binary block with Save()
		/// </summary>
		public int Size
		{
			get
			{
				int dataSize = 0;
				currentResult = NativeMethods.FMOD_Geometry_Save(handle, IntPtr.Zero, ref dataSize);
				return dataSize;
			}
		}
		#endregion
		
		#region UserData
		public IntPtr UserData
		{
			get
			{
				currentResult = NativeMethods.FMOD_Geometry_GetUserData(handle, ref userData);
				return userData;
			}
			set
			{
				userData = value;
				currentResult = NativeMethods.FMOD_Geometry_SetUserData(handle, userData);
			}
		}
		#endregion
		
		#endregion
		
		#region Internal Methods

		#region Save
		internal int Save(IntPtr data)
		{
			int dataSize = 0;
			currentResult = NativeMethods.FMOD_Geometry_Save(handle, data, ref dataSize);
			return dataSize;
		}
		#endregion
		
		#endregion
	}
}
