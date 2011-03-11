using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class DspParameter
	{
		#region Private fields
		private IntPtr dspHandle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private int id = -1;
		private string name = string.Empty;
		private string label = string.Empty;
		private string description = string.Empty;
		private float minimum;
		private float maximum;
		private float currentValue;
		private string currentValueName = string.Empty;		
		#endregion
		
		#region Private methods
				
		#region GetParamterInfo
		private void GetParameterInfo()
		{
			StringBuilder nameBuilder = new StringBuilder(100);
			StringBuilder labelBuilder = new StringBuilder(100);
			StringBuilder descriptionBuilder = new StringBuilder(100);
			currentResult = NativeMethods.FMOD_DSP_GetParameterInfo(dspHandle, id, nameBuilder, labelBuilder, descriptionBuilder, nameBuilder.Capacity, ref minimum, ref maximum);
			name = nameBuilder.ToString();
			label = labelBuilder.ToString();
			description = descriptionBuilder.ToString();
		}
		#endregion

		#region GetParameter
		private void GetParameter()
		{
			StringBuilder valueNameBuilder = new StringBuilder(100);
			currentResult = NativeMethods.FMOD_DSP_GetParameter(dspHandle, id, ref currentValue, valueNameBuilder, valueNameBuilder.Capacity);
			currentValueName = valueNameBuilder.ToString();
		}
		#endregion

		#region SetParameter
		private void SetParameter()
		{
			currentResult = NativeMethods.FMOD_DSP_SetParameter(dspHandle, id, currentValue);
		}
		#endregion
		
		#endregion
		
		#region Constructors
		public DspParameter(IntPtr dspHandle, int id)
		{
			this.dspHandle = dspHandle;
			this.id = id;			
			
			GetParameterInfo();
			GetParameter();
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
		
		#region Id
		public int Id
		{
			get {return id;}
		}
		#endregion
		
		#region Name
		public string Name
		{
			get
			{
				GetParameterInfo();
				return name;
			}
		}
		#endregion
		
		#region Label
		public string Label
		{
			get
			{
				GetParameterInfo();
				return label;
			}
		}
		#endregion
		
		#region Description
		public string Description
		{
			get
			{
				GetParameterInfo();
				return description;
			}
		}
		#endregion

		#region Minimum
		public float Minimum
		{
			get
			{
				GetParameterInfo();
				return minimum;
			}
		}
		#endregion
		
		#region Maximum
		public float Maximum
		{
			get
			{
				GetParameterInfo();
				return maximum;
			}
		}
		#endregion

		#region Value
		public float Value
		{
			get
			{
				GetParameter();
				return currentValue;
			}
			set
			{
				currentValue = value;
				SetParameter();
			}
		}
		#endregion

		#region ValueName
		public string ValueName
		{
			get
			{
				GetParameter();
				return currentValueName;
			}
		}
		#endregion
		
		#endregion
	}
}
