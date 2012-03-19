using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class DspParameterCollection : IEnumerable<DspParameter>
	{
		#region Constructors
		public DspParameterCollection(IntPtr dspHandle, bool initialize)
		{
			this.dspHandle = dspHandle;

			if (initialize) Refresh();
		}
		#endregion
	
		#region Private Fields
		private IntPtr dspHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private List<DspParameter> dspParameters = new List<DspParameter>();
		private int totalCount;
		#endregion
		
		#region Private Properties
		private System.Collections.ObjectModel.ReadOnlyCollection<DspParameter> DspParameters
		{
			get {return dspParameters.AsReadOnly();}
		}
		#endregion
				
		#region Public properties
		
		#region DspHandle
		public IntPtr DspHandle
		{
			get {return dspHandle;}
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
			currentResult = NativeMethods.FMOD_DSP_GetNumParameters(dspHandle, ref totalCount);
			
			DspParameter parameter;
			
			dspParameters.Clear();
			dspParameters.Capacity = totalCount + 1;
			
			for(int i = 0; i < totalCount; i++)
			{
				parameter = new DspParameter(dspHandle, i);

				dspParameters.Add(parameter);
			}
		}
		#endregion
		
		#endregion

		#region IEnumerable<DspParameter> Members
		public IEnumerator<DspParameter> GetEnumerator()
		{
			foreach(DspParameter dspParameter in DspParameters)
				yield return dspParameter;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (DspParameter dspParameter in DspParameters)
				yield return dspParameter;
		}
		#endregion
	}
}
