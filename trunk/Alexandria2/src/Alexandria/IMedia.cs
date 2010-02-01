﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public interface IMedia
		: IEntity, IEquatable<IMedia>
	{
		Uri Path { get; }
		MediaType MediaType { get; }
	}
}
