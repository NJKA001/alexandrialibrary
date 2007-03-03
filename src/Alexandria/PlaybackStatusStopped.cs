using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class PlaybackStatusStopped : PlaybackStatus
	{
		#region Constructors
		public PlaybackStatusStopped() : base("Stopped")
		{
		}
		#endregion
		
		#region Public Methods
		public override void Play(AudioPlayer player)
		{
			if (player != null)
			{
				player.SetStatus(PlaybackStatus.Playing);
				player.PlayCurrentSound();
			}
			else throw new ArgumentNullException("player");
		}

		public override void Pause(AudioPlayer player)
		{
			if (player != null)
			{
				player.SetStatus(PlaybackStatus.Stopped);
				player.StopCurrentSound();
			}
			else throw new ArgumentNullException("player");
		}

		public override void Stop(AudioPlayer player)
		{
			if (player != null)
			{
				player.SetStatus(PlaybackStatus.Stopped);
				player.StopCurrentSound();
			}
			else throw new ArgumentNullException("player");
		}
		#endregion
	}
}
