using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public class Speaker
	{
		#region Private fields
		private SpeakerPosition position = SpeakerPosition.FrontCenter;
		private SpeakerVolume volume;
		#endregion
		
		#region Constructors
		public Speaker(SpeakerPosition position, float volume)
		{
			this.position = position;
			this.volume = new SpeakerVolume(volume);
		}

		public Speaker(SpeakerPosition position, SpeakerVolume volume)
		{
			this.position = position;
			this.volume = volume;
		}
		#endregion
		
		#region Public Properties
		
		#region Position
		public SpeakerPosition Position
		{
			get {return position;}
		}
		#endregion
		
		#region Volume
		public SpeakerVolume Volume
		{
			get {return volume;}
			set {volume = value;}
		}
		#endregion
		
		#region NumberOfVolumeLevels
		public int NumberOfVolumeLevels
		{
			get
			{
				float[] levels = GetVolumeLevels();
				return levels.Length;
			}
		}
		#endregion
		
		#endregion
		
		#region Public Methods
		public float[] GetVolumeLevels()
		{
			switch (volume.Mode)
			{
				case SpeakerMode.Mono :
					return new float[1]{volume.FrontCenterVolume};
				case SpeakerMode.Stereo :
					return new float[2]{volume.FrontLeftVolume, volume.FrontRightVolume};
				case SpeakerMode.Surround4Point1 :
					return new float[5]{volume.FrontLeftVolume, volume.FrontRightVolume, volume.SubwooferVolume, volume.BackLeftVolume, volume.BackRightVolume};
				case SpeakerMode.Surround5Point1 :
					return new float[6]{volume.FrontLeftVolume, volume.FrontRightVolume, volume.FrontCenterVolume, volume.SubwooferVolume, volume.BackLeftVolume, volume.BackRightVolume};
				case SpeakerMode.Surround7Point1 :
					return new float[8]{volume.FrontLeftVolume, volume.FrontRightVolume, volume.FrontCenterVolume, volume.SubwooferVolume, volume.BackLeftVolume, volume.BackRightVolume, volume.SideLeftVolume, volume.SideRightVolume};
				default :
					return new float[1]{volume.FrontCenterVolume};
			}
		}		
		#endregion
	}
}
