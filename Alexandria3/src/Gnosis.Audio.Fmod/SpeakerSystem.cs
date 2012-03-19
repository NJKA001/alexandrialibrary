using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public abstract class SpeakerSystem
	{
		#region Private Fields
		private Result currentResult;
		private Channel systemChannel;
		private SpeakerMode mode;
		private Speaker frontLeftSpeaker = new Speaker(SpeakerPosition.FrontLeft, 0.5f);
		private Speaker frontRightSpeaker = new Speaker(SpeakerPosition.FrontRight, 0.5f);
		private Speaker frontCenterSpeaker = new Speaker(SpeakerPosition.FrontCenter, 0.5f);
		private Speaker subwooferSpeaker = new Speaker(SpeakerPosition.Subwoofer, 0.5f);
		private Speaker backLeftSpeaker = new Speaker(SpeakerPosition.BackLeft, 0.5f);
		private Speaker backRightSpeaker = new Speaker(SpeakerPosition.BackRight, 0.5f);
		private Speaker sideLeftSpeaker = new Speaker(SpeakerPosition.SideLeft, 0.5f);
		private Speaker sideRightSpeaker = new Speaker(SpeakerPosition.SideRight, 0.5f);
		#endregion
		
		#region Constructors
		private SpeakerSystem(Channel systemChannel, SpeakerMode mode)
		{
			this.systemChannel = systemChannel;
			this.mode = mode;
		}
		#endregion
		
		#region Public properties
		
		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
		
		#region SystemChannel
		public Channel SystemChannel
		{
			get {return systemChannel;}
		}
		#endregion
		
		#region Mode
		public SpeakerMode Mode
		{
			get {return mode;}
		}
		#endregion
		
		#region FrontLeftSpeaker
		public Speaker FrontLeftSpeaker
		{
			get {return frontLeftSpeaker;}
		}
		#endregion
		
		#region FrontRightSpeaker
		public Speaker FrontRightSpeaker
		{
			get {return frontRightSpeaker;}
		}
		#endregion
		
		#region FrontCenterSpeaker
		public Speaker FrontCenterSpeaker
		{
			get {return frontCenterSpeaker;}
		}
		#endregion
		
		#region SubwooferSpeaker
		public Speaker SubwooferSpeaker
		{
			get {return subwooferSpeaker;}
		}
		#endregion
		
		#region BackLeftSpeaker
		public Speaker BackLeftSpeaker
		{
			get {return backLeftSpeaker;}
		}
		#endregion
		
		#region BackRightSpeaker
		public Speaker BackRightSpeaker
		{
			get {return backRightSpeaker;}
		}
		#endregion
		
		#region SideLeftSpeaker
		public Speaker SideLeftSpeaker
		{
			get {return sideLeftSpeaker;}
		}
		#endregion
		
		#region SideRightSpeaker
		public Speaker SideRightSpeaker
		{
			get {return sideRightSpeaker;}
		}
		#endregion
		
		#endregion
		
		#region Public Methods
		private void Refresh()
		{			
			currentResult = NativeMethods.FMOD_Channel_SetSpeakerLevels(systemChannel.Handle, SpeakerPosition.FrontLeft, FrontLeftSpeaker.GetVolumeLevels(), FrontLeftSpeaker.NumberOfVolumeLevels);
			currentResult = NativeMethods.FMOD_Channel_SetSpeakerLevels(systemChannel.Handle, SpeakerPosition.FrontRight, FrontRightSpeaker.GetVolumeLevels(), FrontRightSpeaker.NumberOfVolumeLevels);
			currentResult = NativeMethods.FMOD_Channel_SetSpeakerLevels(systemChannel.Handle, SpeakerPosition.FrontCenter, FrontCenterSpeaker.GetVolumeLevels(), FrontCenterSpeaker.NumberOfVolumeLevels);
			currentResult = NativeMethods.FMOD_Channel_SetSpeakerLevels(systemChannel.Handle, SpeakerPosition.Subwoofer, SubwooferSpeaker.GetVolumeLevels(), SubwooferSpeaker.NumberOfVolumeLevels);
			currentResult = NativeMethods.FMOD_Channel_SetSpeakerLevels(systemChannel.Handle, SpeakerPosition.BackLeft, BackLeftSpeaker.GetVolumeLevels(), BackLeftSpeaker.NumberOfVolumeLevels);
			currentResult = NativeMethods.FMOD_Channel_SetSpeakerLevels(systemChannel.Handle, SpeakerPosition.BackRight, BackRightSpeaker.GetVolumeLevels(), BackRightSpeaker.NumberOfVolumeLevels);
			currentResult = NativeMethods.FMOD_Channel_SetSpeakerLevels(systemChannel.Handle, SpeakerPosition.SideLeft, SideLeftSpeaker.GetVolumeLevels(), SideLeftSpeaker.NumberOfVolumeLevels);
			currentResult = NativeMethods.FMOD_Channel_SetSpeakerLevels(systemChannel.Handle, SpeakerPosition.SideRight, SideRightSpeaker.GetVolumeLevels(), SideRightSpeaker.NumberOfVolumeLevels);
		}
		#endregion
	}
}
