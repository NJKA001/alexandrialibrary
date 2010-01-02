﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sophia
{
	public interface IMessage<T>
	{
		Uri ReplyTo { get; }
		T Body { get; }
	}
}