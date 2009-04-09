﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Resources
{
	public interface ILinkCollection : IList<ILink>
	{
		ILinkCollection FilterByLinkType(ILinkType type);
		ILinkCollection FilterByValueType(IEntityType type);
	}
}
