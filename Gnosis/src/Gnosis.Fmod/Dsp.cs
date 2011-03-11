using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Fmod
{
	public class Dsp : IDisposable,IHasDefault
	{
		#region Constructors
		public Dsp()
		{
		}

		public Dsp(IntPtr handle)
		{
			this.handle = handle;
		}
		#endregion

		#region Finalizer
		~Dsp()
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
					NativeMethods.FMOD_DSP_Release(handle);
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
	
		#region Private Fields
		private IntPtr handle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		//private SoundSystem soundSystem;
		private InputDspCollection inputs;		
		private OutputDspCollection outputs;		
		private bool active;
		private bool bypass;
		private DspParameterCollection parameters;
		private string name = string.Empty;
		private uint version;
		private int numberOfChannels;
		private SoundSettings dspDefault;
		private IntPtr userData = IntPtr.Zero;
		private bool disposed;
		#endregion
		
		#region Private Methods
				
		#region GetInfo
		private void GetInfo()
		{
			StringBuilder nameBuilder = new StringBuilder(100); 
			int configWidth = 0;
			int configHeight = 0;
			currentResult = NativeMethods.FMOD_DSP_GetInfo(handle, nameBuilder, ref version, ref numberOfChannels, ref configWidth, ref configHeight);
			name = nameBuilder.ToString();
		}
		#endregion
		
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
			protected set {currentResult = value;}
		}
		#endregion
		
		#region SoundSystem
		/*
		public SoundSystem SoundSystem
		{
			get
			{
				if (soundSystem == null)
				{
					currentResult = Result.Ok;
					IntPtr systemHandle = new IntPtr();

					try
					{
						currentResult = NativeMethods.FMOD_DSP_GetSystemObject(handle, ref systemHandle);
					}
					catch (System.Runtime.InteropServices.ExternalException)
					{
						currentResult = Result.InvalidParameterError;
					}

					if (currentResult == Result.Ok)
					{
						soundSystem = new SoundSystem();
						soundSystem.Handle = systemHandle;
					}
				}

				return soundSystem;
			}
		}
		*/
		#endregion
		
		#region Inputs
		public InputDspCollection Inputs
		{
			get
			{
				// Lazy initialization
				if (inputs == null)
				{
					inputs = new InputDspCollection(handle, true);
				}
				else
				{
					inputs.Refresh();
				}
				
				return inputs;
			}
		}
		#endregion
		
		#region Outputs
		public OutputDspCollection Outputs
		{
			get
			{
				// Lazy initialization
				if (outputs == null)
				{
					outputs = new OutputDspCollection(handle, true);
				}
				else
				{
					outputs.Refresh();
				}
				
				return outputs;
			}
		}
		#endregion

		#region Active
		public bool Active
		{
			get
			{
				currentResult = NativeMethods.FMOD_DSP_GetActive(handle, ref active);
				return active;
			}
			set
			{
				active = value;
				currentResult = NativeMethods.FMOD_DSP_SetActive(handle, active);
			}
		}
		#endregion
		
		#region Bypass
		public bool Bypass
		{
			get
			{
				currentResult = NativeMethods.FMOD_DSP_GetBypass(handle, ref bypass);
				return bypass;
			}
			set
			{
				bypass = value;
				currentResult = NativeMethods.FMOD_DSP_SetBypass(handle, bypass);
			}
		}
		#endregion
		
		#region Parameters
		public DspParameterCollection Parameters
		{
			get
			{
				// Lazy initialization
				if (parameters == null)
				{
					parameters = new DspParameterCollection(handle, true);
				}
				
				return parameters;
			}
		}
		#endregion
		
		#region Name
		public string Name
		{
			get
			{
				GetInfo();
				return name;				
			}
		}
		#endregion
		
		#region Version
		[CLSCompliant(false)]
		public uint Version
		{
			get
			{
				GetInfo();
				return version;
			}
		}
		#endregion
		
		#region NumberOfChannels
		public int NumberOfChannels
		{
			get
			{
				GetInfo();
				return numberOfChannels;
			}
		}		
		#endregion
		
		#region Default
		public SoundSettings DefaultSettings
		{
			get {return dspDefault;}
			set
			{
				dspDefault = value;
				if (dspDefault != null)
				{
					currentResult = NativeMethods.FMOD_DSP_SetDefaults(handle, dspDefault.Frequency, dspDefault.Volume, dspDefault.Pan, dspDefault.Priority);
				}
			}
		}
		#endregion
		
		#region UserData
		public IntPtr UserData
		{
			get
			{
				currentResult = NativeMethods.FMOD_DSP_GetUserData(handle, ref userData);
				return userData;
			}
			set
			{
				userData = value;
				currentResult = NativeMethods.FMOD_DSP_SetUserData(handle, userData);
			}
		}
		#endregion
		
		#endregion
		
		#region Public methods
				
		#region AddInput
		public void AddInput(Dsp target)
		{
			if (target != null)
			{
				currentResult = NativeMethods.FMOD_DSP_AddInput(handle, target.Handle);
			}
		}		
		#endregion
		
		#region DisconnectFrom
		public void DisconnectFrom(Dsp target)
		{
			if (target != null)
			{
				currentResult = NativeMethods.FMOD_DSP_DisconnectFrom(handle, target.Handle);
			}
		}		
		#endregion
		
		#region Remove
		public void Remove()
		{
			currentResult = NativeMethods.FMOD_DSP_Remove(handle);
		}		
		#endregion
								
		#region Reset
		public void Reset()
		{
			currentResult = NativeMethods.FMOD_DSP_Reset(handle);
		}
		#endregion
		
		#endregion
	}
}
