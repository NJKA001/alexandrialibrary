using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Fmod
{
	public class SpeakerVolume
	{
		#region Private Fields
		private SpeakerMode mode;
		private float frontLeftVolume;
		private float frontRightVolume;
		private float frontCenterVolume;
		private float subwooferVolume;
		private float backLeftVolume;
		private float backRightVolume;
		private float sideLeftVolume;
		private float sideRightVolume;
		private float[] volumeLevels;
		#endregion
		
		#region Private Methods
		
		#region InitializeMono
		private void InitializeMono()
		{
			mode = SpeakerMode.Mono;
			FrontCenterVolume = volumeLevels[0];
		}
		#endregion

		#region InitializeStereo
		private void InitializeStereo()
		{
			mode = SpeakerMode.Stereo;
			FrontLeftVolume = volumeLevels[0];
			FrontRightVolume = volumeLevels[1];
		}
		#endregion

		#region InitializeSurround4Point1
		private void InitializeSurround4Point1()
		{
			mode = SpeakerMode.Surround4Point1;
			FrontLeftVolume = volumeLevels[0];
			FrontRightVolume = volumeLevels[1];
			SubwooferVolume = volumeLevels[2];
			BackLeftVolume = volumeLevels[3];
			BackRightVolume = volumeLevels[4];
		}		
		#endregion

		#region InitializeSurround5Point1
		private void InitializeSurround5Point1()
		{
			mode = SpeakerMode.Surround5Point1;
			FrontLeftVolume = volumeLevels[0];
			FrontRightVolume = volumeLevels[1];
			FrontCenterVolume = volumeLevels[2];
			SubwooferVolume = volumeLevels[3];
			BackLeftVolume = volumeLevels[4];
			BackRightVolume = volumeLevels[5];
		}		
		#endregion

		#region InitializeSurround7Point1
		private void InitializeSurround7Point1()
		{
			mode = SpeakerMode.Surround7Point1;
			FrontLeftVolume = volumeLevels[0];
			FrontRightVolume = volumeLevels[1];
			FrontCenterVolume = volumeLevels[2];
			SubwooferVolume = volumeLevels[3];
			BackLeftVolume = volumeLevels[4];
			BackRightVolume = volumeLevels[5];
			SideLeftVolume = volumeLevels[6];
			SideRightVolume = volumeLevels[7];
		}		
		#endregion
		
		#endregion
		
		#region Contructors
		/// <summary>
		/// Speaker volume for a Mono sound
		/// </summary>
		/// <param name="frontCenterVolume"></param>
		public SpeakerVolume(float frontCenterVolume)
		{
			SetVolumeLevels(new float[]{frontCenterVolume});
		}
		
		/// <summary>
		/// Spealer volume for a Stero sound
		/// </summary>
		/// <param name="frontLeftVolume"></param>
		/// <param name="frontRightVolume"></param>
		public SpeakerVolume(float frontLeftVolume, float frontRightVolume)
		{
			SetVolumeLevels(new float[]{
			frontLeftVolume, 
			frontRightVolume });
		}
		
		/// <summary>
		/// Speaker volume for a 4.1 surround sound
		/// </summary>
		/// <param name="frontLeftVolume"></param>
		/// <param name="frontRightVolume"></param>
		/// <param name="subwooferVolume"></param>
		/// <param name="backLeftVolume"></param>
		/// <param name="backRightVolume"></param>
		public SpeakerVolume(float frontLeftVolume, float frontRightVolume, float subwooferVolume, float backLeftVolume, float backRightVolume)
		{
			SetVolumeLevels(new float[]{
			frontLeftVolume,
			frontRightVolume,
			subwooferVolume,
			backLeftVolume,
			backRightVolume});
		}
		
		/// <summary>
		/// Speaker volume for a 5.1 surround sound
		/// </summary>
		/// <param name="frontLeftVolume"></param>
		/// <param name="frontRightVolume"></param>
		/// <param name="frontCenterVolume"></param>
		/// <param name="subwooferVolume"></param>
		/// <param name="backLeftVolume"></param>
		/// <param name="backRightVolume"></param>
		public SpeakerVolume(float frontLeftVolume, float frontRightVolume, float frontCenterVolume, float subwooferVolume, float backLeftVolume, float backRightVolume)
		{
			SetVolumeLevels(new float[]{
			frontLeftVolume,
			frontRightVolume,
			frontCenterVolume,
			subwooferVolume,
			backLeftVolume,
			backRightVolume});
		}
		
		/// <summary>
		/// Speaker volume for a 7.1 surround sound
		/// </summary>
		/// <param name="frontLeftVolume"></param>
		/// <param name="frontRightVolume"></param>
		/// <param name="frontCenterVolume"></param>
		/// <param name="subwooferVolume"></param>
		/// <param name="backLeftVolume"></param>
		/// <param name="backRightVolume"></param>
		/// <param name="sideLeftVolume"></param>
		/// <param name="sideRightVolume"></param>
		public SpeakerVolume(float frontLeftVolume, float frontRightVolume, float frontCenterVolume, float subwooferVolume, float backLeftVolume, float backRightVolume, float sideLeftVolume, float sideRightVolume)
		{
			SetVolumeLevels(new float[]{
			frontLeftVolume,
			frontRightVolume,
			frontCenterVolume,
			subwooferVolume,
			backLeftVolume,
			backRightVolume,
			sideLeftVolume,
			sideRightVolume});
		}
		
		/// <summary>
		/// Creates a SpeakerVolume from an array of volume levels
		/// </summary>
		/// <param name="volumes">An array of floats that represents the volume of each speaker</param>
		/// <remarks>The constructor will determine the speaker mode (Mono, Stero, Surround etc.) based on the length of the array</remarks>
		public SpeakerVolume(float[] volumeLevels)
		{
			SetVolumeLevels(volumeLevels);
		}
		#endregion
		
		#region Public Properties
		
		#region Mode
		public SpeakerMode Mode
		{
			get {return mode;}
		}
		#endregion
		
		#region FrontLeftVolume
		public float FrontLeftVolume
		{
			get {return frontLeftVolume;}
			set {frontLeftVolume = NormalizeVolume(value);}
		}
		#endregion

		#region FrontRightVolume
		public float FrontRightVolume
		{
			get { return frontRightVolume; }
			set { frontRightVolume = NormalizeVolume(value); }
		}
		#endregion

		#region FrontCenterVolume
		public float FrontCenterVolume
		{
			get { return frontCenterVolume; }
			set { frontCenterVolume = NormalizeVolume(value); }
		}
		#endregion

		#region SubwooferVolume
		public float SubwooferVolume
		{
			get {return subwooferVolume;}
			set {subwooferVolume = NormalizeVolume(value);}
		}
		#endregion

		#region BackLeftVolume
		public float BackLeftVolume
		{
			get { return backLeftVolume; }
			set { backLeftVolume = NormalizeVolume(value); }
		}
		#endregion

		#region BackRightVolume
		public float BackRightVolume
		{
			get { return backRightVolume; }
			set { backRightVolume = NormalizeVolume(value); }
		}
		#endregion

		#region SideLeftVolume
		public float SideLeftVolume
		{
			get { return sideLeftVolume; }
			set { sideLeftVolume = NormalizeVolume(value); }
		}
		#endregion

		#region SideRightVolume
		public float SideRightVolume
		{
			get { return sideRightVolume; }
			set { sideRightVolume = NormalizeVolume(value); }
		}
		#endregion
		
		#endregion
		
		#region Public Methods
		
		#region GetVolumeLevels
		public float[] GetVolumeLevels()
		{
			return volumeLevels;
		}
		#endregion
		
		#region SetVolumeLevels
		public void SetVolumeLevels(float[] volumeLevels)
		{
			if (volumeLevels != null)
			{
				this.volumeLevels = volumeLevels;

				switch (volumeLevels.Length)
				{
					case 1: InitializeMono();
						break;
					case 2: InitializeStereo();
						break;
					case 5: InitializeSurround4Point1();
						break;
					case 6: InitializeSurround5Point1();
						break;
					case 8: InitializeSurround7Point1();
						break;
					default:
						throw new ArgumentException("The specified volumeLevels array has an invalid length");
				}
			}
			else throw new ArgumentNullException("volumeLevels");
		}
		#endregion
		
		#endregion

		#region Public Static Methods

		#region NormalizeVolume
		/// <summary>
		/// Normalizes the given volume so that it is between 0.0 and 1.0
		/// </summary>
		/// <param name="volume">The volume to normalize</param>
		/// <returns>The normalized volume as a float between 0.0 and 1.0</returns>
		public static float NormalizeVolume(float volume)
		{
			if (volume < 0) volume = 0;
			if (volume > 1) volume = 1;

			return volume;
		}
		#endregion

		#endregion
	}
}
