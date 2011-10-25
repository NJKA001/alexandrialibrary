using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class DspCollection : IEnumerable<Dsp>
	{
		#region Constructors
		protected DspCollection(IntPtr dspHeadHandle)
		{
			this.dspHeadHandle = dspHeadHandle;
		}
		#endregion

		#region Private Fields
		private IntPtr dspHeadHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		#endregion
		
		#region Public Properties
		
		#region DspHeadHandle
		public IntPtr DspHeadHandle
		{
			get {return dspHeadHandle;}			
		}
		#endregion
		
		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
			protected set {currentResult = value;}
		}
		#endregion
				
		#endregion
		
		#region Public Methods
		
		#region Refresh
		public virtual void Refresh()
		{
		}
		#endregion
		
		#endregion

		#region IEnumerable<Dsp> Members
		public IEnumerator<Dsp> GetEnumerator()
		{
			return null;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return null;
		}
		#endregion
	}
}
