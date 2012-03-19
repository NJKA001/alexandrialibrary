using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class InputDsp : Dsp
	{
		#region Private Fields
		private int id = -1;
		private float volume ;
		private SpeakerVolume frontLeftVolumeLevels;
		private SpeakerVolume frontRightVolumeLevels;
		private SpeakerVolume frontCenterVolumeLevels;
		private SpeakerVolume subwooferVolumeLevels;
		private SpeakerVolume backLeftVolumeLevels;
		private SpeakerVolume backRightVolumeLevels;
		private SpeakerVolume sideLeftVolumeLevels;
		private SpeakerVolume sideRightVolumeLevels;		
		#endregion
		
		#region Private Methods
		
		#region GetVolume
		private void GetVolume()
		{
			CurrentResult = NativeMethods.FMOD_DSP_GetInputMix(Handle, id, ref volume);
		}
		#endregion
		
		#region SetVolume
		private void SetVolume()
		{
			CurrentResult = NativeMethods.FMOD_DSP_SetInputMix(Handle, id, volume);
		}
		#endregion
		
		#region GetLevels
		private void GetLevels(SpeakerPosition speakerPosition)
		{	
			SpeakerVolume currentVolume = null;
			
			switch (speakerPosition)
			{
				case SpeakerPosition.FrontLeft : currentVolume = frontLeftVolumeLevels;
					break;
				case SpeakerPosition.FrontRight : currentVolume = frontRightVolumeLevels;
					break;
				case SpeakerPosition.FrontCenter : currentVolume = frontCenterVolumeLevels;
					break;					
				case SpeakerPosition.Subwoofer : currentVolume = subwooferVolumeLevels;
					break;
				case SpeakerPosition.BackLeft : currentVolume = backLeftVolumeLevels;
					break;
				case SpeakerPosition.BackRight : currentVolume = backRightVolumeLevels;
					break;
				case SpeakerPosition.SideLeft : currentVolume = sideLeftVolumeLevels;
					break;
				case SpeakerPosition.SideRight : currentVolume = sideRightVolumeLevels;
					break;
				default :
					break;
			}
			
			if (currentVolume != null)
			{
				float[] volumeLevels = currentVolume.GetVolumeLevels();
				CurrentResult = NativeMethods.FMOD_DSP_GetInputLevels(Handle, id, speakerPosition, volumeLevels, volumeLevels.Length);
			}
		}
		#endregion
		
		#region SetLevels
		private void SetLevels(SpeakerPosition speakerPosition)
		{
			SpeakerVolume currentVolume = null;

			switch (speakerPosition)
			{
				case SpeakerPosition.FrontLeft: currentVolume = frontLeftVolumeLevels;
					break;
				case SpeakerPosition.FrontRight: currentVolume = frontRightVolumeLevels;
					break;
				case SpeakerPosition.FrontCenter: currentVolume = frontCenterVolumeLevels;
					break;
				case SpeakerPosition.Subwoofer: currentVolume = subwooferVolumeLevels;
					break;
				case SpeakerPosition.BackLeft: currentVolume = backLeftVolumeLevels;
					break;
				case SpeakerPosition.BackRight: currentVolume = backRightVolumeLevels;
					break;
				case SpeakerPosition.SideLeft: currentVolume = sideLeftVolumeLevels;
					break;
				case SpeakerPosition.SideRight: currentVolume = sideRightVolumeLevels;
					break;
				default:
					break;
			}

			if (currentVolume != null)
			{
				float[] volumeLevels = currentVolume.GetVolumeLevels();
				CurrentResult = NativeMethods.FMOD_DSP_SetInputLevels(Handle, id, speakerPosition, volumeLevels, volumeLevels.Length);
			}			
		}
		#endregion
		
		#endregion
		
		#region Constructors
		public InputDsp(IntPtr handle, int id) : base(handle)
		{
			this.id = id;
		}
		#endregion
		
		#region Public properties
		
		#region Id
		public int Id
		{
			get {return Id;}
		}
		#endregion
		
		#region Volume
		public float Volume
		{
			get
			{
				return volume;
			}
			set
			{
				volume = value;
			}
		}
		#endregion
		
		#region FrontLeftVolumeLevels
		public SpeakerVolume FrontLeftVolumeLevels
		{
			get
			{
				GetLevels(SpeakerPosition.FrontLeft);
				return frontLeftVolumeLevels;
			}
			set
			{
				frontLeftVolumeLevels = value;
				SetLevels(SpeakerPosition.FrontLeft);
			}
		}
		#endregion

		#region FrontRightVolumeLevels
		public SpeakerVolume FrontRightVolumeLevels
		{
			get
			{
				GetLevels(SpeakerPosition.FrontRight);
				return frontRightVolumeLevels;
			}
			set
			{
				frontRightVolumeLevels = value;
				SetLevels(SpeakerPosition.FrontRight);
			}
		}
		#endregion

		#region FrontCenterVolumeLevels
		public SpeakerVolume FrontCenterVolumeLevels
		{
			get
			{
				GetLevels(SpeakerPosition.FrontCenter);
				return frontCenterVolumeLevels;
			}
			set
			{
				frontCenterVolumeLevels = value;
				SetLevels(SpeakerPosition.FrontCenter);
			}
		}
		#endregion

		#region SubwooferVolumeLevels
		public SpeakerVolume SubwooferVolumeLevels
		{
			get
			{
				GetLevels(SpeakerPosition.Subwoofer);
				return subwooferVolumeLevels;
			}
			set
			{
				subwooferVolumeLevels = value;
				SetLevels(SpeakerPosition.Subwoofer);
			}
		}
		#endregion

		#region BackLeftVolumeLevels
		public SpeakerVolume BackLeftVolumeLevels
		{
			get
			{
				GetLevels(SpeakerPosition.BackLeft);
				return backLeftVolumeLevels;
			}
			set
			{
				backLeftVolumeLevels = value;
				SetLevels(SpeakerPosition.BackLeft);
			}
		}
		#endregion

		#region BackRightVolumeLevels
		public SpeakerVolume BackRightVolumeLevels
		{
			get
			{
				GetLevels(SpeakerPosition.BackRight);
				return backRightVolumeLevels;
			}
			set
			{
				backRightVolumeLevels = value;
				SetLevels(SpeakerPosition.BackRight);
			}
		}
		#endregion

		#region SideLeftVolumeLevels
		public SpeakerVolume SideLeftVolumeLevels
		{
			get
			{
				GetLevels(SpeakerPosition.SideLeft);
				return sideLeftVolumeLevels;
			}
			set
			{
				sideLeftVolumeLevels = value;
				SetLevels(SpeakerPosition.SideLeft);
			}
		}
		#endregion

		#region SideRightVolumeLevels
		public SpeakerVolume SideRightVolumeLevels
		{
			get
			{
				GetLevels(SpeakerPosition.SideRight);
				return sideRightVolumeLevels;
			}
			set
			{
				sideRightVolumeLevels = value;
				SetLevels(SpeakerPosition.SideRight);
			}
		}
		#endregion

		#endregion
	}
}
