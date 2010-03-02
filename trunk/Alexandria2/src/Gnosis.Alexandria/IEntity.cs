﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IEntity :
		IMessage, 
		ISource<IChangeGraph>
	{
		IKey<IEntity> Id { get; }
	}
}