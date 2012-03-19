using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Audio.Fmod
{
	public interface IHasDefault
	{
		SoundSettings DefaultSettings{get;set;}
	}
}
