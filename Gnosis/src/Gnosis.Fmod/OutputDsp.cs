using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Fmod
{
	public class OutputDsp : Dsp
	{
		#region Private Fields
		private int id = -1;
		#endregion
		
		#region Constructors
		public OutputDsp(IntPtr handle, int id) : base(handle)
		{
			this.id = id;
		}
		#endregion
		
		#region Public Properties
		public int Id
		{
			get {return id;}
		}
		#endregion
	}
}
