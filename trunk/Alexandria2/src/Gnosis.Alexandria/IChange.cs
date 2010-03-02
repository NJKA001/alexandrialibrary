﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IChange
		: IMessage
	{
		Type Type { get; }
		string Property { get; }
		object Value { get; }
	}
}