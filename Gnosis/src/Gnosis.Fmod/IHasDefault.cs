using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Fmod
{
	public interface IHasDefault
	{
		SoundSettings DefaultSettings{get;set;}
	}
}
