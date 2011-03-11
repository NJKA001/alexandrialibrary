using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class Polygon : IEnumerable<Vector>
	{
		#region Private Fields
		private IntPtr geometryHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private int id = -1;
		private float directOcclusion;
		private	float reverbOcclusion;
		private	bool doubleSided;		
		private List<Vector> vertices = new List<Vector>();
		private int totalVertices;
		#endregion
		
		#region Private Properties
		
		#region Vertices
		/// <summary>
		/// Gets a read-only list of the vertices in this polygon
		/// </summary>
		/// <remarks>Vertices cannot be changed directly, instead use ReplaceVertex()</remarks>
		private IList<Vector> Vertices
		{
			get
			{
				GetVertices();
				return vertices.AsReadOnly();
			}
		}
		#endregion
		
		#endregion
		
		#region Private Methods
				
		#region GetPolygonAttributes
		private void GetPolygonAttributes()
		{
			currentResult = NativeMethods.FMOD_Geometry_GetPolygonAttributes(geometryHandle, id, ref directOcclusion, ref reverbOcclusion, ref doubleSided);
		}		
		#endregion
		
		#region SetPolygonAttributes
		private void SetPolygonAttributes()
		{
			currentResult = NativeMethods.FMOD_Geometry_SetPolygonAttributes(geometryHandle, id, directOcclusion, reverbOcclusion, doubleSided);
		}
		#endregion
		
		#region GetVertices
		private void GetVertices()
		{
			currentResult = NativeMethods.FMOD_Geometry_GetPolygonNumVertices(geometryHandle, id, ref totalVertices);

			Vector vertex;

			vertices.Clear();
			vertices.Capacity = totalVertices+1;

			for (int i = 0; i < totalVertices; i++)
			{
				vertex = new Vector();
				currentResult = NativeMethods.FMOD_Geometry_GetPolygonVertex(geometryHandle, id, i, ref vertex);

			}
		}
		#endregion
				
		#endregion
		
		#region Constructors
		/// <summary>
		/// Used for creating a new polygon from scratch
		/// </summary>
		/// <param name="geometryHandle"></param>
		/// <param name="directOcclusion"></param>
		/// <param name="reverbOcclusion"></param>
		/// <param name="doubleSided"></param>
		/// <param name="vertices"></param>
		public Polygon(IntPtr geometryHandle, float directOcclusion, float reverbOcclusion, bool doubleSided, IList<Vector> vertices)
		{
			this.geometryHandle = geometryHandle;			
			this.directOcclusion = directOcclusion;
			this.reverbOcclusion = reverbOcclusion;
			this.doubleSided = doubleSided;

			Vector[] rawVertices = new Vector[vertices.Count];
			int index = -1;
			foreach (Vector vertice in vertices)
			{
				index++;
				rawVertices[index] = vertice;
				this.vertices.Add(vertice);
			}
			
			currentResult = NativeMethods.FMOD_Geometry_AddPolygon(geometryHandle, directOcclusion, reverbOcclusion, doubleSided, rawVertices.Length, rawVertices, ref id);
		}
		
		/// <summary>
		/// Used for accessing an existing polygon by ID
		/// </summary>
		/// <param name="geometryHandle"></param>
		/// <param name="id"></param>
		public Polygon(IntPtr geometryHandle, int id)
		{
			this.geometryHandle = geometryHandle;
			this.id = id;
			
			GetPolygonAttributes();
			GetVertices();
		}
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
		
		#region Id
		public int Id
		{
			get {return id;}
		}
		#endregion
		
		#region DirectOcclusion
		public float DirectOcclusion
		{
			get
			{
				GetPolygonAttributes();
				return directOcclusion;
			}
			set
			{
				directOcclusion = value;
				SetPolygonAttributes();
			}
		}
		#endregion
		
		#region ReverbOcclusion
		public float ReverbOcclusion
		{
			get
			{
				GetPolygonAttributes();
				return reverbOcclusion;
			}
			set
			{
				reverbOcclusion = value;
				SetPolygonAttributes();
			}
		}
		#endregion
		
		#region DoubleSided
		public bool DoubleSided
		{
			get
			{
				GetPolygonAttributes();
				return doubleSided;
			}
			set
			{
				doubleSided = value;
				SetPolygonAttributes();
			}
		}
		#endregion
		
		#region TotalVertices
		public int TotalVertices
		{
			get {return totalVertices;}
		}
		#endregion
		
		#endregion
		
		#region Public Methods
		
		#region ReplaceAt
		/// <summary>
		/// Replaces the vertex at the given index with the vertex provided
		/// </summary>
		/// <param name="index">The index of the vertext to replace</param>
		/// <param name="vertex">The new vertex that will replace the existing vertex</param>
		public void ReplaceAt(int index, Vector vertex)
		{
			if (index > -1 && index < vertices.Count-1)
			{
				currentResult = NativeMethods.FMOD_Geometry_SetPolygonVertex(geometryHandle, id, index, ref vertex);
			}
			else throw new ArgumentOutOfRangeException("index");
		}
		#endregion
		
		#endregion

		#region IEnumerable<Vector> Members
		public IEnumerator<Vector> GetEnumerator()
		{
			foreach(Vector vertice in Vertices)
			{
				yield return vertice;
			}
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach(Vector vertice in Vertices)
			{
				yield return vertice;
			}
		}
		#endregion
	}
}
