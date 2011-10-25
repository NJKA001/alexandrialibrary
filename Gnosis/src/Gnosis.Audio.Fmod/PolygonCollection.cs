using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class PolygonCollection : IEnumerable<Polygon>
	{
		#region Constructors
		public PolygonCollection(IntPtr geometryHandle, bool initialize)
		{
			this.geometryHandle = geometryHandle;

			if (initialize) Refresh();
		}
		#endregion
	
		#region Private Fields
		private IntPtr geometryHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private List<Polygon> polygons = new List<Polygon>();
		private int totalCount;
		#endregion
		
		#region Private Methods
		
		#region DllImport wrapper methods
		/*
		[DllImport(Constants.DllName)]
		private static extern Result FMOD_Geometry_AddPolygon(IntPtr geometry, float directOcclusion, float reverbOcclusion, bool doubleSided, int numVertices, ref Vector vertices, ref int polygonIndex);
		[DllImport(Constants.DllName)]
		private static extern Result FMOD_Geometry_GetNumPolygons(IntPtr geometry, ref int numPolygons);
		[DllImport(Constants.DllName)]
		private static extern Result FMOD_Geometry_GetPolygonNumVertices(IntPtr geometry, int polygonIndex, ref int numVertices);
		[DllImport(Constants.DllName)]
		private static extern Result FMOD_Geometry_SetPolygonVertex(IntPtr geometry, int polygonIndex, int vertexIndex, ref Vector vertex);
		[DllImport(Constants.DllName)]
		private static extern Result FMOD_Geometry_GetPolygonVertex(IntPtr geometry, int polygonIndex, int vertexIndex, ref Vector vertex);
		[DllImport(Constants.DllName)]
		private static extern Result FMOD_Geometry_SetPolygonAttributes(IntPtr geometry, int polygonIndex, float directOcclusion, float reverbOcclusion, bool doubleSided);
		[DllImport(Constants.DllName)]
		private static extern Result FMOD_Geometry_GetPolygonAttributes(IntPtr geometry, int polygonIndex, ref float directOcclusion, ref float reverbOcclusion, ref bool doubleSided);
		*/
		#endregion
		
		#endregion
		
		#region Public Properties
		
		#region GeometryHandle
		public IntPtr GeometryHandle
		{
			get {return geometryHandle;}
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
			currentResult = NativeMethods.FMOD_Geometry_GetNumPolygons(geometryHandle, ref totalCount);
			
			Polygon polygon;
			
			polygons.Clear();
			polygons.Capacity = totalCount + 1;
			
			for(int id = 0; id < totalCount; id++)
			{
				polygon = new Polygon(geometryHandle, id);

				polygons.Add(polygon);
			}
		}
		#endregion
		
		#endregion		
	
		#region IEnumerable<Polygon> Members
		public IEnumerator<Polygon> GetEnumerator()
		{
			Refresh();
			foreach(Polygon polygon in polygons)			
				yield return polygon;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			Refresh();
			foreach (Polygon polygon in polygons)
				yield return polygon;
		}
		#endregion
	}
}
