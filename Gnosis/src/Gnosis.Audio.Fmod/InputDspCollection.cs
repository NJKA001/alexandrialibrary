using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class InputDspCollection : DspCollection, IEnumerable<InputDsp>
	{
		#region Constructors
		public InputDspCollection(IntPtr dspHeadHandle, bool initialize) : base(dspHeadHandle)
		{
			if (initialize) GetInputDspList();
		}
		#endregion
	
		#region Private Fields
		private List<InputDsp> inputDspItems = new List<InputDsp>();
		private int totalCount;
		#endregion
		
		#region Private Properties
		private System.Collections.ObjectModel.ReadOnlyCollection<InputDsp> InputDspItems
		{
			get {return inputDspItems.AsReadOnly();}
		}
		#endregion

		#region Private Methods
		
		#region GetInputDspList
		private void GetInputDspList()
		{
			CurrentResult = NativeMethods.FMOD_DSP_GetNumInputs(DspHeadHandle, ref totalCount);

			InputDsp dsp;
			IntPtr dspHandle;

			inputDspItems.Clear();
			inputDspItems.Capacity = totalCount + 1;

			for (int i = 0; i < totalCount; i++)
			{
				dspHandle = IntPtr.Zero;

				CurrentResult = NativeMethods.FMOD_DSP_GetInput(DspHeadHandle, i, ref dspHandle);

				dsp = new InputDsp(dspHandle, i);

				inputDspItems.Add(dsp);
			}
		}
		#endregion
		
		#endregion

		#region Public Methods
		
		#region Refresh
		public override void Refresh()
		{
			GetInputDspList();
		}
		#endregion
		
		#endregion

		#region IEnumerable<InputDsp> Members
		public new IEnumerator<InputDsp> GetEnumerator()
		{
			foreach(InputDsp inputDsp in InputDspItems)
				yield return inputDsp;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (InputDsp inputDsp in InputDspItems)
				yield return inputDsp;
		}
		#endregion
	}
}

