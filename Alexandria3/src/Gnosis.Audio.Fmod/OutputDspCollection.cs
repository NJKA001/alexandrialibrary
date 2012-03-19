using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class OutputDspCollection : DspCollection,IEnumerable<OutputDsp>
	{
		#region Private Fields
		private List<OutputDsp> outputDspItems = new List<OutputDsp>();
		private int totalCount;
		#endregion
		
		#region Private Properties
		private System.Collections.ObjectModel.ReadOnlyCollection<OutputDsp> OutputDspItems
		{
			get {return outputDspItems.AsReadOnly();}
		}
		#endregion
		
		#region Private Methods
		
		#region GetOutputDspList
		private void GetDspOutputList()
		{
			CurrentResult = NativeMethods.FMOD_DSP_GetNumOutputs(DspHeadHandle, ref totalCount);

			OutputDsp dsp;
			IntPtr dspHandle;

			outputDspItems.Clear();
			outputDspItems.Capacity = totalCount + 1;

			for (int i = 0; i < totalCount; i++)
			{
				dspHandle = IntPtr.Zero;

				CurrentResult = NativeMethods.FMOD_DSP_GetOutput(DspHeadHandle, i, ref dspHandle);

				dsp = new OutputDsp(dspHandle, i);

				outputDspItems.Add(dsp);
			}

		}
		#endregion
		
		#endregion
		
		#region Constructors
		public OutputDspCollection(IntPtr dspHeadHandle, bool initialize) : base(dspHeadHandle)
		{
			if (initialize) GetDspOutputList();
		}
		#endregion
		
		#region Public Methods
		
		#region Refresh
		public override void Refresh()
		{
			GetDspOutputList();
		}
		#endregion
		
		#endregion

		#region IEnumerable<OutputDsp> Members
		public new IEnumerator<OutputDsp> GetEnumerator()
		{
			foreach(OutputDsp outputDsp in OutputDspItems)
				yield return outputDsp;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (OutputDsp outputDsp in OutputDspItems)
				yield return outputDsp;
		}
		#endregion
	}
}
