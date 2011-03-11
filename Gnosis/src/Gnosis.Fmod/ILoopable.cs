using System;
using System.Collections.Generic;
using System.Text;

namespace Gnosis.Fmod
{	
	[CLSCompliant(false)]
	public interface ILoopable
	{
		[CLSCompliant(false)]
		SoundLoop Loop{get;set;}
	}
}
