using System;
using System.Collections.Generic;
using Gnosis.Core;

namespace Gnosis.Fmod
{
	public class AudioStreamFactory : IAudioStreamFactory
	{
		#region Constructors
		public AudioStreamFactory()
		{
		}
		#endregion
		
		#region IAudioStreamFactory Members

		public IAudioStream CreateAudioStream(Uri path)
		{
			if (path != null)
			{
				if (path.IsFile)
				{
					//TODO: come up with a better algoritm for CD's
					if (path.LocalPath.Length > 3)
						return new LocalSound(path.LocalPath);
					else return new CompactDiscSound(path.LocalPath);
				}
				else return new RemoteSound(path.ToString());
				
			}
			else return null;
		}

		#endregion
	}
}
